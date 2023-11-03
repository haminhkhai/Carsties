'use client';

import React, { useState } from 'react';
import Image from 'next/image';

type Props = {
    imageUrl: string;
};

export default function CardImage({ imageUrl }: Props) {
    const [isLoading, setLoading] = useState(true);
    return (
        <Image
            src={imageUrl}
            alt='image'
            fill
            //nextjs attemp to lazy loading image
            //by add priority attribute it wont lazy loading image
            priority
            className={`
                object-cover 
                group-hover:opacity-50
                duration-700 
                ease-in-out
                ${
                    isLoading
                        ? 'grayscale blur-md scale-150'
                        : 'grayscale-0 blur-0 scale-100'
                }
            `}
            sizes='(max-width:768px) 100vw, (max-width:1200px) 50vw, 25vw'
            onLoad={() => setLoading(false)}
        />
    );
}
