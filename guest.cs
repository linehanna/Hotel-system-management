namespace Hotel.HManagement
{
    public class Gäst
    {
        private string creditCardNumber;

        public string Name { get; set; }
        public int Age { get; set; }
        public int Rumsnummer { get; set; }

        public string CreditCardNumber
        {
            get => string.IsNullOrEmpty(creditCardNumber) ? "Ej angivet" : "************" + creditCardNumber[^4..];
            set
            {
                if (!IsValidCreditCard(value))
                    throw new ArgumentException("Ogiltigt kreditkortsnummer.");
                creditCardNumber = value;
            }
        }

        public Gäst() { }

        public Gäst(string name, int age, int rumsnummer)
        {
            Name = name;
            Age = age;
            Rumsnummer = rumsnummer;
        }

        public override string ToString()
        {
            return $"Namn: {Name}, Ålder: {Age}, Rumsnummer: {Rumsnummer}, Kreditkort: {CreditCardNumber}";
        }

        private bool IsValidCreditCard(string cardNumber)
        {
            return !string.IsNullOrWhiteSpace(cardNumber) && cardNumber.Length == 16 && cardNumber.All(char.IsDigit);
        }
    }
}    
