using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Codonbyte.SpaceBilliards.Arena;
using Codonbyte.SpaceBilliards.Arena.GamePieces;
using Codonbyte.SpaceBilliards.Arena.States;
using Codonbyte.SpaceBilliards;

namespace Codonbyte.SpaceBilliards.Debugging
{
	public class ScratchAndThenPocketDeathBall : MonoBehaviour
	{
#pragma warning disable 649
		[SerializeField]
		private StateMachine stateMachine;
#pragma warning restore 649

		void Start()
		{
			stateMachine.OnCurrentStateChanged += (o, e) =>
			{
				if (!enabled) return;
				if (e.NewState == stateMachine.GetComponent<TakeShotGameState>())
				{
					StartCoroutine(ScratchThenDeathBall(e));

				}
			};
		}

		private IEnumerator<YieldInstruction> ScratchThenDeathBall(StateChangedEventArgs e)
		{
			var pocket = stateMachine.Mode.Pockets.First();
			stateMachine.Mode.CueBall.transform.position = pocket.transform.position;

			yield return new WaitForSeconds(Time.deltaTime);

			var deathBall = (from ball in stateMachine.Mode.AllBallsInPlay
						where ball.IsDeathBall
						select ball).First();
			deathBall.transform.position = pocket.transform.position;
		}
	} 
}
