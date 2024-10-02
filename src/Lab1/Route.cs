namespace Itmo.ObjectOrientedProgramming.Lab1;

public class Route
{
    private readonly List<RouteSection> routeSections;
    private readonly double globalSpeedLimit;

    public Route(double speedLimit)
    {
        routeSections = new List<RouteSection>();
        globalSpeedLimit = speedLimit;
    }

    public void AddRouteSection(RouteSection section)
    {
        routeSections.Add(section);
    }

    public (bool IsSuccess, double TotalTime) Traverse(Train train)
    {
        double totalTime = 0;

        foreach (RouteSection section in routeSections)
        {
            if (train.CurrentSpeed > globalSpeedLimit)
            {
                Console.WriteLine("Train exceeded global speed limit.");
                return (false, totalTime);
            }

            (bool isSuccess, double time) = section.Traverse(train);
            if (!isSuccess)
            {
                Console.WriteLine("Train failed to traverse the section.");
                return (false, totalTime);
            }

            totalTime += time;
        }

        return (train.CurrentSpeed <= globalSpeedLimit) ? (true, totalTime) : (false, totalTime);
    }
}