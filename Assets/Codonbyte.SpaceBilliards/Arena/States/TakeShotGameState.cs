using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using Codonbyte;
using Codonbyte.SpaceBilliards.Arena.GamePieces;

namespace Codonbyte.SpaceBilliards.Arena.States
{
    [Serializable]
    public class TakeShotGameState : GameState
    {
        public override void OnStateEnter(GameState previous)
        {
            StartCoroutine(TakeShotCoRoutine(setShotPowerGameState.NormalizedShotPower));
        }

        public override void OnStateExit()
        {
            //if (cueModelExcludingCamera != null) cueModelExcludingCamera.SetActive(true);
            //StopAllCoroutines();
        }

        private IEnumerator TakeShotCoRoutine(float normalizedShotPower)
        {
            yield return new WaitForSeconds(.3f);

            bool successful = cue.ApplyImpulseNormalized(normalizedShotPower);
            if (!successful) Debug.Log("Shot failed for some unknown reason.");

            //yield return new WaitForSeconds(2);

            StateMachine.Current = nextState;
        }

        [SiblingGameState]
        [SerializeField]
        private GameState nextState;

        [SerializeField]
        private SetShotPowerGameState setShotPowerGameState;

        [SerializeField]
        private BilliardCueScript cue;
    }
}
