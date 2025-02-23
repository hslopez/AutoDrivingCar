// See https://aka.ms/new-console-template for more information
using AutoDrivingCar;
using AutoDrivingCar.Interfaces;

var runner = new SimulationRunner();
runner.Run();
public class SimulationRunner
{
    private IField? _field;
    private ISimulation? _simulation;

    public void Run()
    {
    }
}