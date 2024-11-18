/*using System;
namespace HotelManagement
{
// Bas klass för att representera ett rum
    public class Room
    {
        public int RoomNumber { get; set; } // Rumsnumret
        public bool IsOccupied { get; set; } // Indikerar om rummet är upptaget
        public Guest Occupant { get; set; } // Gästen som bor i rummet

        // Metod för att tilldela en gäst till rummet
        public virtual void AssignGuest(Guest guest)
        {
            if (!IsOccupied)
            {
                Occupant = guest;
                IsOccupied = true;
                Console.WriteLine($"Gäst {guest.Name} har blivit tilldelad till rum {RoomNumber}.");
            }
            else
            {
                Console.WriteLine($"Rum {RoomNumber} är redan upptaget.");
            }
        }

        // Metod för att ta bort en gäst från rummet
        public virtual void RemoveGuest()
        {
            if (IsOccupied)
            {
                Console.WriteLine($"Gäst {Occupant.Name} har checkat ut från rum {RoomNumber}.");
                Occupant = null;
                IsOccupied = false;
            }
        }
    }
}
  // Klass för enkelrum
    public class SingleRoom : Room
    {
        public SingleRoom(int roomNumber)
        {
            RoomNumber = roomNumber;
        }
    }


  // Klass för dubbelrum
    public class DoubleRoom : Room
    {
        public DoubleRoom(int roomNumber)
        {
            RoomNumber = roomNumber;
        }
    }

    // Klass för svit
    public class Suite : Room
    {
        public Suite(int roomNumber)
        {
            RoomNumber = roomNumber;
        }
    }*/


