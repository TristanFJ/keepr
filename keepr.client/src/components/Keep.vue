<template>
  <div class="keep" @click="setActive(keep.id)">
    <div class="card keep text-white bg-primary m-2 elevation-5 selectable">
      <img
        class="w-100 keep object-fit-cover rounded-top img"
        :src="keep.img"
        alt=""
      />
      <div class="top-left">
        <h3 class="card-title m-3 p-3 rounded-pill text-shadow gray">
          <img
            class="object-fit-cover rounded-pill me-3 elevation-3"
            height="50"
            :src="keep.creator.picture"
          />{{ keep.name }}
        </h3>
      </div>
    </div>
  </div>
</template>
// NOTE CARD COLUMNS bootstrap 

<script>
import { Modal } from "bootstrap"
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
          Modal.getOrCreateInstance(document.getElementById('keep-modal')).toggle()
          // logger.log(AppState.activeKeep)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }

    }
  }
}
</script>


<style lang="scss" scoped>
.card {
  position: relative;
}

.keep {
  border-radius: 65px !important;
}

.top-left {
  position: absolute;
  left: 0;
  top: 0;
}
</style>