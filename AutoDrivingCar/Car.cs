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
        private static readonly Direction[] RotationOrder = {
            Direction.North,
            Direction.East,
            Direction.South,
            Direction.West
        };

        private static readonly Dictionary<Direction, (int dx, int dy)> DirectionVectors = new()
        {
            { Direction.North, (0, 1) },
            { Direction.East, (1, 0) },
            { Direction.South, (0, -1) },
            { Direction.West, (-1, 0) }
        };

        public string Name { get; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public Direction CurrentDirection { get; private set; }
        public string Commands { get; }
        public CollisionInfo? Collision { get; private set; }

        public Car(string name, int x, int y, Direction direction, string commands)
        {
            Name = name;
            X = x;
            Y = y;
            CurrentDirection = direction;
            Commands = commands;
        }

        public void ProcessCommand(char command, IField field)
        {
            if (Collision != null) return;

            switch (command)
            {
                case 'L':
                    CurrentDirection = Rotate(-1);
                    break;
                case 'R':
                    CurrentDirection = Rotate(1);
                    break;
                case 'F':
                    Move(field);
                    break;
            }
        }

        private Direction Rotate(int step)
        {
            var currentIndex = Array.IndexOf(RotationOrder, CurrentDirection);
            var newIndex = (currentIndex + step + RotationOrder.Length) % RotationOrder.Length;
            return RotationOrder[newIndex];
        }

        private void Move(IField field)
        {
            var (dx, dy) = DirectionVectors[CurrentDirection];
            var newX = X + dx;
            var newY = Y + dy;

            if (field.IsPositionValid(newX, newY))
            {
                X = newX;
                Y = newY;
            }
        }

        public void RegisterCollision(int step, IEnumerable<string> involvedCars)
        {
            Collision = new CollisionInfo(
                step: step,
                position: (X, Y),
                involvedCars: involvedCars.Where(c => c != Name).ToList()
            );
        }
    }
}
