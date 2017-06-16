using System.Collections.Generic;
using BLL.Entities;
using ClassGenerator;

namespace BLL.Interfaces
{
    public interface IService
    {
//        MaterialBLL GetMaterialById(int ID);
//        IEnumerable<MaterialBLL> GetAllMaterials();
        DataBLL GetProcessData(int id);

        int GetProcessDataCount();
        string GetStuff();
    }
}
