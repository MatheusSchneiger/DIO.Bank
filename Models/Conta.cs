using System;
using DIO.Bank.Enums;

namespace DIO.Bank.Models
{
    public class Conta
    {
        public Guid Id { get; set; }
        public TipoConta TipoConta { get; set; }
        public double Saldo { get; set; }
        public double Credito { get; set; }
        public string Nome { get; set; }

        public void MostrarSaldo() => Console.WriteLine($"Saldo atual da conta de {Nome} é {Saldo}");

        public override string ToString() => $"Id {Id} | TipoConta {TipoConta} | Nome {Nome} | Saldo {Saldo} | Crédito {Credito}";
    }
}