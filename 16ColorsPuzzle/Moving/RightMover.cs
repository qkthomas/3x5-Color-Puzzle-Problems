using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _16ColorsPuzzle.Data;

namespace _16ColorsPuzzle.Moving
{
    class RightMover : IMover<State>
    {
        public State Move(State current_state)
        {
            State new_state = current_state.NewShallowClone();
            MoveController.MoveEmptySpaceLeft(current_state);
            return new_state;
        }
    }
}
