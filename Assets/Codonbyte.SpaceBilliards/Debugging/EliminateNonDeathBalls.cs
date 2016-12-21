using UnityEngine;
using System.Collections;
using Codonbyte.SpaceBilliards;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Codonbyte.SpaceBilliards.Debugging
{
    public class EliminateNonDeathBalls : MonoBehaviour
    {
        void Update()
        {
            //  Do nothing to get check box.
        }

        void Start()
        {
            EliminateBalls();
        }

        void OnEnable()
        {
            EliminateBalls();
        }

        private void EliminateBalls()
        {
            var balls = from ball in FindObjectsOfType<BilliardBall>()
                        where ball.BallType == BallType.Object && !ball.IsDeathBall
                        select ball;

            foreach (var ball in balls) ball.gameObject.SetActive(false);
        }
    } 
}
