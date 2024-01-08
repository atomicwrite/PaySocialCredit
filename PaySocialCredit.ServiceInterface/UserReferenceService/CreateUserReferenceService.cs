using PaySocialCredit.ServiceModel.CreateUserReferenceModels;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using PaySocialCredit.ServiceInterface;
using PaySocialCredit.ServiceModel.Types;
using Serilog.Core;
using ServiceStack.Messaging;

namespace PaySocialCredit.ServiceModel.CreateUserReferenceService
{
    public class CreateUserReference : Service
    {
        private readonly Logger _logger;
        private readonly NodeConfigSettings _nodeConfigSettings;
        private readonly EnabledServices _enabledServices;
        private readonly LongTaskCreateUserReference taskCreateUserReference;
        private readonly CancellationTokenSource cts;

        public override void Dispose()
        {
            taskCreateUserReference.Dispose();
            cts.Dispose();
            base.Dispose();
        }

        public CreateUserReference(Logger logger, NodeConfigSettings nodeConfigSettings,
            EnabledServices enabledServices)
        {
            _logger = logger;
            _nodeConfigSettings = nodeConfigSettings;
            _enabledServices = enabledServices;
            cts = new CancellationTokenSource();
            taskCreateUserReference = new LongTaskCreateUserReference(cts);
        }

        public async Task<CreateUserReferenceResponse> Post(CreateUserReferenceRequest request)
        {
            if (_nodeConfigSettings.IsRoutingServer())
            {
                var jsonApiClient = _nodeConfigSettings.GetNodeFor<CreateUserReferenceRequest>();
                return jsonApiClient.Post(request);
            }

            if (!_enabledServices.Allowed(typeof(CreateUserReferenceRequest)))
            {
                return _nodeConfigSettings.RouteServerClient().Post(request);
            }


            await taskCreateUserReference.Put(
                new LongTaskQueueItem<(string name, int age)>((request.name, request.age)));
            return new CreateUserReferenceResponse()
            {
                Success = true
            };
        }

        private class LongTaskRouteCreateUserReferencePool(int count)
        {
            public int Count { get; } = count;
            private int _countIndex { get; set; }

            private List<LongTaskRouteCreateUserReference> Tasks = [];

            public async Task Add(LongTaskQueueItem<(string name, int age, Guid routeId)> task)
            {
                if (_countIndex == 1)
                {
                    await Tasks[_countIndex].Put(task);
                    return;
                }

                var index = GetNextIndex();
                await Tasks[index].Put(task).ConfigureAwait(false);
            }

            private int GetNextIndex()
            {
                int index;
                Monitor.Enter(Tasks);
                if (_countIndex >= Count)
                {
                    _countIndex = 0;
                    index = _countIndex;
                }
                else
                {
                    index = _countIndex;
                    _countIndex = _countIndex++;
                }

                Monitor.Exit(Tasks);
                return index;
            }
        }

        private class LongTaskRouteCreateUserReference(CancellationTokenSource cts)
            : LongTaskWithDb<(string name, int age, Guid routeId)>(cts)
        {
            protected override async Task _action((string name, int age, Guid routeId) value)
            {
                await _db.InsertAsync(new RouteCreateUserReferenceRequest()
                {
                    RouteId = value.routeId,
                    CreateUserReferenceRequest = new CreateUserReferenceRequest() { age = value.age, name = value.name }
                });
            }
        }


        private class LongTaskCreateUserReference(CancellationTokenSource cts)
            : LongTaskWithDb<(string name, int age)>(cts), IDisposable
        {
            protected override async Task _action((string name, int age) value)
            {
                await _db.InsertAsync(new UserReference()
                {
                    Name = value.name,
                    Age = value.age
                });
            }
        }
    }
}