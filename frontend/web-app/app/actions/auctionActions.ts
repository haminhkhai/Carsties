'use server';

import { fetchWrapper } from '@/lib/fetchWrapper';
import { Auction, Bid, PagedResult } from '../types';
import { FieldValues } from 'react-hook-form';
import { revalidatePath } from 'next/cache';

export async function getData(
    query: string
): Promise<PagedResult<Auction>> {
    return await fetchWrapper.get(`search${query}`);
}

export async function updateActionTest() {
    const data = {
        mileage: Math.floor(Math.random() * 100000) + 1,
    };

    return await fetchWrapper.put(
        'auctions/c8c3ec17-01bf-49db-82aa-1ef80b833a9f',
        data
    );
}

export async function createAuction(data: FieldValues) {
    return await fetchWrapper.post('auctions', data);
}

export async function getDetailedViewData(
    id: string
): Promise<Auction> {
    return fetchWrapper.get(`auctions/${id}`);
}

export async function updateAuction(data: FieldValues, id: string) {
    const res = await fetchWrapper.put(`auctions/${id}`, data);
    //revalidate to show new data (refresh cache)
    revalidatePath(`/auctions/${id}`);
    return res;
}

export async function deleteAuction(id: string) {
    return await fetchWrapper.del(`auctions/${id}`);
}

export async function getBidsForAuction(id: string): Promise<Bid[]> {
    return await fetchWrapper.get(`bids/${id}`);
}

export async function placeBidForAuction(
    auctionId: string,
    amount: number
) {
    return await fetchWrapper.post(
        `bids?auctionID=${auctionId}&amount=${amount}`,
        {}
    );
}
