<script setup>
import { AppState } from '@/AppState.js';
import { Keep } from '@/models/Keep.js';
import { keepsService } from '@/services/KeepsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed } from 'vue';

const account = computed(() => AppState.account)

const props = defineProps({
  keepProp: { type: Keep, required: true }
})

async function getKeepById() {
  try {
    await keepsService.getById(props.keepProp.id)
  }
  catch (error) {
    Pop.error(error, `Could not get keep ${props.keepProp.name}`);
    logger.error(`Could not get keep ${props.keepProp.name}`.toUpperCase(), error)
  }
}

// NOTE 💣 Delete keep request to the server.
async function deleteKeep() {
  try {
    await keepsService.delete(props.keepProp.id)
  }
  catch (error) {
    Pop.error(error, `Could not delete keep: ${props.keepProp.name}`);
    logger.error(`Could not delete keep: ${props.keepProp.name}`.toUpperCase(), error)
  }
}


</script>


<template>
  <div class="my-2 position-relative fw-bold transparent-btn-style">
    <div v-if="keepProp?.creatorId == account?.id">
      <div class="position-absolute delete-button fs-4">
        <button @click="deleteKeep()" class="mdi mdi-close-circle p-0"
          :aria-label="`Delete button for keep titled ${keepProp.name} created by ${keepProp.creator.name}`"
          :title="`Delete button for keep: ${keepProp.name}, created by ${keepProp.creator.name}`"></button>
      </div>
    </div>
    <button @click="getKeepById()" class="text-shadow" data-bs-toggle="modal" data-bs-target="#keeps-modal"
      :aria-label="`Button to select keep named ${keepProp.name}, created by ${keepProp.creator.name}`">
      <img class="img-fluid rounded w-100" :src="keepProp?.img"
        :alt="`A picture for the ${keepProp?.name} keep by ${keepProp.creator?.name}`"
        :title="`A picture for the keep titled ${keepProp.name} created by ${keepProp.creator.name}`">
    </button>
  </div>
</template>


<style lang="scss" scoped>
.delete-button {
  left: 89%;
}

.transparent-btn-style * {
  background-color: transparent;
  border: none;
  color: red;
}
</style>