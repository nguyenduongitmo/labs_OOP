namespace Itmo.ObjectOrientedProgramming.Lab1;

public class Train
{
    public double Mass { get; }

    public double Acceleration { get; private set; }

    public double MaxForce { get; }

    public double CurrentSpeed { get; private set; }

    public double TimeStep { get; }

    public Train(double mass, double maxForce, double timeStep)
    {
        Mass = mass;
        Acceleration = 0;
        MaxForce = maxForce;
        CurrentSpeed = 0;
        TimeStep = timeStep;
    }

    public bool ApplyForce(double appliedForce)
    {
        if (Math.Abs(appliedForce) > MaxForce)
            return false;

        Acceleration = appliedForce / Mass;
        return true;
    }

    public void UpdateSpeed(double newSpeed)
    {
        CurrentSpeed = newSpeed;
    }

    public (bool IsSuccess, double TotalTime) CalculateTravelTime(double distance)
    {
        if (distance <= 0)
            throw new ArgumentOutOfRangeException(nameof(distance), "Distance must be positive.");

        double timeElapsed = 0;
        double remainingDistance = distance;

        while (remainingDistance > 0)
        {
            double speedDelta = Acceleration * TimeStep;
            CurrentSpeed += speedDelta;

            if (CurrentSpeed < 0)
                return (false, timeElapsed);

            double distanceDelta = CurrentSpeed * TimeStep;

            if (remainingDistance <= distanceDelta)
            {
                timeElapsed += remainingDistance / CurrentSpeed;
                return (true, timeElapsed);
            }

            remainingDistance -= distanceDelta;
            timeElapsed += TimeStep;
        }

        return (true, timeElapsed);
    }
}