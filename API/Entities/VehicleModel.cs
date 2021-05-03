using System.Collections.Generic;

namespace API.Entities
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public string Description { get; set; }

        //Navigation properties
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}