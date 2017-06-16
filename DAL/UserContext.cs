using System.Data.Entity;
using DAL.Entities;

namespace DAL
{
    public class UserContext : DbContext
    { 
      
            public UserContext() : base("DefaultConnection")
            {
            }
            public DbSet<MaterialDAL> Material { get; set; }
        }
}
