using System;

namespace PassCoder
{
    // Because fuck those static shit
    class Program
    {
        static void Main(string[] args)
        {
            _ = new CLI(args.Length == 0);
        }
    }
}
