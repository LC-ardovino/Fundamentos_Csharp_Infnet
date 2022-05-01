using System;
using System.Collections.Generic;
using Business;

namespace Repository
{
    public static class RevistaRepository
    {

        private static Dictionary<string, Revista> revistaRepository = new Dictionary<string, Revista>();

        public static Dictionary<string, Revista> GetAll()
        {
            return revistaRepository;
        }

        public static Revista GetByName(string nome)
        {
            try
            {
                return revistaRepository[nome];
            }
            catch
            {
                return null;
            }
        }


        public static void IncluirRevista(Revista revista)
        {
            revistaRepository.Add(revista.Nome, revista);
        }
    }
}
