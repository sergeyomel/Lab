using System;
using System.Collections.Generic;

namespace lab1Automat
{
    class Program
    {
        static void Main(string[] args)
        {
            var tOne = new TerminalSymbol("a");
            var tTwo = new TerminalSymbol("b");
            var tThree = new TerminalSymbol("c");
            var lTermSymb = new List<TerminalSymbol>() { tOne, tTwo, tThree};

            var ntOne = new NonTerminalSymbol("<Z>");
            var ntTwo = new NonTerminalSymbol("<A>");
            var ntThree = new NonTerminalSymbol("<B>");
            var ntFour = new NonTerminalSymbol("<C>");
            var lNoTermSymb = new List<NonTerminalSymbol>() { ntOne, ntTwo, ntThree, ntFour};

            var ex1 = new SymbolList(new List<Symbol>()).Add(ntTwo).Add(ntThree).Add(ntFour);
            var ex2 = new SymbolList(new List<Symbol>()).Add(tOne);
            var ex3 = new SymbolList(new List<Symbol>()).Add(tTwo);
            var ex4 = new SymbolList(new List<Symbol>()).Add(tThree);

            var dRules = new List<(Symbol, SymbolList)>
            {
                (ntOne, ex1 ),
                (ntTwo, ex2 ),

                (ntThree, ex3 ),
                (ntFour, ex4 ),
            };

            var automat = new Automat
                (
                    ntOne,
                    lTermSymb,
                    lNoTermSymb,
                    dRules
                );

            automat.GenerateAutomatExpression();

        }
    }
}
