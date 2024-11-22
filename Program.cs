
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text.Json;
using System.Xml.Linq;
namespace Management;

    // Gästklass
    public class Gäst
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Rumsnummer { get; set; }
        public string CreditCardNumber { get; set; }
        public Guid Id { get; set; } // Unikt ID för gästen
         public DateTime CheckOutDate { get; set; } // Datum och tid för utcheckning
        public Gäst() {}
      
         
        public Gäst(string name, int age, int rumsnummer) 
        {
            Name = name;
            Age = age;
            Rumsnummer = rumsnummer;
            CheckOutDate = DateTime.Now; // Sätter utcheckningsdatum till nuvarande tidpunkt
            Id = Guid.NewGuid(); // Genererar ett unikt ID för gästen
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Namn: {Name}, Ålder: {Age}, Rumsnummer: {Rumsnummer}");
        }
    }

    // Rumklass
    public class Rum
    {
        public int Rumsnummer { get; set; }
        public bool IsOccupied { get; set; }
        public Gäst Occupant { get; set; }
        public DateTime? LastCleaned { get; set; } // Nullable för rum som aldrig har städats


        public Rum(int rumsnummer)
        {
            Rumsnummer = rumsnummer;
            IsOccupied = false;
            LastCleaned = null; // Inget städtillfälle vid skapande
        }


        public void AssignGäst(Gäst gäst)
        {
            Occupant = gäst;
            IsOccupied = true;

        }

        public void Vacate()
        {
            Occupant = null;
            IsOccupied = false;
        }
    }
    public class SingleRum : Rum
{
    public SingleRum(int rumsnummer) : base(rumsnummer) { }
}

public class DoubleRum : Rum
{
    public DoubleRum(int rumsnummer) : base(rumsnummer) { }
}

