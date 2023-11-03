export { default } from 'next-auth/middleware';

export const config = {
    matcher: ['/session'],
    //overwrite default route to another login promp page
    pages: {
        signIn: '/api/auth/signin',
    },
};
