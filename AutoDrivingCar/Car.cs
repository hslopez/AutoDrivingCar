using AutoDrivingCar.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrivingCar
{
    public class Car : ICar
    {
        public string Name => throw new NotImplementedException();

        public int X => throw new NotImplementedException();

        public int Y => throw new NotImplementedException();

        public Direction CurrentDirection => throw new NotImplementedException();

        public string Commands => throw new NotImplementedException();

        public CollisionInfo? Collision => throw new NotImplementedException();

        public void ProcessCommand(char command, IField field)
        {
            throw new NotImplementedException();
        }

        public void RegisterCollision(int step, IEnumerable<string> involvedCars)
        {
            throw new NotImplementedException();
        }
    }
}
