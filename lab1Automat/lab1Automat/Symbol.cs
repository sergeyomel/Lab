using System;
using System.Collections.Generic;
using System.Text;

namespace lab1Automat
{
    class Symbol
    {
        private string Value;

        public Symbol(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
