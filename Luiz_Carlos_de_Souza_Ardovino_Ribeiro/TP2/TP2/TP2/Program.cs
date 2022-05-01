using System;
using System.Collections.Generic;

namespace TP2
{
    class Program
    {

        private static void Main(string[] args)
        {
            var opcao = string.Empty;
            Dictionary<string, DateTime> personagens = new Dictionary<string, DateTime>();

            do
            {
                ExibirMenuPrincipal();
                opcao = Console.ReadLine();
                if (opcao == "1")
                {
                    AdicionarPersonagem(ref personagens);
                }
                if (opcao == "2")
                {
                    ConsultarPersonagem(ref personagens);
                }


            } while (opcao != "3");

        }

        private static void ExibirMenuPrincipal()
        {
            //Console.Clear();
            Console.WriteLine("1-Adicionar personagem:");
            Console.WriteLine("2-Verificar personagens pelo nome:");
            Console.WriteLine("3-Sair");
        }

        private static void ConsultarPersonagem(ref Dictionary<string, DateTime> personagens)
        {
            Console.WriteLine("Informe o Nome do personagem:");
            var personagem = Console.ReadLine();
            bool achou = false;

            Dictionary<int, string> filtrados = new Dictionary<int, string>();

            int i = 0;
            foreach (KeyValuePair<string, DateTime> item in personagens)
            {
                i++;

                if (item.Key.IndexOf(personagem) >= 0)
                {
                    achou = true;
                    achou = true;
                    Console.WriteLine(i + " - Nome: {0}", item.Key);
                    filtrados.Add(i, item.Key);
                    //Console.WriteLine(i + " - Nome: {0}, Data: {1}", item.Key, item.Value);
                }
            }

            if (achou)
            {
                Console.WriteLine("Informe o número do personagem para consultar os detalhes");
                var codPersonagem = Console.ReadLine();

                int codigo = Int32.Parse(codPersonagem);

                if (filtrados.ContainsKey(codigo))
                {
                    string nome = filtrados[codigo];
                    DateTime Data = personagens[nome];
                    Console.WriteLine("Nome: " + nome);
                    Console.WriteLine("Data: " + Data.ToString());

                    int idade = CalculaIdade(Data);
                    Console.WriteLine("Idade: " + idade);

                }
                else
                {
                    Console.WriteLine("Código inválido!");
                }
            }
            else
            {
                Console.WriteLine("Não foram encontrados personagens com este nome.");
            }

        }

        private static int CalculaIdade(DateTime dataNascimento)
        {
            int idade = DateTime.Now.Year - dataNascimento.Year;
            if (DateTime.Now.DayOfYear < dataNascimento.DayOfYear)
            {
                idade = idade - 1;
            }
            return idade;
        }

        private static void AdicionarPersonagem(ref Dictionary<string, DateTime> personagens)
        {

            Console.WriteLine("Nome do personagem:");
            var personagem = Console.ReadLine();

            if (personagens.ContainsKey(personagem))
            {
                Console.WriteLine("Este personagem " + personagem + " já esta cadastrado.");
                personagem = Console.ReadLine();
                return;
            }


            Console.WriteLine("Adicione a data de criacao do personagem(valores numericos):");

            string ano = Ano();
            string mes = Mes();
            string dia = Dia();

            string data = dia + "/" + mes + "/" + ano;

            Console.WriteLine("A data informada foi:" + data);

            DateTime tempDate = Convert.ToDateTime(data);
            personagens.Add(personagem, tempDate);


        }

        private static string Ano()
        {
            Console.WriteLine("Ano:");
            var Ano = Console.ReadLine();
            int n;
            while (Ano != null)
            {
                bool result = Int32.TryParse(Ano, out n);
                if (result && Ano.Length <= 4)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("O valor fornecido não é um ano válido. Informe um ano válido:");
                    Ano = Console.ReadLine();
                }
            }

            return Ano;
        }

        private static string Mes()
        {
            Console.WriteLine("Mes:");
            var mes = Console.ReadLine();
            int n;
            while (mes != null)
            {
                bool result = Int32.TryParse(mes, out n);
                if (result)
                {
                    if (n >= 1 & n <= 12)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("O mês deve ser um número entre 1 e 12");
                        mes = Console.ReadLine();

                    }

                }
                else
                {
                    Console.WriteLine("O valor fornecido não é um número. Informe um valor válido:");
                    mes = Console.ReadLine();
                }
            }
            return mes;
        }

        private static string Dia()
        {
            Console.WriteLine("Dia:");
            var dia = Console.ReadLine();
            int n;
            while (dia != null)
            {
                bool result = Int32.TryParse(dia, out n);


                if (result)
                {
                    if (n >= 1 & n <= 31)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("O valor fornecido não é um número de dia válido.");
                        dia = Console.ReadLine();
                    }

                }
                else
                {
                    Console.WriteLine("O valor fornecido não é um número válido. Informe um valor válido:");
                    dia = Console.ReadLine();

                }

            }
            return dia;
        }


    }
}