<script lang="ts">
	import Modal from '$lib/components/Modal.svelte';
	import Register from '$lib/components/Register.svelte';
	import Login from '$lib/components/Login.svelte';

	let showRegisterModal = false;
	let showLoginModal = false;
</script>

<h1>Welcome to Multi-User Task Manager</h1>
<button on:click={() => (showRegisterModal = true)}>Register</button>
<button on:click={() => (showLoginModal = true)}>Login</button>

<Modal bind:showModal={showRegisterModal}>
	<Register></Register>
</Modal>

<Modal bind:showModal={showLoginModal}>
	<Login></Login>
</Modal>
