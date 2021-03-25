using DIO.Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace DIO.Bank.Context
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
        {
        }

        public DbSet<Conta> Contas { get; set; }
    }
}