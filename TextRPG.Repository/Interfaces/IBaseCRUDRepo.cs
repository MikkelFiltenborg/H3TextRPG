using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Models;

namespace TextRPG.Repository.Interfaces
{
    // Generic interface
    public interface IBaseCRUDRepo<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Create(T model);
        Task<T?> Update(T model);
        Task<T> Delete(int id);
    }
}
