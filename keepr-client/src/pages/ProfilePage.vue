<template>
  <div class="profilePage container">
    <div class="row">
      <div class="col-4">
        <h1>
          Welcome: {{ state.profile.picture }}{{ state.profile.name }}
        </h1>
      </div>
    </div>
    <div class="container">
      <h1 class="text-bg-dark">
        Vaults
        <button type="button" data-toggle="modal" data-target="#create-vault" class="btn btn-dark text-info">
          <i class="fa fa-plus" aria-hidden="true"></i>
        </button>
      </h1>
    </div>
    <!-- <img class="rounded" :src="profile.picture" alt="Profile Picture" /> -->
    <p>George</p>
    <MakeVaultModal />
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { profileService } from '../services/AccountService'
import { keeprsService } from '../services/KeeprsService'
import { vaultsService } from '../services/VaultsService'

export default {
  name: 'Profile',
  props: {
    vaultModalProp: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const route = useRoute()
    const state = reactive({

      profile: computed(() => AppState.profile),
      keep: computed(() => AppState.keep),
      vaults: computed(() => AppState.vaults),
      isPrivate: false
    })
    onMounted(async() => {
      await profileService.getProfiles(route.params.id)
      await keeprsService.getById(route.params.id)
      await vaultsService.getById(route.params.id)
    })
    return {
      state,
      createKeepr() {
        keeprsService.createKeepr(state.keep)
        state.newKepr = {}
      },
      createVault() {
        vaultsService.createVault(state.vault)
        state.newVault = {}
      }
    }
  },
  components: {}
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
