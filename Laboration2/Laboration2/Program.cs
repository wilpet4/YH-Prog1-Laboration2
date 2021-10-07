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
                parkingGarage[i] = ""; // Tilldelar alla platser ett värde för att förhindra fel med null.
            }
            parkingGarage[99] = "CAR#ASD123";
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
                    Console.Clear();
                    Console.WriteLine("\t// MENY //\n");
                    switch (menuChoice)
                    {
                        case 1:
                            if (IsGarageFull())
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Det finns inga lediga p-platser!");
                                Console.ForegroundColor = ConsoleColor.White;
                                System.Threading.Thread.Sleep(1800);
                            }
                            else if (!IsGarageFull())
                            {
                                Console.WriteLine("Vill du registrera en bil eller motorcykel?\n" +
                                                  "[1] Bil.\n" +
                                                  "[2] Motorcykel.");
                                Int32.TryParse(Console.ReadLine(), out int vehicleChoice);
                                if (vehicleChoice == 1)
                                {
                                    Console.Write("Mata in registreringsnummer(max 10 tecken): ");
                                    string registrationNumber = Console.ReadLine();
                                    if (registrationNumber.Length <= 10)
                                    {
                                        Console.WriteLine(RegisterNewVehicle("CAR", registrationNumber));
                                        Console.ReadLine();
                                    }
                                }
                                else if (vehicleChoice == 2)
                                {
                                    Console.Write("Mata in registreringsnummer(max 10 tecken): ");
                                    string registrationNumber = Console.ReadLine();
                                    if (registrationNumber.Length <= 10)
                                    {
                                        Console.WriteLine(RegisterNewVehicle("MC", registrationNumber));
                                        Console.ReadLine();
                                    }
                                }
                                else
                                {
                                    InputErrorMessage();
                                }
                            }
                            break;
                        case 2:
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
                            Console.Write("Ange från vilken plats du vill flytta ett fordon: ");
                            if (Int32.TryParse(Console.ReadLine(), out userChoice) && userChoice < parkingGarage.Length + 1)
                            {
                                Console.WriteLine($"Till vilken plats vill du flytta {parkingGarage[userChoice - 1]}?");
                                if (Int32.TryParse(Console.ReadLine(), out userChoice) && userChoice < parkingGarage.Length + 1)
                                {
                                    //TODO
                                }
                                else
                                {
                                    InputErrorMessage();
                                }
                            }
                            else
                            {
                                InputErrorMessage();
                            }
                            break;
                        case 4:
                            Console.Write("Mata in registreringsnummret på fordonet du vill söka efter: "); 
                            string regSearch = Console.ReadLine();
                            (bool isFound, int index) searchResults = Search(regSearch.ToUpper());
                            if (searchResults.isFound)
                            {
                                Console.WriteLine($"Fordonet {parkingGarage[searchResults.index]} hittades på plats {searchResults.index + 1}!");
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Fordonet hittades inte i parkeringen!");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.ReadLine();
                            }
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
            }
            string RegisterNewVehicle(in string vehicleType, in string registrationNumber)
            {
                string[] strings = { vehicleType, registrationNumber };
                string vehicleID = string.Join("#", strings);
                for (int i = 0; i < parkingGarage.Length; i++)
                {
                    if (parkingGarage[i] == "")
                    {
                        parkingGarage[i] = vehicleID;
                        string returnMessage = $"{vehicleID} är nu registrerad på plats {i + 1}";
                        return returnMessage;
                    }
                }
                return null;
            }
            bool IsGarageFull()
            {
                int fullSpots = 0;
                int emptySpots = 0;
                for (int i = 0; i < parkingGarage.Length; i++)
                {
                    if (parkingGarage[i] == "")
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
            (bool isFound, int index) Search(in string registrationNumber) // Hittade detta intressanta sättet att returnera
            {                                                              // mer än en variabel och ville verkligen testa det.
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
        static void InputErrorMessage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Fel vid inmatning, försök igen!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
        }
    }
}