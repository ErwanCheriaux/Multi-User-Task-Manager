import { redirect } from '@sveltejs/kit';
import type { PageServerLoad } from './$types';

// redirect to login page if not logged in
const getStatus = async (fetch: any) => {
    try {
		const endpoint = import.meta.env.VITE_API_BASE_URL + '/status';
		const response = await fetch(endpoint, {
			method: 'GET',
			credentials: 'include'
		});

		if (!response.ok) {
			throw new Error(response.statusText);
		}

		return await response.json();
	} catch (error) {
		console.error('Get status failed!', error);
		throw redirect(302, '/auth/login');
	}
}

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
		console.error('Get status failed!', error);
	}
}

export const load = (async ({ fetch }) => {

    return {
        user: await getStatus(fetch),
		duties: await getDuties(fetch)
    }
}) satisfies PageServerLoad;
