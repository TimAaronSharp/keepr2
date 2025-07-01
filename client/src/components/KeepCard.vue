<script setup>
import { AppState } from '@/AppState.js';
import { Keep } from '@/models/Keep.js';
import { keepsService } from '@/services/KeepsService.js';
import { vaultKeepsService } from '@/services/VaultKeepsService.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed } from 'vue';

const account = computed(() => AppState.account)

const props = defineProps({
  keepProp: { type: Keep, required: true }
})

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

async function getKeepById() {
  try {
    await keepsService.getById(props.keepProp?.id)
  }
  catch (error) {
    Pop.error(error, `Could not get keep ${props.keepProp?.name}`);
    logger.error(`Could not get keep ${props.keepProp?.name}`.toUpperCase(), error)
  }
}

async function getVaultsByProfileId() {
  try {
    if (!account.value) return
    await vaultsService.getByProfileId(account.value?.id)
  }
  catch (error) {
    Pop.error(error, `Could not get vaults by profile id ${account.value?.id}`);
    logger.error(`Could not get vaults by profile id ${account.value?.id}`.toUpperCase(), error)
  }
}

async function removeKeep() {
  try {
    const confirmed = await Pop.confirm(`Are you sure you want to remove keep: ${props.keepProp.name} from this vault?`, 'This will only remove this entry from this vault and will not delete it from the database.', 'Yes, Remove', "No, Don't Remove")
    if (!confirmed) return
    // @ts-ignore
    await vaultKeepsService.delete(props.keepProp.vaultKeepId)
  }
  catch (error) {
    Pop.error(error, `Could not remove keep: ${props.keepProp.name} from vault.`);
    logger.error(`Could not remove keep: ${props.keepProp.name} from vault.`.toUpperCase(), error)
  }
}
</script>

<!-- NOTE ❓ keepProp will have a vaultKeepId when being rendered on VaultPage.vue (in that case keepProp will be a VaultKeepTracker (which extends/inherits from Keep), instead of a Keep) -->
<template>
  <div @click="getVaultsByProfileId()" class="my-2 position-relative fw-bold transparent-btn-style">
    <button v-if="keepProp?.vaultKeepId" @click="removeKeep()"
      class="mdi mdi-eye-remove-outline position-absolute fs-4 text-light modal-margin-left"
      :aria-label="`Button to remove ${keepProp?.name} from vault.`"></button>
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
.position-right {
  right: 0;
}

.modal-margin-left {
  margin-left: 12px;
}
</style>