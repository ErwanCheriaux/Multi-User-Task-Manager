<script lang="ts">
	export let duty: {
		id: number | null;
		label: string;
		category: string;
		endDate: Date;
		isCompleted: boolean;
	};
</script>

<form method="POST" action="?/saveDutyForm">
	<label>
		Label:
		<input type="text" name="label" bind:value={duty.label} required />
	</label>

	<label>
		Category:
		<input type="text" name="category" bind:value={duty.category} required />
	</label>

	<label>
		End Date:
		<input type="date" name="endDate" bind:value={duty.endDate} required />
	</label>

	<label>
		Is Completed:
		<input type="checkbox" name="isCompleted" bind:checked={duty.isCompleted} />
	</label>

	<button type="submit">Save Changes</button>
</form>

<style>
	label {
		display: block;
		margin-top: 0.5rem;
	}
	input,
	button {
		margin-top: 0.5rem;
	}
</style>
