<template>
  <div class="row">
    <div class="modal fade modal-center-lg"
         :id="'keep-modal-' + keep.id"
         tabindex="-1"
         role="dialog"
         aria-labelledby="myLargeModalLabel"
         aria-hidden="true"
    >
      <div class="modal-dialog modal-xl">
        <div class="modal-content">
          <div class="container">
            <div class="row">
              <div class="col-6">
                <img :src="keep.img" class="border-raduis img-size" alt="Keep Photo">
              </div>
              <div class="col-6 text-center">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
                <div class="row">
                  <div class="col-12 text-center">
                    <div class="text-center">
                      <i class="fa fa-street-view" aria-hidden="true">{{ keep.views }}</i>  <span><i class="fa fa-key" aria-hidden="true"></i> {{ keep.keeps }}</span> <span><i class="fa fa-share" aria-hidden="true"></i>{{ keep.shares }}</span>
                      <h1 class="">
                        {{ keep.name }}
                      </h1>
                      <h4 class="">
                        {{ keep.description }}
                      </h4>
                    </div>
                  </div>
                </div>
                <div class="row bottomrow align-items-end">
                  <div class="col-5">
                    <router-link :to="{name: 'VaultsPage', params: {id: keep.id}}">
                      <button class="btn btn-info">
                        Add to Vault
                      </button>
                    </router-link>
                  </div>
                  <div class="col-4">
                    <i class="fa fa-trash" @click="deleteKeep" aria-hidden="true"></i>
                  </div>
                  <div class="col-3 text-right">
                    <router-link :to="{name: 'Account', params:{id: state.user.id}}">
                      <span>{{ state.user.name }}</span>
                      <img :src="state.user.picture"
                           alt="user photo"
                           height="60"
                           class="rounded"
                      />
                    </router-link>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { keeprsService } from '../services/KeeprsService'

export default ({
  name: 'KeeprModal',
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const state = reactive({
      user: computed(() => AppState.user),
      vault: computed(() => AppState.vaults)
    })
    return {
      state,
      deleteKeep() {
        keeprsService.delete(props.keep.id)
      }

    }
  }
})
</script>

<style lang="scss" scoped>
.img-size{
  width: inherit;
  padding: 2%;
}
.bottomrow {
    position:absolute;
    left: 10%;
    bottom:5%;
}

</style>
