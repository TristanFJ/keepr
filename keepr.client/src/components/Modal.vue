<template>
  <div class="modal" tabindex="-1">
    <div class="modal-dialog modal-dialog-centered modal-xl">
      <div class="modal-content">
        <div class="modal-body">
          <button
            type="button"
            class="btn-close m-2"
            aria-label="Close"
            data-bs-target="modal"
            data-bs-dismiss="modal"
          ></button>
          <slot name="modal-body"></slot>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
.modal-body {
  background-color: hsl(49, 60%, 93%);
}
</style>