// AutoDrivingCar.Tests/CarTests.cs
using NUnit.Framework;
using AutoDrivingCar;
using AutoDrivingCar.Interfaces;

namespace AutoDrivingCar.Tests
{
    [TestFixture]
    public class CarTests
    {
        private IField _field;

        [SetUp]
        public void Setup()
        {
            _field = new Field(10, 10);
        }

        [Test]
        public void ProcessCommand_F_MovesNorth()
        {
            var car = new Car("A", 5, 5, Direction.North, "");
            car.ProcessCommand('F', _field);
            Assert.That((car.X, car.Y), Is.EqualTo((5, 6)));
        }

        [Test]
        public void ProcessCommand_L_RotatesWest()
        {
            var car = new Car("B", 5, 5, Direction.North, "");
            car.ProcessCommand('L', _field);
            Assert.That(car.CurrentDirection, Is.EqualTo(Direction.West));
        }

        [Test]
        public void ProcessCommand_R_RotatesEastToSouth()
        {
            var car = new Car("C", 5, 5, Direction.East, "");
            car.ProcessCommand('R', _field);
            Assert.That(car.CurrentDirection, Is.EqualTo(Direction.South));
        }

        [Test]
        public void ProcessCommand_InvalidMove_StaysInPlace()
        {
            var car = new Car("D", 0, 0, Direction.South, "");
            car.ProcessCommand('F', _field);
            Assert.That((car.X, car.Y), Is.EqualTo((0, 0)));
        }
    }
}