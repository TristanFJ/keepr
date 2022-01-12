<template>
  <div class="profile-page p-0 container-fluid">
    <div class="row m-3">
      <div class="col d-flex">
        <img class="rounded-pill" :src="profile.picture" alt="" />
        <h3 class="m-3">{{ profile.name }}</h3>
      </div>
      <!-- <div class="col-12">
        <h5 class="m-1">Vaults: {{ vaults.length }}</h5>
        <h5 class="m-1">Keeps: {{ keeps.length }}</h5>
      </div> -->
    </div>
    <div class="container-fluid">
      <div class="row">
        <h1 class="m-0 my-4 py-4 bg-primary">
          Vaults: {{ vaults.length }}
          <i
            class="mdi mdi-plus text-info selectable rounded"
            v-if="profile.id === account.id"
          ></i>
        </h1>
        <div class="masonry-with-columns">
          <Vault :vault="vault" v-for="vault in vaults" :key="vault.id" />
        </div>
      </div>
    </div>
    <div class="container-fluid">
      <div class="row">
        <h1 class="m-0 my-4 py-4 bg-primary">
          Keeps: {{ keeps.length }}
          <i
            class="mdi mdi-plus text-info selectable rounded"
            v-if="profile.id === account.id"
          ></i>
        </h1>
        <div class="masonry-with-columns">
          <Keep :keep="keep" v-for="keep in keeps" :key="keep.id" />
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
import { profilesService } from "../services/ProfilesService"
import { useRoute } from "vue-router"
import { vaultsService } from "../services/VaultsService"
export default {
  name: 'Profile',
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await profilesService.getProfile(route.params.id)
        await keepsService.getByProfile(AppState.profile.id)
        await profilesService.getVaults(AppState.profile.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.profileVaults),
      account: computed(() => AppState.account)

    }
  }
}
</script>


<style lang="scss" scoped>
</style>