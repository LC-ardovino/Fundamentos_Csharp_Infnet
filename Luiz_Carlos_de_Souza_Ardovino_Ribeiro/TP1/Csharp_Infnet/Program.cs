Console.WriteLine("Informe sua idade:");

//Palavras chaves sao sempre minusculas
//leitura de dados no console
var idade = Console.ReadLine();

byte idade1 = 95;

int idade2 = 96;

//convercoes implicitas int
int Idade1 = 25;
long Idade2 = Idade1;
float Idade3 = Idade1;
decimal Idade4 = Idade1;

//conversoes implicitas long
long qtdeFilhoes1 = 4;
float qtdeFilhoes2 = qtdeFilhoes1;
double qtdeFilhoes3 = qtdeFilhoes1;
decimal qtdeFilhoes4 = qtdeFilhoes1;

//conversoes explicitas long => int
long IDade1 = 25;
int IDade2 = (int)IDade1;

//conversoes explicitas long => int
float salario1 = 4_000_000;
long salario2 = (long)salario1;

//conversoes explicitas long => int
double imposto1 = 2_500;
float imposto2 = (float)imposto1;

//Obtendo a idade do usuario
Console.WriteLine("Informe a sua idade:");
var idadeInformada = Console.ReadLine();

//Conversao com classe convert
var IDADE1 = Convert.ToInt32(idadeInformada);

//Conversao com parse
var IDADE2 = int.Parse(idadeInformada);

//Conversao com try parse
var conversao = int.TryParse(idadeInformada, out var idade3);
if (!conversao)
    Console.WriteLine("Numero invalido");

