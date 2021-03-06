﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _16ColorsPuzzle.Data;

namespace _16ColorsPuzzle.Moving
{
    class DownMover : IMover<State>
    {
        State IMover<State>.Move(State current_state)
        {
            State new_state = current_state.NewShallowClone();
            MoveController.MoveEmptySpaceUp(new_state);
            return new_state;
        }

        bool IMover<State>.CanMove(State current_state)
        {
            return MoveController.EmptySpaceUpMovable(current_state);
        }
    }
}
