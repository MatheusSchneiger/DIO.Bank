using System;
using DIO.Bank.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DIO.Bank.Interfaces;
using DIO.Bank.Repositories;
using DIO.Bank.Services;
using System.Threading.Tasks;
using DIO.Bank.Enums;
using DIO.Bank.Exceptions;
using DIO.Bank.Context;
using Microsoft.EntityFrameworkCore;

namespace DIO.Bank
{
    class Program
    {
        static IContaService contaService;

        static async Task Main(string[] args)
        {
            ConfigurarServicos(args);

            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                try
                {
                    switch (opcaoUsuario)
                    {
                        case "1":
                            await ListarContas();
                            break;
                        case "2":
                            await InserirConta();
                            break;
                        case "3":
                            await Transferir();
                            break;
                        case "4":
                            await Sacar();
                            break;
                        case "5":
                            await Depositar();
                            break;
                        case "6":
                            await ExcluirConta();
                            break;
                        case "C":
                            Console.Clear();
                            break;
                        default:
                            Console.WriteLine("Opção inválida");
                            break;
                    }
                }
                catch (ContaNaoEncontradaException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (SaldoInsuficienteException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Obrigado por utilizar nossos serviços.");
            Console.ReadLine();
        }

        private static void ConfigurarServicos(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                services.AddDbContext<BankDbContext>(options => options.UseSqlite("Data Source=Bank.db")).AddScoped<IContaService, ContaService>()
                        .AddScoped<IContaRepository, ContaRepository>()).Build();

            IServiceScope scope = host.Services.CreateScope();
            var provider = scope.ServiceProvider;
            contaService = provider.GetRequiredService<IContaService>();
        }

        private static async Task<Conta> BuscarConta()
        {
            Console.Write("Digite o id da conta: ");
            string idConta = Console.ReadLine();

            return await contaService.BuscarConta(Guid.Parse(idConta));
        }

        private static async Task Depositar()
        {
            var conta = await BuscarConta();

            Console.Write("Digite o valor a ser depositado: ");
            double valorDeposito = double.Parse(Console.ReadLine());

            await contaService.Depositar(conta, valorDeposito);
        }

        private static async Task Sacar()
        {
            var conta = await BuscarConta();

            Console.Write("Digite o valor a ser sacado: ");
            double valorSaque = double.Parse(Console.ReadLine());
            await contaService.Sacar(conta, valorSaque);
        }

        private static async Task Transferir()
        {
            Console.Write("Digite o id da conta de origem: ");
            string idContaOrigem = Console.ReadLine();
            var contaOrigem = await contaService.BuscarConta(Guid.Parse(idContaOrigem));

            Console.Write("Digite o id da conta de destino: ");
            string idContaDestino = Console.ReadLine();
            var contaDestino = await contaService.BuscarConta(Guid.Parse(idContaDestino));

            Console.Write("Digite o valor a ser transferido: ");
            double valorTransferencia = double.Parse(Console.ReadLine());

            await contaService.Transferir(contaOrigem, contaDestino, valorTransferencia);
        }

        private static async Task InserirConta()
        {
            Console.WriteLine("Inserir nova conta");

            Console.Write("Digite 1 para Conta Fisica ou 2 para Juridica: ");
            int entradaTipoConta = int.Parse(Console.ReadLine());

            Console.Write("Digite o Nome do Cliente: ");
            string entradaNome = Console.ReadLine();

            Console.Write("Digite o saldo inicial: ");
            double entradaSaldo = double.Parse(Console.ReadLine());

            Console.Write("Digite o crédito: ");
            double entradaCredito = double.Parse(Console.ReadLine());

            var novaConta = new Conta
            {
                TipoConta = (TipoConta)entradaTipoConta,
                Saldo = entradaSaldo,
                Credito = entradaCredito,
                Nome = entradaNome
            };

            await contaService.AdicionarConta(novaConta);
        }

        private static async Task ListarContas()
        {
            Console.WriteLine("Listar contas");

            var contas = await contaService.ListarContas();

            if (contas.Count == 0)
            {
                Console.WriteLine("Nenhuma conta cadastrada.");
                return;
            }

            foreach (var conta in contas)
                Console.WriteLine(conta);
        }

        private static async Task ExcluirConta()
        {
            var conta = await BuscarConta();

            await contaService.ExcluirConta(conta);

            Console.WriteLine("Conta excluída!");
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Bank a seu dispor!!!");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1- Listar contas");
            Console.WriteLine("2- Inserir nova conta");
            Console.WriteLine("3- Transferir");
            Console.WriteLine("4- Sacar");
            Console.WriteLine("5- Depositar");
            Console.WriteLine("6- Excluir conta");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}
