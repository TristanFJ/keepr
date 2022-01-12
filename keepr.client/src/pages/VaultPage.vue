<template>
  <div class="vault-page">
    <div class="container">
      <div class="col-12 m-3">
        <h1>
          {{ vault.name }}
          <i
            class="mdi mdi-delete-outline selectable rounded text-danger"
            v-if="vault.creatorId === account.id"
            @click="deleteVault(vault.id)"
            title="delete vault"
          ></i>
          <!-- TODO SWEET ALERTS -->
        </h1>
        <h4>{{ vault.description }}</h4>
        <h4>Keeps: {{ keeps.length }}</h4>
      </div>
      <div class="masonry-with-columns">
        <Keep :keep="keep" v-for="keep in keeps" :key="keep.id" />
      </div>
    </div>
  </div>
  <KeepModal />
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { vaultKeepsService } from "../services/VaultKeepsService"
import { onMounted } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { useRoute, useRouter } from "vue-router"
import { vaultsService } from "../services/VaultsService"
import { keepsService } from "../services/KeepsService"
export default {
  setup() {
    const router = useRouter()
    const route = useRoute()
    onMounted(async () => {
      try {
        await vaultsService.getById(route.params.id)
        logger.log("push vault")
      } catch (error) {
        router.push({ name: "Home" })
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),

      async deleteVault(id) {
        try {
          if (await Pop.confirm('Do you want to delete your vault?')) {
            await vaultsService.deleteVault(id)
            router.push({ name: "Account" })
          } else { return }
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
</style>