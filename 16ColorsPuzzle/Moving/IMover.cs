using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16ColorsPuzzle.Moving
{
    interface IMover<T>
    {
        void Move(T obj);
    }
}
