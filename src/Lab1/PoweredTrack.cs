namespace Itmo.ObjectOrientedProgramming.Lab1;

public class PoweredTrack : RouteSection
{
    private readonly double appliedForce;
    private readonly double maxAllowedSpeed;

    public PoweredTrack(double length, double force, double speedLimit)
    {
        if (length <= 0)
            throw new ArgumentOutOfRangeException(nameof(length), "Track length must be positive.");

        Length = length;
        appliedForce = force;
        maxAllowedSpeed = speedLimit;
    }

    public sealed override double Length { get; protected set; }

    public override (bool IsSuccess, double TimeTaken) Traverse(Train train)
    {
        if (!train.ApplyForce(appliedForce))
        {
            Console.WriteLine("Failed to apply force to the train.");
            return (false, -1);
        }

        double newSpeed = train.CurrentSpeed + (appliedForce / train.Mass * train.TimeStep);
        if (newSpeed > maxAllowedSpeed)
        {
            Console.WriteLine("Train exceeded speed limit.");
            return (false, -1);
        }

        train.UpdateSpeed(newSpeed);

        double travelTime = train.CalculateTravelTime(Length).TotalTime;
        return travelTime > 0 ? (true, travelTime) : (false, -1);
    }
}