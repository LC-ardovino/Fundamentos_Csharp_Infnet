using System;

namespace Dominio
{
    public class Revista
    {
        public string Nome { get; set; }
        public int Edicao { get; set; }
        public DateTime DataCriacao { get; set; }
        public double Valor { get; set; }
        public bool Disponivel { get; set; }

        public Revista(string nome, int edicao, DateTime dataCriacao, double valor, bool disponivel)
        {
            Nome = nome;
            Edicao = edicao;
            DataCriacao = dataCriacao;
            Valor = valor;
            Disponivel = disponivel;
        }


        public int CalculaIdade(DateTime dataCriacao)
        {
            int idade = DateTime.Now.Year - dataCriacao.Year;
            if (DateTime.Now.DayOfYear < dataCriacao.DayOfYear)
            {
                idade = idade - 1;
            }
            return idade;
        }
    }
}
