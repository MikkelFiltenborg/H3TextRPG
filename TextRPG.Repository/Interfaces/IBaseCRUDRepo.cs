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
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<int> Create(T model);
        void Update(T model);
        void Delete(int id);
    }
}
