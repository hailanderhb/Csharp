using System;

namespace ReajSalario
{

    class Program
    {

        static void Main(string[] args)
        {
            double salario;
            double reajustado;

            Console.WriteLine("Calcule o seu Salário Reajustado");
            for (int contador = 0; contador < 10; contador++)
            {
                Console.WriteLine("");
                Console.WriteLine($"Informe o seu salário funcionário {contador + 1}: ");
                salario = Convert.ToDouble(Console.ReadLine());

                if (salario <= 300)
                {
                    reajustado = salario + (salario * 0.5);
                }
                else
                {
                    reajustado = salario + (salario * 0.3);
                }
                Console.WriteLine($"Salário reajustado: {reajustado}");
            }
            Console.ReadKey();
        }
    }

}
