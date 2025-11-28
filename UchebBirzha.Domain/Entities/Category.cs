using UchebBirzha.Domain.Common;

namespace UchebBirzha.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public virtual ICollection<Task> Tasks { get; private set; } = new List<Task>();

       
        private Category()
        {
            
        }

  
        internal Category(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

  
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