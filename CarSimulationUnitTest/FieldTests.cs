// AutoDrivingCar.Tests/FieldTests.cs
using NUnit.Framework;
using AutoDrivingCar;

namespace AutoDrivingCar.Tests
{
    [TestFixture]
    public class FieldTests
    {
        [Test]
        public void IsPositionValid_EdgeCases_ValidatesCorrectly()
        {
            var field = new Field(5, 5);

            Assert.Multiple(() =>
            {
                Assert.That(field.IsPositionValid(0, 0), Is.True);
                Assert.That(field.IsPositionValid(4, 4), Is.True);
                Assert.That(field.IsPositionValid(-1, 2), Is.False);
                Assert.That(field.IsPositionValid(5, 3), Is.False);
            });
        }
    }
}