using UnityEngine;
using System.Collections.Generic;
using Codonbyte.SpaceBilliards.Arena.States;
using Codonbyte.SpaceBilliards.Arena;
using System.Linq;

namespace Codonbyte.SpaceBilliards.Debugging
{
    public class PocketDeathBallOnBreak : MonoBehaviour
    {
        [SerializeField]
        private StateMachine stateMachine;

        void Start()
        {
            stateMachine.OnCurrentStateChanged += (o, e) =>
            {
                if (stateMachine.Current.GetType() == typeof(TakeShotGameState))
                {
                    if (!stateMachine.Mode.IsBreak) return;
                    var deathBall = (from ball in stateMachine.Mode.AllBallsInPlay
                                     where ball.IsDeathBall
                                     select ball).First();
                    var pocket = stateMachine.Mode.Pockets.First();
                    deathBall.transform.position = pocket.transform.position;
                }
            };
        }
    } 
}
