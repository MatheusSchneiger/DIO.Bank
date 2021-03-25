using System;

namespace DIO.Bank.Exceptions
{
    public class SaldoInsuficienteException : Exception
    {
        public SaldoInsuficienteException() : base("Saldo insuficiente!")
        {
        }
    }
}