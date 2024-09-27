namespace GMS.API.Entities
{
    public class Vehicle: BaseEntity
    {
        public string LicensePlate { get; set; }
        public string Description { get; set; }
        public VehicleStatus Status { get; set; }
        public int OwnerId { get; set; }
        public int BrandId { get; set; }

        public Owner Owner { get; set; }
        public Brand Brand { get; set; }
    }
}
