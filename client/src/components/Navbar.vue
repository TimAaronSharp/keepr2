<script setup>
import { computed, ref, watch } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import { AppState } from '@/AppState.js';

const theme = ref(loadState('theme') || 'light')
const account = computed(() => AppState.account)

function toggleTheme() {
  theme.value = theme.value == 'light' ? 'dark' : 'light'
}

watch(theme, () => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
  saveState('theme', theme.value)
}, { immediate: true })

</script>

<template>
  <nav class="navbar navbar-expand-md border-bottom sticky-navbar bg-color">
    <div class="container">
      <div class="d-flex gap-2 flex-1">
        <RouterLink :to="{ name: 'Home' }" class="home-button-router-link">
          <button class="d-flex align-items-center btn home-button fw-bold">Home</button>
        </RouterLink>
        <div v-if="account" class="d-flex align-items-center dropdown">
          <button role="button" class="transparent-btn-style fw-bold py-0 create-menu-mobile" data-bs-toggle="dropdown"
            aria-expanded="false" title="Open Create Menu">
            Create
            <span class="mdi mdi-arrow-down-drop-circle-outline"></span>
          </button>
          <div class="dropdown-menu dropdown-menu-start p-0" role="menu">
            <div class="list-group">
              <button data-bs-toggle="modal" data-bs-target="#create-keep-modal"
                class="list-group-item dropdown-item list-group-item-action selectable" title="Create New Keep">
                Create Keep</button>
              <button data-bs-toggle="modal" data-bs-target="#create-vault-modal"
                class="list-group-item dropdown-item list-group-item-action selectable" title="Create New Vault">
                Create Vault</button>
            </div>
          </div>
        </div>
      </div>
      <RouterLink :to="{ name: 'Home' }" id="keepr-logo" class="d-flex align-items-center flex-1">
        <img alt=" logo" src="/src\assets\img\keepr-logo.png" height="45" />
      </RouterLink>
      <!-- <b class="fs-5">Vue Starter</b> -->
      <!-- NOTE Will revisit if I decide to implement anything that a collapsable button would be useful for -->
      <!-- collapse button -->
      <!-- <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbar-links"
        aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
        <span class="mdi mdi-menu text-light"></span>
      </button> -->
      <!-- collapsing menu -->
      <!-- <div class="collapse navbar-collapse " id="navbar-links">
        <ul class="navbar-nav">
          <li>
            <RouterLink :to="{ name: 'About' }" class="btn text-green selectable">
              About
            </RouterLink>
          </li>
        </ul> -->
      <!-- LOGIN COMPONENT HERE -->
      <!-- NOTE Will revisit integrating theme toggling later -->
      <!-- <div class="ms-auto"> -->
      <!-- <button class="btn text-light" @click="toggleTheme"
            :title="`Enable ${theme == 'light' ? 'dark' : 'light'} theme.`">
            <i v-if="theme == 'dark'" class="mdi mdi-weather-sunny"></i>
            <i v-if="theme == 'light'" class="mdi mdi-weather-night"></i>
          </button> -->
      <!-- </div> -->
      <Login class="py-0" />
    </div>
    <!-- </div> -->
  </nav>
</template>

<style lang="scss" scoped>
a {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.border-bottom {
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.5);
}

.sticky-navbar {
  position: fixed;
  width: 100%;
  z-index: 6;
}

.flex-1 {
  flex: 1;
}

.home-button {
  background-color: #e9d8d6;
  border-radius: 12px;
  padding: 0 10px 0 10px;
}

@media (hover: none) and (pointer: coarse) {
  .home-button-router-link {
    display: none;
  }

  .create-menu-mobile {
    padding: 0;
  }

}

a:hover {
  color: black;
}
</style>
