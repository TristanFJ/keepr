<template>
  <div class="keep">
    <div
      class="card text-white bg-primary m-3 my-4 rounded elevation-5 selectable"
    >
      <img
        class="card-img-top w-100 object-fit-cover rounded-top"
        height="250"
        :src="keep.img"
        alt=""
      />
      <div class="card-body">
        <h4 class="card-title text-shadow">
          <img
            class="object-fit-cover rounded-pill me-3 elevation-3"
            height="50"
            :src="keep.creator.picture"
          />{{ keep.name }}
        </h4>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  props: { keep: { type: Object, required: true } },
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
</style>