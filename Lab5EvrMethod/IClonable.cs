using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5EvrMethod
{
    internal interface ICloneable
    {
        Individual CloneIndividualNumber();
        Individual CloneIndividualFull();
    }
}
