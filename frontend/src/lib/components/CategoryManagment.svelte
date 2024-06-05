<script lang="ts">
	export let categories: { id: number; name: string; isReadOnly: boolean }[];

	let newCategoryName = '';
	let selectedCategoryId: number | null = null;
	let selectedCategoryName = '';

	async function addCategory() {
		try {
			const endpoint = import.meta.env.VITE_API_BASE_URL + '/api/category';
			const response = await fetch(endpoint, {
				method: 'POST',
				headers: { 'Content-Type': 'application/json' },
				body: JSON.stringify({ name: newCategoryName }),
				credentials: 'include'
			});

			if (!response.ok) {
				console.error('Create category failed!', response.statusText);
				return { success: false };
			}

			const newCategory = await response.json();
			categories.push(newCategory);
			categories = categories;

			newCategoryName = '';
			return { success: true };
		} catch (error) {
			console.error('Error adding category:', error);
			return { success: false };
		}
	}

	async function updateCategory() {
		try {
			const endpoint = import.meta.env.VITE_API_BASE_URL + '/api/category/' + selectedCategoryId;
			const response = await fetch(endpoint, {
				method: 'PUT',
				headers: { 'Content-Type': 'application/json' },
				body: JSON.stringify({ name: selectedCategoryName }),
				credentials: 'include'
			});

			if (!response.ok) {
				console.error('Update category failed!', response.statusText);
				return { success: false };
			}

			const updatedCategory = await response.json();
			const index = categories.findIndex((c) => c.id === updatedCategory.id);
			if (index !== -1) {
				categories[index] = updatedCategory;
				categories = categories;
			}

			selectedCategoryId = null;
			selectedCategoryName = '';
		} catch (error) {
			console.error('Error updating category:', error);
			return { success: false };
		}
	}

	async function deleteCategory(id: number) {
		try {
			const endpoint = import.meta.env.VITE_API_BASE_URL + '/api/category/' + id;
			const response = await fetch(endpoint, {
				method: 'DELETE',
				credentials: 'include'
			});

			if (!response.ok) {
				console.error('Delete category failed!', response.statusText);
				return { success: false };
			}

			const index = categories.findIndex((c) => c.id === id);
			if (index !== -1) {
				categories.splice(index, 1);
				categories = categories;
			}
		} catch (error) {
			console.error('Error deleting category:', error);
			return { success: false };
		}
	}

	function selectCategory(category: { id: number; name: string }) {
		selectedCategoryId = category.id;
		selectedCategoryName = category.name;
	}
</script>

<h2>Categories</h2>
<ul>
	{#each categories as category}
		<li>
			{category.name}
			{#if !category.isReadOnly}
				<button on:click={() => selectCategory(category)}>Edit</button>
				<button on:click={() => deleteCategory(category.id)}>Delete</button>
			{/if}
		</li>
	{/each}
</ul>

{#if selectedCategoryId === null}
	<h2>Add Category</h2>
	<input type="text" bind:value={newCategoryName} placeholder="New category name" />
	<button on:click={addCategory}>Add</button>
{:else}
	<h2>Update Category</h2>
	<input type="text" bind:value={selectedCategoryName} />
	<button on:click={updateCategory}>Update</button>
{/if}
