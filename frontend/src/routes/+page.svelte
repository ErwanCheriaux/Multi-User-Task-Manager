<script lang="ts">
	import { goto } from '$app/navigation';
	import CategoryManagment from '$lib/components/CategoryManagment.svelte';
	import DutyCard from '$lib/components/DutyCard.svelte';
	import DutyForm from '$lib/components/DutyForm.svelte';
	import Modal from '$lib/components/Modal.svelte';
	import type { PageData } from './$types';

	export let data: PageData;

	$: user = data.user;
	$: categories = data.categories;

	let showDutyFormModal = false;
	let showCategoryManagmentModal = false;

	let emptyDuty = {
		id: null,
		label: 'My new task',
		categoryId: null,
		endDate: new Date(),
		isCompleted: false
	};

	let selectedDuty = emptyDuty;

	// eslint-disable-next-line @typescript-eslint/no-explicit-any
	const handleEditDuty = (duty?: any) => {
		selectedDuty = duty || emptyDuty;
		showDutyFormModal = true;
	};

	const handleEditCategory = () => {
		showCategoryManagmentModal = true;
	};

	const handleLogout = async () => {
		try {
			const endpoint = import.meta.env.VITE_API_BASE_URL + '/api/auth/logout';
			const response = await fetch(endpoint, {
				method: 'POST',
				credentials: 'include'
			});

			if (!response.ok) {
				console.error('Logout failed!', response.statusText);
				return { success: false };
			}

			goto('auth/login');
		} catch (error) {
			console.error('Logout failed!', error);
			return { success: false };
		}
	};
</script>

<h1 class="center">Welcome {user.firstname} {user.lastname}</h1>

<button on:click={() => handleEditDuty()}>Add Task</button>
<Modal bind:showModal={showDutyFormModal}>
	<DutyForm duty={selectedDuty} {categories}></DutyForm>
</Modal>

<button on:click={() => handleEditCategory()}>Edit category</button>
<Modal bind:showModal={showCategoryManagmentModal}>
	<CategoryManagment bind:categories></CategoryManagment>
</Modal>

{#each data.duties as duty}
	<DutyCard {duty} {categories}>
		<form method="POST">
			<input type="hidden" name="id" value={duty.id} />
			<button on:click|preventDefault={() => handleEditDuty(duty)}>Edit</button>
			<button formaction="?/delete">Remove</button>
		</form>
	</DutyCard>
{:else}
	<p>No task</p>
{/each}

<button class="logout" on:click={handleLogout}>Logout</button>

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
