using UnityEngine;
using System.Collections.Generic;
using System;

namespace Codonbyte.SpaceBilliards.Arena.States
{
    /// <summary>
    /// Base class for representing a state that the arena is in, such as player taking shot, etc. 
    /// </summary>
    [RequireComponent(typeof(StateMachine))]
    [System.Serializable]
    public abstract class GameState : MonoBehaviour
    {
        public abstract void OnStateEnter(GameState previous);
        public abstract void OnStateExit();
        public virtual void OnStateUpdate() { }

        /// <summary>
        /// Gets the StateMachine that the current GameState is associated with.
        /// </summary>
        public StateMachine StateMachine
        {
            get
            {
                return gameObject.GetComponent<StateMachine>();
            }
        }
    }
}
