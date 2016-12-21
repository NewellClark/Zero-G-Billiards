using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using Codonbyte.SpaceBilliards;
using Codonbyte.SpaceBilliards.GameLogic;
using Codonbyte.SpaceBilliards.Arena;
using Codonbyte.SpaceBilliards.Arena.GamePieces;
using Codonbyte.SpaceBilliards.Arena.States;
using System.Linq;
using System;

namespace Codonbyte.SpaceBilliards.UI
{
    public class PocketSelectionUI : UIBehaviour
    {
        private int _selectedPocketIndex = 0;
        private void UpdateSelectedPocketIndex(int value)
        {
            _selectedPocketIndex = value;
            for (int index = 0; index < buttonText.Length; index++)
            {
                buttonText[index].color = index == value ? selectedPocketTextColor : standardPocketTextColor;
            }

            foreach (var pocket in Pockets) Pockets.SetPocketHighlightState(pocket, false);
            Pockets.SetPocketHighlightState(value, true);

            callPocketState.CallPocket(SelectedPocketIndex);
        }


        private void UpdateSelectedPocketIndex()
        {
            UpdateSelectedPocketIndex(SelectedPocketIndex);
        }
        public int SelectedPocketIndex
        {
            get { return _selectedPocketIndex; }
            set
            {
                UpdateSelectedPocketIndex(value);
            }
        }

        public void ConfirmSelection()
        {
            callPocketState.GoToNextState();
        }

        [SerializeField]
        private UnifiedIPocketSet _pockets;
        private IPocketSet Pockets { get { return _pockets.Result; } }

        [SerializeField]
        private CallPocketGameState callPocketState;

        [SerializeField]
        private Color selectedPocketTextColor = Color.green;

        [SerializeField]
        private Color standardPocketTextColor = Color.white;

        private PocketSelectionButtonScript[] buttons;

        private Text[] buttonText;

        protected override void Awake()
        {
            base.Awake();
            FindAndInitializePocketSelectionButtons();
            InitializeButtonText();
            InitializeEventHandlers();
            UpdateSelectedPocketIndex();
        }

        private void FindAndInitializePocketSelectionButtons()
        {
            var q = from butt in GetComponentsInChildren<PocketSelectionButtonScript>()
                    orderby butt.Value
                    select butt;
            buttons = q.ToArray();

            try
            {
                foreach (var pocket in Pockets.Pockets)
                {
                    buttons[pocket.PocketIndex].Value = pocket.PocketIndex;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Debug.Log("PocketSelectionUI must be given an IPocketSet with the same number of" + 
                    " pockets as there are IPocketSelectionButton objects in the children of its containing game object.");
            }
        }

        private void InitializeButtonText()
        {
            var q = from butt in buttons
                    select butt.GetComponentInChildren<Text>();
            buttonText = q.ToArray();
        }

        private void InitializeEventHandlers()
        {
            foreach (var butt in buttons)
            {
                butt.OnClicked += (s, e) =>
                {
                    SelectedPocketIndex = e.Value;
                };
            }
        }

    }
}
