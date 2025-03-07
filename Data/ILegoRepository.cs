using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lego_Inventory.Models;

namespace Lego_Inventory.Data
{
    public interface ILegoRepository
    {
        Task<List<Legoset>> GetAllAsync();
        Task AddAsync(Legoset legoset);

    }
}