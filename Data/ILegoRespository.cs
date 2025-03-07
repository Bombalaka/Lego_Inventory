using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lego_Inventory.Models;

namespace Lego_Inventory.Data
{
    public interface ILegoRespository
    {
        Task<List<Legoset>> GetAllAsyns();
        Task AddAsync(Legoset legoset);

    }
}