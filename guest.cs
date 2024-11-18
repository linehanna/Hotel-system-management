/*namespace HotelManagement;


// Metod för att spara gästlistan i en JSON-fil

    public static void SaveToJsonFile()
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(guests, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("guest.json", jsonString);
            Console.WriteLine("Gästinformation sparad i guest.json.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ett fel uppstod vid sparandet av filen: {ex.Message}");
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
    }*/
