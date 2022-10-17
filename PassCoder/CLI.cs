namespace PassCoder;

using System;

public partial class Cli
{
	public Cli(bool debug)
	{
		Console.WriteLine("----------------------------");
		Console.WriteLine("|    Enter Pass & Tag      |");
		Console.WriteLine("----------------------------");
		Console.Write("PASS : ");
		var myPhrase = new PhraseProcessed(Console.ReadLine(), debug);

		Console.Write("TAG  : ");
		var mySite = new PhraseProcessed(Console.ReadLine(), debug);

		var myGeneratePassword = new GeneratePassword(myPhrase, mySite);
		myGeneratePassword.WriteFinal();
	}
}