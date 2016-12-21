using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Codonbyte.SpaceBilliards.Arena.States;

namespace Codonbyte.SpaceBilliards.UI.Debugging
{
    [ExecuteInEditMode]
    internal class DrawCurrentGameState : UIBehaviour
    {
        [SerializeField]
        private Text text;

        [SerializeField]
        private StateMachine sm;

        void Update()
        {
            if (sm == null) return;
            if (text == null) return;
            if (sm.Current == null)
            {
                text.text = "Current Game State: Null";
                return;
            }
            text.text = "Current Game State: " + sm.Current.GetType().Name;
        }
    } 
}
