using Course;
using System;
using System.Globalization;

namespace Exercícios
{
    class Program
    {
        static void menuDS(ContaBancaria conta)
        {
            Console.WriteLine("O que você deseja fazer ?\n1 - Depositar \n2- Sacar");
            int numero = int.Parse(Console.ReadLine());
            switch (numero)
            {
                case 1:
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine("Digite o valor do deposito: ");
                    double quantia = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    conta.Deposito(quantia);
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine("Dados da conta atualizados: ");
                    Console.WriteLine(conta);
                    break;
                case 2:
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine("Digite o valor do saque: ");
                    quantia = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    conta.Saque(quantia);
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine("Dados da conta atualizados: ");
                    Console.WriteLine(conta);
                    break;

            }

        }

        static void Main()

        {
            Console.Clear();
            ContaBancaria conta;

            Console.WriteLine("Entre o número da conta: ");
            int numero = int.Parse(Console.ReadLine());
            Console.WriteLine("Entre com o títular da conta: ");
            string titular = Console.ReadLine();
            Console.WriteLine("Haverá depósito inicial (s/n) ?");
            char resp = char.Parse(Console.ReadLine().ToUpper());
            if (resp == 'S')
            {
                Console.Write("Entre o valor de depósito inicial: ");
                double depositoInicial = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                conta = new ContaBancaria(numero, titular, depositoInicial);
                Console.WriteLine();
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("Dados da conta: ");
                Console.WriteLine(conta);
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine();
                menuDS(conta);

            }
            else
            {
                conta = new ContaBancaria(numero, titular);
                Console.WriteLine();
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("Dados da conta: ");
                Console.WriteLine(conta);
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine();
                menuDS(conta);
            }
            Console.WriteLine("------------------------------");
            Console.WriteLine("Acesso Finalizado!");
            Console.WriteLine("------------------------------");
            Console.ReadKey();
            Main();

        }
    }
}




//--------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Course
{
    class ContaBancaria
    {

        public int Numero { get; private set; }
        public string Titular { get; set; }
        public double Saldo { get; private set; } // importância do encapsulamento utilizando get/set

        public ContaBancaria(int numero, string titular)
        {
            Numero = numero;
            Titular = titular;
        }

        public ContaBancaria(int numero, string titular, double saldo) : this(numero, titular)
        {
            Saldo = saldo;
        }

        public void Deposito(double quantia)
        {
            Saldo += quantia;
        }

        public void Saque(double quantia)
        {
            Saldo -= quantia + 5.00;
        }

        public override string ToString()
        {
            return "Conta " + Numero + ", Titular: " + Titular + ", Saldo: $ " + Saldo.ToString("F2", CultureInfo.InvariantCulture);
        }
    }
}