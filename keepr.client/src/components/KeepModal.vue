<template>
  <div class="keep-modal">
    <Modal id="keep-modal" class="opacity">
      <template #modal-body>
        <div class="container">
          <div class="row">
            <div class="col-lg-5">
              <img
                :src="keep.img"
                class="object-fit-cover w-100 rounded elevation-1"
                width="500"
                alt=""
              />
            </div>
            <div class="col-lg-7 p-0">
              <div class="row mb-1 justify-content-around">
                <div class="col-md-2 text-center bg-dark rounded p-1 m-1">
                  <i class="mdi mdi-eye"></i> {{ keep.views }}
                </div>
                <div class="col-md-2 text-center bg-dark rounded p-1 m-1">
                  <i class="mdi mdi-bookmark"></i>{{ keep.keeps }}
                </div>
                <div class="col-md-2 text-center bg-dark rounded p-1 m-1">
                  <i class="mdi mdi-share"></i>{{ keep.shares }}
                </div>
              </div>
              <div class="row">
                <h1 class="p-0 m-0 my-5 text-center">{{ keep.name }}</h1>
                <h5>{{ keep.description }}</h5>
              </div>
              <div class="row mt-4 justify-content-around">
                <div
                  class="col-2 d-flex align-items-center justify-content-center"
                >
                  <!-- <button class="btn btn-dark btn-sm">Add To Vault</button> -->
                  <div class="dropdown open" v-if="route.name !== 'Vault'">
                    <button
                      class="btn btn-secondary dropdown-toggle"
                      type="button"
                      id="triggerId"
                      data-bs-toggle="dropdown"
                      aria-haspopup="true"
                      aria-expanded="false"
                    >
                      Add To Vault
                    </button>
                    <div class="dropdown-menu" aria-labelledby="triggerId">
                      <button
                        class="btn btn-primary m-1"
                        @click="addToVault(vault.id)"
                        v-for="vault in vaults"
                        :key="vault.id"
                      >
                        {{ vault.name }}
                      </button>
                    </div>
                  </div>
                  <div v-else>
                    <button
                      class="btn btn-sm btn-danger text-white"
                      @click="deleteVaultKeep(keep.id)"
                    >
                      Remove from vault
                    </button>
                  </div>
                </div>
                <div
                  class="col-2 d-flex align-items-center justify-content-center"
                >
                  <button
                    class="btn btn-danger"
                    v-if="account.id === keep.creatorId"
                  >
                    <i class="mdi mdi-delete-outline text-white"></i>
                  </button>
                </div>
                <div
                  class="
                    col-md-6
                    d-flex
                    text-end
                    selectable
                    rounded
                    align-items-center
                  "
                  @click="setProfile(keep.creatorId)"
                >
                  <img
                    class="object-fit-cover rounded-pill me-3 elevation-3"
                    height="50"
                    :src="keep.creator?.picture"
                  />
                  <p class="text-end p-2 m-1">{{ keep.creator?.name }}</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </template>
    </Modal>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { useRoute, useRouter } from "vue-router"
import { Modal } from "bootstrap"
import { profilesService } from "../services/ProfilesService"
import { vaultsService } from "../services/VaultsService"
import { onMounted } from "@vue/runtime-core"
import { vaultKeepsService } from "../services/VaultKeepsService"
export default {
  setup() {
    const router = useRouter()
    const route = useRoute()

    return {
      keep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.myVaults),
      route,
      async setProfile(id) {
        try {
          await profilesService.getProfile(id)
          router.push({ name: "Profile", params: { id: id } })
          Modal.getOrCreateInstance(document.getElementById('keep-modal')).hide()
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },

      async addToVault(vaultId) {
        try {
          const keepId = AppState.activeKeep.id
          await vaultsService.addToVault(vaultId, keepId)
          await vaultsService.getById(vaultId)
          router.push({ name: "Vault", params: { id: vaultId } })
          Modal.getOrCreateInstance(document.getElementById('keep-modal')).hide()
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')

        }
      },

      async deleteVaultKeep(keepId) {
        try {
          if (await Pop.confirm('Do you want to delete your vault?')) {
            await vaultKeepsService.deleteVaultKeep(keepId)
            Modal.getOrCreateInstance(document.getElementById('keep-modal')).hide()
          }
          else { return }
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