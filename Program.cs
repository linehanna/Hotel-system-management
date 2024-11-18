using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text.Json;
using System.Xml.Linq;
using HotelManagement;

//using Newtonsoft.Json;

public class Guest 
{
    public string Name { get; set; }
    public int Age { get; set; }
    public int RoomNumber { get; set; }
    public string CreditCardNumber { get; set; }

    public Guest () {}

    public Guest(string name, int age, int roomNumber)
    {
        Name = name;
        Age = age;
        RoomNumber = roomNumber;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Namn: {Name}, Ålder: {Age}, Rumsnummer: {RoomNumber}");
    }
}

public class Room
{
    public int RoomNumber { get; set; }
    public bool IsOccupied { get; set; }
    public Guest Occupant { get; set; }

    public Room(int roomNumber)
    {
        RoomNumber = roomNumber;
        IsOccupied = false;
    }

    public void AssignGuest(Guest guest)
    {
        Occupant = guest;
        IsOccupied = true;
    }

    public void Vacate()
    {
        Occupant = null;
        IsOccupied = false;
    }
}

public class SingleRoom : Room
{
    public SingleRoom(int roomNumber) : base(roomNumber) { }
}

public class DoubleRoom : Room
{
    public DoubleRoom(int roomNumber) : base(roomNumber) { }
}

public class Suite : Room
{
    public Suite(int roomNumber) : base(roomNumber) { }
}

public static class HotelManager
{
    public static List<Room> rooms = new List<Room>();
    public static List<Guest> guests = new List<Guest>();

    public static void InitializeRooms()
    {
        for (int i = 100; i < 110; i++)
        {
            rooms.Add(new SingleRoom(i));
        }
        for (int i = 200; i < 210; i++)
        {
            rooms.Add(new DoubleRoom(i));
        }
        for (int i = 300; i < 310; i++)
        {
            rooms.Add(new Suite(i));
        }
    }
 



// Metod för att läsa in gästlistan från en JSON-fil
    public static void LoadFromJsonFile()
    {
        try
        {
            if (File.Exists("guest.json"))
            {
                string jsonString = File.ReadAllText("guest.json");
                guests = JsonSerializer.Deserialize<List<Guest>>(jsonString);
                Console.WriteLine("Gästinformation laddad från guest.json.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ett fel uppstod vid läsningen av filen: {ex.Message}");
        }
    }
    public static void SaveToJsonFile()
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(guests, new JsonSerializerOptions { WriteIndented = true });
              Console.WriteLine(jsonString+"TEST TEST TEST");
           // File.WriteAllText("testjson.json", jsonString);
            File.AppendAllText("testjson.json", jsonString);
            Console.WriteLine("Gästinformation sparad i guest.json.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ett fel uppstod vid sparandet av filen: {ex.Message}");
        }
    }



    public static Guest FindGuest(string name, int age)
    {
        return guests.FirstOrDefault(g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && g.Age == age);
       //return guests.FirstOrDefault(g=>g.RoomNumber.Equals(roomNo));
    }

    public static Guest CheckOutGuest()
    {
        Console.Write("Ange namnet på gästen som ska checkas ut: ");
        string guestName = Console.ReadLine();
        Console.Write("Ange åldern på gästen som ska checkas ut: ");
        if (!int.TryParse(Console.ReadLine(), out int guestAge))
        {
            Console.WriteLine("Ogiltig ålder. Avbryter utcheckning.");
            return null;
        }

        Guest guest = FindGuest(guestName, guestAge);
        if (guest != null)
        {
            guests.Remove(guest);
           HotelManager.SaveToJsonFile();
        }
        return guest;
    }

    public static void DisplayAvailableRooms()
    {
        var availableRooms = rooms.Where(r => !r.IsOccupied).ToList();
        Console.WriteLine("Tillgängliga rum:");
        foreach (var room in availableRooms)
        {
            Console.WriteLine($"Rum {room.RoomNumber}");
        }
    }
}

