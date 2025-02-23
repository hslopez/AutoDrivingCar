using AutoDrivingCar.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrivingCar
{
    public class Simulation : ISimulation
    {
        private readonly IField _field;
        private readonly List<ICar> _cars = new List<ICar>();

        public Simulation(IField field)
        {
            _field = field;
        }

        public void AddCar(ICar car)
        {
            if (_cars.Any(c => c.Name == car.Name))
                throw new System.ArgumentException("Car name must be unique.");
            _cars.Add(car);
        }

        public void Run()
        {
            if (_cars.Count == 0) return;

            int maxSteps = _cars.Max(c => c.Commands.Length);
            for (int step = 0; step < maxSteps; step++)
            {
                ProcessStep(step);
                CheckCollisions(step);
                if (AllCarsCollided()) break;
            }
        }

        private void ProcessStep(int step)
        {
            foreach (var car in _cars.Where(c => c.Collision == null))
            {
                if (step < car.Commands.Length)
                    car.ProcessCommand(car.Commands[step], _field);
            }
        }

        private void CheckCollisions(int step)
        {
            var positionGroups = _cars
                .Where(c => c.Collision == null)
                .GroupBy(c => (c.X, c.Y))
                .Where(g => g.Count() >= 2);

            foreach (var group in positionGroups)
            {
                var carsInGroup = group.ToList();
                var allNames = carsInGroup.Select(c => c.Name).ToList();
                foreach (var car in carsInGroup)
                {
                    car.RegisterCollision(step + 1, allNames);
                }
            }
        }

        private bool AllCarsCollided() => _cars.All(c => c.Collision != null);
        public IEnumerable<ICar> GetCars() => _cars.AsReadOnly();
    }
}
