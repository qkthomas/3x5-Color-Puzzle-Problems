using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _16ColorsPuzzle.Data;

namespace _16ColorsPuzzle.Moving
{
    class UpMover : IMover<State>
    {
        State Move(State current_state)
        {
            State new_state = current_state.NewShallowClone();
            MoveController.MoveEmptySpaceDown(new_state);
            return new_state;
        }
    }
}
