import EmptyFilter from '@/app/components/EmptyFilter';
import React from 'react';

//it's gonna look for searchParams in the query string
export default function Page({
    searchParams,
}: {
    searchParams: { callbackUrl: string };
}) {
    console.log('searchParams: ' + searchParams.callbackUrl);
    return (
        <EmptyFilter
            title='You need to be logged in to do that'
            subtitle='Please click below to sign in'
            showLogin
            callbackUrl={searchParams.callbackUrl}
        />
    );
}
