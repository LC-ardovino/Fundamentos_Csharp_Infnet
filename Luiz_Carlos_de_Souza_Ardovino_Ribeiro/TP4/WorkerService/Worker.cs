using Dominio;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly IRevistaRepositorio _repositorio;

        public Worker(IRevistaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        const string pressioneQualquerTecla = "Pressione qualquer tecla para exibir o menu principal ...";

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");

            string opcao;
            do
            {
                Console.Clear();

                Console.WriteLine("###################################");
                Console.WriteLine("�ltimas Revistas Cadastradas");
                ExibirUltimasRevistas();
                Console.WriteLine("###################################");


                Console.WriteLine("*** Gerenciador de Revistas *** ");
                Console.WriteLine("1 - Pesquisar Revista:");
                Console.WriteLine("2 - Adicionar Revista:");
                Console.WriteLine("3 - Alterar Revista:");
                Console.WriteLine("4 - Excluir Revista:");
                Console.WriteLine("5 - Sair:");
                Console.WriteLine("\nEscolha uma das op��es acima: ");

                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        ConsultarRevista();
                        break;
                    case "2":
                        AdicionarRevista();
                        break;
                    case "3":
                        AlterarRevista();
                        break;
                    case "4":
                        ExcluirRevista();
                        break;
                    case "5":
                        Console.Write("Saindo do programa... ");
                        break;
                    default:
                        Console.Write($"Opcao inv�lida! Escolha uma op��o v�lida. {pressioneQualquerTecla}");
                        Console.ReadKey();
                        break;
                }

            } while (opcao != "5");
        }

        private void ExibirUltimasRevistas()
        {
            var entidadesEncontradas = _repositorio.BuscarUltimasCinco();

            int i = 0;
            for (var index = 0; index < entidadesEncontradas.Count; index++)
            {

                Console.WriteLine($"[Nome]: {entidadesEncontradas[index].Nome}");
                Console.WriteLine($"[Data Criacao]: {entidadesEncontradas[index].DataCriacao:dd/MM/yyyy}");
                Console.WriteLine($"[Valor]: {entidadesEncontradas[index].Valor}");
                Console.WriteLine($"[Edicao]: {entidadesEncontradas[index].Edicao}");
                Console.WriteLine($"[Dispon�vel]: {(entidadesEncontradas[index].Disponivel == true ? "Sim" : "N�o")}");
                Console.WriteLine(" ");

                if (i == 5)
                {
                    break;
                }
            }
            

            if (entidadesEncontradas.Count < 5)
            {
                Console.WriteLine("Total de revistas cadastradas: " + entidadesEncontradas.Count);
            }
        }


        private void ConsultarRevista()
        {
            Console.WriteLine("Informe o nome ou parte do nome da Revista que deseja pesquisar:");
            var termoDePesquisa = Console.ReadLine();
            var entidadesEncontradas = _repositorio.Pesquisar(termoDePesquisa);

            if (entidadesEncontradas.Count > 0)
            {
                Console.WriteLine("Selecione uma das op��es abaixo para visualizar os dados das Revistas encontradas:");
                for (var index = 0; index < entidadesEncontradas.Count; index++)
                    Console.WriteLine($"{index} - {entidadesEncontradas[index].Nome}");

                if (!ushort.TryParse(Console.ReadLine(), out var indexAExibir) || indexAExibir >= entidadesEncontradas.Count)
                {
                    Console.WriteLine($"Op��o inv�lida! {pressioneQualquerTecla}");
                    Console.ReadKey();
                    return;
                }

                if (indexAExibir < entidadesEncontradas.Count)
                {
                    var entidade = entidadesEncontradas[indexAExibir];

                    Console.WriteLine("Dados da Revista");
                    Console.WriteLine($"[Nome]: {entidade.Nome}");
                    Console.WriteLine($"[Data Criacao]: {entidade.DataCriacao:dd/MM/yyyy}");
                    Console.WriteLine($"[Valor]: {entidade.Valor}");
                    Console.WriteLine($"[Edicao]: {entidade.Edicao}");
                    Console.WriteLine($"[Dispon�vel]: {(entidade.Disponivel == true ? "Sim" : "N�o")}");

                    int idade = entidade.CalculaIdade(entidade.DataCriacao);
                    Console.WriteLine($"[Idade]: {idade}");


                    Console.Write(pressioneQualquerTecla);
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine($"N�o foi encontrado nenhuma Revista! {pressioneQualquerTecla}");
                Console.ReadKey();
            }
        }


        private void AdicionarRevista()
        {
            Console.WriteLine("Informe o nome da Revista que deseja adicionar:");
            var nome = Console.ReadLine();

            Console.WriteLine("Informe a data de cria��o da revista (formato dd/MM/yyyy):");
            if (!DateTime.TryParse(Console.ReadLine(), out var dataCriacao))
            {
                Console.WriteLine($"Data inv�lida! Dados descartados! {pressioneQualquerTecla}");
                Console.ReadKey();
                return;
            }

            var valor = ValorValido();


            Console.WriteLine("Revista est� dispon�vel? (S/N)");
            var situacao = Console.ReadLine();

            while (situacao.ToUpper() != "S" && situacao.ToUpper() != "N")
            {
                Console.WriteLine("Revista est� dispon�vel? (S/N)");
                situacao = Console.ReadLine();
            }

            bool disponivel = situacao.ToUpper() == "S" ? true : false;

            var edicao = ValidaEdicao();

            Revista NovaRevista = new Revista(nome, int.Parse(edicao), dataCriacao, double.Parse(valor), disponivel);
      

            Console.WriteLine("Os dados est�o corretos?");
            Console.WriteLine($"[Nome]: {NovaRevista.Nome}");
            Console.WriteLine($"[Data da Cria��o]: {NovaRevista.DataCriacao:dd/MM/yyyy}");
            Console.WriteLine($"[Edicao]: {NovaRevista.Edicao}");
            Console.WriteLine($"[Valor]: {NovaRevista.Valor}");
            Console.WriteLine($"[Dispon�vel]: {situacao.ToUpper()}");
            Console.WriteLine("1 - Sim \n2 - N�o");

            var opcaoParaAdicionar = Console.ReadLine();
            if (opcaoParaAdicionar == "1")
            {
                _repositorio.Adicionar(NovaRevista);

                Console.WriteLine($"Dados adicionados com sucesso! {pressioneQualquerTecla}");
                Console.ReadKey();
            }
            else if (opcaoParaAdicionar == "2")
            {
                Console.WriteLine($"Dados descartados! {pressioneQualquerTecla}");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Op��o inv�lida! {pressioneQualquerTecla}");
                Console.ReadKey();
            }
        }

        private void AlterarRevista()
        {
            Console.WriteLine("Informe o nome ou parte do nome da Revista que deseja alterar:");
            var termoDePesquisa = Console.ReadLine();
            var entidadesEncontradas = _repositorio.Pesquisar(termoDePesquisa);

            if (entidadesEncontradas.Count > 0)
            {
                Console.WriteLine("Selecione uma das op��es abaixo para visualizar os dados das Revistas encontradas:");
                for (var index = 0; index < entidadesEncontradas.Count; index++)
                    Console.WriteLine($"{index} - {entidadesEncontradas[index].Nome}");

                if (!ushort.TryParse(Console.ReadLine(), out var indexAExibir) || indexAExibir >= entidadesEncontradas.Count)
                {
                    Console.WriteLine($"Op��o inv�lida! {pressioneQualquerTecla}");
                    Console.ReadKey();
                    return;
                }

                if (indexAExibir < entidadesEncontradas.Count)
                {
                    var entidade = entidadesEncontradas[indexAExibir];

                    Console.WriteLine("Dados da Revista");
                    Console.WriteLine($"[Nome]: {entidade.Nome}");
                    Console.WriteLine($"[Data Criacao]: {entidade.DataCriacao:dd/MM/yyyy}");
                    Console.WriteLine($"[Valor]: {entidade.Valor}");
                    Console.WriteLine($"[Edicao]: {entidade.Edicao}");
                    Console.WriteLine($"[Dispon�vel]: {(entidade.Disponivel == true ? "Sim" : "N�o")}");


                    Console.WriteLine(" ");
                    Console.WriteLine("#### Dados para altera��o da revista ####");

                    Console.WriteLine("Informe a data de cria��o da revista (formato dd/MM/yyyy):");
                    if (!DateTime.TryParse(Console.ReadLine(), out var dataCriacao))
                    {
                        Console.WriteLine($"Data inv�lida! Dados descartados! {pressioneQualquerTecla}");
                        Console.ReadKey();
                        return;
                    }

                    var valor = ValorValido();


                    Console.WriteLine("Revista est� dispon�vel? (S/N)");
                    var situacao = Console.ReadLine();

                    while (situacao.ToUpper() != "S" && situacao.ToUpper() != "N")
                    {
                        Console.WriteLine("Revista est� dispon�vel? (S/N)");
                        situacao = Console.ReadLine();
                    }

                    bool disponivel = situacao.ToUpper() == "S" ? true : false;

                    var edicao = ValidaEdicao();

                    Revista RevistaAlterada = new Revista(entidade.Nome, int.Parse(edicao), dataCriacao, double.Parse(valor), disponivel);

                    Console.WriteLine("Os dados est�o corretos?");
                    Console.WriteLine($"[Nome]: {RevistaAlterada.Nome}");
                    Console.WriteLine($"[Data da Cria��o]: {RevistaAlterada.DataCriacao:dd/MM/yyyy}");
                    Console.WriteLine($"[Edicao]: {RevistaAlterada.Edicao}");
                    Console.WriteLine($"[Valor]: {RevistaAlterada.Valor}");
                    Console.WriteLine($"[Dispon�vel]: {situacao.ToUpper()}");
                    Console.WriteLine("1 - Sim \n2 - N�o");

                    var opcaoParaAlterar = Console.ReadLine();
                    if (opcaoParaAlterar == "1")
                    {
                        _repositorio.Alterar(RevistaAlterada.Nome,RevistaAlterada);

                        Console.WriteLine($"Dados alterados com sucesso! {pressioneQualquerTecla}");
                        Console.ReadKey();
                    }
                    else if (opcaoParaAlterar == "2")
                    {
                        Console.WriteLine($"Dados descartados! {pressioneQualquerTecla}");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine($"Op��o inv�lida! {pressioneQualquerTecla}");
                        Console.ReadKey();
                    }

                }
            }
            else
            {
                Console.WriteLine($"N�o foi encontrado nenhuma Revista! {pressioneQualquerTecla}");
                Console.ReadKey();
            }
        }

        private void ExcluirRevista()
        {
            Console.WriteLine("Informe o nome ou parte do nome da Revista que deseja excluir:");
            var termoDePesquisa = Console.ReadLine();
            var entidadesEncontradas = _repositorio.Pesquisar(termoDePesquisa);

            if (entidadesEncontradas.Count > 0)
            {
                Console.WriteLine("Selecione uma das op��es abaixo para visualizar os dados das Revistas encontradas:");
                for (var index = 0; index < entidadesEncontradas.Count; index++)
                    Console.WriteLine($"{index} - {entidadesEncontradas[index].Nome}");

                if (!ushort.TryParse(Console.ReadLine(), out var indexAExibir) || indexAExibir >= entidadesEncontradas.Count)
                {
                    Console.WriteLine($"Op��o inv�lida! {pressioneQualquerTecla}");
                    Console.ReadKey();
                    return;
                }

                if (indexAExibir < entidadesEncontradas.Count)
                {
                    var entidade = entidadesEncontradas[indexAExibir];

                    Console.WriteLine("Dados da Revista");
                    Console.WriteLine($"[Nome]: {entidade.Nome}");
                    Console.WriteLine($"[Data Criacao]: {entidade.DataCriacao:dd/MM/yyyy}");
                    Console.WriteLine($"[Valor]: {entidade.Valor}");
                    Console.WriteLine($"[Edicao]: {entidade.Edicao}");
                    Console.WriteLine($"[Dispon�vel]: {(entidade.Disponivel == true ? "Sim" : "N�o")}");



                    Console.WriteLine(" ");
                    Console.WriteLine("Deseja excluir esta revista? (S/N)");
                    var situacao = Console.ReadLine();

                    while (situacao.ToUpper() != "S" && situacao.ToUpper() != "N")
                    {
                        Console.WriteLine("Deseja excluir esta revista? (S/N)");
                        situacao = Console.ReadLine();
                    }

                    if (situacao.ToUpper() == "S")
                    {
                        _repositorio.ExcluirRevista(entidade.Nome);
                        Console.WriteLine($"Dados exclu�dos com sucesso! {pressioneQualquerTecla}");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine($"Exclus�o cancelada! {pressioneQualquerTecla}");
                        Console.ReadKey();
                    }
                }
            }
            else
            {
                Console.WriteLine($"N�o foi encontrado nenhuma Revista! {pressioneQualquerTecla}");
                Console.ReadKey();
            }
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
                    Console.WriteLine("O valor fornecido n�o � um valor v�lido. Informe um valor de venda v�lido:");
                    valor = Console.ReadLine();
                }
            }

            return valor;
        }

        private static string ValidaEdicao()
        {
            Console.WriteLine("N�mero da Edi��o da revista:");
            var valor = Console.ReadLine();
            int n;
            while (valor != null)
            {
                bool result = int.TryParse(valor, out n);
                if (result)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("O valor fornecido n�o � um n�mero inteiro. Informe um n�mero inteiro v�lido:");
                    valor = Console.ReadLine();
                }
            }
            return valor;
        }

    }
}
