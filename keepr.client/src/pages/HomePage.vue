<template>
  <div class="container-fluid">
    <div class="row">
      <!-- <div class="masonry-with-columns"> -->
      <div class="col-lg-4" v-for="keep in keeps" :key="keep.id">
        <Keep :keep="keep" />
      </div>
      <!-- </div> -->
    </div>
  </div>
  <KeepModal />
</template>

<script>
import { computed, watchEffect } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import { keepsService } from "../services/KeepsService"
import Pop from "../utils/Pop"
import { AppState } from "../AppState"
import { vaultsService } from "../services/VaultsService"
export default {
  name: 'Home',
  setup() {
    watchEffect(async () => {
      try {
        await keepsService.getAll()
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })

    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;
  .home-card {
    width: 50vw;
    > img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
