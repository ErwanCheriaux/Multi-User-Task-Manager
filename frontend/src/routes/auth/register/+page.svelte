<script lang="ts">
	import type { ActionData } from './$types';

	export let form: ActionData;
</script>

<h1>Register page</h1>
<form method="POST">
	<label>
		First Name
		<input name="firstname" type="text" />
	</label>
	<label>
		Last Name
		<input name="lastname" type="text" />
	</label>
	<label>
		Email
		<input name="email" type="email" />
	</label>
	<label>
		Password
		<input name="password" type="password" />
	</label>
	<button>Register</button>
</form>

<a class="login" href="/auth/login">Login</a>

{#if form?.success === false}
	<p>Something went wrong...</p>
{/if}

<style>
	form {
		display: grid;
		row-gap: 0.3em;
	}
	.login {
		font-size: x-large;
		margin-top: 10px;
		margin-right: 20px;
		position: absolute;
		top: 0;
		right: 0;
	}
</style>
