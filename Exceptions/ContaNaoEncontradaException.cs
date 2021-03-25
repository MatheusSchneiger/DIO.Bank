using System;

namespace DIO.Bank.Exceptions
{
    public class ContaNaoEncontradaException : Exception
    {
        public ContaNaoEncontradaException() : base("Conta não encontrada")
        {
        }
    }
}