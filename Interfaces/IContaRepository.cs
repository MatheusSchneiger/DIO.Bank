using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIO.Bank.Models;

namespace DIO.Bank.Interfaces
{
    public interface IContaRepository
    {
        Task AdicionarConta(Conta conta);
        Task<Conta> BuscarConta(Guid idConta);
        Task<List<Conta>> ListarContas();
        Task AtualizarConta(Conta conta);
        Task ExcluirConta(Conta conta);
    }
}