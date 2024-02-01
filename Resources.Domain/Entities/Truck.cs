namespace Resources.Domain.Entities
{
    public class Truck
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public byte Status { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
