namespace Core.Entities
{
    public class BaseEntity<TPrimary>
    {
        public TPrimary Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