public class RoomCleaning
{
    public void CleanRoom(Room room)
    {
        Console.WriteLine($"Rummet {room.RoomNumber} städas.");
        room.Vacate();
    }
}

public class Program
{
    static void Main(string[] args)
    {
        HotelManager.InitializeRooms();
        HotelManager.guests.Add(new Guest("Test person",30,100 ));
        HotelManager.SaveToJsonFile();

        HotelManager.guests.Clear();
        HotelManager.LoadFromJsonFile();

        foreach(var guest in HotelManager.guests)
        {
            guest.DisplayInfo();
        }
        for (int i = 0; i < 3; i++)
        {
            CheckInGuest();
        }

        for (int i = 0; i < 3; i++)
        {
            CheckOutGuestAndCleanRoom();
        }
    }


    static void CheckInGuest()
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

        Guest guest = new Guest(name, age, 0) { CreditCardNumber = creditCardNumber };
        AssignRoomToGuest(guest);
        guest.DisplayInfo();
        HotelManager.guests.Add(guest);
        HotelManager.SaveToJsonFile();

        // Meddelande som skriver att rummet har debiterats på gästens kort
        Console.WriteLine($"Rummet har debiterats på ditt kort {guest.CreditCardNumber}.");
    }

    static void AssignRoomToGuest(Guest guest)
    {
        bool assigned = false;
        while (!assigned)
        {
            HotelManager.DisplayAvailableRooms();
            Console.WriteLine($"Tilldelar rum för {guest.Name}, {guest.Age}.");
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

            Room selectedRoom = choice switch
            {
                1 => HotelManager.rooms.FirstOrDefault(r => r is SingleRoom && !r.IsOccupied),
                2 => HotelManager.rooms.FirstOrDefault(r => r is DoubleRoom && !r.IsOccupied),
                3 => HotelManager.rooms.FirstOrDefault(r => r is Suite && !r.IsOccupied),
                _ => null
            };

            if (selectedRoom != null)
            {
                selectedRoom.AssignGuest(guest);
                guest.RoomNumber = selectedRoom.RoomNumber;
                assigned = true;
                HotelManager.SaveToJsonFile();
            }
            
            else
            {
                Console.WriteLine("Valt rum är inte tillgängligt. Försök igen.");
            }
        }
    }

    static void CheckOutGuestAndCleanRoom()
    {
        Guest checkedOutGuest = HotelManager.CheckOutGuest();
        if (checkedOutGuest == null)
        {
            Console.WriteLine("Gäst hittades inte.");
            return;
        }

        Room checkedOutRoom = HotelManager.rooms.FirstOrDefault(r => r.Occupant == checkedOutGuest);
        if (checkedOutRoom == null)
        {
            Console.WriteLine("Rummet för gästen hittades inte.");
            HotelManager.SaveToJsonFile();
            return;
        }

        BehandlaBetalning(checkedOutGuest);
        RoomCleaning roomCleaning = new RoomCleaning();
        roomCleaning.CleanRoom(checkedOutRoom);

        // Beräkna summa att debitera
        decimal summaAttDebitera = BeräknaRumsAvgift(checkedOutGuest);

        // Skriv ut meddelanden oavsett om det är null eller inte
        Console.WriteLine($" {summaAttDebitera:C} dras från ditt kort {checkedOutGuest.CreditCardNumber} för rum {checkedOutRoom.RoomNumber}");
        Console.WriteLine($"Rummet {checkedOutRoom.RoomNumber} städas.");
        HotelManager.SaveToJsonFile();
    }

    public static void BehandlaBetalning(Guest guest)
    {
        if (string.IsNullOrWhiteSpace(guest.CreditCardNumber))
        {
            Console.WriteLine("Inget giltigt kreditkortsnummer finns tillgängligt.");
            return;
        }

        decimal summaAttDebitera = BeräknaRumsAvgift(guest);
        Console.WriteLine($"Dra avgift {summaAttDebitera:C} från kortnummer {guest.CreditCardNumber}");
    }
    public static decimal BeräknaRumsAvgift(Guest guest)
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
}


       


