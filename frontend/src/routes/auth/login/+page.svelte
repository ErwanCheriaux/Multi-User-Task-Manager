<script lang="ts">
	import type { ActionData } from './$types';

	export let form: ActionData;
</script>

<h1>Login page</h1>
<form method="POST">
	<label>
		Email
		<input name="email" type="email" />
	</label>
	<label>
		Password
		<input name="password" type="password" />
	</label>
	<label>
		Remember Me
		<input name="remember" type="checkbox" />
	</label>
	<button>Log in</button>
</form>

<a href="/auth/register">Register</a>

{#if form?.success === false}
	<p>Wrong credentials!</p>
{/if}

<style>
	form {
		display: grid;
		row-gap: 0.3em;
	}
</style>
