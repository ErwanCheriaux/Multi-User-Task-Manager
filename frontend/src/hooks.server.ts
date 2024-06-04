export async function handle({ event, resolve }) {
	try {
		const endpoint = import.meta.env.VITE_API_BASE_URL + '/status';
		const response = await event.fetch(endpoint, {
			method: 'GET',
			credentials: 'include'
		});

		if (!response.ok) {
			throw new Error(response.statusText);
		}

		const { email, firstname, lastname } = await response.json();
		event.locals.session = { email, firstname, lastname };
	} catch (error) {
		console.error('Get status failed!', error);
	}
	return await resolve(event);
}
