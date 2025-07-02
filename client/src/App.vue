<script setup>
import CreateKeepFormModal from './components/CreateKeepFormModal.vue';
import CreateVaultFormModal from './components/CreateVaultFormModal.vue';
import EditAccountFormModal from './components/EditAccountFormModal.vue';
import EditKeepFormModal from './components/EditKeepFormModal.vue';
import EditVaultFormModal from './components/EditVaultFormModal.vue';
import KeepModal from './components/KeepModal.vue';
import Navbar from './components/Navbar.vue';
import { RouterView } from 'vue-router';

</script>

<template>
  <header>
    <Navbar />
  </header>
  <main>
    <RouterView />
  </main>
  <footer class=" text-center">
    Made with <i class="mdi mdi-heart text-pink"></i> by CodeWorks
  </footer>
  <CreateKeepFormModal />
  <CreateVaultFormModal />
  <EditAccountFormModal />
  <EditKeepFormModal />
  <EditVaultFormModal />
  <KeepModal />
</template>

<style lang="scss"></style>
