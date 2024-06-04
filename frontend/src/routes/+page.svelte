<script lang="ts">
	import type { PageData } from './$types';

	export let data: PageData;
</script>

<h1>Welcome {data.firstname} {data.lastname}</h1>
<a href="/">Logout</a>
