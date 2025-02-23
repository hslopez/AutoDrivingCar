namespace AutoDrivingCar.Interfaces
{
    public interface IField
    {
        int Height { get; }
        int Width { get; }

        bool IsPositionValid(int x, int y);
    }
}