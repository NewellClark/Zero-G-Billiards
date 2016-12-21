using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using Codonbyte.SpaceBilliards.GameLogic;

namespace Codonbyte.SpaceBilliards.Arena.States
{
    [Serializable]
    public partial class StateMachine : MonoBehaviour
    {
        [SerializeField]
        [SiblingGameState]
        private GameState _current;
        public void ChangeState(GameState newState)
        {
            if (!GameStates.Contains(newState) && newState != null)
            {
                throw new InvalidOperationException("You can only set Current property to a GameState instance that is attached to the StateMachine object.");
            }

            if (!Application.isPlaying)
            {
                _current = newState;
                return;
            }

            var previous = _current;
            if (previous != null) previous.OnStateExit();
            _current = newState;
            if (_current != null) _current.OnStateEnter(previous);
            if (OnCurrentStateChanged != null) OnCurrentStateChanged(this, new StateChangedEventArgs(previous, _current));
        }

        [SerializeField]
        private GameModeHolder modeHolder;
        public GameMode Mode
        {
            get { return modeHolder.Mode; }
        }

        public GameState Current
        {
            get { return _current; }
            set
            {
                ChangeState(value);
            }
        }

        public event EventHandler<StateChangedEventArgs> OnCurrentStateChanged;

        /// <summary>
        /// Gets a read-only collection of all the GameStates available to the current GameStateMachine.
        /// </summary>
        public ICollection<GameState> GameStates
        {
            get
            {
                return new List<GameState>(GetComponents<GameState>()).AsReadOnly();
            }
        }

        void Start()
        {
            ChangeState(Current);
        }
    }
}
