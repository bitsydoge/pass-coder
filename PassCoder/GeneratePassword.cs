using System;
using System.Text;

namespace PassCoder
{
    public partial class CLI
    {
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
                    if (char.IsLetter(phrase._base[i]))
                    {
                        uint valueOffseted = Constant.LetterSymbole.Symbole2Value(phrase._base[i]);
                        valueOffseted += offset_letter;
                        if (valueOffseted >= Constant.LetterSymbole.Length())
                            valueOffseted -= Constant.LetterSymbole.Length();
                        char charOffseted = Constant.LetterSymbole.Value2Symbole(valueOffseted);
                        final.Append(charOffseted);
                    }
                    else if (char.IsNumber(phrase._base[i]))
                    {
                        uint to_int = (uint)Char.GetNumericValue(phrase._base[i]);
                        to_int += offset_number;
                        if (to_int >= Constant.FigureSymbole.Length())
                            to_int -= (uint)Constant.FigureSymbole.Length();
                        char charOffseted = Constant.FigureSymbole.Value2Symbole(to_int);
                        final.Append(charOffseted);
                    }
                }

                // 
                uint newSeed = phrase._weightBlended + phrase._weightOriginal + site._weightBlended + site._weightOriginal;
                RandGen newChaos = new RandGen(newSeed);

                ValuedSymboleList post_last = new ValuedSymboleList(final.ToString());
                post_last.Randomize(newChaos);
                final = new StringBuilder(post_last.List.ToString());

                // Check Size and compensate with adding symbole
                UniqueSymbolePool poolSpecialSymbole = new UniqueSymbolePool(Constant.SpecialSymbole);
                int number_special_to_add = 4;
                if (final.Length <= 14)
                {
                    number_special_to_add += (14 - final.Length);
                }

                for (int i = 0; i < number_special_to_add; i++)
                {
                    final.Append(Constant.SpecialSymbole.Value2Symbole(newChaos.Rand() % Constant.SpecialSymbole.Length()));
                }

                ValuedSymboleList last = new ValuedSymboleList(final.ToString());
                last.Randomize(newChaos);
                final = new StringBuilder(last.List.ToString());
            }

            public void WriteFinal()
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine("|     Password Generated   |");
                Console.WriteLine("----------------------------");
                Console.WriteLine();
                Console.WriteLine(final);
                Console.WriteLine();
                Console.WriteLine("----------------------------");
                Console.WriteLine("|  Press Anykey to quit    |");
                Console.WriteLine("----------------------------");
                Console.ReadLine();
            }
        }
    }
}