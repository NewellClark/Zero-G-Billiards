using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Codonbyte.SpaceBilliards.GameLogic;

namespace Codonbyte.SpaceBilliards.Arena
{
    /// <summary>
    /// Disables any ball that gets pocketed.
    /// </summary>
    public class DisablePocketedBalls : BilliardEventHandlingScript
    {
        protected override void RegisterBilliardEventHandlers(InitializationData data)
        {
            Mode.OnBallPocketed += HandleBallPocketed;
        }

        protected override void UnregisterBilliardEventHandlers(InitializationData data)
        {
            Mode.OnBallPocketed -= HandleBallPocketed;
        }

        private void HandleBallPocketed(object sender, BallPocketedEventArgs e)
        {

            e.Ball.gameObject.SetActive(false);
        }
    }
}
