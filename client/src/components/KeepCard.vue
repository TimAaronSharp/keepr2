<script setup>
import { Keep } from '@/models/Keep.js';



const props = defineProps({
  keepProp: { type: Keep, required: true }
})


</script>


<template>
  <div>
    <img :src="keepProp?.img" :alt="`A picture for the ${keepProp?.name} keep by ${keepProp.creator?.name}`"
      :title="`A picture for the keep titled ${keepProp.name} created by ${keepProp.creator.name}`">
  </div>
</template>


<style lang="scss" scoped></style>