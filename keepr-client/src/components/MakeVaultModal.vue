<template>
  <div class="create-vault" v-if="state.viewportWidth <= 700">
    <div class="modal fade"
         id="create-vault"
         tabindex="-1"
         role="dialog"
         aria-labelledby="modelTitleId"
         aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header bg-primary border-bottom">
            <h4 class="modal-title text-info">
              New Vault
            </h4>
            <span type="button" class="close py-3" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </span>
          </div>

          <div class="modal-body">
            <form class="form-inline" @submit.prevent="createVault">
              <div class="form-group">
                <div>
                  <input class="mb-3" type="text" placeholder="Vault Title" v-model="state.newVault.name">
                </div>
                <div>
                  <input class="mb-3" type="img" placeholder="Photo?" v-model="state.newVault.img">
                </div>
                <textarea
                  name="vault-text"
                  id="vault-description"
                  cols="30"
                  rows="5"
                  placeholder="Add a Description"
                  aria-describedby="helpId"
                  v-model="state.newVault.description"
                  required
                >
                </textarea>
                <div>
                  <span class="mr-3">Make Private</span><input class="mb-3" type="checkbox" v-model="state.keep.isPrivate">
                </div>
              </div>
              <div>
                <button class="btn btn-success btn-block" type="submit">
                  Make a Vault
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="create-vault" v-else>
    <div class="modal fade"
         id="create-vault"
         tabindex="-1"
         role="dialog"
         aria-labelledby="modelTitleId"
         aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header bg-primary border-bottom">
            <h4 class="modal-title text-info">
              New Vault
            </h4>
            <span type="button" class="close py-3" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </span>
          </div>

          <div class="modal-body">
            <form class="form-inline" @submit.prevent="createVault">
              <div class="form-group">
                <div>
                  <input class="mb-3" type="text" placeholder="Vault Title" v-model="state.newVault.name">
                </div>
                <div>
                  <input class="mb-3" type="img" placeholder="Photo?" v-model="state.newVault.img">
                </div>
                <textarea
                  name="vault-text"
                  id="vault-description"
                  cols="30"
                  rows="5"
                  placeholder="Add a Description"
                  aria-describedby="helpId"
                  v-model="state.newVault.description"
                  required
                >
                </textarea>
                <div>
                  <span class="mr-3">Make Private</span><input class="mb-3" type="checkbox" v-model="state.keep.isPrivate">
                </div>
              </div>
              <div>
                <button class="btn btn-success btn-block" type="submit">
                  Make a Vault
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive, onMounted, computed } from 'vue'
import $ from 'jquery'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { keeprsService } from '../services/KeeprsService'

export default ({
  name: 'MakeVaultModal',
  props: {
    mvProp: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const route = useRoute()
    const state = reactive({
      newVault: {},
      user: computed(() => AppState.user),
      keep: computed(() => AppState.keep),
      viewportWidth: window.innerWidth
    })
    onMounted(() => { window.addEventListener('resize', () => { state.viewportWidth = window.innerWidth }) })
    onMounted(() => keeprsService.getKeepsByProfileId(route.params.id))
    onMounted(() => vaultsService.getVaultsByProfileId(route.params.id))

    return {
      state,

      async createVault() {
        try {
          await vaultsService.createVault(state.newVault)
          state.newVault = {}
          $('#create-vault').modal('hide')
          $('.modal-backdrop').remove()
        } catch (error) {
          logger.error(error)
        }
      }

    }
  }
})
</script>

<style lang="scss" scoped>

</style>
