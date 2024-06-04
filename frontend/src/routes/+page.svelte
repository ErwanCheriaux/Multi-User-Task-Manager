<script lang="ts">
	import DutyCard from '$lib/components/DutyCard.svelte';
	import DutyForm from '$lib/components/DutyForm.svelte';
	import Modal from '$lib/components/Modal.svelte';
	import type { PageData } from './$types';

	export let data: PageData;

	$: user = data.user;

	let showDutyFormModal = false;
	let emptyDuty = {
		id: null,
		label: 'my label',
		category: 'my category',
		endDate: new Date(),
		isCompleted: false
	};

	let selectedDuty = emptyDuty;
</script>

<h1 class="center">Welcome {user.firstname} {user.lastname}</h1>

<button on:click={() => (showDutyFormModal = true)}>Add Task</button>
<Modal bind:showModal={showDutyFormModal}>
	<DutyForm bind:duty={selectedDuty}></DutyForm>
</Modal>

{#each data.duties as duty}
	<DutyCard bind:duty></DutyCard>
{:else}
	<p>No task</p>
{/each}

<a class="logout" href="/">Logout</a>

<style>
	.center {
		margin: auto;
		width: 50%;
	}
	.logout {
		font-size: x-large;
		margin-top: 10px;
		margin-right: 20px;
		position: absolute;
		top: 0;
		right: 0;
	}
</style>
