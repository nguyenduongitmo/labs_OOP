namespace Itmo.ObjectOrientedProgramming.Lab1;

public class RegularTrack : RouteSection
{
    public RegularTrack(double length)
    {
        if (length <= 0)
            throw new ArgumentOutOfRangeException(nameof(length), "Track length must be positive.");

        Length = length;
    }

    public sealed override double Length { get; protected set; }

    public override (bool IsSuccess, double TimeTaken) Traverse(Train train)
    {
        if (!ValidateTrainSpeed(train))
            return (false, -1);

        double travelTime = train.CalculateTravelTime(Length).TotalTime;
        return travelTime > 0 ? (true, travelTime) : (false, -1);
    }
}