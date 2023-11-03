import React from 'react';
import {
    getSession,
    getTokenWorkAround,
} from '../actions/authActions';
import Heading from '../components/Heading';
import AuthTest from './AuthTest';

export default async function Session() {
    const session = await getSession();
    const token = await getTokenWorkAround();

    return (
        <div>
            <Heading title='Session dashboard' />

            <div className='bg-blue-200 border-2 border-blue-500 p-2'>
                <h3 className='text-lg'>Session data</h3>
                <pre>{JSON.stringify(session, null, 2)}</pre>
            </div>
            <div className='mt-4'>
                <AuthTest />
            </div>
            <div className='bg-blue-200 border-2 border-blue-500 p-2 mt-4'>
                <h3 className='text-lg'>Token data</h3>
                <pre className='overflow-auto'>
                    {JSON.stringify(token, null, 2)}
                </pre>
            </div>
        </div>
    );
}
