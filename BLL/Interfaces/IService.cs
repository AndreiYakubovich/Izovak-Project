using System.Collections.Generic;
using BLL.Entities;
using ClassGenerator;

namespace BLL.Interfaces
{
    public interface IService
    {
        List<DataBLL> GetProcessData();
    }
}
