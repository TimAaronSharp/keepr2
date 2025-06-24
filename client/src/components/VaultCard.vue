<script setup>
import { AppState } from '@/AppState.js';
import { Vault } from '@/models/Vault.js';
import { computed } from 'vue';


const account = computed(() => AppState.account)

const props = defineProps({
  vaultProp: { type: Vault, required: true }
})



</script>


<template>
  <div class="position-relative text-light text-shadow fw-bold my-3 transparent-btn-style">
    <div v-if="account?.id == vaultProp.creatorId" class="position-absolute justify-content-between fs-4 d-flex w-100">
      <div v-if="vaultProp.isPrivate" class="d-flex lock-icon-pos">
        <span class="mdi mdi-lock fs-4"
          :aria-label="`Vault: ${vaultProp?.name} is set to private. Only you can see and access it.`"
          :title="`Vault: ${vaultProp?.name} is set to private. Only you can see and access it.`"></span>
      </div>
      <button class="mdi mdi-pencil text-light p-0 edit-button" data-bs-toggle="modal"
        data-bs-target="#edit-vault-modal" :aria-label="`Edit button for vault: ${vaultProp?.name}`"></button>
      <button class="mdi mdi-close-circle text-red p-0 delete-button"
        :aria-label="`Delete button for vault: ${vaultProp?.name}`"
        :title="`Delete button for vault: ${vaultProp?.name}`"></button>
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