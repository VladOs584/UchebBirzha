using UchebBirzha.Domain.Common;

namespace UchebBirzha.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public virtual ICollection<Task> Tasks { get; private set; } = new List<Task>();

        private Category() { }

        public Category(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}