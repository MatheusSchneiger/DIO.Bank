using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIO.Bank.Context;
using DIO.Bank.Interfaces;
using DIO.Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace DIO.Bank.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private readonly BankDbContext _context;

        public ContaRepository(BankDbContext context) => _context = context;

        public async Task AdicionarConta(Conta conta)
        {
            await _context.Contas.AddAsync(conta);
            await _context.SaveChangesAsync();
        }

        public async Task<Conta> BuscarConta(Guid idConta) => await _context.Contas.FindAsync(idConta);

        public async Task<List<Conta>> ListarContas() => await _context.Contas.AsNoTracking().ToListAsync();

        public async Task AtualizarConta(Conta conta)
        {
            _context.Contas.Update(conta);
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirConta(Conta conta)
        {
            _context.Contas.Remove(conta);
            await _context.SaveChangesAsync();
        }
    }
}