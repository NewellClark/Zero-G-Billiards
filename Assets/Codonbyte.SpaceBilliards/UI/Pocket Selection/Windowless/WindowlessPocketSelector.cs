using UnityEngine;
using System.Collections;
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
    public class WindowlessPocketSelector : MonoBehaviour
    {
        [SerializeField]
        private Camera selectionCamera;

        [SerializeField]
        private UnifiedIPocketSet _pockets;
        private IPocketSet Pockets { get { return _pockets.Result; } }

        [SerializeField]
        private float raycastRange = 10;

        [SerializeField]
        private CallPocketGameState callPocketState;

        public void ConfirmSelection()
        {
            callPocketState.GoToNextState();
        }

        //private IEnumerable<Collider> colliders;

        void Start()
        {
            //colliders = from pocket in Pockets
            //            select pocket.GetComponent<Collider>();
        }

        void Update()
        {
            if (!Input.GetMouseButton(0)) return;
            var pocket = RaycastPockets(Input.mousePosition);
            if (pocket == null) return;
            foreach (var p in Pockets) p.Selected = false;
            pocket.Selected = true;

            callPocketState.CallPocket(pocket.PocketIndex);
        }

        private BilliardPocket RaycastPockets(Vector3 screenPoint)
        {
            Ray ray = selectionCamera.ScreenPointToRay(screenPoint);
            foreach (var pocket in Pockets)
            {
                var collider = pocket.GetComponent<Collider>();
                RaycastHit hit;
                if (collider.Raycast(ray, out hit, raycastRange)) return pocket;
            }

            return null;
        }
    }
}
