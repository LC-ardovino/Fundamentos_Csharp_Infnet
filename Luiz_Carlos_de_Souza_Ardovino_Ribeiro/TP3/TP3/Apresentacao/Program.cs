using System;
using System.Collections.Generic;
using Business;
using Repository;


namespace Apresentacao
{
    class Program
    {

        static void Main(string[] args)
        {
            var opcao = string.Empty;

            Dictionary<string, Revista> revistas = RevistaRepository.GetAll();

            do
            {
                ExibirMenuPrincipal();
                opcao = Console.ReadLine();
                if (opcao == "1")
                {
                    AdicionarRevista();
                }
                if (opcao == "2")
                {
                    ConsultarRevista(revistas);
                }


            } while (opcao != "3");
        }

        private static void ExibirMenuPrincipal()
        {
            Console.WriteLine("");
            Console.WriteLine("Menu de Opções");
            Console.WriteLine("  1-Adicionar revista:");
            Console.WriteLine("  2-Consultar revista pelo nome:");
            Console.WriteLine("  3-Sair");
            Console.WriteLine("");
        }

        private static void ConsultarRevista(Dictionary<string, Revista> revistas)
        {

            Console.WriteLine("Informe o Nome da revista:");
            var nomeRevista = Console.ReadLine();
            Console.WriteLine("");
            bool achou = false;

            Dictionary<int, string> filtrados = new Dictionary<int, string>();

            int i = 0;
            foreach (KeyValuePair<string, Revista> item in revistas)
            {
                if (item.Key.ToUpper().IndexOf(nomeRevista.ToUpper()) >= 0)
                {
                    achou = true;
                    i++;
                    Console.WriteLine(i + " - Nome: {0}", item.Key);
                    filtrados.Add(i, item.Key);
                }
            }

            if (achou)
            {
                Console.WriteLine("Informe o número da revista para consultar os detalhes");
                var codRevista = Console.ReadLine();

                int codigo = 0;
                try
                {
                     codigo = Int32.Parse(codRevista);
                }
                catch
                {
                    Console.WriteLine("Código inválido!");
                    return;
                }
                

                if (filtrados.ContainsKey(codigo))
                {
                    string nome = filtrados[codigo];

                    Revista detRevista = RevistaRepository.GetByName(nome);

                    bool disponivel = detRevista.Disponivel;

                    DateTime Data = detRevista.DataCriacao;
                    Console.WriteLine("Nome: " + nome);
                    Console.WriteLine("Data Criação: " + Data.ToString());
                    Console.WriteLine("Valor: R$" + detRevista.Valor.ToString());
                    Console.WriteLine("Disponível: " + (detRevista.Disponivel == true? "Sim":"Não"));

                    int idade = detRevista.CalculaIdade(Data);
                    Console.WriteLine("Idade: " + idade + " anos");

                }
                else
                {
                    Console.WriteLine("Código inválido!");
                }
            }
            else
            {
                Console.WriteLine("Não foram encontradas revistas com este nome.");
            }

        }


        private static void AdicionarRevista()
        {


            Console.WriteLine("Nome da revista:");
            var nome = Console.ReadLine();

            if (RevistaRepository.GetAll().Count > 0)
            {
                if (RevistaRepository.GetByName(nome) != null)
                {
                    Console.WriteLine("Esta revista " + nome + " já esta cadastrada.");
                    nome = Console.ReadLine();
                    return;
                }
            }

           
            Console.WriteLine("Informe a data de criação da revista (valores numéricos):");

            string ano = Ano();
            string mes = Mes();
            string dia = Dia();

            string data = dia + "/" + mes + "/" + ano;
            DateTime dataCriacao = Convert.ToDateTime(data);

            Console.WriteLine("A data informada foi:" + data);

            //Console.WriteLine("Valor de venda da revista:");
            //var valor = Console.ReadLine();

            var valor = ValorValido();


            Console.WriteLine("Revista está disponível? (S/N)");
            var situacao = Console.ReadLine();

            while (situacao.ToUpper() != "S" && situacao.ToUpper() != "N")
            {
                Console.WriteLine("Revista está disponível? (S/N)");
                situacao = Console.ReadLine();
            }

            bool disponivel = situacao.ToUpper() == "S" ? true : false;


            DateTime tempDate = Convert.ToDateTime(data);

            Revista NovaRevista = new Revista(nome,0,dataCriacao,double.Parse(valor),disponivel);

            NovaRevista.Idade = NovaRevista.CalculaIdade(dataCriacao);

            RevistaRepository.IncluirRevista(NovaRevista);


        }


        private static string ValorValido()
        {
            Console.WriteLine("Valor de venda da revista:");
            var valor = Console.ReadLine();
            double n;
            while (valor != null)
            {
                bool result = Double.TryParse(valor, out n);
                if (result)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("O valor fornecido não é um valor válido. Informe um valor de venda válido:");
                    valor = Console.ReadLine();
                }
            }

            return valor;
        }


        private static string Ano()
        {
            Console.WriteLine("Informe o Ano:");
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
            Console.WriteLine("Informe o Mês:");
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
            Console.WriteLine("Informe o Dia:");
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
