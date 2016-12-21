using UnityEngine;
using System.Collections;
using System;
using Codonbyte.SpaceBilliards;
using Codonbyte.SpaceBilliards.GameLogic;
using Codonbyte;
using Codonbyte.SpaceBilliards.Arena.States;
using Codonbyte.UI;

namespace Codonbyte.SpaceBilliards.Arena
{
    public class PlayerScratchedHandler : BilliardEventHandlingScript
    {
        protected override void RegisterBilliardEventHandlers(InitializationData data)
        {
            Mode.OnPlayerScratch += HandlePlayerScratched;
        }

        protected override void UnregisterBilliardEventHandlers(InitializationData data)
        {
            Mode.OnPlayerScratch -= HandlePlayerScratched;
        }

        private void HandlePlayerScratched(object sender, BilliardPlayEventArgs e)
        {
            messageWall.Result.AddItem(e.Player.Name + " Scratched! ", 8);
            turnEndState.SetStartingStateForNextRound(playerScratchedState, NextStatePriority.PlayerScratched);
        }

        [SerializeField]
        private TurnEndGameState turnEndState;

        [SerializeField]
        private UnifiedTimedStringQueue messageWall;

        [SerializeField]
        private PlayerScratchedGameState playerScratchedState;
    }
}
