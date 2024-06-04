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

<a class="register" href="/auth/register">Register</a>

{#if form?.success === false}
	<p>Wrong credentials!</p>
{/if}

<style>
	form {
		display: grid;
		row-gap: 0.3em;
	}
	.register {
		font-size: x-large;
		margin-top: 10px;
		margin-right: 20px;
		position: absolute;
		top: 0;
		right: 0;
	}
</style>