public class Suite : Rum
{
    public Suite(int rumsnummer) : base(rumsnummer) { }
}

    // HotelManager-klass
    public static class HManagement
    {
        public static List<Rum> rumms = new List<Rum>();
        public static List<Gäst> gäster = new List<Gäst>();

        public static void InitializeRooms()
    {
        for (int i = 100; i < 110; i++)
        {
            rumms.Add(new SingleRum(i));
        }
        for (int i = 200; i < 210; i++)
        {
            rumms.Add(new DoubleRum(i));
        }
        for (int i = 300; i < 310; i++)
        {
            rumms.Add(new Suite(i));
        }
    }
       public static Gäst FindGästByRoomNumber(int rumsnummer)
    {
        Rum rum = rumms.FirstOrDefault(r => r.Rumsnummer == rumsnummer);
        if (rum == null || !rum.IsOccupied)
        {
            return null; // Om rummet inte finns eller inte är upptaget, returnera null
        }

        return rum.Occupant; // Returnera gästen som är incheckad i rummet
    }


        // Ladda data från JSON-filer
        public static void LoadFromJsonFile()
{
    try
    {
        if (File.Exists("rooms.json"))
        {
            string roomJson = File.ReadAllText("rooms.json");
            rumms = JsonSerializer.Deserialize<List<Rum>>(roomJson) ?? new List<Rum>();
        }

        if (File.Exists("guests.json"))
        {
            string guestJson = File.ReadAllText("guests.json");
            gäster = JsonSerializer.Deserialize<List<Gäst>>(guestJson) ?? new List<Gäst>();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Fel vid laddning av data: {ex.Message}");
    }
}

        // Spara data till JSON-filer
        //AppendAlltext istället för File.WriteAllText
        public static void SaveToJsonFile()
        {
            try
            {
                string guestJsonString = JsonSerializer.Serialize(gäster, new JsonSerializerOptions { WriteIndented = true });
                File.AppendAllText("guests.json", guestJsonString);

                string roomJsonString = JsonSerializer.Serialize(rumms, new JsonSerializerOptions { WriteIndented = true });
                File.AppendAllText("rooms.json", roomJsonString);

                Console.WriteLine("Data har sparats till guests.json och rooms.json.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel uppstod vid sparandet av filen: {ex.Message}");
            }
        }
         public static Gäst FindGäst(string name, int age)
    {

        return gäster.FirstOrDefault(g=>g.Rumsnummer.Equals(rumms));
       // return gäster.FirstOrDefault(g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && g.Age == age);
        

    }
          
        // Visa tillgängliga rum
        public static void DisplayAvailableRum()
        {
            var availableRum = rumms.Where(r => !r.IsOccupied).ToList();
            Console.WriteLine("Tillgängliga rum:");
            foreach (var rumms in availableRum)
            {
                Console.WriteLine($"Rum {rumms.Rumsnummer}");
            }
        }

        // Visa gäster
        public static void DisplayGäster()
        {
            Console.WriteLine("Gästinformation:");
            foreach (var gäst in gäster)
            {
                Console.WriteLine($"Namn: {gäst.Name}, Ålder: {gäst.Age}, Rumsnummer: {gäst.Rumsnummer}");
            }
        }
      
      

        

         public class RoomCleaning
{
    public void CleanRoom(Rum rum)
    {
        Console.WriteLine($"Rummet {rum.Rumsnummer} städas.");
        rum.Vacate();
    }
}



class Program
{
    static void Main(string[] args)
    {
        HManagement.InitializeRooms();
        HManagement.LoadFromJsonFile(); // Ladda rum och gäster från JSON
   
        bool running = true;
        

        while (running)
        {
            Console.WriteLine("\n--- HOTEL MANAGEMENT ---");
            Console.WriteLine("1. Checka in");
            Console.WriteLine("2. Checka ut");
            Console.WriteLine("3. Visa tillgängliga rum");
            Console.WriteLine("4. Avsluta");
            Console.Write("Välj ett alternativ: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        CheckInGäst();
                        break;
                    case 2:
                        CheckOutGäst();
                        break;
                    case 3:
                        HManagement.DisplayAvailableRum();
                        break;
                    case 4:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Ange ett giltigt nummer.");
            }
        }

        Console.WriteLine("Avslutar programmet...");
    }


   
        };

       public static void CheckInGäst()
{
    Console.Write("Ange namn: ");
    string name = Console.ReadLine();
    Console.Write("Ange ålder: ");
    int age = int.Parse(Console.ReadLine());
    Console.Write("Ange kreditkort (16 siffror): ");
    string creditCard = Console.ReadLine();

    Console.WriteLine("Välj rumstyp:");
    Console.WriteLine("1. Single Rum (1000 kr/natt)");
    Console.WriteLine("2. Double Rum (1500 kr/natt)");
    Console.WriteLine("3. Suite (3000 kr/natt)");
    int roomChoice = int.Parse(Console.ReadLine());

    int roomPrice = roomChoice switch
    {
        1 => 1000,
        2 => 1500,
        3 => 3000,
        _ => 0 
    };

    var availableRoom = HManagement.rumms.FirstOrDefault(r =>
        !r.IsOccupied && ((roomChoice == 1 && r is SingleRum) ||
                          (roomChoice == 2 && r is DoubleRum) ||
                          (roomChoice == 3 && r is Suite)));

    if (availableRoom == null)
    {
        Console.WriteLine("Tyvärr finns inga lediga rum av vald typ.");
        return; // Avsluta metoden om inget rum är tillgängligt
    }

    Gäst gäst = new Gäst(name, age, availableRoom.Rumsnummer)
    {
        CreditCardNumber = creditCard
    };

    availableRoom.AssignGäst(gäst);
    HManagement.gäster.Add(gäst);

    Console.WriteLine($"Rum {availableRoom.Rumsnummer} tilldelat {name}. Pris: {roomPrice} kr.");
    Console.WriteLine($"Beloppet {roomPrice} kr har reserverats på kreditkortet.");

    HManagement.SaveToJsonFile();
}
// Metod för att checka ut en gäst och spara informationen i en JSON-fil
    public static Gäst CheckOutGäst() // Returnerar nu en `Guest` istället för `Room`
    {
        Console.WriteLine("Vill du checka ut?");
        Console.Write("Vad är ditt namn? ");
        string name = Console.ReadLine();
        Console.Write("Hur gammal är du? ");
        
        if (!int.TryParse(Console.ReadLine(), out int age)) // Validerar ålder som ett heltal
        {
            Console.WriteLine("Felaktig ålder. Avbryter utcheckning.");
            return null;
        }

        // Sök efter gästen i listan över gäster
        Gäst gäst = gäster.FirstOrDefault(g => g.Name == name && g.Age == age);

        if (gäst != null)
        {
            gäst.CheckOutDate = DateTime.Now;
            Rum rum = rumms.FirstOrDefault(r => r.Rumsnummer == gäst.Rumsnummer);
            
    int roomPrice = rum switch
    {
        SingleRum => 1000,
        DoubleRum => 1500,
        Suite => 3000,
        _ => 0
    };
          
           
            Console.WriteLine($"Gäst {gäst.Name} (ID: {gäst.Id}) Rumspris {roomPrice} har dragits från kreditkortet & gästen är utcheckad och sparad i JSON-fil.");
            rum.Vacate();
            HManagement.gäster.Remove(gäst);
             Console.WriteLine($"Rum {rum.Rumsnummer} måste städas.");

            new RoomCleaning().CleanRoom(rum);

            HManagement.SaveToJsonFile();
            return gäst;
             
        }
        else
        {
            Console.WriteLine("Gäst hittades inte.");
            return null;
        }
    }
 
    public static void AssignRoomToGäst(Gäst gäst)
    {
        bool assigned = false;
        while (!assigned)
        {
            HManagement.DisplayAvailableRum();
            Console.WriteLine($"Tilldelar rum för {gäst.Name}, {gäst.Age}.");
            Console.WriteLine("Välj en rumstyp:");
            Console.WriteLine("1. Single Room");
            Console.WriteLine("2. Double Room");
            Console.WriteLine("3. Suite");
            Console.Write("Skriv in nummer 1 för singel rum, 2 för dubbelrum, 3 för svit: ");

            if (!int.TryParse(Console.ReadLine(), out int choice)) // Validerar val
            {
                Console.WriteLine("Ogiltigt val, vänligen ange ett nummer mellan 1 och 3.");
                continue;
            }

            Rum selectedRum = choice switch
            {
                1 => HManagement.rumms.FirstOrDefault(r => r is SingleRum && !r.IsOccupied),
                2 => HManagement.rumms.FirstOrDefault(r => r is DoubleRum && !r.IsOccupied),
                3 => HManagement.rumms.FirstOrDefault(r => r is Suite && !r.IsOccupied),
                _ => null
            };

            if (selectedRum != null)
            {
                selectedRum.AssignGäst(gäst);
                gäst.Rumsnummer = selectedRum.Rumsnummer;
                assigned = true;
                HManagement.SaveToJsonFile();
            }
        }
    }
   public static void BehandlaBetalning(Gäst gäst)
    {
        if (string.IsNullOrWhiteSpace(gäst.CreditCardNumber))
        {
            Console.WriteLine("Inget giltigt kreditkortsnummer finns tillgängligt.");
            return;
        }

        decimal summaAttDebitera = BeräknaRumsAvgift(gäst);
        Console.WriteLine($"Dra avgift {summaAttDebitera:C} från kortnummer {gäst.CreditCardNumber}");
    }
    public static decimal BeräknaRumsAvgift(Gäst gäst)
    {
        decimal rumRate = 0;
        switch (gäst.Rumsnummer)
        {
            case int n when n >= 100 && n < 200: // Single Room
                rumRate = 900;
                break;
            case int n when n >= 200 && n < 300: // Double Room
                rumRate = 1350;
                break;
            case int n when n >= 300 && n < 400: // Suite
                rumRate = 1750;
                break;
            default:
                Console.WriteLine("Ogiltigt rumsnummer");
                break;
        }
    int numberOfNights = 1; // Antal nätter gästen har bott
    return rumRate * numberOfNights;
    }
}




         
        
       
     
       


