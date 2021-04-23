<template>
  <div class="about container-fluid">
    <div class="row justify-content-center">
      <div class="col-3" v-if="!state.loading && state.profile.name">
        <div>
          <img :src="state.profile.picture" alt="Profile Picture">
        </div>
        <h1> {{ state.account.name.includes('@') ? state.profile.name.split('@')[0] : state.profile.name }} </h1>
      </div>
    </div>
    <div class="row">
      <div class="col">
        <h3>Create a </h3>
        <form @submit.prevent="createKeepr">
          <input type="text" v-model="state.newKeepr.title" placeholder="Keepr Title?">
          <input type="text" v-model="state.newKeepr.picture" placeholder="Picture?">
          <button type="submit" class="btn btn-primary">
            Create Keepr
          </button>
        </form>
      </div>
    </div>

    <div class="card-columns">
      <keepr-component v-for="k in state.keeps" :key="k._id" :keep-prop="k" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { keeprsService } from '../services/KeeprsService'
// import { vaultKeep } from '..services/VaultKeepService'
export default {
  name: 'Account',
  setup() {
    const state = reactive({
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      newVault: {}
    })
    onMounted(() => vaultsService.getById())
    onMounted(() => keeprsService.getById())
    return {
      state,
      createKeepr() {
        keeprsService.createKeepr(state.newKeepr)
        state.newKeepr = {}
      }
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
