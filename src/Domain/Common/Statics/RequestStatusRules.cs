using Domain.Common.Enums;

namespace Domain.Common.Statics;

public static class RequestStatusRules
{
    private readonly static Dictionary<RequestStatus, HashSet<RequestStatus>> _transitions = new()
    {
        [RequestStatus.New] = [RequestStatus.InProgress],   
        [RequestStatus.InProgress] = [RequestStatus.Completed],   
        [RequestStatus.Completed] = [],   
    };

    public static bool CanTransitin(RequestStatus from, RequestStatus to) =>
        _transitions[from].Contains(to);
}