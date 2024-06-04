<script lang="ts">
	import type { PageData } from './$types';

	export let data: PageData;

	$: user = data.user;
</script>

<h1>Welcome {user.firstname} {user.lastname}</h1>
<a href="/">Logout</a>

{#each data.duties as duty}
	<pre>{duty}</pre>
{:else}
	<p>No task</p>
{/each}
