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

namespace Codonbyte.SpaceBilliards.Arena.States
{
    public class WaitForBallsToSettleGameState : GameState
    {
        public override void OnStateEnter(GameState previous)
        {
            StartCoroutine(OnStateEnterCoRoutine(MaxSettledSpeed));
        }

        public override void OnStateExit()
        {
            StopAllCoroutines();
        }

        private bool HaveBallsSettled(IEnumerable<Rigidbody> bodies, float maxSpeed)
        {
            foreach (var body in bodies)
            {
                if (!body.gameObject.activeInHierarchy) continue;
                if (body.velocity.magnitude > maxSpeed) return false;
            }
            return true;
        }

        private IEnumerator<YieldInstruction> WaitForBodiesToSettle(IEnumerable<Rigidbody> bodies, float maxSpeed, int testItterations)
        {
            //var bodies = from ball in StateMachine.Mode.AllBallsInPlay select ball.GetComponent<Rigidbody>();

            for (int n = 0; n < testItterations; n++)
            {
                yield return new WaitForFixedUpdate();
                while (!HaveBallsSettled(bodies, maxSpeed)) yield return new WaitForFixedUpdate();
            }
        }

        private IEnumerator<YieldInstruction> OnStateEnterCoRoutine(float maxSpeed)
        {
            var bodies = from ball in StateMachine.Mode.AllBallsInPlay select ball.GetComponent<Rigidbody>();

            yield return StartCoroutine(WaitForBodiesToSettle(bodies, maxSpeed, 3));

            foreach (var body in bodies) body.velocity = Vector3.zero;

            StateMachine.Current = NextState;
        }

        [SerializeField]
        [SiblingGameState]
        private GameState _nextState;
        public GameState NextState
        {
            get { return _nextState; }
            set
            {
                _nextState = value;
            }
        }
        
        /*
        [SerializeField]
        private RootGameModeHolder gameModeHelper;
        public GameMode Mode
        {
            get { return gameModeHelper.Mode; }
        }*/

        [SerializeField]
        private float _maxSettledSpeed = .065f;
        /// <summary>
        /// Gets or sets the maximum speed a ball can be moving for it to be considered to have settled.
        /// </summary>
        public float MaxSettledSpeed
        {
            get { return _maxSettledSpeed; }
            set
            {
                _maxSettledSpeed = value;
            }
        }

        //[SerializeField]
        //private GameObject cueModelExcludingCameraRig;
    }
}
