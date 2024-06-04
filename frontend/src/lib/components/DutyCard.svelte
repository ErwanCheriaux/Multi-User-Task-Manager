<script lang="ts">
	// Duty object
	export let duty: {
		id: number;
		label: string;
		category: string;
		endDate: Date;
		isCompleted: boolean;
	};

	// Format the date for display
	const options: Intl.DateTimeFormatOptions = {
		year: 'numeric',
		month: 'long',
		day: 'numeric'
	};

	const formattedEndDate = new Date(duty.endDate).toLocaleDateString('en-US', options);
</script>

<div class="card">
	<h2 class={duty.isCompleted ? 'completed' : ''}>{duty.label}</h2>
	<p>Category: {duty.category}</p>
	<p>End Date: {formattedEndDate}</p>
	<p>Status: {duty.isCompleted ? 'Completed' : 'Pending'}</p>
	<slot />
</div>

<style>
	.card {
		border: 1px solid #ccc;
		border-radius: 8px;
		padding: 16px;
		margin: 10px;
		width: 300px;

		/* Add shadows to create the "card" effect */
		box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2);
		transition: 0.3s;
	}
	.completed {
		text-decoration: line-through;
	}
</style>
