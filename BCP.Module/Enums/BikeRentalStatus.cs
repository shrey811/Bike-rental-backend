namespace BCP.Core.Enums
{
    public class BikeRentalStatus : BaseEnum
    {
       
        public int Id { get; private set; }
        public string Value { get; private set; }

        private BikeRentalStatus(int id, string value) : base(id, value)
        {
            Id = id;
            Value = value;
        }


        public static BikeRentalStatus Available = new(1, "Available");
        public static BikeRentalStatus Rented = new(2, "Rented");
      
    }
}
