<template>
  <div class="keep-modal">
    <Modal id="keep-modal" class="opacity">
      <template #modal-body>
        <div class="container">
          <div class="row">
            <div class="col-lg-5 d-flex align-items-center">
              <img
                :src="keep.img"
                class="object-fit-cover w-100 rounded elevation-1"
                width="500"
                alt=""
              />
            </div>
            <div class="col-lg-7 p-0 d-flex flex-column align-items-apart">
              <div class="row mb-1 justify-content-around">
                <div
                  class="col-md-2 text-center bg-dark rounded-pill p-1 m-1"
                  title="views"
                >
                  <i class="mdi mdi-eye"></i> {{ keep.views }}
                </div>
                <div
                  class="col-md-2 text-center bg-dark rounded-pill p-1 m-1"
                  title="keeps"
                >
                  <i class="mdi mdi-bookmark"></i>{{ keep.keeps }}
                </div>
                <div
                  class="col-md-2 text-center bg-dark rounded-pill p-1 m-1"
                  title="shares"
                >
                  <i class="mdi mdi-share"></i>{{ keep.shares }}
                </div>
              </div>
              <div class="row justify-content-center">
                <div class="col-12">
                  <h1 class="p-0 m-0 my-3 text-center">{{ keep.name }}</h1>
                </div>
                <div class="col-11">
                  <h5 class="p-3 m-1 bg-success border border-primary rounded">
                    {{ keep.description }}
                  </h5>
                </div>
              </div>
              <div class="row mt-4 justify-content-around mt-auto">
                <div
                  class="col-4 d-flex align-items-center justify-content-center"
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
                      <!-- TODO stretch goal: disable vaults that contain THIS keep -->
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
                  <div v-else-if="vault.creatorId === account.id">
                    <button
                      class="btn btn-sm btn-danger text-white"
                      @click="deleteVaultKeep(keep.id)"
                    >
                      Remove from vault
                    </button>
                  </div>
                </div>
                <div
                  class="col-1 d-flex align-items-center justify-content-center"
                  v-if="account.id === keep.creatorId"
                  @click="deleteKeep(keep.id)"
                >
                  <button class="btn btn-danger" title="delete keep">
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
                    ms-auto
                    p-1
                  "
                  title="go to profile"
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
import { keepsService } from "../services/KeepsService"
import { applyStyles } from "@popperjs/core"
export default {
  setup() {
    const router = useRouter()
    const route = useRoute()


    return {
      keep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.myVaults),
      vault: computed(() => AppState.activeVault),
      // inVault: computed(async () => {

      //   let keepId = AppState.activeKeep.id
      //   // I have an activeKeepId, I have my vaults. I want to take that Id, find if it's contained in any of the vaults in the list, and if it is then return true. 
      // }),
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
          Pop.toast('Added to vault', 'success')
          router.push({ name: "Vault", params: { id: vaultId } })
          Modal.getOrCreateInstance(document.getElementById('keep-modal')).hide()
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')

        }
      },


      async deleteVaultKeep(keepId) {
        try {
          if (await Pop.confirm('Do you want to delete your keep?')) {
            await vaultKeepsService.deleteVaultKeep(keepId)
            Modal.getOrCreateInstance(document.getElementById('keep-modal')).hide()
            Pop.toast("Deleted", 'success')
          }
          else { return }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },


      async deleteKeep(id) {
        try {
          if (await Pop.confirm('Do you want to delete your keep?')) {
            await keepsService.deleteKeep(id)
            Modal.getOrCreateInstance(document.getElementById('keep-modal')).hide()
            Pop.toast("Deleted", 'success')
          }

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