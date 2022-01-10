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
                  <button class="btn btn-dark btn-sm">Add To Vault</button>
                </div>
                <div
                  class="col-2 d-flex align-items-center justify-content-center"
                >
                  <button class="btn btn-danger">
                    <i class="mdi mdi-delete-outline text-white"></i>
                  </button>
                </div>
                <div class="col-md-6 d-flex text-end">
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
export default {
  setup() {
    return {
      keep: computed(() => AppState.activeKeep)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>