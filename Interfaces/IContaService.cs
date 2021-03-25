using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIO.Bank.Models;

namespace DIO.Bank.Interfaces
{
    public interface IContaService
    {
        Task AdicionarConta(Conta conta);
        Task<Conta> BuscarConta(Guid idConta);
        Task<List<Conta>> ListarContas();
        Task Sacar(Conta conta, double valorSaque);
        Task Depositar(Conta conta, double valorDeposito);
        Task Transferir(Conta contaOrigem, Conta contaDestino, double valorTransferencia);
        Task ExcluirConta(Conta conta);
    }
}