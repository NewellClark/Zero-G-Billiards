using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codonbyte.SpaceBilliards.GameLogic;
using UnityEngine;

namespace Codonbyte.SpaceBilliards.Arena
{
    /// <summary>
    /// A <code>GameModeHolder</code> that gives components on a <code>GameObject</code> access to another <code>GameModeHolder</code> 
    /// component on a different object.
    /// </summary>
    public class AliasGameModeHolder : GameModeHolder
    {
        public override GameMode Mode
        {
            get
            {
                return Root.Mode;
            }
        }

        public override event EventHandler<PropertyChangedEventArgs<GameMode>> OnGameModeChanged
        {
            add { Root.OnGameModeChanged += value; }
            remove { Root.OnGameModeChanged -= value; }
        }

        [SerializeField]
        private GameModeHolder _root;
        internal override GameModeHolder Root
        {
            get { return _root; }
        }

    }
}
