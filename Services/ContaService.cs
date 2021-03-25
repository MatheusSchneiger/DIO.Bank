using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIO.Bank.Exceptions;
using DIO.Bank.Interfaces;
using DIO.Bank.Models;

namespace DIO.Bank.Services
{
    public class ContaService : IContaService
    {
        private readonly IContaRepository _contaRepository;

        public ContaService(IContaRepository contaRepository) => _contaRepository = contaRepository;

        public async Task AdicionarConta(Conta conta) => await _contaRepository.AdicionarConta(conta);

        public async Task<Conta> BuscarConta(Guid idConta)
        {
            Conta conta = await _contaRepository.BuscarConta(idConta);

            if (conta == null)
                throw new ContaNaoEncontradaException();

            return conta;
        }

        public async Task<List<Conta>> ListarContas() => await _contaRepository.ListarContas();

        public async Task Sacar(Conta conta, double valorSaque)
        {
            if (valorSaque > conta.Saldo + conta.Credito)
                throw new SaldoInsuficienteException();

            conta.Saldo -= valorSaque;

            await _contaRepository.AtualizarConta(conta);
        }

        public async Task Depositar(Conta conta, double valorDeposito)
        {
            conta.Saldo += valorDeposito;

            await _contaRepository.AtualizarConta(conta);
        }

        public async Task Transferir(Conta contaOrigem, Conta contaDestino,double valorTransferencia)
        {
            await Sacar(contaOrigem, valorTransferencia);
            await Depositar(contaDestino, valorTransferencia);
        }

        public async Task ExcluirConta(Conta conta) => await _contaRepository.ExcluirConta(conta);
    }
}