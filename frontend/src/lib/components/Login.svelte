<script lang="ts">
	import { createEventDispatcher } from 'svelte';
	const dispatch = createEventDispatcher();

	let email = '';
	let password = '';

	const login = async () => {
		dispatch('login', { email, password });
	};
</script>

<form on:submit|preventDefault={login}>
	<h2>Login</h2>
	<div>
		<label for="email">Email:</label>
		<input id="email" type="email" bind:value={email} required />
	</div>
	<div>
		<label for="password">Password:</label>
		<input id="password" type="password" bind:value={password} required />
	</div>
	<button type="submit">Login</button>
</form>
