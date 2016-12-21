using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Codonbyte;
using Codonbyte.SpaceBilliards.GameLogic;
using Codonbyte.SpaceBilliards.Arena;
using Codonbyte.SpaceBilliards.Arena.GamePieces;

namespace Codonbyte.SpaceBilliards.Arena.States
{
    public class PlayerScratchedGameState : GameState
    {
        public override void OnStateEnter(GameState previous)
        {
            StartCoroutine(EnterStateCoRoutine(previous));
        }

        private IEnumerator<YieldInstruction> EnterStateCoRoutine(GameState previous)
        {
            yield return new WaitForSeconds(2f);
            cueBall.gameObject.SetActive(false);
            ballPlacementRig.SetActive(true);
            placerController.SetActive(true);
            BallPlacer.transform.position = placerStartPoint.position;
        }

        public override void OnStateExit()
        {
            ballPlacementRig.SetActive(false);
            placerController.SetActive(false);
            cueBall.gameObject.SetActive(true);
        }

        public Vector3 PlacementPosition
        {
            get { return BallPlacer.transform.position; }
            set { BallPlacer.transform.position = value; }
        }

        /// <summary>
        /// Places the cueball as close to the current PlacementPosition as possible.
        /// </summary>
        public void PlaceCueBall()
        {
            StartCoroutine(PlaceCueBallCoRoutine());
        }

        private IEnumerator<object> PlaceCueBallCoRoutine()
        {
            yield return new WaitForEndOfFrame();
            cueBall.transform.position = PlacementPosition;
            cueBall.velocity = Vector3.zero;
            cueBall.angularVelocity = Vector3.zero;
            StateMachine.Current = nextState;
        }

        [Tooltip("Parent of the entire ball-placing system, except the controller.")]
        [SerializeField]
        private GameObject ballPlacementRig;

        [SerializeField]
        private BallPlacerScript _ballPlacer;
        private BallPlacerScript BallPlacer
        {
            get { return _ballPlacer; }
        }

        [SerializeField]
        private Transform placerStartPoint;

        [SerializeField]
        private GameObject placerController;

        [SerializeField]
        private Rigidbody cueBall;

        [SerializeField]
        [SiblingGameState]
        private GameState nextState;
    }
}
