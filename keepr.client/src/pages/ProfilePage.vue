<template>
  <div class="profile-page">
    <div class="container-fluid">
      <div class="row">
        <div class="col-md-4" v-for="keep in keeps" :key="keep.id">
          <Keep :keep="keep" />
        </div>
      </div>
    </div>
    <KeepModal />
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { onMounted } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { keepsService } from "../services/KeepsService"
export default {
  setup() {
    onMounted(async () => {
      try {
        await keepsService.getByProfile(AppState.profile.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>