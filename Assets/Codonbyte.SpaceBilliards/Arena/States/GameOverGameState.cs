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
using UnityEngine.SceneManagement;

namespace Codonbyte.SpaceBilliards.Arena.States
{
	public class GameOverGameState : GameState
	{
		public interface IGameOverScreen
		{
			GameObject gameObject { get; }
			string GameOverReason { get; set; }
			GameOverEventArgs GameInfo { get; set; }
		}
		[Serializable]
		private class UnifiedIGameOverScreen : IUnifiedContainer<IGameOverScreen> { }

#pragma warning disable 649
		[SerializeField]
		private UnifiedIGameOverScreen _gameOverScreen;
		private IGameOverScreen GameOverScreen
		{
			get { return _gameOverScreen.Result; }
		}
#pragma warning restore 649

		/// <summary>
		/// Gets or sets the explanation that will be displayed about why the game is over.
		/// </summary>
		public string GameOverReason
		{
			get { return GameOverScreen.GameOverReason; }
			set { GameOverScreen.GameOverReason = value; }
		}

		public GameOverEventArgs GameOverData
		{
			get { return GameOverScreen.GameInfo; }
			set { GameOverScreen.GameInfo = value; }
		}

		public override void OnStateEnter(GameState previous)
		{
			GameOverScreen.gameObject.SetActive(true);
		}

		public override void OnStateExit()
		{
			GameOverScreen.gameObject.SetActive(false);
		}

		public void NewGame()
		{
			//Application.LoadLevel(Application.loadedLevel);
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

	}
}
