using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public interface IRevistaRepositorio
    {
        IList<Revista> Pesquisar(string nomeRevista);
        IList<Revista> BuscarUltimasCinco();
        void Adicionar(Revista revista);
        void Alterar(string nome, Revista revista);
        void ExcluirRevista(string nome);
    }
}
