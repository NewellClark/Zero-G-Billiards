using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Codonbyte;
using Codonbyte.Development;
using Codonbyte.SpaceBilliards.GameLogic;
using System.ComponentModel;

namespace Codonbyte.SpaceBilliards.Arena
{
    /// <summary>
    /// Stores a GameMode object so that it may be used by other scripts.
    /// </summary>
    /*public abstract class GameModeHolder : MonoBehaviour
    {
        public abstract GameMode Mode { get; }

        /// <summary>
        /// Gets the GameModeHolder that is the source of the GameMode for the current GameModeHolder.
        /// Returns the current component if the current GameModeHolder isn't getting its GameMode from another GameModeHolder.
        /// </summary>
        internal abstract GameModeHolder Root { get; }

        
    }*/

    public abstract class GameModeHolder : MonoBehaviour
    {
        public abstract GameMode Mode { get; }

        public abstract event EventHandler<PropertyChangedEventArgs<GameMode>> OnGameModeChanged;

        internal abstract GameModeHolder Root { get; }
    }
}
