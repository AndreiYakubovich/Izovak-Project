using DAL.Entities;

namespace DAL.Repositories
{
    public class MaterialRepository : BaseRepository<MaterialDAL>
    {
        public MaterialRepository(UserContext db)
            : base(db)
        {
        }

        public MaterialDAL GetById(int id)
        {
            return db.Set<MaterialDAL>().Find(id);
        }

    }
}