using Itmo.ObjectOrientedProgramming.Lab1;
using Xunit;

namespace Lab1.Tests;

public class Testcases
{
    [Fact]
    public void TrainShouldSucceedOnPoweredAndRegularTrack()
    {
        var train = new Train(1200, 200, 0.1);
        var route = new Route(120); // Speed limit is 120
        train.UpdateSpeed(50); // Initial speed is below the limit

        route.AddRouteSection(new PoweredTrack(50, 100, 120)); // Accelerate to allowable speed
        route.AddRouteSection(new RegularTrack(50)); // Regular track

        (bool IsSuccess, double TotalTime) result = route.Traverse(train);

        Assert.True(result.IsSuccess, "Expected the train to succeed on the powered and regular track, but it failed.");
    }

    [Fact]
    public void TrainShouldFailOnExceedingPoweredTrackSpeedLimit()
    {
        var train = new Train(1200, 200, 0.2);
        var route = new Route(150);
        train.UpdateSpeed(60);

        route.AddRouteSection(new PoweredTrack(60, 220, 130)); // Exceeds allowable speed
        route.AddRouteSection(new RegularTrack(60));

        (bool IsSuccess, double TotalTime) result = route.Traverse(train);

        Assert.False(result.IsSuccess, "Expected the train to fail on exceeding powered track speed limit, but it succeeded.");
    }

    [Fact]
    public void TrainShouldSucceedOnPoweredTrackAndStation()
    {
        var train = new Train(1200, 200, 500);
        var route = new Route(120);
        train.UpdateSpeed(60);

        route.AddRouteSection(new PoweredTrack(60, 15, 100));
        route.AddRouteSection(new RegularTrack(60));
        route.AddRouteSection(new Station(15, 100));
        route.AddRouteSection(new RegularTrack(60));

        (bool IsSuccess, double TotalTime) result = route.Traverse(train);

        Assert.True(result.IsSuccess,  "Expected the train to succeed on the powered track and station, but it failed.");
    }

    [Fact]
    public void TrainShouldFailOnExceedingStationSpeedLimit()
    {
        // Arrange
        var train = new Train(1200, 200, 0.2);
        var route = new Route(130);
        train.UpdateSpeed(60);

        route.AddRouteSection(new PoweredTrack(60, 220, 130)); // Exceeds allowable station speed
        route.AddRouteSection(new Station(15, 80));
        route.AddRouteSection(new RegularTrack(60));

        // Act
        (bool IsSuccess, double TotalTime) result = route.Traverse(train);

        // Assert
        Assert.False(result.IsSuccess, "Expected the train to fail on exceeding station speed limit, but it succeeded.");
    }

    [Fact]
    public void TrainShouldFailOnExceedingRouteLimitBeforeStation()
    {
        // Arrange
        var train = new Train(1000, 150, 0.1);
        var route = new Route(120);
        train.UpdateSpeed(50);

        route.AddRouteSection(new PoweredTrack(50, 150, 120)); // Exceeds route speed limit but within station limit
        route.AddRouteSection(new RegularTrack(50));
        route.AddRouteSection(new Station(10, 50));
        route.AddRouteSection(new RegularTrack(50));

        (bool IsSuccess, double TotalTime) result = route.Traverse(train);

        Assert.False(result.IsSuccess, "Expected the train to fail on exceeding route limit before station, but it succeeded.");
    }

    [Fact]
    public void TrainShouldSucceedWithValidSpeedAdjustments()
    {
        var train = new Train(1200, 200, 500);
        var route = new Route(120);
        train.UpdateSpeed(60);

        route.AddRouteSection(new PoweredTrack(60, 15, 100));
        route.AddRouteSection(new RegularTrack(60));
        route.AddRouteSection(new PoweredTrack(60, -50, 100));
        route.AddRouteSection(new Station(15, 80));
        route.AddRouteSection(new RegularTrack(60));
        route.AddRouteSection(new PoweredTrack(60, 15, 100));
        route.AddRouteSection(new RegularTrack(60));
        route.AddRouteSection(new PoweredTrack(60, -50, 100));

        (bool IsSuccess, double TotalTime) result = route.Traverse(train);

        Assert.True(result.IsSuccess, "Expected the train to succeed with valid speed adjustments, but it failed.");
    }

    [Fact]
    public void TrainShouldFailOnStationWithNoInitialSpeed()
    {
        var train = new Train(1200, 200, 0.2);
        var route = new Route(130);
        train.UpdateSpeed(0);

        route.AddRouteSection(new RegularTrack(60));

        (bool IsSuccess, double TotalTime) result = route.Traverse(train);

        Assert.False(result.IsSuccess, "Expected the train to fail on station with no initial speed, but it succeeded.");
    }

    [Fact]
    public void TrainShouldFailOnExcessiveNegativeForce()
    {
        var train = new Train(1000, 150, 0.1);
        var route = new Route(100);
        train.UpdateSpeed(60);

        route.AddRouteSection(new PoweredTrack(60, 100, 100));
        route.AddRouteSection(new PoweredTrack(60, -200, 100));

        (bool IsSuccess, double TotalTime) result = route.Traverse(train);

        Assert.False(result.IsSuccess, "Expected the train to fail on excessive negative force, but it succeeded.");
    }
}
