<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/KeepCard.vue';
import { vaultKeepsService } from '@/services/VaultKeepsService.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';


const account = computed(() => AppState.account)
const vault = computed(() => AppState.activeVault)
const vaultKeepTrackers = computed(() => AppState.vaultKeepTrackers)
const route = useRoute()
const router = useRouter()

onMounted(() => {
  getVaultKeepTrackersByVaultId()
  getVault()
})

async function getVault() {
  try {
    await vaultsService.getById(route.params.vaultId)
  }
  catch (error) {
    Pop.error(error, `Could not get vault: ${route.params.vaultId}`);
    logger.error(`Could not get vault: ${route.params.vaultId}`.toUpperCase(), error)
    router.push({ name: 'Home' })
  }
}

async function getVaultKeepTrackersByVaultId() {
  try {
    await vaultKeepsService.getVaultKeepTrackersByVaultId(route.params.vaultId)
  }
  catch (error) {
    Pop.error(error, `Could not get vaultKeepTrackers for vault: ${vault.value.name} id: ${vault.value.id}`);
    logger.error(`Could not get vaultKeepTrackers for vault: ${vault.value.name} id: ${vault.value.id}`.toUpperCase(), error)
  }
}

</script>


<template>
  <section class="container">
    <div class="row">
      <div class="col-12">
        <!-- NOTE Change JCC to margin-auto left/right? (likely no functional difference to what I need but experiment?) -->
        <div class="d-flex justify-content-center fw-bold">
          <div class="position-relative my-4">
            <div>
              <img class="img-fluid vault-cover-img rounded" :src="vault?.img"
                :alt="`Picture for vault: ${vault?.name}`" :title="`Picture for vault: ${vault?.name}`">
            </div>
            <div class="d-flex justify-content-center text-light text-shadow">
              <div class="position-absolute absolute-bottom text-center">
                <p class="ms-3 mb-0 fs-1 font-quando">{{ vault?.name }}</p>
                <p class="ms-3">by: {{ vault?.creator.name }}</p>
              </div>
            </div>
            <div v-if="account?.id == vault?.creatorId">
              <div class="position-absolute position-absolute-delete transparent-btn-style">
                <button class="mdi mdi-pencil fs-1" data-bs-toggle="modal" data-bs-target="#edit-vault-modal"
                  :aria-label="`Button to edit vault: ${vault?.name} id: ${vault?.id}`"></button>
              </div>
            </div>
          </div>
        </div>
        <div class="text-center">
          <!-- NOTE Will need vaultKeepTrackers to display how many keeps are in the vault (vaultKeepTrackers.length) -->
          <span class="lil-bg p-2 fw-bold">{{ vaultKeepTrackers.length }} Keeps</span>
        </div>
      </div>
    </div>
    <div class="text-center my-4 fw-bold">
      <span class="remove-keep-border rounded p-1">{{ vault?.description }}</span>
    </div>
    <div class="row">
      <div class="col-12">
        <div class="masonry-container mt-4">
          <div v-for="vaultKeepTracker in vaultKeepTrackers" :key="'vaultKeepTracker ' + vaultKeepTracker.id">
            <KeepCard :keep-prop="vaultKeepTracker" />
          </div>
        </div>
      </div>
    </div>
  </section>
</template>


<style lang="scss" scoped>
.vault-cover-img {
  height: 30dvh;
  width: 30dvw;
  object-fit: cover;
  object-position: center;
}

.lil-bg {
  background-color: #ded6e9;
  border-radius: 15px;
}

.absolute-bottom {
  bottom: 0;
}

.position-absolute-delete {
  top: 80%;
  left: 100%;
}

.remove-keep-border {
  // border: 1px solid black;
  background-color: #ded6e9;
}

.remove-keep-toggle {
  display: none;
  overflow: hidden;
}

// select {
//   // width:100%;
//   padding-left: 10px;
//   margin: 8px 0;
//   box-sizing: border-box;
//   border: none;
//   // border-color: transparent;
//   outline: none;
//   border-bottom: 2px solid #7a8186;
// }</style>