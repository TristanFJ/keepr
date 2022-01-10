<template>
  <div class="vault" @click="setActive(vault.id)">
    <div
      class="card text-white bg-primary m-3 my-4 rounded elevation-5 selectable"
    >
      <img
        class="card-img-top w-100 object-fit-cover rounded-top"
        height="250"
        :src="vault.img"
        alt=""
      />
      <div class="card-body">
        <h4 class="card-title text-shadow">
          {{ vault.name }}
        </h4>
      </div>
    </div>
  </div>
</template>


<script>
import { vaultsService } from "../services/VaultsService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
export default {
  props: { vault: { type: Object, required: true } },

  setup() {
    return {

      async setActive(id) {
        try {
          await vaultsService.getById(id)
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