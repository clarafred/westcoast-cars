using System.Collections.Generic;

namespace API.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        //Navigation properties
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}