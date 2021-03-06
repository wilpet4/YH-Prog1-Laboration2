using System;

namespace Laboration2
{
    // GITHUB: https://github.com/wilpet4/YH-Prog1-Laboration2.git
    class Program
    {
        public static void Main(string[] args)
        {
            string[] parkingGarage = new string[100];
            for (int i = 0; i < parkingGarage.Length; i++)
            {
                parkingGarage[i] = ""; // Tilldelar alla platser ett värde för att förhindra fel med null.
            }
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("\t// MENY //\n");
                Console.WriteLine("[1] Registrera nytt fordon.\n" +
                                  "[2] Hämta ut fordon.\n" +
                                  "[3] Flytta fordon.\n" +
                                  "[4] Sök efter fordon.\n" +
                                  "[5] Visa alla p-platser.\n" +
                                  "[6] Avsluta programmet.");
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
                            else if (IsGarageFull() == false)
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
                                    else
                                    {
                                        InputErrorMessage();
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
                                    else
                                    {
                                        InputErrorMessage();
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
                                Console.WriteLine(RemoveVehicle("CAR", search));
                                Console.ReadLine();
                            }
                            else if (userChoice == 2)
                            {
                                Console.Write("Mata in registreringsnummret på motorcykeln du vill hämta ut: ");
                                string search = Console.ReadLine();
                                Console.WriteLine(RemoveVehicle("MC", search));
                                Console.ReadLine();
                            }
                            else
                            {
                                InputErrorMessage();
                            }
                            break;
                        case 3:
                            Console.Write("Ange från vilken plats du vill flytta ett fordon: ");
                            bool parseSuccess = Int32.TryParse(Console.ReadLine(), out userChoice);
                            if (parseSuccess && userChoice < parkingGarage.Length + 1 && userChoice > 0 && parkingGarage[userChoice - 1] != "") // I dessa if-satsen controllerar jag alltid
                            {                                                                                                                   // att inmatning gick igenom parse och är över 0.
                                if (parkingGarage[userChoice - 1].Contains("|")) // OM det finns två mc på platsen man vill flytta ifrån.
                                {
                                    string firstMC = parkingGarage[userChoice - 1].Substring(0, parkingGarage[userChoice - 1].IndexOf("|"));
                                    string secondMC = parkingGarage[userChoice - 1].Substring(parkingGarage[userChoice - 1].IndexOf("|") + 1);
                                    Console.WriteLine("Det finns 2 motorcyklar på denna plats, vilken vill du välja?");
                                    Console.WriteLine($"[1] {firstMC}\n" +
                                                      $"[2] {secondMC}");
                                    Int32.TryParse(Console.ReadLine(), out int input);
                                    if (input == 1)
                                    {
                                        Console.WriteLine($"Till vilken plats vill du flytta {firstMC}?");
                                        int fromIndex = userChoice - 1;
                                        parseSuccess = Int32.TryParse(Console.ReadLine(), out userChoice);
                                        if (parseSuccess && userChoice < parkingGarage.Length + 1 && parkingGarage[userChoice - 1] == "") // OM platsen att flytta till är tom.
                                        {
                                            int toIndex = userChoice - 1;
                                            parkingGarage[toIndex] += firstMC;
                                            parkingGarage[fromIndex] = secondMC;
                                            Console.WriteLine($"{firstMC} har flyttats tills plats {userChoice}!");


                                        }
                                        else if (parseSuccess && userChoice < parkingGarage.Length + 1 && parkingGarage[userChoice - 1].Contains("MC")) // OM platsen innehåller en MC.
                                        {
                                            int toIndex = userChoice - 1;
                                            parkingGarage[toIndex] += $"|{firstMC}";
                                            parkingGarage[fromIndex] = secondMC;
                                            Console.WriteLine($"{firstMC} har flyttats tills plats {userChoice}!");
                                        }
                                        Console.ReadLine();
                                    }
                                    else if (input == 2)
                                    {
                                        Console.WriteLine($"Till vilken plats vill du flytta {secondMC}?");
                                        int fromIndex = userChoice - 1;
                                        parseSuccess = Int32.TryParse(Console.ReadLine(), out userChoice);
                                        if (parseSuccess && userChoice < parkingGarage.Length + 1 && parkingGarage[userChoice - 1] == "")
                                        {
                                            int toIndex = userChoice - 1;
                                            parkingGarage[toIndex] += secondMC;
                                            parkingGarage[fromIndex] = firstMC;
                                            Console.WriteLine($"{secondMC} har flyttats tills plats {userChoice}!");
                                            Console.ReadLine();
                                        }
                                        else if (parseSuccess && userChoice < parkingGarage.Length + 1 && parkingGarage[userChoice - 1].Contains("MC"))
                                        {
                                            int toIndex = userChoice - 1;
                                            parkingGarage[toIndex] += $"|{secondMC}";
                                            parkingGarage[fromIndex] = firstMC;
                                            Console.WriteLine($"{secondMC} har flyttats tills plats {userChoice}!");
                                            Console.ReadLine();
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
                                }
                                else
                                {
                                    Console.WriteLine($"Till vilken plats vill du flytta {parkingGarage[userChoice - 1]}?");
                                    int fromIndex = userChoice - 1;
                                    parseSuccess = Int32.TryParse(Console.ReadLine(), out userChoice);
                                    if (parseSuccess && userChoice < parkingGarage.Length + 1 && parkingGarage[userChoice - 1] == "")
                                    {
                                        int toIndex = userChoice - 1;
                                        parkingGarage[toIndex] = parkingGarage[fromIndex];
                                        parkingGarage[fromIndex] = "";
                                        Console.WriteLine($"{parkingGarage[fromIndex]} har flyttats till plats {userChoice}!");
                                        Console.ReadLine();
                                    }
                                    // ANNARS OM användaren vill flytta en MC till en plats som redan innehåller en MC.
                                    else if (parseSuccess && userChoice < parkingGarage.Length + 1 && parkingGarage[userChoice - 1].Contains("MC#") && !parkingGarage[userChoice - 1].Contains("|") && !parkingGarage[fromIndex].Contains("CAR#"))
                                    {
                                        int toIndex = userChoice - 1;
                                        parkingGarage[toIndex] += $"|{parkingGarage[fromIndex]}";
                                        parkingGarage[fromIndex] = "";
                                        Console.WriteLine($"{parkingGarage[fromIndex]} har flyttats till plats {userChoice}!");
                                        Console.ReadLine();
                                    }
                                    else if (parseSuccess && parkingGarage[userChoice - 1] != "")
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("P-platsen är redan upptagen av ett annat fordon!");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.ReadLine();
                                    }
                                    else
                                    {
                                        InputErrorMessage();
                                    }
                                }
                            }
                            else if (parseSuccess && userChoice > 0 && parkingGarage[userChoice - 1] == "") // OM platsen man vill flytta ett fordon ifrån är tomt.
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Det finns inget fordon att flytta på p-platsen!");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.ReadLine();
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
                                Console.WriteLine($"Sökningen hittade fordon på plats {searchResults.index + 1}!" +
                                                  $"\nPlats {searchResults.index + 1} innehåller {parkingGarage[searchResults.index]}.");
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
                                    Console.WriteLine($"Plats {i + 1}:   {parkingGarage[i]}"); // Använder extra mellanslag för att göra så att alla utmatningar
                                }                                                              // hamnar på samma plats i konsolen oavsett om platsnummret har en, två, eller tre siffror.
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
                        case 6:
                            isRunning = false;
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
                string vehicleID = string.Join("#", strings); // Sätter ihop fordonstypen med regnummer.
                for (int i = 0; i < parkingGarage.Length; i++)
                {
                    if (parkingGarage[i] == "") //Om platsen är tom så registreras fordonet på den platsen.
                    {
                        parkingGarage[i] = vehicleID.ToUpper();
                        string returnMessage = $"{vehicleID} är nu registrerad på plats {i + 1}!";
                        return returnMessage;
                    }
                    else if (vehicleType == "MC" && parkingGarage[i].Contains("MC#") && parkingGarage[i].Contains('|') == false)
                    {
                        parkingGarage[i] += $"|{vehicleID}".ToUpper();
                        string returnMessage = $"{vehicleID} är nu registrerad på plats {i + 1}!";
                        return returnMessage;
                    }
                }
                return null;
            }
            string RemoveVehicle(in string vehicleType, in string registrationNumber)
            {
                (bool isFound, int index) searchResults = Search(registrationNumber.ToUpper());
                if (searchResults.isFound && parkingGarage[searchResults.index].Contains($"{vehicleType}#"))
                {
                    if (parkingGarage[searchResults.index].Contains('|'))
                    { // I denna if-sats hämtar jag start- och slutindex för motorcykeln som ska tas ut från platsen om det finns två på samma plats.
                        string findIndexOfString = vehicleType + "#" + registrationNumber.ToUpper();
                        int lengthOfString = findIndexOfString.ToCharArray().Length;
                        int indexOfVehicle = parkingGarage[searchResults.index].IndexOf(findIndexOfString);
                        string returnMessage = $"{parkingGarage[searchResults.index].Substring(indexOfVehicle, lengthOfString)} har nu hämtats ut från plats {searchResults.index + 1}!";

                        parkingGarage[searchResults.index] = parkingGarage[searchResults.index].Remove(indexOfVehicle, lengthOfString);
                        parkingGarage[searchResults.index] = parkingGarage[searchResults.index].Replace("|", string.Empty);
                        return returnMessage;
                    }
                    else
                    {
                        string returnMessage = $"{parkingGarage[searchResults.index]} har nu hämtats ut från plats {searchResults.index + 1}!";
                        parkingGarage[searchResults.index] = "";
                        return returnMessage;
                    }
                }
                else
                {
                    string returnMessage = "Fordonet hittades inte i parkeringen!";
                    return returnMessage;
                }
            }
            bool IsGarageFull() // Stegar enkelt igenom parkeringen och returnerar false om en enda tom plats hittas.
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
        static void InputErrorMessage() // Enkelt error-meddelande som jag använder mig av varje gång 
        {                               // användaren matar in något oönskat.
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Fel vid inmatning, försök igen!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
        }
    }
}