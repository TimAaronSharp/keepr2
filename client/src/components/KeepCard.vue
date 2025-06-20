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
    <div v-if="keepProp?.creatorId == account?.id" class="position-absolute d-flex creator-buttons-pos fs-4">
      <button @click="getKeepById()" class="mdi mdi-pencil text-light" data-bs-toggle="modal"
        data-bs-target="#edit-keep-modal" :aria-label="`Edit button for keep named ${keepProp?.name}`"></button>
      <button @click="deleteKeep()" class="mdi mdi-close-circle p-0 text-red"
        :aria-label="`Delete button for keep titled ${keepProp?.name}`"
        :title="`Delete button for keep: ${keepProp?.name}`"></button>
    </div>
    <button @click="getKeepById()" class="text-shadow w-100" data-bs-toggle="modal" data-bs-target="#keeps-modal"
      :aria-label="`Button to select keep named ${keepProp?.name}`">
      <img class="img-fluid rounded w-100 element-shadow" :src="keepProp?.img"
        :alt="`A picture for the ${keepProp?.name}`" :title="`A picture for the keep titled ${keepProp?.name}`">
    </button>
    <div class="row w-100 position-absolute absolute-bottom text-shadow">
      <div class="col-12 px-0">
        <div class="d-flex modal-margin-left px-2 pb-2 justify-content-between align-items-end w-100">
          <button @click="getKeepById()" data-bs-toggle="modal" data-bs-target="#keeps-modal"
            :title="`Keep titled: ${keepProp?.name} created by ${keepProp?.creator.name}.`"> {{ keepProp?.name
            }}</button>
          <RouterLink v-if="keepProp" :to="{ name: 'Profile Page', params: { profileId: keepProp?.creatorId } }">
            <img class="profile-thumbnail" :src="keepProp?.creator?.picture"
              :alt="`${keepProp?.creator?.name}'s profile picture.'`"
              :title="`${keepProp?.creator?.name}'s profile picture.'`">
          </RouterLink>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
.creator-buttons-pos {
  left: 77%;
}

.absolute-bottom {
  bottom: 0;
}

.position-right {
  right: 0;
}

.modal-margin-left {
  margin-left: 12px;
}

.edit-button {
  left: 0;
  margin-left: 5px;
}
</style>