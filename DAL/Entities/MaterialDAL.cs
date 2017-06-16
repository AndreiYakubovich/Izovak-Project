using DAL.Interfaces;

namespace DAL.Entities
{
    public class MaterialDAL : IEntity
    {
        public int ID { get; set; }
        public string TEXT { get; set; }
        public string Material_rDensity { get; set; }
        public string Material_rZfactor { get; set; }
        public string Material_rTooling { get; set; }

    }
}