using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Codonbyte;
using Codonbyte.Development;
using Codonbyte.SpaceBilliards.GameLogic;
using Codonbyte.SpaceBilliards.Arena.GamePieces;
using System;

namespace Codonbyte.SpaceBilliards.Arena
{
	/// <summary>
	/// The GameModeHolder that is responsible for creating the GameMode object. This will be the source
	/// for all other GameModeHolder objects.
	/// </summary>
	[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
	public class RootGameModeHolder : GameModeHolder
	{
		private GameMode _modeField;
		private GameMode modeInternalProperty
		{
			get { return _modeField; }
			set
			{
				var old = _modeField;
				_modeField = value;
				if (OnGameModeChanged == null) return;
				OnGameModeChanged(this, new PropertyChangedEventArgs<GameMode>(old, _modeField));
			}
		}
		public override GameMode Mode
		{
			get { return modeInternalProperty; }
		}

		public override event EventHandler<PropertyChangedEventArgs<GameMode>> OnGameModeChanged;

		/// <summary>
		/// Returns the current GameModeHolder.
		/// </summary>
		internal override GameModeHolder Root
		{
			get { return this; }
		}

		public void StartTwoPlayerGame()
		{
			EnforceNoStartingWhileGameInProgress();
			modeInternalProperty = new DoublePlayerMode(GetAllBalls(), GetAllPockets());
		}

		public void StartOnePlayerGame()
		{
			EnforceNoStartingWhileGameInProgress();
			modeInternalProperty = new SinglePlayerMode(GetAllBalls(), GetAllPockets());
		}

		private bool gameStarted = false;
		private void EnforceNoStartingWhileGameInProgress()
		{
			if (gameStarted)
			{
				throw new System.InvalidOperationException("You cannot start a new game from the GameModeHelper while a game is already in progress. " +
					"To start a new game, reload the scene using Application.LoadLevel(Application.LoadedLevel);");
			}
		}

		[SerializeField]
		private GameObject arenaOutermost = null;

		private IEnumerable<BilliardBall> GetAllBalls()
		{
			return arenaOutermost.GetComponentsInChildren<BilliardBall>();
		}

		private IEnumerable<BilliardPocket> GetAllPockets()
		{
			return arenaOutermost.GetComponentsInChildren<BilliardPocket>();
		}
	} 
}
