using UnityEngine;
using System.Collections.Generic;
using Codonbyte.Development;
using System.ComponentModel;

namespace Codonbyte.Development.TransformRendering
{
	/// <summary>
	/// Vector gizmo that shows the main value directly with a single vector,
	/// and also shows the value broken up into xyz components.
	/// </summary>
	public class HybridVectorGizmo : MonoBehaviour, IVectorGizmo
	{
#pragma warning disable 649
		[SerializeField]
		private VectorGizmo _mainGizmo;

		[SerializeField]
		private ComponentWiseVectorGizmo _componentWiseGizmo;
#pragma warning restore 649

		[SerializeField]
		private Vector3 _value = Vector3.one;
		public Vector3 Value
		{
			get { return _value; }
			set
			{
				_value = value;
				UpdateEverything();
			}
		}

		[SerializeField]
		private Space _space = Space.Self;
		public Space Space
		{
			get { return _space; }
			set
			{
				_space = value;
				UpdateEverything();
			}
		}

		private void UpdateEverything()
		{
			if (_mainGizmo != null)
			{
				_mainGizmo.Value = Value;
				_mainGizmo.Space = Space;
			}
			if (_componentWiseGizmo != null)
			{
				_componentWiseGizmo.Value = Value;
				_componentWiseGizmo.Space = Space;
			}
		}

		void OnValidate()
		{
			UpdateEverything();
		}
	}
}
