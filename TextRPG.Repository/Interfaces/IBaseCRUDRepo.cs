﻿using System;
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
        void Create(T InObj);
        void Update(T InObj);
        void Delete(int id);
    }
}
