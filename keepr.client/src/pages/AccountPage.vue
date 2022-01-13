<template>
  <div class="about p-0 container-fluid">
    <div class="row m-3">
      <div class="col d-flex">
        <img class="rounded-pill" :src="account.picture" alt="" />
        <h3 class="m-3">{{ account.name }}</h3>
      </div>
      <!-- <div class="col-12">
        <h5 class="m-1">Vaults: {{ vaults.length }}</h5>
        <h5 class="m-1">Keeps: {{ keeps.length }}</h5>
      </div> -->
    </div>
  </div>

  <div class="container-fluid">
    <div class="row">
      <h1 class="m-0 my-4 py-4 bg-primary">
        Vaults: {{ vaults.length }}
        <i
          class="mdi mdi-plus text-info selectable rounded"
          data-bs-toggle="modal"
          data-bs-target="#create-vault-modal"
          title="create vault"
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
          data-bs-toggle="modal"
          data-bs-target="#create-keep-modal"
          title="create keep"
        ></i>
      </h1>
      <div class="masonry-with-columns">
        <Keep :keep="keep" v-for="keep in keeps" :key="keep.id" />
      </div>
    </div>
  </div>
  <KeepModal />
  <CreateVaultModal />
  <CreateKeepModal />
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
        document.title = "Keepr | Account"
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
</style>
