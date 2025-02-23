using AutoDrivingCar.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrivingCar
{
    public class Field : IField
    {
        public int Height => throw new NotImplementedException();

        public int Width => throw new NotImplementedException();

        public bool IsPositionValid(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
