<template>
  <div class="create-vault-modal">
    <Modal id="create-keep-modal" class="opacity">
      <template #modal-body>
        <div class="container">
          <div class="row">
            <div class="col-12 text-center my-3"><h1>New Keep</h1></div>
            <div class="col-12 my-3">
              <form @submit.prevent="createKeep()">
                <div class="input-group mb-3">
                  <input
                    type="text"
                    class="form-control"
                    placeholder="Title"
                    aria-label="Title"
                    maxlength="50"
                    aria-describedby="title-input"
                    v-model="state.name"
                  />
                </div>
                <div class="input-group mb-3">
                  <input
                    type="text"
                    class="form-control"
                    placeholder="Description"
                    aria-label="description"
                    maxlength="500"
                    aria-describedby="description-input"
                    v-model="state.description"
                  />
                </div>
                <div class="input-group mb-3">
                  <input
                    type="text"
                    class="form-control"
                    placeholder="Image url"
                    aria-label="img"
                    aria-describedby="img-input"
                    v-model="state.img"
                  />
                </div>
                <div class="modal-footer">
                  <button
                    type="submit"
                    class="btn btn-primary rounded-pill"
                    data-bs-dismiss="modal"
                  >
                    Share
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      </template>
    </Modal>
  </div>
</template>


<script>
import { reactive } from "@vue/reactivity"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { keepsService } from "../services/KeepsService"
export default {
  setup() {
    const state = reactive({})
    return {
      state,
      async createKeep() {
        try {
          await keepsService.createKeep(state)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>