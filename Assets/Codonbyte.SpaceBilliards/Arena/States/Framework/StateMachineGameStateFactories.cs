using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace Codonbyte.SpaceBilliards.Arena.States
{
    public enum StopCoRoutineOnExitOptions { None, OnlyThisOne, All };

    public partial class StateMachine
    {
        private class CoRoutineState : GameState
        {
            public override void OnStateEnter(GameState previous)
            {
                coroutine = StartCoroutine(CoRoutineDelegate(StateMachine));
            }

            public override void OnStateExit()
            {
                switch (Options)
                {
                    case StopCoRoutineOnExitOptions.All:
                        StopAllCoroutines();
                        break;
                    case StopCoRoutineOnExitOptions.OnlyThisOne:
                        StopCoroutine(coroutine);
                        break;
                    default:
                        break;
                }

                Destroy(this);
            }

            public Func<StateMachine, IEnumerator> CoRoutineDelegate { get; set; }

            public StopCoRoutineOnExitOptions Options { get; set; }

            private Coroutine coroutine;
        }

        /// <summary>
        /// Sets the current GameState to a state that simply executes the specified co-routine.
        /// Note that the state will NOT automatically be changed after the co-routine finishes. If you want to change to a different state,
        /// you'll need to supply a co-routine that does that during the course of its operation.
        /// </summary>
        /// <param name="coRoutine">The co-routine to run. The method takes the current StateMachine as a parameter. </param>
        /// <param name="options">Options specifying which co-routines should be stopped when the state exits.</param>
        public void ExecuteCoRoutineState(Func<StateMachine, IEnumerator> coRoutine, StopCoRoutineOnExitOptions options)
        {
            var state = gameObject.AddComponent<CoRoutineState>();
            state.CoRoutineDelegate = coRoutine;
            state.Options = options;
            Current = state;
        }

        public void ExecuteCoRoutineState(Func<StateMachine, IEnumerator> coRoutine)
        {
            ExecuteCoRoutineState(coRoutine, StopCoRoutineOnExitOptions.All);
        }

        public void ExecuteCoRoutineState(Func<IEnumerator> coRoutine, StopCoRoutineOnExitOptions options)
        {
            Func<StateMachine, IEnumerator> method = (sm) => { return coRoutine(); };
            ExecuteCoRoutineState(method, options);
        }

        /// <summary>
        /// Changes the current game state to a state that executes the specified co-routine.
        /// By default, all co-routines are stopped when the current state exits.
        /// </summary>
        /// <param name="coRoutine">The co-routine to execute.</param>
        public void ExecuteCoRoutineState(Func<IEnumerator> coRoutine)
        {
            ExecuteCoRoutineState(coRoutine, StopCoRoutineOnExitOptions.All);
        }
    }

    public class StateChangedEventArgs : EventArgs
    {
        public GameState OldState { get; private set; }
        public GameState NewState { get; private set; }
        public StateChangedEventArgs(GameState old, GameState newState )
        {
            OldState = old;
            NewState = newState;
        }
    }
}
