import { redirect } from '@sveltejs/kit';
import type { Actions } from './$types';

export const actions = {
	default: async ({ fetch, request, locals }) => {
		const data = await request.formData();
		const firstname = data.get('firstname');
		const lastname = data.get('lastname');
		const email = data.get('email');
		const password = data.get('password');

		try {
			// register the new user
			const endpoint = import.meta.env.VITE_API_BASE_URL + '/register';
			const response = await fetch(endpoint, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify({ email, password })
			});

			if (!response.ok) {
				console.error('Register failed!', response.statusText);
				return { success: false };
			}
		} catch (error) {
			console.error('Register failed!', error);
			return { success: false };
		}

		redirect(302, '/auth/login');
	}
} satisfies Actions;
