using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Codonbyte;
using Codonbyte.SpaceBilliards.GameLogic;

namespace Codonbyte.SpaceBilliards.Arena.GamePieces
{
    [Serializable]
    //[ExecuteInEditMode]
    public class BilliardPocket : MonoBehaviour, IBallPocketedNotifier
    {
        public event EventHandler<BallPocketedEventArgs> OnBallPocketed;

        [SerializeField]
        private int _pocketIndex = 0;
        /// <summary>
        /// Gets or sets the index used to identify the current pocket in events, etc.
        /// </summary>
        public int PocketIndex
        {
            get { return _pocketIndex; }
            set
            {
                _pocketIndex = value;
            }
        }

#pragma warning disable 649
		[SerializeField]
        private GameObject selectedHalo;

        [SerializeField]
        private GameObject unselectedHalo;
#pragma warning restore 649

		public bool Selected
        {
            get { return selectedHalo.activeSelf; }

            set
            {
                selectedHalo.SetActive(value);
                unselectedHalo.SetActive(!value);
            }
        }

        void Awake()
        {
            Selected = Selected;
        }

        void OnTriggerEnter(Collider other)
        {
            var ball = other.gameObject.GetComponent<BilliardBall>();
            if (ball == null) return;
            if (OnBallPocketed != null) OnBallPocketed(this, new BallPocketedEventArgs(ball, PocketIndex));
        }
    } 
}
