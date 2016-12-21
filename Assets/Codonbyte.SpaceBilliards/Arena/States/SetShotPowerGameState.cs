using UnityEngine;
using System.Collections;
using System;
using Codonbyte.SpaceBilliards.Arena.GamePieces;

namespace Codonbyte.SpaceBilliards.Arena.States
{
	/// <summary>
	/// Player is setting the power of their shot.
	/// </summary>
	[Serializable]
	public class SetShotPowerGameState : GameState
	{
		public override void OnStateEnter(GameState previous)
		{
			this.previous = previous;
			firingController.SetActive(true);
			//cueModelExcludingCamera.SetActive(true);
			cueRig.Result.FollowCueBall = cueRig.Result.ModelVisible = true;
		}

		public override void OnStateExit()
		{
			firingController.SetActive(false);
			//cueModelExcludingCamera.SetActive(false);
			cueRig.Result.FollowCueBall = cueRig.Result.ModelVisible = false;
		}

		private GameState previous;
#pragma warning disable 649
		[SerializeField]
		private BilliardCueScript cue;

		[SerializeField]
		private UnifiedICueRig cueRig;
		//private GameObject cueModelExcludingCamera;

		[SerializeField]
		private GameObject firingController;

		[SiblingGameState]
		[SerializeField]
		private GameState nextState;
#pragma warning restore 649

		public void PreviousState()
		{
			StateMachine.Current = previous;
		}

		public void NextState()
		{
			StateMachine.Current = nextState;
		}

		public void SetShotPower(float normalizedShotPower)
		{
			NormalizedShotPower = normalizedShotPower;
		}
		
		public float NormalizedShotPower { get; set; }
	} 
}
