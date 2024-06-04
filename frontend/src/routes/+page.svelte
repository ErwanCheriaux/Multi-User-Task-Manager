<script lang="ts">
	import Modal from '$lib/components/Modal.svelte';
	import Register from '$lib/components/Register.svelte';

	let showRegisterModal = false;
</script>

<h1>Welcome to Multi-User Task Manager</h1>
<button on:click={() => (showRegisterModal = true)}>Register</button>
<a href="/auth/login">Login</a>

<Modal bind:showModal={showRegisterModal}>
	<Register></Register>
</Modal>
