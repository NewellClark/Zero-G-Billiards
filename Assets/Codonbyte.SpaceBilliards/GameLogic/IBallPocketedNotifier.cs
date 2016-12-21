using UnityEngine;
using System.Collections.Generic;
using System;
using Codonbyte.SpaceBilliards.Arena.Balls;

namespace Codonbyte.SpaceBilliards.GameLogic
{
    public interface IBallPocketedNotifier
    {
        event EventHandler<BallPocketedEventArgs> OnBallPocketed;
    }

    public class BallPocketedEventArgs : System.EventArgs
    {
        public BallPocketedEventArgs(BilliardBall ball, int pocketIndex)
        {
            Ball = ball;
            PocketIndex = pocketIndex;
        }
        public BilliardBall Ball { get; private set; }
        public int PocketIndex { get; private set; }
    } 
}
