<template>
  <section>
    <div><h4>Create UserReferenceMask</h4>
      <b-alert :show="true" v-if="DataModel.Message.length >0">{{ DataModel.Message }}</b-alert>
      <b-form-group id="fieldset-Id2" description="" label="Id2" label-for="input-Id2" valid-feedback="">
        <b-form-input id="fieldset-Id2" :disabled="false" v-model="DataModel.Id2" type="text"
                      :trim="true"></b-form-input>
      </b-form-group>
      <b-button @click="CreateUserReference(DataModel)">Create</b-button>
    </div>
  </section>
</template>
<script lang="ts">
console.log("");
import {Component, Vue} from 'vue-property-decorator'
import {Mixins} from 'vue-property-decorator'
import {client} from '@/shared'
import {CreateUserReferenceRequest, UserReference, CreateUserReferenceResponse} from '@/shared/dtos'


export class UserReferenceCreateMask extends UserReference {
                                    

  Message: string = ""


  Success: boolean = true


  Completed: boolean = true


  Error: string = ""


  constructor(init?: Partial<UserReferenceCreateMask>) {

    super()
    ;
    (Object as any).assign(this, init)

  }

}


@Component({components: {}})
export class UserReferenceApiMixin extends Vue {


  async CreateUserReference(DataModel: UserReferenceCreateMask) {

    try {
      const Response: CreateUserReferenceResponse = await client.post(new CreateUserReferenceRequest({UserReference: DataModel}))
      DataModel.Success = Response.Success;
      if (Response.Id > 0) {
        DataModel.Message = 'Created'
      } else {
        DataModel.Message = Response.Message;
      }
    } catch (e: WebException) {
      DataModel.Message = e.message;
      console.log(e)
      const fieldErrors = e.GetFieldErrors()
      if (fieldErrors) {
      }
    } finally {

    }


  }

}


@Component({components: {}})
export default class CreateUserReference extends Mixins(UserReferenceApiMixin) {


  DataModel: UserReferenceCreateMask = new UserReferenceCreateMask(new UserReference({}))


}


</script>