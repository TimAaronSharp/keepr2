<script setup>
import { AppState } from '@/AppState.js';
import { Vault } from '@/models/Vault.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed } from 'vue';


const account = computed(() => AppState.account)

const props = defineProps({
  vaultProp: { type: Vault, required: true }
})

async function deleteVault() {
  try {
    const confirmed = await Pop.confirm(`Are you sure you want to permanently delete vault: ${props.vaultProp.name}?`, "This action is permanent", "Yes, Delete", "No, Don't Delete")
    if (!confirmed) return

    await vaultsService.delete(props.vaultProp.id)
  }
  catch (error) {
    Pop.error(error, `Could not delete vault: ${props.vaultProp.name}, id: ${props.vaultProp.id}`);
    logger.error(`Could not delete vault: ${props.vaultProp.name}, id: ${props.vaultProp.id}`.toUpperCase(), error)
  }
}

</script>


<template>
  <div class="position-relative text-light text-shadow fw-bold my-3 transparent-btn-style">
    <div v-if="account?.id == vaultProp.creatorId" class="creator-buttons fs-4 d-flex">
      <div class="position-absolute lock-icon-pos">
        <span v-if="vaultProp.isPrivate" class="mdi mdi-lock fs-4"
          :aria-label="`Vault: ${vaultProp?.name} is set to private. Only you can see and access it.`"
          :title="`Vault: ${vaultProp?.name} is set to private. Only you can see and access it.`"></span>
      </div>
      <div class="position-absolute creator-buttons-pos d-flex">
        <button class="mdi mdi-pencil text-light" data-bs-toggle="modal" data-bs-target="#edit-vault-modal"
          :aria-label="`Edit button for vault: ${vaultProp?.name}`"></button>
        <button @click="deleteVault()" class="mdi mdi-close-circle text-red"
          :aria-label="`Delete button for vault: ${vaultProp?.name}`"
          :title="`Delete button for vault: ${vaultProp?.name}`"></button>
      </div>
    </div>
    <RouterLink :to="{ name: 'Vault Page', params: { vaultId: vaultProp?.id } }">
      <div>
        <img :src="vaultProp?.img" :alt="`A picture for vault: ${vaultProp?.name}`"
          :title="`A picture for vault: ${vaultProp?.name}`" class="img-fluid rounded">
        <div class="position-absolute absolute-bottom">
          <button class="ms-3 fs-2">{{ vaultProp?.name }}</button>
        </div>
      </div>
      <!-- NOTE Add a blurred glass background to title and creator buttons to see if that makes them easier to see in all cases (ie same color back ground as text/buttons) -->
    </RouterLink>
  </div>
</template>


<style lang="scss" scoped>
.lock-icon-pos {
  left: 0;
  margin-left: 5px;
}
</style>