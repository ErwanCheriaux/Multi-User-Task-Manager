import { redirect } from '@sveltejs/kit';
import type { PageServerLoad, Actions } from './$types';

// eslint-disable-next-line @typescript-eslint/no-explicit-any
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

// eslint-disable-next-line @typescript-eslint/no-explicit-any
const getCategories = async (fetch: any) => {
	try {
		const endpoint = import.meta.env.VITE_API_BASE_URL + '/api/category';
		const response = await fetch(endpoint, {
			method: 'GET',
			credentials: 'include'
		});

		if (!response.ok) {
			throw new Error(response.statusText);
		}

		return await response.json();
	} catch (error) {
		console.error('Get category failed!', error);
	}
};

export const load = (async ({ fetch, locals }) => {
	// redirect to login page if no session
	if (!locals.session) throw redirect(302, '/auth/login');

	const firstname = locals.session.firstname;
	const lastname = locals.session.lastname;

	return {
		user: { firstname, lastname },
		duties: await getDuties(fetch),
		categories: await getCategories(fetch)
	};
}) satisfies PageServerLoad;

export const actions = {
	save: async ({ fetch, request }) => {
		const data = await request.formData();
		const id = data.get('id');
		const label = data.get('label');
		const categoryId = data.get('categoryId');
		const endDate = data.get('endDate');
		const isCompleted = data.has('isCompleted');

		try {
			let endpoint = import.meta.env.VITE_API_BASE_URL + '/api/duty';
			let method = 'POST';

			// save existing duty
			if (id) {
				endpoint += '/' + id;
				method = 'PUT';
			}

			const response = await fetch(endpoint, {
				method,
				headers: { 'Content-Type': 'application/json' },
				body: JSON.stringify({ label, categoryId, endDate, isCompleted }),
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
	},
	delete: async ({ fetch, request }) => {
		const data = await request.formData();
		const id = data.get('id');

		try {
			const endpoint = import.meta.env.VITE_API_BASE_URL + '/api/duty/' + id;
			const response = await fetch(endpoint, {
				method: 'DELETE',
				credentials: 'include'
			});

			if (!response.ok) {
				console.error('Delete task failed!', response.statusText);
				return { success: false };
			}
		} catch (error) {
			console.error('Delete task failed!', error);
			return { success: false };
		}
	}
} satisfies Actions;
