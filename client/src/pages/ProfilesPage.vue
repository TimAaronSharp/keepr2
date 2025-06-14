<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/KeepCard.vue';
import { computed } from 'vue';

const account = computed(() => AppState.account)
const profile = computed(() => AppState.profile)
const vaults = computed(() => AppState.vaults)
const keeps = computed(() => AppState.keeps)

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
            <img v-else
              src="https://images.pexels.com/photos/268941/pexels-photo-268941.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500"
              :alt="`Default cover image for ${profile?.name}'s profile page.'`"
              :title="`Default cover image for ${profile?.name}'s profile page.'`">
          </div>
          <div class="position-absolute position-absolute-picture text-center">
            <img class="profile-img element-shadow" :src="profile?.picture" :alt="`${profile.name}'s profile picture.'`"
              :title="`${profile.name}'s profile picture.'`">
            <p class="mt-4 fs-1-fw-bold-mb-0">{{ profile?.name }}</p>
            <p class="fs-5">{{ vaults?.length }} Vaults | {{ keeps?.length }} Keeps</p>
          </div>
          <div v-if="account?.id == profile?.id">
            <div class="position-absolute position-absolute-edit">
              <button @click="editToggle()" class="mdi mdi-dots-horizontal fs-1"></button>
            </div>
            <div class="position-absolute position-absolute-edit-option" id="border-edit-account">
              <button class="edit-toggle fw-bold" id="toggle-edit-account" data-bs-toggle="modal"
                data-bs-target="#edit-account-modal">Edit Profile</button>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="row content-margin-top">
      <p class="fs-1 fw-bold">Vaults</p>
      <div class="col-6 col-md-3" v-for="vault in vaults" :key="'vault ' + vault?.id">
        <!-- NOTE Put vaults here -->
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


<style lang="scss" scoped></style>