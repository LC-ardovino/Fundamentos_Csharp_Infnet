using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infraestrutura
{
    public class RevistaDictionaryRepository : IRevistaRepositorio
    {
        private static Dictionary<string, Revista> revistaRepository = new Dictionary<string, Revista>();

        public IList<Revista> Pesquisar(string nomeRevista)
        {

            IList<Revista> filtrados = new List<Revista>();

            int i = 0;
            foreach (KeyValuePair<string, Revista> item in revistaRepository)
            {
                if (item.Key.ToUpper().IndexOf(nomeRevista.ToUpper()) >= 0)
                {
                    i++;
                    filtrados.Add(item.Value);
                }
            }

            return filtrados;
        }

        public IList<Revista> BuscarUltimasCinco()
        {
            IList<Revista> filtrados = new List<Revista>();

            int i = 0;
            for (int index = revistaRepository.Count - 1; index >= 0; index--)
            {
                i++;
                var item = revistaRepository.ElementAt(index);
                var itemValue = item.Value;

                filtrados.Add(itemValue);

                if (i == 5)
                {
                    break;
                }
            }

            return filtrados;
        }

        public void Adicionar(Revista revista)
        {
            revistaRepository.Add(revista.Nome, revista);
        }

        public void Alterar(string nome, Revista revista)
        {
            revistaRepository[nome] = revista;
        }

        public void ExcluirRevista(string nome)
        {
            revistaRepository.Remove(nome);
        }
    }
}
