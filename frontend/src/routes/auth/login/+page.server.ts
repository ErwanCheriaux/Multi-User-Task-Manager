import { redirect } from '@sveltejs/kit';
import type { Actions } from './$types';

export const actions = {
	default: async ({fetch, request}) => {
        const data = await request.formData();
        const email = data.get('email');
        const password = data.get('password');

        try {
            const endpoint = import.meta.env.VITE_API_BASE_URL + '/login';
            const response = await fetch(endpoint, {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json'
            },
            body: JSON.stringify({ email, password })
          });
    
          if (response.ok) {
            const result = await response.json();
            console.log('result: ', result);
          } else {
            console.error('Login failed!', response.statusText);
            return { success: false };
          }
        } catch (error) {
            console.error('Login failed!', error);
            return { success: false };
        }

        redirect(302, '/');
	}
} satisfies Actions;