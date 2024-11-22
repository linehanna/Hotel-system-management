namespace Hotel.HManagement
{
    public class Rum
    {
        public int Rumsnummer { get; private set; }
        public bool IsOccupied { get; private set; }
        public Gäst Occupant { get; private set; }
        public DateTime? LastCleaned { get; private set; } // Tillagd för att matcha `Program.cs`

        public Rum(int rumsnummer)
        {
            this.Rumsnummer = rumsnummer;
            this.IsOccupied = false;
        }

        public virtual void AssignGäst(Gäst gäst)
        {
            if (IsOccupied)
            {
                Console.WriteLine($"Rum {Rumsnummer} är redan upptaget.");
                return;
            }

            Occupant = gäst;
            IsOccupied = true;
            Console.WriteLine($"Gäst {gäst.Name} har checkat in i rum {Rumsnummer}.");
        }

        public void Vacate()
        {
            if (!IsOccupied)
            {
                Console.WriteLine($"Rum {Rumsnummer} är redan ledigt.");
                return;
            }

            Console.WriteLine($"Rum {Rumsnummer} frigörs från gäst {Occupant.Name}.");
            Occupant = null;
            IsOccupied = false;
        }

        public void MarkAsCleaned()
        {
            LastCleaned = DateTime.Now;
            Console.WriteLine($"Rum {Rumsnummer} är nu städat. Senast städad: {LastCleaned}");
        }

        public override string ToString()
        {
            return $"Rum {Rumsnummer} - {(IsOccupied ? $"Upptaget av {Occupant?.Name}" : "Ledigt")}";
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
}

