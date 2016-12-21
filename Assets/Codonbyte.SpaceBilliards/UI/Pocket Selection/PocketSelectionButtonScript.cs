using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Codonbyte;
using Codonbyte.SpaceBilliards;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Codonbyte.SpaceBilliards.UI
{
	[RequireComponent(typeof(Button))]
	public class PocketSelectionButtonScript : UIBehaviour, IPocketSelectionButton
	{
#pragma warning disable 649
		[SerializeField]
		private Text caption;
#pragma warning restore 649

		//private Button button;

		[SerializeField]
		[Range(0, 7)]
		private int _value;
		private void UpdateValue(int value)
		{
			_value = value;
			caption.text = value.ToString();
		}
		private void UpdateValue()
		{
			UpdateValue(_value);
		}
		public int Value
		{
			get { return _value; }
			set { UpdateValue(value); }
		}

		/// <summary>
		/// Raised when the current button is clicked.
		/// </summary>
		public event EventHandler<PocketSelectedEventArgs> OnClicked;

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public void RaiseClickedEvent()
		{
			if (OnClicked == null) return;
			OnClicked(this, new PocketSelectedEventArgs(Value));
		}

		protected override void Awake()
		{
			//button = GetComponent<Button>();
		}

#if UNITY_EDITOR
		protected override void OnValidate()
		{
			base.OnValidate();
			UpdateValue();
		}
#endif
	} 
}
