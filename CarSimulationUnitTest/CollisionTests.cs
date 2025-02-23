using NUnit.Framework;
using AutoDrivingCar;
using System.Linq;

namespace AutoDrivingCar.Tests
{
    [TestFixture]
    public class CollisionTests
    {
        [Test]
        public void RegisterCollision_MultipleCars_RecordsAllInvolved()
        {
            var car = new Car("A", 5, 5, Direction.North, "");
            var otherCars = new[] { "B", "C" };

            car.RegisterCollision(5, otherCars);

            Assert.Multiple(() =>
            {
                Assert.That(car.Collision.Step, Is.EqualTo(5));
                Assert.That(car.Collision.InvolvedCars, Is.EquivalentTo(otherCars));
                Assert.That(car.Collision.Position, Is.EqualTo((5, 5)));
            });
        }
    }
}