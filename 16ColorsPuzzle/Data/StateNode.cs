using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16ColorsPuzzle.Data
{
    class StateNode
    {
        State mCurrentState;
        StateNode mParent;
        List<StateNode> mChildren = new List<StateNode>();
    }
}
