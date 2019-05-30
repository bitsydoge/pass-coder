using System;
using System.Text;
using System.Text.RegularExpressions;

namespace PassCoder
{

    public class Core
    {
        private PhraseProcessed _myPhrase;
        private PhraseProcessed _mySite;

        public Core()
        {
            Console.Write("Enter your Pass Phrase : ");
            _myPhrase = new PhraseProcessed(Console.ReadLine());

            Console.Write("Enter your Site Name   : ");
            _mySite = new PhraseProcessed(Console.ReadLine());
        }
    }
}