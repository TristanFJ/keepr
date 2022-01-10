<template>
  <div class="vault-page">
    <div class="container">
      <div class="row">
        <div class="col-12 m-3">
          <h1>
            {{ vault.name }}
            <i
              class="mdi mdi-delete-outline selectable rounded text-danger"
              @click="deleteVault(vault.id)"
            ></i>
          </h1>
          <h4>Keeps: {{ keeps.length }}</h4>
        </div>
        <div class="col-lg-4" v-for="keep in keeps" :key="keep.id">
          <Keep :keep="keep" />
        </div>
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
import { useRouter } from "vue-router"
import { vaultsService } from "../services/VaultsService"
export default {
  setup() {
    const router = useRouter()
    return {
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps),

      async deleteVault(id) {
        try {
          if (await Pop.confirm('Do you want to delete your vault?')) {
            await vaultsService.deleteVault(id)
            router.push({ name: "Home" })
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