using UnityEngine;
using System.Collections;
using Codonbyte.SpaceBilliards;
using Codonbyte.SpaceBilliards.GameLogic;
using Codonbyte.SpaceBilliards.Arena.States;
using Codonbyte.SpaceBilliards.UI;

namespace Codonbyte.SpaceBilliards.UI.Debugging
{
    public class ShowGameStateChanges : MonoBehaviour
    {
        [SerializeField]
        private StateMachine sm;

        [SerializeField]
        private UnifiedTimedStringQueue notificationWall;

        [SerializeField]
        private float messageLifeInSeconds = 60;

        void Awake()
        {
            sm.OnCurrentStateChanged += (s, e) => { notificationWall.Result.AddItem(e.NewState.GetType().Name, messageLifeInSeconds); };
        }
    } 
}
