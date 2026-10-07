namespace ContactsApp.Domain.Entities
{
    public sealed class PhoneNumber
    {
        public int Id { get; private set; }

        public string Number { get; private set; } = string.Empty;

        public string Type { get; private set; } = string.Empty;

        public bool IsPrimary { get; private set; }

        private PhoneNumber()
        {
        }

        /// <summary>
        /// Phone number factory method to create a new instance of PhoneNumber.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="type"></param>
        /// <param name="isPrimary"></param>
        /// <returns></returns>
        public static PhoneNumber Create(string number, string type, bool isPrimary)
        {
            var phoneNumber = new PhoneNumber();
            phoneNumber.SetPhoneNumber(number, type, isPrimary);
            return phoneNumber;
        }

        public void SetPhoneNumber(string number, string type, bool isPrimary)
        {
            Number = number;
            Type = type;
            IsPrimary = isPrimary;
        }
    }
}
