using System.Collections.Generic;
using ClassGenerator;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IMainDalInterface
    {
        List<ProcessData> GetData();
    }
}
