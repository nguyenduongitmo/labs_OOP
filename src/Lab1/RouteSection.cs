namespace Itmo.ObjectOrientedProgramming.Lab1;

public abstract class RouteSection
{
    public abstract double Length { get; protected set; }

    public abstract (bool IsSuccess, double TimeTaken) Traverse(Train train);

    protected bool ValidateTrainSpeed(Train train)
    {
        if (!(train.CurrentSpeed <= 0)) return true;
        Console.WriteLine("Train cannot traverse the section with non-positive speed.");
        return false;
    }
}