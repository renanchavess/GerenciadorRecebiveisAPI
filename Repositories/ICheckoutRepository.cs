using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorRecebiveisAPI.Models;

namespace GerenciadorRecebiveisAPI.Repositories
{
    public interface ICheckoutRepository
    {
        Checkout GetCheckout(int id);
        Checkout Create(Checkout checkout);
    }
}