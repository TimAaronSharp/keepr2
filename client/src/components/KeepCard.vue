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
    await keepsService.getById(props.keepProp?.id)
  }
  catch (error) {
    Pop.error(error, `Could not get keep ${props.keepProp?.name}`);
    logger.error(`Could not get keep ${props.keepProp?.name}`.toUpperCase(), error)
  }
}

// NOTE 💣 Delete keep request to the server.
async function deleteKeep() {
  try {
    const confirmed = await Pop.confirm(`Are you sure you want to delete "${props.keepProp?.name}?"`, "This action is permanent".toUpperCase(), "Yes, Delete", "No, Don't Delete")
    if (!confirmed) return
    await keepsService.delete(props.keepProp?.id)
  }
  catch (error) {
    Pop.error(error, `Could not delete keep: ${props.keepProp?.name}`);
    logger.error(`Could not delete keep: ${props.keepProp?.name}`.toUpperCase(), error)
  }
}



</script>


<template>
  <div class="my-2 position-relative fw-bold transparent-btn-style">
    <div v-if="keepProp?.creatorId == account?.id" class="position-absolute d-flex delete-button fs-4">
      <button @click="getKeepById()" class="mdi mdi-pencil text-light" data-bs-toggle="modal"
        data-bs-target="#edit-keep-modal" :aria-label="`Edit button for keep named ${keepProp?.name}`"></button>
      <button @click="deleteKeep()" class="mdi mdi-close-circle p-0"
        :aria-label="`Delete button for keep titled ${keepProp?.name}`"
        :title="`Delete button for keep: ${keepProp?.name}`"></button>
    </div>
    <button @click="getKeepById()" class="text-shadow" data-bs-toggle="modal" data-bs-target="#keeps-modal"
      :aria-label="`Button to select keep named ${keepProp?.name}`">
      <img class="img-fluid rounded w-100" :src="keepProp?.img" :alt="`A picture for the ${keepProp?.name}`"
        :title="`A picture for the keep titled ${keepProp?.name}`">
    </button>
  </div>
</template>


<style lang="scss" scoped>
.delete-button {
  left: 77%;
}

.transparent-btn-style * {
  background-color: transparent;
  border: none;
  color: red;
}
</style>