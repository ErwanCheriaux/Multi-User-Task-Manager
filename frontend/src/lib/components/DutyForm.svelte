<script lang="ts">
	export let duty: {
		id: number | null;
		label: string;
		categoryId: number | null;
		endDate: Date;
		isCompleted: boolean;
	};

	$: formatedEnddate = new Date(duty.endDate).toISOString().split('T')[0];
</script>

<form method="POST" action="?/save">
	<input type="hidden" name="id" value={duty.id} />

	<label>
		Label:
		<input type="text" name="label" value={duty.label} required />
	</label>

	<label>
		CategoryId:
		<input type="number" name="categoryId" value={duty.categoryId} />
	</label>

	<label>
		End Date:
		<input type="date" name="endDate" value={formatedEnddate} required />
	</label>

	<label>
		Is Completed:
		<input type="checkbox" name="isCompleted" checked={duty.isCompleted} />
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
