using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Codonbyte;
using System.ComponentModel;
using Codonbyte.SpaceBilliards.GameLogic;
using Codonbyte.SpaceBilliards.Arena;
using Codonbyte.SpaceBilliards.Arena.GamePieces;

namespace Codonbyte.SpaceBilliards
{
    public interface ICueRig
    {
        /// <summary>
        /// Gets or sets whether the physical model that the player sees should be visible.
        /// </summary>
        bool ModelVisible { get; set; }
        /// <summary>
        /// Gets or sets whether the rig should follow the cue-ball.
        /// </summary>
        bool FollowCueBall { get; set; }

        bool AimingControllerActive { get; set; }
    }

    public interface IPocketSelectionButton
    {
        int Value { get; set; }

        event EventHandler<PocketSelectedEventArgs> OnClicked;
    }

    public interface IPocketSet : IBallPocketedNotifier, IEnumerable<BilliardPocket>
    {
        IList<BilliardPocket> Pockets { get; }

        void SetPocketHighlightState(BilliardPocket pocket, bool highlight);
    }

    public class PocketSelectedEventArgs : EventArgs
    {
        public int Value { get; private set; }
        public PocketSelectedEventArgs(int value) : base()
        {
            Value = value;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class InterfaceExtensions
    {
        public static void SetVisible(this ICueRig @this, bool visible)
        {
            @this.ModelVisible = visible;
        }

        public static void Activate(this ICueRig @this)
        {
            @this.ModelVisible = @this.FollowCueBall = @this.AimingControllerActive = true;
        }

        public static void Deactivate(this ICueRig @this)
        {
            @this.ModelVisible = @this.FollowCueBall = @this.AimingControllerActive = false;
        }

        public static void SetPocketHighlightState(this IPocketSet @this, int index, bool highlight)
        {
            @this.SetPocketHighlightState(@this.Pockets[index], highlight);
        }
    }
}
