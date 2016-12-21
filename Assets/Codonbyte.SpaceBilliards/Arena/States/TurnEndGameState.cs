using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Codonbyte;
using Codonbyte.Development;
using Codonbyte.SpaceBilliards;
using Codonbyte.SpaceBilliards.GameLogic;
using Codonbyte.SpaceBilliards.Arena;

namespace Codonbyte.SpaceBilliards.Arena.States
{
    public class TurnEndGameState : GameState
    {
        public override void OnStateEnter(GameState previous)
        {
            StartCoroutine(EnterCoRoutine(previous));
        }

        public override void OnStateExit()
        {
            _specialCaseForNextRound = new SpecialCaseState(null, NextStatePriority.Default);
            foreach (var pocket in Pockets) Pockets.SetPocketHighlightState(pocket, false);
        }

        private IEnumerator<YieldInstruction> EnterCoRoutine(GameState previous)
        {
            for (int n = 0; n < 3; n++) yield return null;

            StateMachine.Mode.TurnEnded();
            if (StateMachine.Mode.DeathBallShotAllowed(StateMachine.Mode.CurrentPlayer))
                SpecialCaseForNextRound = new SpecialCaseState(callPocketState, NextStatePriority.CallPocket);

            if (SpecialCaseForNextRound.State != null) StateMachine.Current = SpecialCaseForNextRound.State;
            else StateMachine.Current = DefaultNextState;
        }

        [SiblingGameState]
        [SerializeField]
        private GameState _defaultNextState;
        public GameState DefaultNextState
        {
            get { return _defaultNextState; }
            private set { _defaultNextState = value; }
        }

        [SiblingGameState]
        [SerializeField]
        private GameState callPocketState;

        [SerializeField]
        private UnifiedIPocketSet _pockets;
        private IPocketSet Pockets { get { return _pockets.Result; } }

        /// <summary>
        /// Specify the GameState that will be set at the beginning of next round. This only applies to the very next round, after which
        /// the current TurnEndGameState will revert to its default value.
        /// </summary>
        /// <param name="state"></param>
        public void SetStartingStateForNextRound(GameState state, NextStatePriority priority)
        {
            SpecialCaseForNextRound = new SpecialCaseState(state, priority);
        }
        private struct SpecialCaseState
        {
            public GameState State { get; private set; }
            public NextStatePriority Priority { get; private set; }
            public SpecialCaseState(GameState state, NextStatePriority priority) : this()
            {
                State = state;
                Priority = priority;
            }
        }
        private SpecialCaseState _specialCaseForNextRound;
        private SpecialCaseState SpecialCaseForNextRound
        {
            get { return _specialCaseForNextRound; }
            set
            {
                if (value.Priority >= _specialCaseForNextRound.Priority)
                    _specialCaseForNextRound = value;
            }
        }
    }

    public enum NextStatePriority { Default, CallPocket, PlayerScratched, BadBreak }
}
