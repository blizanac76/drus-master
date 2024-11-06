// See https://aka.ms/new-console-template for more information

using System.Reflection.Emit;

Console.WriteLine("Unesite ime");
labelaIme:
string? ime = Console.ReadLine();
if (ime == null)
{
    Console.WriteLine("unestie Ime u pritisni ENTER!");
    goto labelaIme;
}

Console.WriteLine("Unesite Grad");
labelaGrad:
string? grad =Console.ReadLine();

Console.WriteLine("Unesite Vase godine");
labelaGodine:
if (!Int32.TryParse(Console.ReadLine(), out int godine))
{
    Console.WriteLine("unestie Godine u ispravnom celobrojnom formatu i pritisni ENTER!");
    goto labelaGodine;
}

Console.WriteLine("Unesite Broj telefona");
labelaBrojTel:
string? brTel = Console.ReadLine();
if (brTel == null)
{
    Console.WriteLine("unestie broj Telefona i pritisni ENTER!");
    goto labelaBrojTel;
}

Console.WriteLine($"Ime: {ime} Grad: {grad} Godine: {godine} Broj Telefona {brTel}");










Console.ReadKey();