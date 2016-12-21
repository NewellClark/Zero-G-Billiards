using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Codonbyte;
using Codonbyte.SpaceBilliards.GameLogic;
using System.Linq;

namespace Codonbyte.SpaceBilliards.Arena.GamePieces
{
	/// <summary>
	/// The set of all pockets in an arena.
	/// </summary>
	internal class PocketSet : MonoBehaviour, IPocketSet
	{
		/// <summary>
		/// Raised when a ball is pocketed in any pocket in the collection.
		/// </summary>
		public event EventHandler<BallPocketedEventArgs> OnBallPocketed
		{
			add
			{
				foreach (var pocket in Pockets) pocket.OnBallPocketed += value;
			}
			remove
			{
				foreach (var pocket in Pockets) pocket.OnBallPocketed -= value;
			}
		}

#pragma warning disable 649
		[SerializeField]
		private BilliardPocket[] _pocketsArray;
		private IList<BilliardPocket> _pocketsReadOnly;
#pragma warning restore 649
		/// <summary>
		/// Gets a read-only list of all the pockets in the current collection.
		/// </summary>
		public IList<BilliardPocket> Pockets
		{
			get { return _pocketsReadOnly; }
		}

		 IEnumerator<BilliardPocket> IEnumerable<BilliardPocket>.GetEnumerator()
		{
			return Pockets.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() { return Pockets.GetEnumerator(); }

		/// <summary>
		/// Sets whether the specified pocket should be visibly highlighted.
		/// </summary>
		/// <param name="pocket"></param>
		/// <param name="highlighted"></param>
		[Obsolete("You can just use the Selected property on BilliardPocket.")]
		public void SetPocketHighlightState(BilliardPocket pocket, bool highlighted)
		{
			pocket.Selected = highlighted;
		}

		public void SetPocketsVisible(bool visible)
		{
			foreach (var pocket in this)
			{
				pocket.GetComponent<MeshRenderer>().enabled = visible;
			}
		}

		void OnValidate()
		{
			UpdatePocketsList();
		}

		void Awake()
		{
			UpdatePocketsList();
		}

		private void UpdatePocketsList()
		{
			if (_pocketsArray == null) return;
			_pocketsReadOnly = _pocketsArray.ToList().AsReadOnly();
			for (int index = 0; index < Pockets.Count; index++)
			{
				var pocket = Pockets[index];
				if (pocket == null) continue;
				pocket.PocketIndex = index;
			}
		}
	}
}
