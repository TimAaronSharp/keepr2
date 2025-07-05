<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/KeepCard.vue';
import VaultCard from '@/components/VaultCard.vue';
import { accountService } from '@/services/AccountService.js';
import { keepsService } from '@/services/KeepsService.js';
import { profilesService } from '@/services/ProfilesService.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted, watch } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const account = computed(() => AppState.account)
const profile = computed(() => AppState.profile)
const vaults = computed(() => AppState.vaults)
const keeps = computed(() => AppState.keeps)
const route = useRoute()
const router = useRouter()

onMounted(() => {
  getProfile()
})

watch(route, () => {
  getProfile()
})



async function getProfile() {
  try {
    await profilesService.getProfile(route.params.profileId)
    getKeepsByProfileId()
    getVaultsByProfileId()
  }
  catch (error) {
    Pop.error(error, `Could not get profile ${route.params.profileId}`);
    logger.error(`Could not get profile ${route.params.profileId}`.toUpperCase(), error)
    router.push({ name: 'Home' })
  }
}

async function getKeepsByProfileId() {
  try {
    await keepsService.getByProfileId(route.params.profileId)
  }
  catch (error) {
    Pop.error(error, "Could not get keeps by profile id");
    logger.error("Could not get keeps by profile id", error)
  }
}

async function getVaultsByProfileId() {
  try {
    await vaultsService.getByProfileId(profile.value.id)
  }
  catch (error) {
    Pop.error(error, `Could not get vaults by profile id ${profile.value.id}`);
    logger.error(`Could not get vaults by profile id ${profile.value.id}`.toUpperCase(), error)
  }
}

async function editToggle() {
  document.getElementById('toggle-edit-account').classList.toggle('edit-toggle')
  document.getElementById('border-edit-account').classList.toggle('edit-border')
}

</script>


<template>
  <section class="container">
    <div class="row justify-content-center mt-5">
      <div class="col-11">
        <div v-if="profile" class="position-relative d-flex flex-column w-100 align-items-center">
          <div class="w-100">
            <img v-if="profile?.coverImg" class="img-fluid rounded w-100 cover-img element-shadow"
              :src="profile?.coverImg" :alt="`Cover image for ${profile?.name}'s profile page.'`"
              :title="`Cover image for ${profile?.name}'s profile page.'`">
            <img v-else class="img-fluid rounded w-100 cover-img element-shadow"
              src="https://images.pexels.com/photos/268941/pexels-photo-268941.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500"
              :alt="`Default cover image for ${profile?.name}'s profile page.'`"
              :title="`Default cover image for ${profile?.name}'s profile page.'`">
          </div>
          <div class="position-absolute position-absolute-picture text-center">
            <img class="profile-img element-shadow" :src="profile?.picture" :alt="`${profile.name}'s profile picture.'`"
              :title="`${profile.name}'s profile picture.'`">
            <p class="mt-4 fs-1 fw-bold mb-0">{{ profile?.name }}</p>
            <p class="fs-5">{{ vaults?.length }} Vaults | {{ keeps?.length }} Keeps</p>
          </div>
          <div v-if="account?.id == profile?.id" class="transparent-btn-style">
            <div class="position-absolute position-absolute-edit">
              <button @click="editToggle()" type="button" class="mdi mdi-dots-horizontal fs-1"></button>
            </div>
            <div class="position-absolute position-absolute-edit-option" id="border-edit-account">
              <button class="edit-toggle fw-bold" type="button" id="toggle-edit-account" data-bs-toggle="modal"
                data-bs-target="#edit-account-modal">Edit Profile</button>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="row content-margin-top">
      <p class="fs-1 fw-bold">Vaults</p>
      <div class="col-6 col-md-3 outline-style" v-for="vault in vaults" :key="'vault ' + vault?.id">
        <VaultCard :vaultProp="vault" />
      </div>
    </div>
    <div class="row">
      <p class="fs-1 fw-bold">Keeps</p>
      <div class="col-12">
        <div class="masonry-container mt-4">
          <div class="w-100" v-for="keep in keeps" :key="'key ' + keep?.id">
            <KeepCard :keepProp="keep" />
          </div>
        </div>
      </div>
    </div>
  </section>
</template>


<style lang="scss" scoped>
.cover-img {
  height: 50dvh;
  object-fit: cover;
  object-position: center;
}

.profile-img {
  height: 20dvh;
  border-radius: 50%;
  border: 2px solid white;
}

.position-absolute-picture {
  top: 360px;
}

.position-absolute-edit {
  top: 460px;
  right: 20px;
}

.position-absolute-edit-option {
  top: 520px;
  right: 0;
  border-radius: 20px;
  padding: 5px;
}

.content-margin-top {
  margin-top: 25dvh;
}

.edit-border {
  border: 2px solid black;
  background-color: #ded6e9;
}

.edit-toggle {
  display: none;
  overflow: hidden;
}
</style>