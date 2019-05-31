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

            GeneratePassword myGeneratePassword = new GeneratePassword(_myPhrase, _mySite);
            myGeneratePassword.WriteFinal();

            Console.WriteLine("Press Anykey to quit");
            Console.ReadLine();
        }

        public class GeneratePassword
        {
            private PhraseProcessed _phrase;
            private PhraseProcessed _site;
            private StringBuilder final = new StringBuilder("");

            public GeneratePassword(PhraseProcessed phrase, PhraseProcessed site)
            {
                _phrase = phrase;
                _site = site;

                uint offset_number = 0;
                uint offset_letter = 0;

                for (int i = 0; i < _site._base.Length; i++)
                {
                    if (Char.IsLetter(site._base[i]))
                    {
                        offset_letter += _site._blendedSymboleList.Symbole2Value(site._base[i]) - _phrase._blendedSymboleList.Symbole2Value(site._base[i]);
                    }
                    else if (Char.IsNumber(site._base[i]))
                    {
                        offset_number += _site._blendedSymboleList.Symbole2Value(site._base[i]) - _phrase._blendedSymboleList.Symbole2Value(site._base[i]);
                    }
                }

                for (int i = 0; i < phrase._base.Length; i++)
                {
                    if (Char.IsLetter(phrase._base[i]))
                    {
                        uint valueOffseted = Constante.LetterSymbole.Symbole2Value(phrase._base[i]);
                        valueOffseted += offset_letter;
                        if (valueOffseted >= Constante.LetterSymbole.Length())
                            valueOffseted -= Constante.LetterSymbole.Length();
                        char charOffseted = Constante.LetterSymbole.Value2Symbole(valueOffseted);
                        final.Append(charOffseted);
                    }
                    else if (Char.IsNumber(phrase._base[i]))
                    {
                        uint to_int = (uint)Char.GetNumericValue(phrase._base[i]);
                        to_int += offset_number;
                        if (to_int >= Constante.FigureSymbole.Length())
                            to_int -= (uint) Constante.FigureSymbole.Length();
                        char charOffseted = Constante.FigureSymbole.Value2Symbole(to_int);
                        final.Append(charOffseted);
                    }
                }

                //uint newSeed = 0;
                //for (int i = 0; i < ((phrase._lenght+site._lenght)%phrase._blendedSymboleList.List.Length); i++)
                //{
                 //   newSeed += (uint)(phrase._blendedSymboleList.List[i] + site._blendedSymboleList.List[i]);
                //}

                uint newSeed = phrase._weightBlended + phrase._weightOriginal + site._weightBlended + site._weightOriginal;

                Chaos newChaos = new Chaos(newSeed);

                ValuedSymboleList last = new ValuedSymboleList(final.ToString());
                last.Randomize(newChaos);
                final = new StringBuilder(last.List.ToString());
            }

            public void WriteFinal()
            {
                Console.WriteLine("");
                Console.WriteLine("------------------------");
                Console.WriteLine("Final Password : " + final);
            }
        }
    }
}