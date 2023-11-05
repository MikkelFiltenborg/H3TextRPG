using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Models;

namespace TextRPG.Repository.Interfaces
{
    public interface IBaseCRUDRepo<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void Create(T model);
        void Update(T model);
        void Delete(int id);
    }
}