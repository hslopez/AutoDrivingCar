using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrivingCar
{
    public class CollisionInfo
    {
        public int Step { get; }
        public (int X, int Y) Position { get; }
        public IReadOnlyList<string> InvolvedCars { get; }

        public CollisionInfo(int step, (int X, int Y) position, IReadOnlyList<string> involvedCars)
        {
            Step = step;
            Position = position;
            InvolvedCars = involvedCars;
        }
    }
}
