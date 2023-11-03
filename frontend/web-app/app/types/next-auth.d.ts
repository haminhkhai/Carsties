import { DefaultSession } from 'next-auth';

//extend session type
declare module 'next-auth' {
    interface Session {
        user: {
            username: string;
        } & DefaultSession['user'];
    }

    interface Profile {
        username: string;
    }
}

//extend jwt type
declare module 'next-auth/jwt' {
    interface JWT {
        username: string;
        access_token?: string;
    }
}
