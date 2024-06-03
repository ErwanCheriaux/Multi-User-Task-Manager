<script lang="ts">
	import { createEventDispatcher } from 'svelte';
	const dispatch = createEventDispatcher();

	let email = '';
	let password = '';
	let firstName = '';
	let lastName = '';

	const register = async () => {
		dispatch('register', { email, password, firstName, lastName });
	};
</script>

<form on:submit|preventDefault={register}>
	<h2>Register</h2>
	<div>
		<label for="email">Email:</label>
		<input id="email" type="email" bind:value={email} required />
	</div>
	<div>
		<label for="password">Password:</label>
		<input id="password" type="password" bind:value={password} required />
	</div>
	<div>
		<label for="firstName">First Name:</label>
		<input id="firstName" type="text" bind:value={firstName} required />
	</div>
	<div>
		<label for="lastName">Last Name:</label>
		<input id="lastName" type="text" bind:value={lastName} required />
	</div>
	<button type="submit">Register</button>
</form>
