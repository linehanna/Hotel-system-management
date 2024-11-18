namespace HotelManagement
{
    public class RoomCleaning
    {
        public void CleanRoom(Room room)
        {
            if (room.IsOccupied)
            {
                Console.WriteLine($"Kan inte städa rum {room.RoomNumber} eftersom det är upptaget.");
            }
            else
            {
                Console.WriteLine($"Städar rum {room.RoomNumber}...");
                // Lägg till logik för att städa rummet här
                Console.WriteLine($"Rum {room.RoomNumber} har städats.");
            }
        }
    }
}