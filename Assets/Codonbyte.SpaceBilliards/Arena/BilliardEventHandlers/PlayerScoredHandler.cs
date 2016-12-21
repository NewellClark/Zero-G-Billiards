using UnityEngine;
using System.Collections;
using System;
using Codonbyte.SpaceBilliards;
using Codonbyte.SpaceBilliards.GameLogic;
using Codonbyte;

namespace Codonbyte.SpaceBilliards.Arena
{
    public class PlayerScoredHandler : BilliardEventHandlingScript
    {
		[SerializeField]
		private UnifiedTimedStringQueue messageWall = null;

        [SerializeField]
        private float _messageLifeInSeconds = 3;
        public float MessageLifeInSeconds
        {
            get { return _messageLifeInSeconds; }
            set { _messageLifeInSeconds = value; }
        }

        protected override void RegisterBilliardEventHandlers(InitializationData data)
        {
            Mode.OnPlayerScored += HandlePlayerScored;
            Mode.OnPlayerScoredForEnemy += HandlePlayerScoredForOpponent;
            Mode.OnAllianceDecided += HandleAllianceDecided;
            Mode.OnTurnEnded += HandleTurnEnded;
        }

        protected override void UnregisterBilliardEventHandlers(InitializationData data)
        {
            Mode.OnPlayerScored -= HandlePlayerScored;
            Mode.OnPlayerScoredForEnemy -= HandlePlayerScoredForOpponent;
            Mode.OnAllianceDecided -= HandleAllianceDecided;
            Mode.OnTurnEnded -= HandleTurnEnded;
        }

        private void HandlePlayerScored(object sender, BilliardPlayEventArgs e)
        {
            string message = e.Player.Name + " pocketed the " + e.Ball.BallNumber + "-ball!";
            messageWall.Result.AddItem(message, MessageLifeInSeconds);
        }

        private void HandlePlayerScoredForOpponent(object sender, BilliardPlayEventArgs e)
        {
            string message = e.Player.Name + " pocketed opponent's " + e.Ball.BallNumber + "-ball!";
            messageWall.Result.AddItem(message, MessageLifeInSeconds);
        }

        private void HandleAllianceDecided(object sender, BilliardPlayEventArgs e)
        {
            string message = e.Player.Name + " is " + e.Ball.BallAlliance + "!";
            messageWall.Result.AddItem(message, MessageLifeInSeconds);
        }

        private void HandleTurnEnded(object sender, EventArgs e)
        {
            messageWall.Result.AddItem(Mode.CurrentPlayer.Name + "'s turn.", MessageLifeInSeconds);
        }
    }
}
