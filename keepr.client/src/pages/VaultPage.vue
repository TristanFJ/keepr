<template>
  <div class="vault-page">
    <div class="container">
      <div class="row">
        <div class="col-12 m-3">
          <h1>{{ vault.name }}</h1>
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
import { useRoute } from "vue-router"
export default {
  setup() {

    return {
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps)


    }
  }
}
</script>


<style lang="scss" scoped>
</style>