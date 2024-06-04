import { redirect } from '@sveltejs/kit';
import type { PageServerLoad } from './$types';

export const load = (async ({ fetch }) => {
	try {
		const endpoint = import.meta.env.VITE_API_BASE_URL + '/status';
		const response = await fetch(endpoint, {
			method: 'GET',
			credentials: 'include'
		});

		if (!response.ok) {
			throw new Error(response.statusText);
		}

		const result = await response.json();
		console.log('result: ', result);

		const { email, firstname, lastname } = result;
		return { email, firstname, lastname };
	} catch (error) {
		console.error('Get status failed!', error);
		throw redirect(302, '/auth/login');
	}
}) satisfies PageServerLoad;
