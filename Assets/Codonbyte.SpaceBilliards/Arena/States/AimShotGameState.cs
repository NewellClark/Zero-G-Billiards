using UnityEngine;
using System.Collections.Generic;
using System;
using Codonbyte.SpaceBilliards;

namespace Codonbyte.SpaceBilliards.Arena.States
{
    /// <summary>
    /// Player is setting the direction of their shot.
    /// </summary>
    [Serializable]
    public class AimShotGameState : GameState
    {
        [SerializeField]
        private GameObject submitTrajectoryUIPanel;

        [SerializeField]
        [SiblingGameState]
        [Tooltip("The GameState to transition to once the player has finished aiming their shot.")]
        private GameState nextState;

        [SerializeField]
        private UnifiedICueRig cueRig;

        public override void OnStateEnter(GameState previous)
        {
            cueRig.Result.Activate();
            submitTrajectoryUIPanel.SetActive(true);
        }

        public override void OnStateExit()
        {
            cueRig.Result.Deactivate();
            submitTrajectoryUIPanel.SetActive(false);
        }

        public void NextState()
        {
            StateMachine.Current = nextState;
        }
    } 
}
