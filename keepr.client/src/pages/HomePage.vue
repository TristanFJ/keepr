<template>
  <div class="container-fluid py-3">
    <div class="masonry-with-columns">
      <Keep :keep="keep" v-for="keep in keeps" :key="keep.id" />
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
</style>
