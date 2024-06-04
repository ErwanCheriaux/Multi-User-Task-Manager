import { redirect } from '@sveltejs/kit';
import type { Actions } from './$types';
import cookie, { type CookieSerializeOptions } from 'cookie';

export const actions = {
	default: async ({ fetch, request, cookies }) => {
		const data = await request.formData();
		const email = data.get('email');
		const password = data.get('password');
		const remember = data.get('remember');

		try {
			let endpoint = import.meta.env.VITE_API_BASE_URL + '/login';
			endpoint += remember ? '?useCookies=true' : '?useSessionCookies=true';

			const response = await fetch(endpoint, {
				method: 'POST',
				headers: { 'Content-Type': 'application/json' },
				body: JSON.stringify({ email, password }),
				credentials: 'include'
			});

			if (!response.ok) {
				console.error('Login failed!', response.statusText);
				return { success: false };
			}

			const setCookieHeader = response.headers.get('Set-Cookie') ?? '';
			const parsedCookie = cookie.parse(setCookieHeader);
			if (parsedCookie['.AspNetCore.Identity.Application']) {
				const name = '.AspNetCore.Identity.Application';
				const value = parsedCookie['.AspNetCore.Identity.Application'];
				const path = parsedCookie.path;
				const opts: CookieSerializeOptions = {
					expires: parsedCookie.expires ? new Date(parsedCookie.expires) : undefined,
					sameSite: parsedCookie.samesite as 'lax' | 'strict' | 'none' | undefined
				};

				cookies.set(name, value, { ...opts, path: path });
			}
		} catch (error) {
			console.error('Login failed!', error);
			return { success: false };
		}

		redirect(302, '/');
	}
} satisfies Actions;
