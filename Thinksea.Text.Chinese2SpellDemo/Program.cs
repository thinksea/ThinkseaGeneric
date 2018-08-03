using System;
using System.Collections.Generic;
using System.Text;

namespace Thinksea.Text.Chinese2SpellDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "中华人民共和国.国民党abcd。";
            string r = Thinksea.Text.Chinese2Spell.GetFullSpell(str, " ");
            Console.WriteLine(r);
            r = Thinksea.Text.Chinese2Spell.GetSpell(str, false, " ");
            Console.WriteLine(r);
            Console.ReadLine();
        }
    }
}
