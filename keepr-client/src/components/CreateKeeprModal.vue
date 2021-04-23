<template>
  <div class="create-keep" v-if="state.viewportWidth <= 700">
    <div class="modal fade"
         id="create-keep"
         tabindex="-1"
         role="dialog"
         aria-labelledby="modelTitleId"
         aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header bg-primary border-bottom">
            <h4 class="modal-title text-info">
              New Keepr
            </h4>
            <span type="button" class="close py-3" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </span>
          </div>

          <div class="modal-body">
            <form class="form-inline" @submit.prevent="createKeepr">
              <div class="form-group">
                <div>
                  <input class="mb-3" type="text" placeholder="Keepr Title" v-model="state.keep.name">
                </div>
                <div>
                  <input class="mb-3" type="img" placeholder="Photo?" v-model="state.keep.img">
                </div>
                <textarea
                  name="keep-text"
                  id="keep-description"
                  cols="30"
                  rows="5"
                  placeholder="Add a Description"
                  aria-describedby="helpId"
                  v-model="state.keep.description"
                  required
                >
                </textarea>
              </div>
              <div>
                <button class="btn btn-success btn-block" type="submit">
                  Keep it!
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="create-keep" v-else>
    <div class="modal fade"
         id="create-keep"
         tabindex="-1"
         role="dialog"
         aria-labelledby="modelTitleId"
         aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header bg-primary border-bottom">
            <h4 class="modal-title text-info">
              New Keepr
            </h4>
            <span type="button" class="close py-3" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </span>
          </div>

          <div class="modal-body">
            <form class="form-inline" @submit.prevent="createKeepr">
              <div>
                <div>
                  <input class="mb-3" type="text" placeholder="Keepr Title" v-model="state.keep.name">
                </div>
                <div>
                  <input class="mb-3" type="img" placeholder="Photo?" v-model="state.keep.img">
                </div>

                <textarea name="keepr-text"
                          id="keep-description"
                          cols="30"
                          rows="5"
                          placeholder="Add a Description"
                          aria-describedby="helpId"
                          v-model="state.keep.description"
                          required
                >
                  </textarea>

                <button class="btn btn-success mt-3" type="submit">
                  Keep it!
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
import { reactive, onMounted } from 'vue'
import $ from 'jquery'
import { keeprsService } from '../services/KeeprsService'
import { logger } from '../utils/Logger'

export default ({
  name: 'CreateKeeprModal',

  setup() {
    const state = reactive({
      keep: {},
      viewportWidth: window.innerWidth

    })
    onMounted(() => { window.addEventListener('resize', () => { state.viewportWidth = window.innerWidth }) })

    return {
      state,
      async createKeepr() {
        try {
          await keeprsService.createKeepr(state.keep)
          state.keep = {}
          $('#create-keep').modal('hide')
          $('.modal-backdrop').remove()
        } catch (error) {
          logger.error(error)
        }
      }

    }
  }
})
</script>

<style scoped>
</style>
