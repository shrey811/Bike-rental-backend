namespace BCP.Core.Enums
{
    public class BikeRentalStatus : BaseEnum
    {
        // private BikeRentalStatus(int id, string value) : base(id, value)
        // {
        // }
        public int Id { get; private set; }
        public string Value { get; private set; }

        private BikeRentalStatus(int id, string value) : base(id, value)
        {
            Id = id;
            Value = value;
        }


        public static BikeRentalStatus Available = new(1, "Available");
        public static BikeRentalStatus Rented = new(2, "Rented");
        public static BikeRentalStatus OverTime = new(3, "Overtime");
    }
}
// namespace BCP.Core.Enums
// {
//     public class BikeRentalStatus
//     {
//         public int Id { get; set; }
//         public RentalStatus Status { get; set; }
//     }
//
//     public enum RentalStatus
//     {
//         Available,
//         Rented,
//         InRepair
//     }
// }