export async function handle({ event, resolve }) {
	try {
		const endpoint = import.meta.env.VITE_API_BASE_URL + '/api/auth/status';
		const response = await event.fetch(endpoint, {
			method: 'GET',
			credentials: 'include'
		});

		if (response.ok) {
			const { email, firstname, lastname } = await response.json();
			event.locals.session = { email, firstname, lastname };
		} else {
			event.locals.session = undefined;
			// Throw error if not Unauthorize
			if (response.status !== 401) throw new Error(response.statusText);
		}
	} catch (error) {
		console.error('Get status failed!', error);
	}
	return await resolve(event);
}
