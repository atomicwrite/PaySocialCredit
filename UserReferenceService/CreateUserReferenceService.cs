using PaySocialCredit.ServiceInterface.CreateUserReferenceModels;
using Microsoft.AspNetCore.Hosting;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Configuration;
using ServiceStack.Logging;
using System.Linq;
using ServiceStack.OrmLite;
using ServiceStack.FluentValidation;
using System;
using System.Threading.Tasks;

                    namespace PaySocialCredit.ServiceInterface.CreateUserReferenceService {

                                 
                                 public class CreateUserReference : ServiceStack.Service {
                                    
                                    
                          
                      public  async Task<CreateUserReferenceResponse>  Post (CreateUserReferenceRequest request) {
                                    
                                    try {
                             var id= Db.Insert( request.UserReference,true);
return 
new CreateUserReferenceResponse() {  Id = id }
;
                            }catch(Exception e){ 
                        return 
new CreateUserReferenceResponse() {  Success = false
,Message = e.Message }
;
                }finally{  
                        
}



                                        
                                    }

                    }
}
