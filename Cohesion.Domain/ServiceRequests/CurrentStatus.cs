namespace Cohesion.Domain.ServiceRequests
{
    public enum CurrentStatus
    {
        NotApplicable = 0,
        Created = 1,
        InProgress = 2,
        Complete = 3,
        Canceled = 99
    }
}
