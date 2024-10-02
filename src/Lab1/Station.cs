namespace Itmo.ObjectOrientedProgramming.Lab1;

public class Station : RouteSection
{
    public sealed override double Length { get; protected set; }

    private readonly double maxAllowedSpeed;

    public Station(double length, double maxAllowedSpeed)
    {
        if (length <= 0)
            throw new ArgumentOutOfRangeException(nameof(length), "Station length must be positive.");

        Length = length;
        this.maxAllowedSpeed = maxAllowedSpeed;
    }

    public override (bool IsSuccess, double TimeTaken) Traverse(Train train)
    {
        if (!ValidateTrainSpeed(train) || train.CurrentSpeed > maxAllowedSpeed)
        {
            Console.WriteLine("Train is moving too fast to stop at the station.");
            return (false, -1);
        }

        if (train.CurrentSpeed > 0)
        {
            double deceleration = -train.Mass * train.Acceleration;
            if (!train.ApplyForce(deceleration))
            {
                return (false, -1);
            }
        }

        double timeToStop = train.CalculateTravelTime(Length).TotalTime;

        return (timeToStop >= 0) ? (true, timeToStop) : (false, -1);
    }
}