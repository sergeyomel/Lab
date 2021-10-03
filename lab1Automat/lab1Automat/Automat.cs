using System;
using System.Collections.Generic;
using System.Text;

namespace lab1Automat
{
    class Automat
    {
        private bool IsWord = false;

        private List<(Symbol key, SymbolList exp)> dRules;
        private Symbol currentState;

        private List<TerminalSymbol> lTermSymbol;
        private List<NonTerminalSymbol> lNonTermSymbol;

        private Queue<Symbol> qCurrentSymbol;
        private Queue<Symbol> qNextSymbol;

        private List<SymbolList> currentLevelExpress;
        private List<SymbolList> nextLevelExpress;

        public Automat(Symbol startSymbol, 
                       List<TerminalSymbol> termSymb, 
                       List<NonTerminalSymbol> nonTermSymb,
                       List<(Symbol, SymbolList)> rules)
        {
            dRules = rules;
            lTermSymbol = termSymb;
            lNonTermSymbol = nonTermSymb;

            qCurrentSymbol = new Queue<Symbol>();
            qCurrentSymbol.Enqueue(startSymbol);
            qNextSymbol = new Queue<Symbol>();

            currentLevelExpress = new List<SymbolList>();
            var lSymb = new List<Symbol>();
            lSymb.Add(startSymbol);
            var express = new SymbolList(lSymb);
            currentLevelExpress.Add(express);
            nextLevelExpress = new List<SymbolList>();
        }

        private List<SymbolList> FindExpression(Symbol cState)
        {
            var lExpression = new List<SymbolList>();
            foreach (var item in dRules)
                if (item.key.ToString() == cState.ToString())
                    lExpression.Add(item.exp);
            return lExpression;
        }

        private void InsertingUniqueSymbol(Symbol symb)
        {
            if (!qNextSymbol.Contains(symb))
                qNextSymbol.Enqueue(symb);
        }

        private void FillQueueSymbol(List<SymbolList> lSymbolList)
        {
            foreach (var lSymb in lSymbolList)
                foreach (var item in lSymb.GetLSymbol())
                    InsertingUniqueSymbol(item);
        }

        private List<SymbolList> FindExpressionForState()
        {
            var lSymbolList = new List<SymbolList>();
            foreach (var item in dRules)
                if (item.key.ToString() == currentState.ToString())
                    lSymbolList.Add(item.exp);
            return lSymbolList;
        }

        private void GeneratingExpressionsAccordingRules(List<SymbolList> lSymbList)
        {
            foreach (var expression in currentLevelExpress)
            {
                foreach (var rule in lSymbList)
                {
                    var res = expression.ReplaceSymbol(currentState, rule);
                    IsWord = res.IsWord();
                    nextLevelExpress.Add(res);
                    Console.WriteLine(String.Format("{0} -> {1}: {2}",currentState, rule, res));
                }
            }
        }

        public void GenerateAutomatExpression() 
        {
            while (!IsWord)
            {
                while (qCurrentSymbol.Count != 0)
                {
                    currentState = qCurrentSymbol.Dequeue();
                    var lSymbolList = FindExpressionForState();
                    if (lSymbolList.Count != 0)
                    {
                        FillQueueSymbol(lSymbolList);
                        GeneratingExpressionsAccordingRules(lSymbolList);
                    }
                }
                if (qNextSymbol.Count == 0)
                    return;
                currentLevelExpress = new List<SymbolList>(nextLevelExpress);
                nextLevelExpress = new List<SymbolList>();

                qCurrentSymbol = new Queue<Symbol>(qNextSymbol);
                qNextSymbol = new Queue<Symbol>();

            }
        }

    }
}
