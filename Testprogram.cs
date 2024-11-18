
/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text.Json;
using System.Xml.Linq;
using HotelManagement;*/


/*Refaktorering- Omstrukturering av befintlig kod, för bättre struktur, ökad läsbarhet. 

I min kod har jag refaktorerat ett flertal gånger genom att öka läsbarheten. 

Jag behöver refaktorera min kod, genom att dela upp koden i enskilda filer, klasser som tillhör mitt main program, för att öka läsbarheten.  

Singel-Responsibility- I min kod finns det inte mycket single-responsibility då en del klasser har fler ansvar än ett. 
Exempelvis gästklassen har olika strängar och int.  

Annan lösning- Ja jag ska ändra i koden så jag kan söka på rumsnummer istället för gästens namn och ålder. 
Så att programmet söker på en gäst baserat på rumsnumret.  

Utvecklingspotential- Programmet har utvecklings potential så det går att bygga på programmet i oändlighet.
Exempelvis kan jag lägga till en ny loggning för att spåra händelser i mitt system. 
Jag kan lägga till en egenskap som hanterar en gäst preferenser. 

Jag har jobbat med modularitet genom att dela upp koden i mindre återanvändbara komponenter. 
Jag kan hantera gäst och rum genom att skapa en hanteringsklass som tar hand extra preferenser utan att påverka andra delar av programmet.  

Inkapsling: Inkapsling används för att dölja implementeringsdetaljer och endast exponera nödvändig funktionalitet. 
I min Rum-klass är till exempel egenskaperna RoomId, IsOccupied och Occupant privata set, vilket innebär att de endast kan ändras inom klassen. 
Detta skyddar data och säkerställer att de endast kan ändras på ett kontrollerat sätt. 

Arv: Min nuvarande kod använder inte arv, men det går att göra den mer modulär. 
Genom att skapa en basklass rum och skapa underklasser för specifika rumstyper, det är en möjlig lösning jag har till slutet av programmet.  

Polymorfism- Jag använder inte polymorfism, men ska se över om jag vill använda det. 
Genom att definiera en basklass rum som innehåller en virtuell metod displayroominfo() som kan överskrivas av underklasser. 

Skapa underklasser för en specifik rumstyp, Skapa underklasser för specifika rumstyper: 
Dessa klasser ärver från rum och överskriver DisplayRumInfo för att ge specifik funktionalitet. 

Polymorfism säkerställer att rätt implementation av DisplayRumInfo anropas för varje rum. 

 

Abstraktion- I min room klass har jag använt mig av abstraktion. Egenskaper, metoder- assignguest och vacate. 
Konstruktorn använder sig av abstraktion.  */
//Modulär kod- Är uppdelade i mindre återanvändbara enheter som kallas för moduler, klasser metoder, namespaces.  
//Klassen guest är en modul som kapslar in all funktionalitet relaterad till en gäst. 

//Inkapsling- klassens fält och metoder är inkapslade inom klassen, vilket gör att de kan hanteras oberoende av andra delar av programmet. 

/*public class Guest 
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
            if (File.Exists("testjson.json"))
            {
                string jsonString = File.ReadAllText("testjson.json");
                guests = JsonSerializer.Deserialize<List<Guest>>(jsonString);
                Console.WriteLine("Gästinformation laddad från testjson.json.");
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
            Console.WriteLine("Gästinformation sparad i testjson.json.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ett fel uppstod vid sparandet av filen: {ex.Message}");
        }
    }



// Ändra denna till att söka på rumsnummer istället för guest name och ålder
// ex. public static Guest FindGuest(int roomNo)
// guests.FirstOrDefault(g.RoomNumber=roomNo);
    //public static Guest FindGuest(string name, int age)
   //public static Guest FindGuest(int roomNo)
   //problem, findGuest kan inte ta emot roomNo
   public static Guest FindGuest(string name, int age)
    {
        //return guests.FirstOrDefault(g=>g.RoomNumber.Equals(roomNo));
        return guests.FirstOrDefault(g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && g.Age == age);
        
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

public class Progra
{
    static void Main(string[] args)
    {
        HotelManager.InitializeRooms();
       // HotelManager.guests.Add(new Guest("Test person",30,100 ));
        HotelManager.SaveToJsonFile();

        HotelManager.guests.Clear();
        HotelManager.LoadFromJsonFile();

      //  foreach(var guest in HotelManager.guests)
       // {
            guest.DisplayInfo();
       // }
        
        //for (int i = 0; i < 3; i++)
        //{
          CheckInGuest();
        //}
// instead of looping the records you should read the whole file.. loop until eof (end of file)
//change this loop to read file instead
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
            // if (selectedRoom == null)
          //  {
           //     selectedRoom.AssignGuest(guest);
            //    guest.RoomNumber = selectedRoom.RoomNumber;
             //   assigned = true;
             //   HotelManager.SaveToJsonFile();
         //   }
            
      //  }
   // }

    static void CheckOutGuestAndCleanRoom()
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
}*/
