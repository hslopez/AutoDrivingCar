// See https://aka.ms/new-console-template for more information
using AutoDrivingCar;
using AutoDrivingCar.Interfaces;

var runner = new SimulationRunner();
runner.Run();

public class SimulationRunner
{
    private IField? _field;
    private ISimulation? _simulation;

    public void Run()
    {
        while (true)
        {
            InitializeSimulation();
            bool restartRequested = false;

            while (!restartRequested)
            {
                Console.WriteLine($"\nField size: {_field!.Width}x{_field!.Height}");
                Console.WriteLine("[1] Add car");
                Console.WriteLine("[2] Run simulation");
                Console.WriteLine("[3] Start over");
                Console.WriteLine("[4] Exit");
                Console.Write("Select option: ");

                var choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        AddCarToSimulation();
                        break;
                    case "2":
                        RunSimulation();
                        break;
                    case "3":
                        restartRequested = true;
                        break;
                    case "4":
                        ExitApplication();
                        return;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }
            }
        }
    }

    private void InitializeSimulation()
    {
        Console.Clear();
        Console.WriteLine("Welcome to Auto Driving Car Simulation!");
        _field = CreateField();
        _simulation = new Simulation(_field);
    }

    private IField CreateField()
    {
        while (true)
        {
            Console.Write("\nEnter field width and height (e.g., 10 10): ");
            var input = Console.ReadLine()?.Trim().Split();

            if (input != null && input.Length == 2 &&
                int.TryParse(input[0], out int width) &&
                int.TryParse(input[1], out int height) &&
                width > 0 &&
                height > 0)
            {
                return new Field(width, height);
            }
            Console.WriteLine("Invalid input. Please enter positive integers separated by space.");
        }
    }

    private void AddCarToSimulation()
    {
        var car = CreateCar();
        try
        {
            _simulation!.AddCar(car);
            DisplayCars(_simulation.GetCars());
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private ICar CreateCar()
    {
        var name = ReadCarName();
        var (x, y, direction) = ReadCarPosition();
        var commands = ReadCarCommands();
        return new Car(name, x, y, direction, commands);
    }

    private string ReadCarName()
    {
        while (true)
        {
            Console.Write("\nEnter car name: ");
            var name = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(name)) return name;
            Console.WriteLine("Name cannot be empty!");
        }
    }

    private (int x, int y, Direction direction) ReadCarPosition()
    {
        while (true)
        {
            Console.Write("Enter initial position (x y Direction): ");
            var parts = Console.ReadLine()?.Trim().Split();

            if (parts!.Length == 3 &&
                int.TryParse(parts[0], out int x) &&
                int.TryParse(parts[1], out int y))
            {
                var direction = ParseDirection(parts[2]);
                if (direction.HasValue && _field!.IsPositionValid(x, y))
                {
                    return (x, y, direction.Value);
                }
            }
            Console.WriteLine("Invalid position! Valid format: 'x y N' (N/S/E/W)");
        }
    }

    private Direction? ParseDirection(string input)
    {
        return input.ToUpper() switch
        {
            "N" => Direction.North,
            "S" => Direction.South,
            "E" => Direction.East,
            "W" => Direction.West,
            _ => null
        };
    }

    private string ReadCarCommands()
    {
        while (true)
        {
            Console.Write("Enter commands (L/R/F): ");
            var commands = Console.ReadLine()?.Trim().ToUpper();
            if (commands!.All(c => "LRF".Contains(c))) return commands!;
            Console.WriteLine("Invalid commands! Only L, R, F allowed.");
        }
    }

    private void RunSimulation()
    {
        if (!_simulation!.GetCars().Any())
        {
            Console.WriteLine("No cars to simulate!");
            return;
        }

        _simulation.Run();
        DisplayResults(_simulation.GetCars());
        PromptRestart();
    }

    private void DisplayCars(IEnumerable<ICar> cars)
    {
        Console.WriteLine("\nCurrent cars:");
        foreach (var car in cars)
        {
            Console.WriteLine($"- {car.Name}: ({car.X},{car.Y}) {car.CurrentDirection}, Commands: {car.Commands}");
        }
    }

    private void DisplayResults(IEnumerable<ICar> cars)
    {
        Console.WriteLine("\nSimulation Results:");
        foreach (var car in cars)
        {
            if (car.Collision != null)
            {
                Console.WriteLine($"{car.Name} collided with {string.Join(",", car.Collision.InvolvedCars)} " +
                                  $"at ({car.Collision.Position.X},{car.Collision.Position.Y}) " +
                                  $"on step {car.Collision.Step}");
            }
            else
            {
                Console.WriteLine($"{car.Name} final position: ({car.X},{car.Y}) {car.CurrentDirection}");
            }
        }
    }

    private void PromptRestart()
    {
        Console.Write("\n[1] Start over\n[2] Continue\nSelect option: ");
        var choice = Console.ReadLine()?.Trim();
        if (choice == "1") InitializeSimulation();
    }

    private void ExitApplication()
    {
        Console.WriteLine("\nThank you for using Auto Driving Car Simulation!");
        Environment.Exit(0);
    }
}