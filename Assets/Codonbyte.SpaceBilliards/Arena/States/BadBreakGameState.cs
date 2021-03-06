﻿using System;
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
	public class BadBreakGameState : GameState
	{
		public override void OnStateEnter(GameState previous)
		{
			StartCoroutine(EntranceCoRoutine(previous));
		}

		public override void OnStateExit()
		{
			
		}

		private IEnumerator<YieldInstruction> EntranceCoRoutine(GameState previous)
		{
			MessageWall.AddItem("Re-break!", restartAfterSeconds);
			yield return new WaitForSeconds(restartAfterSeconds);
			//Application.LoadLevel(Application.loadedLevel);
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

#pragma warning disable 649
		[SerializeField]
		private UnifiedTimedStringQueue _messageWall;
		private ITimedQueue<string> MessageWall { get { return _messageWall.Result; } }
#pragma warning restore 649

		[SerializeField]
		private float restartAfterSeconds = 1;
	}
}
