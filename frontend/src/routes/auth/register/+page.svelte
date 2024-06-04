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
	<input type="submit" value="Register" />

	<a class="login" href="/auth/login">Login</a>

	{#if form?.success === false}
		<p class="error-message">Something went wrong!</p>
	{/if}
</form>

<style>
	/* General form styles */
	form {
		max-width: 300px;
		margin: 50px auto;
		padding: 20px;
		background-color: #f3f3f3;
		border-radius: 8px;
		box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
	}

	/* Style for the form elements */
	form input[type='text'],
	form input[type='password'],
	form input[type='email'],
	form input[type='submit'] {
		width: 100%;
		padding: 10px;
		margin: 10px 0;
		border: 1px solid #ddd;
		border-radius: 4px;
		box-sizing: border-box; /* Include padding and border in the element's width and height */
	}

	/* Style for the submit button */
	form input[type='submit'] {
		background-color: #5cb85c;
		color: white;
		cursor: pointer;
	}

	form input[type='submit']:hover {
		background-color: #4cae4c;
	}

	/* Placeholder text style */
	form input::placeholder {
		color: #999;
	}

	/* Focus styles for form elements */
	form input[type='text']:focus,
	form input[type='password']:focus,
	form input[type='email']:focus {
		border-color: #66afe9;
		outline: none;
	}

	/* Label styles */
	form label {
		display: block;
		margin-bottom: 5px;
		color: #333;
	}

	/* Form header styles */
	h1 {
		text-align: center;
		margin-bottom: 20px;
	}

	/* Error message styles */
	.error-message {
		color: #d9534f;
		margin-bottom: 10px;
	}
</style>
