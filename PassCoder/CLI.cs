using System;
using System.Text.RegularExpressions;

namespace PassCoder
{
    public partial class CLI
    {
        private PhraseProcessed _myPhrase;
        private PhraseProcessed _mySite;

        public CLI(bool debug)
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine("|    Enter Pass & Tag      |");
            Console.WriteLine("----------------------------");
            Console.Write("PASS : ");
            _myPhrase = new PhraseProcessed(Console.ReadLine(), debug);

            Console.Write("TAG  : ");
            _mySite = new PhraseProcessed(Console.ReadLine(), debug);

            GeneratePassword myGeneratePassword = new GeneratePassword(_myPhrase, _mySite);
            myGeneratePassword.WriteFinal();
        }
    }
}