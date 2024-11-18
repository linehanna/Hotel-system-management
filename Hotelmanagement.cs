/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class Rum
{
    // Egenskap för att lagra rums-ID som GUID
    public Guid RumId { get; private set; }
    public int RumNumber { get; set; }
    public bool IsOccupied { get; private set; }
    public gäster Occupant { get; private set; }

    // Konstruktor som genererar ett nytt unikt rums-ID
    public Rum(int rumNumber)
    {
        RumId = Guid.NewGuid();
        RumNumber = rumNumber;
        IsOccupied = false;
    }

    // Metod för att tilldela en gäst till rummet
    public void AssignGäst(gäster gäster)
    {
        Occupant = gäster;
        IsOccupied = true;
    }

    // Metod för att tömma rummet
    public void Vacate()
    {
        Occupant = null;
        IsOccupied = false;
    }
    public class SingleRum : Rum
{
    public SingleRum(int rumNumber) : base(rumNumber) { }
}

public class DoubleRum : Rum
{
    public DoubleRum(int rumNumber) : base(rumNumber) { }
}

public class Suite : Room
{
    public Suite(int rumNumber) : base(rumNumber) { }
}

    // Metod för att visa rumsinformationen
    public void DisplayRumInfo()
    {
        Console.WriteLine($"Room ID: {RumId}, Room Number: {RumNumber}, Is Occupied: {IsOccupied}");
        if (IsOccupied)
        {
            Console.WriteLine($"Occupant: {Occupant.Name}, Age: {Occupant.Age}");
        }
    }
}

public class gäster
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string CreditCardNumber { get; set; }
    public int RumNumber { get; set; }

    public gäster(string name, int age, string creditCardNumber, int RumNumber)
    {
        Name = name;
        Age = age;
        CreditCardNumber = creditCardNumber;
        RumNumber = RumNumber;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Namn: {Name}, Ålder: {Age}, Rumsnummer: {RumNumber}");
    }
}

public static class HManager
{
    public static List<Rum> rum = new List<Rum>();
    public static List<gäster> gäster = new List<gäster>();

    public static void InitializeRooms()
    {
        for (int i = 100; i < 110; i++)
        {
            rum.Add(new Rum(i));
        }
    }

    public static void AddGästtToRum(gäster guest, int roomNumber)
    {
        Rum rum = rum.FirstOrDefault(r => r.RumNumber == rumNumber);
        if (rum != null && !rum.IsOccupied)
        {
            rum.AssignGäst(gäster);
            gäster.Add(gäster);
            Console.WriteLine($"Gäst {guest.Name} har tilldelats rum {roomNumber}.");
        }
        else
        {
            Console.WriteLine($"Rum {roomNumber} är inte tillgängligt.");
        }
    }

    public static void DisplayRoomStatus()
    {
        foreach (var r in rum)
        {
            r.DisplayRumInfo();
        }
    }

    // Metod för att läsa in gästlistan från en JSON-fil
    public static void LoadFromJsonFile()
    {
        try
        {
            if (File.Exists("Room.json"))
            {
                string jsonString = File.ReadAllText("Room.json");
                if (!string.IsNullOrWhiteSpace(jsonString))
                {
                    gäster = JsonSerializer.Deserialize<List<gäster>>(jsonString) ?? new List<gäster>();
                    Console.WriteLine("Gästinformation laddad från Room.json.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ett fel uppstod vid läsningen av filen: {ex.Message}");
        }
    }

    // Metod för att spara gästlistan till en JSON-fil
    public static void SaveToJsonFile()
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(gäster, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("Room.json", jsonString);
            Console.WriteLine("Gästinformation sparad i Room.json.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ett fel uppstod vid sparandet av filen: {ex.Message}");
        }
    }

    // Metod för att lägga till en ny gäst
    public static void AddGäster(gäster gäst)
    {
        gäster.Add(gäst);
        SaveToJsonFile();
    }

    // Metod för att visa alla gäster
    public static void DisplayAllGuests()
    {
        foreach (var gäst in gäster)
        {
           gäst.DisplayInfo();
        }
    }

    // Metod för att hitta en gäst baserat på rumsnummer
    public static gäster FindGuest(int roomNo)
    {
        return gäster.FirstOrDefault(g => g.RumNumber == roomNo);
    }

    internal static void DisplayAvailableRooms()
    {
        throw new NotImplementedException();
    }

    internal static gäster FindGäst(int v)
    {
        throw new NotImplementedException();
    }
}

  public static Gäster CheckOutGäst()
    {
        Console.Write("Ange namnet på gästen som ska checkas ut: ");
        string gästName = Console.ReadLine();
        Console.Write("Ange åldern på gästen som ska checkas ut: ");
        if (!int.TryParse(Console.ReadLine(), out int gästAge))
        {
            Console.WriteLine("Ogiltig ålder. Avbryter utcheckning.");
            return null;
        }

        Gäster gäst = FindGuest(gästName, gästAge);
        
        if (guest != null)
        {
            Gäster.Remove(gäst);
           HotelM.SaveToJsonFile();
        }
        return gäst;
    }

    
    public static void DisplayAvailableRooms()
    {
        var availableRooms = Room.Where(r => !r.IsOccupied).ToList();
        Console.WriteLine("Tillgängliga rum:");
        foreach (var room in availableRooms)
        {
            Console.WriteLine($"Rum {room.RoomNumber}");
        }
    }

