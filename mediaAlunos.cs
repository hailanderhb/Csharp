using System;

namespace mediaAlunos
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.Clear();
            int soma = 0;
            float media;


            for (int aluno = 0; aluno < 5; aluno++)
            {
                Console.WriteLine("Digite a idade do primeiro aluno: ");
                int idade = int.Parse(Console.ReadLine());
                soma += idade;
            }

            media = soma / 5;
            Console.WriteLine($"A média dos 5 alunos é de {media}");
            Console.ReadLine();
        }
    }
}