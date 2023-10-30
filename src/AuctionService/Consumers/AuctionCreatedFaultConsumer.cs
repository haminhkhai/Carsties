﻿using Contracts;
using MassTransit;

namespace AuctionService;

public class AuctionCreatedFaultConsumer : IConsumer<Fault<AuctionCreated>>
{
    public async Task Consume(ConsumeContext<Fault<AuctionCreated>> context)
    {
        //faulty handling
        System.Console.WriteLine("--> Consuming faulty creation");
        var exception = context.Message.Exceptions.First();

        if (exception.ExceptionType == "System.ArgumentException")
        {
            context.Message.Message.Model = "Foobar";
            await context.Publish(context.Message.Message);
        }
        else
        {
            System.Console.WriteLine(
                "Not an argument exception - update error dashboard somewhere"
            );
        }
    }
}