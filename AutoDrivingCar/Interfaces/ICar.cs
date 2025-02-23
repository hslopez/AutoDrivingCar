namespace AutoDrivingCar.Interfaces
{
    public interface ICar
    {
        string Name { get; }
        int X { get; }
        int Y { get; }
        Direction CurrentDirection { get; }
        string Commands { get; }
        CollisionInfo? Collision { get; }
        void ProcessCommand(char command, IField field);
        void RegisterCollision(int step, IEnumerable<string> involvedCars);
    }
}