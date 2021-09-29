using System;

namespace Laboration2
{
    class Program
    {
        public static void Main(string[] args)
        {
            string[] parkingGarage = new string[100];
            for (int i = 0; i < parkingGarage.Length; i++)
            {
                parkingGarage[i] = i.ToString();
                Console.WriteLine(parkingGarage[i]);
            }

            bool isRunning = true;
            while (isRunning)
            {
                MainMenu(parkingGarage);
                isRunning = false;
            }
        }
        static void MainMenu(in string[] parkingGarage)
        {
            Console.Clear();
            Console.WriteLine("\t// MENY //\n");
            Console.WriteLine("[1] Registrera ny bil.\n" +
                              "[2] Registrera ny motorcykel.\n" +
                              "[3] Sök efter fordon.\n" +
                              "[4] Visa alla p-platser.\n" +
                              "[5] Avsluta programmet.\n");
            Console.Write("Mata in siffran för motsvarande ärende: ");
            if (Int32.TryParse(Console.ReadLine(), out int menuChoice))
            {
                switch (menuChoice)
                {
                    case 1:
                        IsGarageFull(parkingGarage);
                        Console.Clear();
                        Console.Write("Mata in registreringsnummer(max 10 siffror): ");
                        string registrationNumber = Console.ReadLine();
                        if (registrationNumber.Length <= 10)
                        {
                            RegisterNewVehicle("CAR", registrationNumber);
                        }
                        else
                        {
                            InputErrorMessage();
                        }
                        break;
                    case 2:
                        Console.Clear();
                        Console.Write("Mata in registreringsnummer(max 10 siffror): ");
                        registrationNumber = Console.ReadLine();
                        if (registrationNumber.Length <= 10)
                        {
                            RegisterNewVehicle("MC", registrationNumber);
                        }
                        else
                        {
                            InputErrorMessage();
                        }
                        break;
                    case 3:
                        //TODO: Sökfunktion,
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    default:
                        break;
                }
            }
            else
            {
                InputErrorMessage();
            }
        }
        static void InputErrorMessage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Fel vid inmatning, försök igen!");
            Console.ForegroundColor = ConsoleColor.White;
            System.Threading.Thread.Sleep(2000);
        }
        static string RegisterNewVehicle(in string vehicleType, in string registrationNumber)
        {
            string[] strings = { vehicleType, registrationNumber };
            string vehicleID = string.Join("#", strings);
            return vehicleID; //TODO: Lägg in i parkingGarage.
        }
        static bool IsGarageFull(in string[] parkingGarage)
        {
            int fullSpots = 0;
            int emptySpots = 0;
            for (int i = 0; i < parkingGarage.Length; i++)
            {
                if (parkingGarage[i].Contains("CAR") || parkingGarage[i].Contains("MC"))
                {
                    fullSpots++;
                }
                else
                {
                    emptySpots++;
                }
            }
            if (emptySpots > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
