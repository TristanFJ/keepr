<template>
  <div class="keep" @click="setActive(keep.id)">
    <div
      class="card text-white bg-primary m-3 my-4 rounded elevation-5 selectable"
      data-bs-toggle="modal"
      data-bs-target="#keep-modal"
    >
      <img
        class="card-img-top w-100 object-fit-cover rounded-top"
        height="250"
        :src="keep.img"
        alt=""
      />
      <div class="card-body">
        <h4 class="card-title text-shadow">
          <img
            class="object-fit-cover rounded-pill me-3 elevation-3"
            height="50"
            :src="keep.creator.picture"
          />{{ keep.name }}
        </h4>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from "../AppState"
import { keepsService } from "../services/KeepsService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
export default {
  props: { keep: { type: Object, required: true } },
  setup() {
    return {

      async setActive(id) {
        try {
          await keepsService.getById(id)
          logger.log(AppState.activeKeep)
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