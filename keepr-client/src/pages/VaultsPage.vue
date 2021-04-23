<template>
  <div class="container vaultspage">
    <div class="row">
      <div class="col-3 text-center title">
        Vaults
      </div>
      <div class="col-9">
        <i class="fa fa-trash fa-2x pt-5" @click="deleteVault" aria-hidden="true"></i>
      </div>
    </div>
    <div class="row">
      <div class="card-columns">
        <keepr-component v-for="k in state.keeps" :key="k._id" :keep-prop="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { reactive, computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { keeprsService } from '../services/KeeprsService'
import { vaultsService } from '../services/VaultsService'
import { useRoute } from 'vue-router'
import { logger } from '../utils/Logger'
export default {
  name: 'VaultsPage',
  setup() {
    const route = useRoute()
    const state = reactive({
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.vaults),
      newVault: {}
    })
    onMounted(() => {
      vaultsService.getVaultbyId(route.params.vaultId)
      keeprsService.getKeepsByVaultId(route.params.vaultId)
    })
    return {
      state,
      createVault() {
        logger.log(state.newVault)
        state.newVault.vaultId = route.params.vaultId
        vaultsService.createVault(state.newVault)
        state.newVault = {}
      },
      deleteVault() {
        state.vaults = route.params.vaultId
        vaultsService.delete()
      }

    }
  }
}
</script>

<style lang="scss" scoped>
.title{
  font-size: 72px;
  ;
}
</style>
