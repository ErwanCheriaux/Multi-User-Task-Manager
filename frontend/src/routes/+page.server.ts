import { redirect } from '@sveltejs/kit';
import type { PageServerLoad, Actions } from './$types';

const getDuties = async (fetch: any) => {
	try {
		const endpoint = import.meta.env.VITE_API_BASE_URL + '/api/duty';
		const response = await fetch(endpoint, {
			method: 'GET',
			credentials: 'include'
		});

		if (!response.ok) {
			throw new Error(response.statusText);
		}

		return await response.json();
	} catch (error) {
		console.error('Get duty failed!', error);
	}
};

export const load = (async ({ fetch, locals }) => {
	// redirect to login page if no session
	if (!locals.session) throw redirect(302, '/auth/login');

	const firstname = locals.session.firstname;
	const lastname = locals.session.lastname;

	return {
		user: { firstname, lastname },
		duties: await getDuties(fetch)
	};
}) satisfies PageServerLoad;

export const actions = {
	saveDutyForm: async ({ request, locals }) => {
		const data = await request.formData();
		const id = data.get('id');
		const email = locals.session?.email;
		const label = data.get('label');
		const category = data.get('category');
		const endDate = data.get('endDate');
		const isCompleted = data.has('isCompleted');

		try {
			let endpoint = import.meta.env.VITE_API_BASE_URL + '/api/duty';
			let method = 'POST';

			// save existing duty
			if (id !== null) {
				endpoint += '/' + id;
				method = 'PUT';
			}

			const response = await fetch(endpoint, {
				method,
				headers: { 'Content-Type': 'application/json' },
				body: JSON.stringify({ email, label, category, endDate, isCompleted }),
				credentials: 'include'
			});

			if (!response.ok) {
				console.error('Save task failed!', response.statusText);
				return { success: false };
			}
		} catch (error) {
			console.error('Save task failed!', error);
			return { success: false };
		}
	}
} satisfies Actions;
