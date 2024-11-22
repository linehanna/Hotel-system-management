namespace Hotel.HManagement
{
    public class RoomCleaning
    {
        public void CleanRoom(Rum rum)
        {
            if (rum.IsOccupied)
            {
                Console.WriteLine($"Kan inte städa rum {rum.Rumsnummer} eftersom det är upptaget.");
            }
            else
            {
                rum.MarkAsCleaned();
            }
        }
    }
}