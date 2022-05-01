using Dominio;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

namespace Infraestrutura
{
    public class RevistaFileRepository : IRevistaRepositorio
    {
        public IList<Revista> Pesquisar(string nomeRevista)
        {

            IList<Revista> revistas = new List<Revista>();
            try
            {
                string path = Directory.GetCurrentDirectory() + "\\Sample.txt";

                if (File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        while (sr.Peek() >= 0)
                        {
                            string str;
                            string[] strArray;
                            str = sr.ReadLine();

                            strArray = str.Split(';');
                            string nome = strArray[0];

                            if (nome.ToUpper().IndexOf(nomeRevista.ToUpper()) >= 0)
                            {
                                int edicao = int.Parse(strArray[1]);
                                DateTime dataCriacao = DateTime.Parse(strArray[2]);
                                double valor = double.Parse(strArray[3]);
                                bool disponivel = (strArray[4] == "True" ? true : false);
                                Revista revista = new Revista(nome, edicao, dataCriacao, valor, disponivel);

                                revistas.Add(revista);
                            }
                        }
                        sr.Close();
                    }
                }
                return revistas;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return revistas;
            }

        }

        public void Adicionar(Revista revista)
        {
            string linha = "";

            IList<Revista> revistas = new List<Revista>();
            try
            {
                string path = Directory.GetCurrentDirectory() + "\\Sample.txt";

                if (!File.Exists(path))
                {
                    FileStream fs = File.Create(path);
                    fs.Close();
                }

                linha = revista.Nome + ";" + revista.Edicao + ";" + revista.DataCriacao + ";" +  revista.Valor + ";" + revista.Disponivel;

                StreamWriter sw = new StreamWriter(path,true);
                sw.Write(linha + "\r\n");
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public void Alterar(string nomeRevista, Revista revistaAlterada)
        {
            IList<Revista> revistas = new List<Revista>();
            try
            {
                string path = Directory.GetCurrentDirectory() + "\\Sample.txt";

                if (File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        while (sr.Peek() >= 0)
                        {
                            string str;
                            string[] strArray;
                            str = sr.ReadLine();

                            strArray = str.Split(';');
                            string nome = strArray[0];

                            if (nome.ToUpper() == (nomeRevista.ToUpper()))
                            {
                                int edicao = revistaAlterada.Edicao;
                                DateTime dataCriacao = revistaAlterada.DataCriacao;
                                double valor = revistaAlterada.Valor;
                                bool disponivel = revistaAlterada.Disponivel;
                                Revista revista = new Revista(nome, edicao, dataCriacao, valor, disponivel);
                                revistas.Add(revista);
                            }
                            else 
                            { 
                                int edicao = int.Parse(strArray[1]);
                                DateTime dataCriacao = DateTime.Parse(strArray[2]);
                                double valor = double.Parse(strArray[3]);
                                bool disponivel = (strArray[4] == "True" ? true : false);
                                Revista revista = new Revista(nome, edicao, dataCriacao, valor, disponivel);
                                revistas.Add(revista);
                            }
                            
                        }
                        sr.Close();
                    }

                    //Salva no arquivo
                    int i = 0;
                    string linha = "";
                    FileStream fs = File.Create(path);
                    fs.Close();

                    StreamWriter sw = new StreamWriter(path, true, Encoding.ASCII);

                    for (var index = 0; index < revistas.Count; index++)
                    {                        
                        linha = revistas[index].Nome + ";" + revistas[index].Edicao + ";" + revistas[index].DataCriacao + ";" + revistas[index].Valor + ";" + revistas[index].Disponivel;
                        sw.Write(linha + "\r\n");
                    }
                    sw.Close();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

        }
        public void ExcluirRevista(string nomeRevista)
        {

            IList<Revista> revistas = new List<Revista>();
            try
            {
                string path = Directory.GetCurrentDirectory() + "\\Sample.txt";

                if (File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        while (sr.Peek() >= 0)
                        {
                            string str;
                            string[] strArray;
                            str = sr.ReadLine();

                            strArray = str.Split(';');
                            string nome = strArray[0];

                            if (nome.ToUpper() != (nomeRevista.ToUpper()))
                            {
                                int edicao = int.Parse(strArray[1]);
                                DateTime dataCriacao = DateTime.Parse(strArray[2]);
                                double valor = double.Parse(strArray[3]);
                                bool disponivel = (strArray[4] == "True" ? true : false);
                                Revista revista = new Revista(nome, edicao, dataCriacao, valor, disponivel);

                                revistas.Add(revista);
                            }
                        }
                        sr.Close();
                    }

                    int i = 0;
                    string linha = "";
                    FileStream fs = File.Create(path);
                    fs.Close();

                    StreamWriter sw = new StreamWriter(path, true, Encoding.ASCII);

                    for (var index = 0; index < revistas.Count; index++)
                    {
                        linha = revistas[index].Nome + ";" + revistas[index].Edicao + ";" + revistas[index].DataCriacao + ";" + revistas[index].Valor + ";" + revistas[index].Disponivel;
                        sw.Write(linha + "\r\n");
                    }
                    sw.Close();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public IList<Revista> BuscarUltimasCinco()
        {
            IList<Revista> revistas = new List<Revista>();
            IList<Revista> filtrados = new List<Revista>();
            try
            {
                string path = Directory.GetCurrentDirectory() + "\\Sample.txt";

                if (File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        while (sr.Peek() >= 0)
                        {
                            string str;
                            string[] strArray;
                            str = sr.ReadLine();

                            strArray = str.Split(';');
                            string nome = strArray[0];

                            
                            int edicao = int.Parse(strArray[1]);
                            DateTime dataCriacao = DateTime.Parse(strArray[2]);
                            double valor = double.Parse(strArray[3]);
                            bool disponivel = (strArray[4] == "True" ? true : false);
                            Revista revista = new Revista(nome, edicao, dataCriacao, valor, disponivel);

                            revistas.Add(revista);
                        }
                        sr.Close();
                    }



                    int i = 0;
                    for (int index = revistas.Count - 1; index >= 0; index--)
                    {
                        i++;

                        filtrados.Add(revistas[index]);

                        if (i == 5)
                        {
                            break;
                        }
                    }
                }
                return filtrados;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return filtrados;
            }
        }

    }
}
