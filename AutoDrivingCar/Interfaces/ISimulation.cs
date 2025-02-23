namespace AutoDrivingCar.Interfaces
{
    public interface ISimulation
    {
        void AddCar(ICar car);
        void Run();
        IEnumerable<ICar> GetCars();
    }
}