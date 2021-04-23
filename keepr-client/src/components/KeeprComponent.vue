<template>
  <button type="button" class="btn" data-toggle="modal" :data-target="'#keep-modal-'+ keepProp.id">
    <div class="card bg-dark text-dark mx-2">
      <div class="card keep">
        <img class="card-img" :src="keepProp.img" alt="Keepr Image">
        <div class="card-image-overlay">
          <h4 class="card-title">
            {{ keepProp.name }}
          </h4>
          <div class="card-text">
            {{ keepProp.description }}
          </div>
          <router-link :to="{name:'Account', params:{id: state.user.id}}">
            <i class="fa fa-user-circle-o" aria-hidden="true"></i>
          </router-link>
        </div>
      </div>
    </div>
  </button>
  <KeeprModal :keep="keepProp" />
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'

export default {
  name: 'KeeprComponent',
  props: {
    keepProp: {
      type: Object,
      required: true
    }
  },
  setup() {
    const state = reactive({
      user: computed(() => AppState.user)
    })
    return {
      state
    }
  }
}
</script>

<style scoped>
.keep:hover {
  transform: translateY(-5px);
  box-shadow: 1px 3px 5px rgb(94, 94, 94);
  cursor: pointer
}

.keep {
  transition: all .2s;
  color: black
}
</style>
