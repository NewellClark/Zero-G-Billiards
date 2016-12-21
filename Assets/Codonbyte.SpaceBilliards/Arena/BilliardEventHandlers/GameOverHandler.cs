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
	public class GameOverHandler : BilliardEventHandlingScript
	{
		protected override void RegisterBilliardEventHandlers(InitializationData data)
		{
			Mode.OnPlayerIllegallyPocketDeathBall += HandleEarlyDeathBall;
			Mode.OnPlayerScratchOnDeathBall += HandleDeathBallScratch;
			Mode.OnPlayerWinByClearingArena += HandleClearedArena;
			Mode.OnGameOver += HandleGameOver;
		}

		protected override void UnregisterBilliardEventHandlers(InitializationData data)
		{
			Mode.OnPlayerIllegallyPocketDeathBall -= HandleEarlyDeathBall;
			Mode.OnPlayerScratchOnDeathBall -= HandleDeathBallScratch;
			Mode.OnPlayerWinByClearingArena -= HandleClearedArena;
			Mode.OnGameOver -= HandleGameOver;
		}

		private void HandleEarlyDeathBall(object sender, BilliardPlayEventArgs e)
		{
			MessageWall.AddItem(GetEarlyDeathBallMessage(e), messageLifeInSeconds);
			gameOverState.GameOverReason = GetEarlyDeathBallLongerExplanation(e);
		}

		private string GetEarlyDeathBallMessage(BilliardPlayEventArgs e)
		{
			if (e.Player.BallAlliance == e.Ball.BallAlliance || e.Player.BallAlliance == null)
				return e.Player.Name + " sinks death ball too early!\n" + e.Player.Name + " auto-loses!";
			return e.Player.Name + " pocketed opponent's death ball!\n" + e.Player.Name + " auto-loses!";
		}

		private string GetEarlyDeathBallLongerExplanation(BilliardPlayEventArgs e)
		{
			if (e.Player.BallAlliance == e.Ball.BallAlliance || e.Player.BallAlliance == null)
				return e.Player.Name + " pocketed their \"death\" ball before clearing the rest of their balls, which is an auto-lose.";
			return e.Player.Name + " pocketed their opponent's death ball, which is a guaranteed auto-lose under all circumstances.";
		}

		private void HandleDeathBallScratch(object sender, BilliardPlayEventArgs e)
		{
			string message = e.Player.Name + " scratched on death ball!\n" + e.Player.Name + " auto-loses!";
			MessageWall.AddItem(message, messageLifeInSeconds);
			gameOverState.GameOverReason = e.Player.Name + " scratched while attempting to sink their death ball, which is an auto-lose.";
		}

		private void HandleClearedArena(object sender, BilliardPlayEventArgs e)
		{
			string message = e.Player.Name + " cleared their field! " + e.Player.Name + " wins!";
			MessageWall.AddItem(message, messageLifeInSeconds);
			gameOverState.GameOverReason = e.Player.Name + " won the good old-fashioned way: by pocketing all of their object balls!";
		}

		private void HandleGameOver(object sender, GameOverEventArgs e)
		{
			gameOverState.GameOverData = e;
			turnEndState.SetStartingStateForNextRound(gameOverState, NextStatePriority.BadBreak);
		}

		[SerializeField]
		private GameOverGameState gameOverState = null;

		[SerializeField]
		private TurnEndGameState turnEndState = null;

		[SerializeField]
		private float messageLifeInSeconds = 8;

		[SerializeField]
		private UnifiedTimedStringQueue _messageWall = null;
		private ITimedQueue<string> MessageWall { get { return _messageWall.Result; } }
	}
}
