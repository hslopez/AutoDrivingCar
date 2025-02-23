// AutoDrivingCar.Tests/SimulationTests.cs
using NUnit.Framework;
using AutoDrivingCar;
using System.Linq;
using AutoDrivingCar.Interfaces;

namespace AutoDrivingCar.Tests
{
    [TestFixture]
    public class SimulationTests
    {
        private IField _field;
        private ISimulation _simulation;

        [SetUp]
        public void Setup()
        {
            _field = new Field(10, 10);
            _simulation = new Simulation(_field);
        }

        [Test]
        public void Run_WithCollidingCars_RegistersCollisions()
        {
            var car1 = new Car("A", 0, 0, Direction.East, "FF");
            var car2 = new Car("B", 2, 2, Direction.South, "FF");

            _simulation.AddCar(car1);
            _simulation.AddCar(car2);
            _simulation.Run();

            Assert.Multiple(() =>
            {
                Assert.That(car1.Collision, Is.Not.Null);
                Assert.That(car2.Collision, Is.Not.Null);
                Assert.That(car1.Collision.Step, Is.EqualTo(2));
                Assert.That(car1.Collision.Position, Is.EqualTo((2, 0)));
            });
        }

        [Test]
        public void Run_WithDifferentCommandLengths_ProcessesAllSteps()
        {
            var car = new Car("C", 0, 0, Direction.East, "FF");
            _simulation.AddCar(car);
            _simulation.Run();

            Assert.That(car.X, Is.EqualTo(2));
        }

        [Test]
        public void AddCar_DuplicateName_ThrowsException()
        {
            var car1 = new Car("X", 0, 0, Direction.North, "");
            var car2 = new Car("X", 1, 1, Direction.South, "");

            _simulation.AddCar(car1);
            Assert.Throws<System.ArgumentException>(() => _simulation.AddCar(car2));
        }
    }
}