    //public class RoomCleaning
//{
 //   public void CleanRoom(Room room)
  //  {
     //   Console.WriteLine($"Rummet {room.RoomNumber} städas.");
     //   room.Vacate();
   // }


class Progr
{
    static void Main(string[] args)
    {
        // Ladda gästlistan från filen
        HManager.LoadFromJsonFile();

        // Initialisera rummen
        HManager.InitializeRooms();

        // Skapa en ny gäst och tilldela till ett rum
        gäster gäst = new gäster("Alice", 30, "1234-5678-9012-3456", 101);
        HManager.AddGästtToRum(gäst, 101);

        // Sök efter gäst baserat på rumsnummer
        gäster foundGäst = HManager.FindGäst(101);

        if (foundGäst != null)
        {
            Console.WriteLine($"Gäst hittad: {foundGäst.Name}, Ålder: {foundGäst.Age}, Rumsnummer: {foundGäst.RumNumber}");
        }
        else
        {
            Console.WriteLine("Ingen gäst hittades för det angivna rumsnumret.");
        }

        // Visa status för alla rum
        HManager.DisplayRoomStatus();
         for (int i = 0; i < 3; i++)
        {
            CheckOutGästAndCleanRoom();
        }
    }
}

static void CheckInGäst()
    {
        Console.WriteLine("Vill du checka in?");
        Console.Write("Skriv in ditt namn: ");
        string name = Console.ReadLine();
        Console.Write("Skriv in din ålder: ");

        if (!int.TryParse(Console.ReadLine(), out int age)) // Validerar ålder
        {
            Console.WriteLine("Felaktig ålder. Avbryter incheckning.");
            return;
        }

        Console.Write("Skriv in ditt betalkortsnummer: ");
        string creditCardNumber = Console.ReadLine();

        gäster gäster = new gäster(name, age, 0) { CreditCardNumber = creditCardNumber };
        AssignRumToGäst(gäster);
        gäster.DisplayInfo();
        HManager.gäster.Add(gäster);
        HotelManager.SaveToJsonFile();

        // Meddelande som skriver att rummet har debiterats på gästens kort
        Console.WriteLine($"Rummet har debiterats på ditt kort {gäster.CreditCardNumber}.");
    }


static void AssignRumToGäst(gäster gäster)
    {
        bool assigned = false;
        while (!assigned)
        {
            HManager.DisplayAvailableRooms();
            Console.WriteLine($"Tilldelar rum för {gäster.Name}, {gäster.Age}.");
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

            Rum selectedRoom = choice switch
            {
                1 => HotelManager.rooms
.FirstOrDefault(r => r is SingleRum && !r.IsOccupied),
                2 => HotelManager.rooms.FirstOrDefault(r => r is DoubleRum && !r.IsOccupied),
                3 => HotelManager.rooms.FirstOrDefault(r => r is Suite && !r.IsOccupied),
                _ => null
            };

            if (selectedRoom != null)
            {
                selectedRoom.AssignGäst(gäster);
                gäster.RumNumber = selectedRoom.RumNumber;
                assigned = true;
                HManager.SaveToJsonFile();
            }
        }
    }
    
static void CheckOutGästAndCleanRoom()
    {
        Guest checkedOutGuest = HotelManager.CheckOutGuest();
      
        if (checkedOutGuest == null)
        {
            Console.WriteLine("Gäst hittades inte.");
            return;
        }

        Room checkedOutRoom = HotelManager.rooms.FirstOrDefault(r => r.Occupant == checkedOutGuest);
      
        if (checkedOutRoom == null )
        {
            Console.WriteLine("Rummet för gästen hittades inte.");
            HotelManager.SaveToJsonFile();
            return;
        }

        BehandlaBetalning(checkedOutgäst);
        RoomCleaning roomCleaning = new RoomCleaning();
        roomCleaning.CleanRoom(checkedOutRoom);

        // Beräkna summa att debitera
        decimal summaAttDebitera = BeräknaRumsAvgift(checkedOutGuest);

        // Skriv ut meddelanden oavsett om det är null eller inte
        Console.WriteLine($" {summaAttDebitera:C} dras från ditt kort {checkedOutGuest.CreditCardNumber} för rum {checkedOutRoom.RoomNumber}");
        Console.WriteLine($"Rummet {checkedOutRoom.RoomNumber} städas.");
        HManager.SaveToJsonFile();
    }

    public static void BehandlaBetalning(Gäster gäster)
    {
        if (string.IsNullOrWhiteSpace(gäster.CreditCardNumber))
        {
            Console.WriteLine("Inget giltigt kreditkortsnummer finns tillgängligt.");
            return;
        }

        decimal summaAttDebitera = BeräknaRumsAvgift(Gäster);
        Console.WriteLine($"Dra avgift {summaAttDebitera:C} från kortnummer {Gäster.CreditCardNumber}");
    }
    public static decimal BeräknaRumsAvgift(Gäster gäster)
    {
        decimal roomRate = 0;
        switch (guest.RoomNumber)
        {
            case int n when (n >= 100 && n < 200): // Single Room
                roomRate = 900;
                break;
            case int n when (n >= 200 && n < 300): // Double Room
                roomRate = 1350;
                break;
            case int n when (n >= 300 && n < 400): // Suite
                roomRate = 1750;
                break;
            default:
                Console.WriteLine("Ogiltigt rumsnummer");
                break;
        }
    int numberOfNights = 1; // Antal nätter gästen har bott
    return roomRate * numberOfNights;
    }
*/

