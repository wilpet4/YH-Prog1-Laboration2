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
                //parkingGarage[i] = "Tom plats";
            }
            parkingGarage[3] = "123";
            Console.ReadLine();

            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("\t// MENY //\n");
                Console.WriteLine("[1] Registrera nytt fordon.\n" +
                                  "[2] Hämta ut fordon.\n" +
                                  "[3] Flytta fordon.\n" +
                                  "[4] Sök efter fordon.\n" +
                                  "[5] Visa alla p-platser.\n");
                Console.Write("Mata in siffran för motsvarande ärende: ");
                if (Int32.TryParse(Console.ReadLine(), out int menuChoice))
                {
                    switch (menuChoice)
                    {
                        case 1:
                            Console.Clear();
                            if (IsGarageFull(parkingGarage))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Det finns inga lediga p-platser!");
                                Console.ForegroundColor = ConsoleColor.White;
                                System.Threading.Thread.Sleep(1800);
                            }
                            else if (!IsGarageFull(parkingGarage))
                            {
                                Console.WriteLine("Vill du registrera en bil eller motorcykel?\n" +
                                                  "[1] Bil.\n" +
                                                  "[2] Motorcykel.");
                                Int32.TryParse(Console.ReadLine(), out int vehicleChoice);
                                if (vehicleChoice == 1)
                                {
                                    Console.Write("Mata in registreringsnummer(max 10 siffror): ");
                                    string registrationNumber = Console.ReadLine();
                                    if (registrationNumber.Length <= 10)
                                    {
                                        Console.WriteLine(RegisterNewVehicle("CAR", registrationNumber, parkingGarage));
                                    }
                                }
                                else if (vehicleChoice == 2)
                                {
                                    Console.Write("Mata in registreringsnummer(max 10 siffror): ");
                                    string registrationNumber = Console.ReadLine();
                                    if (registrationNumber.Length <= 10)
                                    {
                                        Console.WriteLine(RegisterNewVehicle("MC", registrationNumber, parkingGarage));
                                    }
                                }
                                else
                                {
                                    InputErrorMessage();
                                }
                            }
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine("Vill du hämta ut en bil eller motorcykel?\n" +
                                              "[1] Bil.\n" +
                                              "[2] Motorcykel.\n");
                            Int32.TryParse(Console.ReadLine(), out int userChoice);
                            if (userChoice == 1)
                            {
                                Console.Write("Mata in registreringsnummret på bilen du vill hämta ut: ");
                                string search = Console.ReadLine();
                            }
                            else if (userChoice == 2)
                            {
                                Console.Write("Mata in registreringsnummret på motorcykeln du vill hämta ut: ");
                                string search = Console.ReadLine();
                            }
                            break;
                        case 3:
                            break;
                        case 4:
                            Console.Clear();
                            Console.Write("Mata in registreringsnummret på fordonet du vill söka efter: "); 
                            string regSearch = Console.ReadLine();
                            if (Search(regSearch, parkingGarage).isFound)
                            {

                            }
                            else
                            {

                            }
                            //TODO: Sökfunktion
                            break;
                        case 5:
                            Console.Clear();
                            for (int i = 0; i < parkingGarage.Length; i++)
                            {
                                if (i < 9)
                                {
                                    Console.WriteLine($"Plats {i + 1}:   {parkingGarage[i]}");
                                }
                                if (i >= 9 && i < 99)
                                {
                                    Console.WriteLine($"Plats {i + 1}:  {parkingGarage[i]}");
                                }
                                if (i >= 99)
                                {
                                    Console.WriteLine($"Plats {i + 1}: {parkingGarage[i]}");
                                }
                            }
                            Console.WriteLine("\nTryck på 'Enter' för att gå tillbaka till menyn...");
                            Console.ReadLine();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    InputErrorMessage();
                }
                //isRunning = false;
            }
        }
        static void InputErrorMessage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Fel vid inmatning, försök igen!");
            Console.ForegroundColor = ConsoleColor.White;
            System.Threading.Thread.Sleep(1800);
        }
        static string RegisterNewVehicle(in string vehicleType, in string registrationNumber, in string[] parkingGarage)
        {
            string[] strings = { vehicleType, registrationNumber };
            string vehicleID = string.Join("#", strings);
            for (int i = 0; i < parkingGarage.Length; i++)
            {
                if (parkingGarage[i] == "Tom plats")
                {
                    parkingGarage[i] = vehicleID;
                    string returnMessage = $"{vehicleID} är nu registrerad på plats {i + 1}";
                    return returnMessage;
                }
            }
            return null;
        }
        static bool IsGarageFull(in string[] parkingGarage)
        {
            int fullSpots = 0;
            int emptySpots = 0;
            for (int i = 0; i < parkingGarage.Length; i++)
            {
                if (parkingGarage[i] == null)
                {
                    emptySpots++;
                }
                else
                {
                    fullSpots++;
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
        static (bool isFound, int index) Search(in string registrationNumber, in string[] parkingGarage) // Hittade detta intressanta sättet att returnera
        {                                                                                                // mer än en variabel och ville verkligen testa det.
            for (int i = 0; i < parkingGarage.Length; i++)
            {
                if (parkingGarage[i].Contains(registrationNumber))
                {
                    return (true, i);
                }
            }
            return (false, 0);
        }
    }
}