namespace BiddingService;

public enum BidStatus
{
    Accepted,
    //current highest bid when the bid was placed but it didn't meet the reserve price
    AcceptedBelowReserve,
    TooLow,
    Finished
}
