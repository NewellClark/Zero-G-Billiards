using UnityEngine;
using System.Collections.Generic;
using System;
using Codonbyte.Development;
using Codonbyte.SpaceBilliards.GameLogic;
using Codonbyte.SpaceBilliards;
using Codonbyte.SpaceBilliards.Arena.States;

namespace Codonbyte.SpaceBilliards.Arena
{
    public class BadBreakHandler : BilliardEventHandlingScript
    {
        protected override void RegisterBilliardEventHandlers(InitializationData data)
        {
            Mode.OnBadBreak += HandleBadBreak;
        }

        protected override void UnregisterBilliardEventHandlers(InitializationData data)
        {
            Mode.OnBadBreak -= HandleBadBreak;
        }

        private void HandleBadBreak(object sender, BilliardPlayEventArgs e)
        {
            turnEndState.SetStartingStateForNextRound(badBreakState, NextStatePriority.BadBreak);
            MessageWall.AddItem("Death ball pocketed on break! Bad break!", messageLifeInSeconds);
        }

        [SerializeField] 
        private BadBreakGameState badBreakState;

        [SerializeField]
        private TurnEndGameState turnEndState;

        [SerializeField]
        private UnifiedTimedStringQueue _messageWall;
        private ITimedQueue<string> MessageWall { get { return _messageWall.Result; } }

        [SerializeField]
        private float messageLifeInSeconds = 8;
    }
}
