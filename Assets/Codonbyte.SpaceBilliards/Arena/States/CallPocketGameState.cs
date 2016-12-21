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
using Codonbyte.SpaceBilliards.Arena.GamePieces;

namespace Codonbyte.SpaceBilliards.Arena.States
{
    /// <summary>
    /// Shows the pocket-selection UI to the player who's turn it is. 
    /// </summary>
    public class CallPocketGameState : GameState
    {
        public override void OnStateEnter(GameState previous)
        {
            callPocketUI.SetActive(true);
        }

        public override void OnStateExit()
        {
            callPocketUI.SetActive(false);
        }

        public void CallPocket(int pocketIndex)
        {
            StateMachine.Mode.CalledPocket = pocketIndex;
        }

        public void GoToNextState()
        {
            StateMachine.Current = nextState;
        }

        [SerializeField]
        private GameObject callPocketUI;

        [SerializeField]
        [SiblingGameState]
        private GameState nextState;
    }
}
