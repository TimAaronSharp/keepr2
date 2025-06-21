<script setup>
import { AppState } from '@/AppState.js';
import { Vault } from '@/models/Vault.js';
import { computed } from 'vue';


const account = computed(() => AppState.account)

const props = defineProps({
  vaultProp: { type: Vault, required: true }
})

async function deleteVault() {

}

</script>


<template>
  <div class="position-relative text-light text-shadow fw-bold my-3">
    <button v-if="account?.id == vaultProp.creatorId" @click="deleteVault()"
      class="creator-buttons position-absolute mdi mdi-close-circle fs-6"
      :aria-label="`Delete button for keep titled ${vaultProp?.name}`"
      :title="`Delete button for keep: ${vaultProp?.name}`"></button>
    <RouterLink :to="{ name: 'Vault Page', params: { vaultId: vaultProp?.id } }">
      <div v-if="vaultProp.isPrivate">
        <div class="position-absolute fs-4">
          <span class="mdi mdi-lock"
            :aria-label="`${vaultProp?.name} is set to private. Only you can see and access it.`"
            :title="`${vaultProp?.name} is set to private. Only you can see and access it.`"></span>
        </div>
      </div>
      <div>
        <img :src="vaultProp?.img" :alt="`A picture for vault ${vaultProp?.name}`"
          :title="`A picture for vault ${vaultProp?.name}`">
      </div>
      <div class="position-absolute">
        <p class="ms-3 fs-2">{{ vaultProp?.name }}</p>
      </div>
    </RouterLink>
  </div>
</template>


<style lang="scss" scoped></style>