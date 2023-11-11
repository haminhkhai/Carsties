'use client';

import { useParamsStore } from '@/hooks/useParamsStore';
import { Dropdown } from 'flowbite-react';
import { DropdownDivider } from 'flowbite-react/lib/esm/components/Dropdown/DropdownDivider';
import { User } from 'next-auth';
import { signOut } from 'next-auth/react';
import Link from 'next/link';
import { usePathname, useRouter } from 'next/navigation';
import React from 'react';
import { AiFillCar, AiFillTrophy, AiOutlineLogout } from 'react-icons/ai';
import { HiCog, HiUser } from 'react-icons/hi';

type Props = {
    user: User;
};

export default function UserActions({ user }: Props) {
    const router = useRouter();
    const pathName = usePathname();
    const resetParams = useParamsStore((state) => state.reset);
    const setParams = useParamsStore((state) => state.setParams);

    const setWinner = () => {
        setParams({ winner: user.username, seller: undefined, filterBy: 'finished' });
        //if path is not at home page then route back to home page
        if (pathName !== '/') router.push('/');
    };

    const setSeller = () => {
        resetParams();
        setParams({ winner: undefined, seller: user.username });
        //if path is not at home page then route back to home page
        if (pathName !== '/') router.push('/');
    };

    return (
        <Dropdown label={`Welcome ${user.name}`} inline>
            <Dropdown.Item icon={HiUser} onClick={setSeller}>
                My Auctions
            </Dropdown.Item>
            <Dropdown.Item icon={AiFillTrophy} onClick={setWinner}>
                Auctions won
            </Dropdown.Item>
            <Dropdown.Item icon={AiFillCar}>
                <Link href='/auctions/create'>Sell my car</Link>
            </Dropdown.Item>
            <Dropdown.Item icon={HiCog}>
                <Link href='/session'>Session (dev only)</Link>
            </Dropdown.Item>
            <DropdownDivider />

            <Dropdown.Item
                icon={AiOutlineLogout}
                onClick={() => signOut({ callbackUrl: '/' })}>
                Sign out
            </Dropdown.Item>
        </Dropdown>
    );
}
