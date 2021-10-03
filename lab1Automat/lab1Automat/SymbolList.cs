using System;
using System.Collections.Generic;
using System.Text;

namespace lab1Automat
{
    class SymbolList
    {
        private List<Symbol> ListSymbol;

        public List<Symbol> GetLSymbol() => ListSymbol;

        public SymbolList(List<Symbol> lListSymb)
        {
            ListSymbol = lListSymb;
        }

        public SymbolList Add(Symbol symb)
        {
            ListSymbol.Add(symb);
            return this;
        }

        public LinkedList<Symbol> GetListSymbol() => new LinkedList<Symbol>(ListSymbol);

        public bool FindElementInLinkList(Symbol symb)
        {
            foreach (var item in ListSymbol)
                if (item.ToString() == symb.ToString())
                    return true;
            return false;
        }

        public SymbolList ReplaceSymbol(Symbol symb, SymbolList lLinkSymb)
        {
            var bufferLinkList = new List<Symbol>();
            foreach(var item in ListSymbol)
            {
                if (item.ToString() != symb.ToString())
                    bufferLinkList.Add(item);
                else
                    foreach (var subItem in lLinkSymb.ListSymbol)
                        bufferLinkList.Add(subItem);
            }
            ListSymbol = bufferLinkList;
            return new SymbolList(ListSymbol);
        }

        public bool IsWord()
        {
            foreach (var symbol in ListSymbol)
                if (symbol.GetType() != typeof(TerminalSymbol))
                    return false;
            return true;
        }

        public override string ToString()
        {
            var str = new StringBuilder("");
            foreach (var item in ListSymbol)
                str.Append(item);
            return str.ToString();
        }

    }
}
