using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Models;

namespace GerenciadorRecebiveisAPI.Repositories
{
    public interface ICheckoutRepository
    {
        Task<Checkout> CreateAsync(Checkout checkout);
        Task<Checkout> GetAsync(int id);
        Task<Checkout> GetCheckoutByCarrinhoId(int carrinhoId);
    }
}