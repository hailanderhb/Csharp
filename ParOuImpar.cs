
using System;

namespace ParOuImpar
{
    class Program
    {
        static void Main(string[] args)
        {
            int numero = 0, resto = 0;

            Console.Clear();
            Console.WriteLine("Vamos descobrir se o numero é Par ou Impar");
            Console.WriteLine("Digite um numero inteiro: ");
            numero = int.Parse(Console.ReadLine());
            resto = numero % 2;

            if (resto == 1)
            {
                Console.WriteLine("----------------------");
                Console.WriteLine("Este numero é Impar");
                Console.WriteLine("----------------------");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("----------------------");
                Console.WriteLine("Este numero é Par");
                Console.WriteLine("----------------------");
                Console.ReadKey();
            }

        }
    }
}
