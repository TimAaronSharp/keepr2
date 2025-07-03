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
  <div class="position-relative text-light text-shadow fw-bold my-3 transparent-btn-style element-shadow rounded">
    <div v-if="account?.id == vaultProp.creatorId" class="position-absolute justify-content-between fs-4 d-flex w-100">
      <div v-if="vaultProp.isPrivate" class="d-flex lock-icon-pos">
        <span class="mdi mdi-lock fs-4"
          :aria-label="`Vault: ${vaultProp?.name} is set to private. Only you can see and access it.`"
          :title="`Vault: ${vaultProp?.name} is set to private. Only you can see and access it.`"></span>
      </div>
      <button class="mdi mdi-pencil text-light p-0 edit-button" data-bs-toggle="modal"
        data-bs-target="#edit-vault-modal" :aria-label="`Edit button for vault: ${vaultProp?.name}`"></button>
      <button @click="deleteVault()" class="mdi mdi-close-circle text-red p-0 delete-button"
        :aria-label="`Delete button for vault: ${vaultProp?.name}`"
        :title="`Delete button for vault: ${vaultProp?.name}`"></button>
    </div>
    <RouterLink :to="{ name: 'Vault Page', params: { vaultId: vaultProp?.id } }">
      <div>
        <!-- NOTE Investigate solutions for ensuring the img fills the div like the test images do (in the final you ended up giving the imgs specific px sizes and that ended up working, even seeming to resize properly, but it would be good to contact Jeremy/Mick/Jake to see if I'm forgetting how some things with bootstrap work (why the sizing with the test images seems to work differently compared to the big landscape img I used, imgs don't seem to fill to the top/bottom of the card, etc.))-->
        <img :src="vaultProp?.img" :alt="`A picture for vault: ${vaultProp?.name}`"
          :title="`A picture for vault: ${vaultProp?.name}`" class="img-fluid rounded img-pos">
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

.img-pos {
  object-fit: cover;
  aspect-ratio: 1.25/1;
}

.creator-buttons {
  left: 0;
  top: 0;
  margin-right: 5px;
}

.edit-button {
  margin-left: auto;
}

.delete-button {
  margin-right: 5px;
}
</style>