<template>
  <div class="home-page container">
    <div class="row">
      <div class="col text-center mb-5">
        <h1 class="text-primary">
          Keepr!
        </h1>
        <button type="button" data-toggle="modal" data-target="#create-keep" class="btn btn-dark text-info">
          Create a Keep
        </button>
      </div>
      <div class="row">
        <div class="card-columns">
          <keepr-component v-for="k in state.keeps" :key="k._id" :keep-prop="k" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted, computed, reactive } from 'vue'
import { keeprsService } from '../services/KeeprsService'
import { AppState } from '../AppState'
import KeeprComponent from '../components/KeeprComponent.vue'

export default {
  components: { KeeprComponent },
  name: 'Home',
  setup() {
    const state = reactive({
      keeps: computed(() => AppState.keeps)
    })
    onMounted(() => keeprsService.get())
    return {
      state
    }
  }

}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
</style>
