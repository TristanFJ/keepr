<template>
  <div class="about p-5 container-fluid">
    <div class="row">
      <div class="col d-flex">
        <img class="rounded" :src="account.picture" alt="" />
        <h3 class="m-1">{{ account.name }}</h3>
      </div>
      <div class="col-12">
        <h5 class="m-1">Vaults: {{ vaults.length }}</h5>
        <h5 class="m-1">Keeps: {{ keeps.length }}</h5>
      </div>
    </div>
  </div>

  <div class="container-fluid">
    <div class="row">
      <h1 class="m-0 my-4 py-4 bg-primary">
        Vaults
        <i class="mdi mdi-plus text-success selectable rounded"></i>
      </h1>
      <div class="col-md-4" v-for="vault in vaults" :key="vault.id">
        <Vault :vault="vault" />
      </div>
    </div>
  </div>

  <div class="container-fluid">
    <div class="row">
      <h1 class="m-0 my-4 py-4 bg-primary">
        Keeps
        <i class="mdi mdi-plus text-success selectable rounded"></i>
      </h1>
      <div class="col-md-4" v-for="keep in keeps" :key="keep.id">
        <Keep :keep="keep" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { logger } from "../utils/Logger"
import { vaultsService } from "../services/VaultsService"
import Pop from "../utils/Pop"
import { keepsService } from "../services/KeepsService"
export default {
  name: 'Account',
  setup() {
    onMounted(async () => {
      try {
        await vaultsService.getMyVaults()
        await keepsService.getByProfile(AppState.account.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })


    return {
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.myVaults),
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
