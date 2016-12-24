using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System;

namespace Codonbyte.Development.TransformRendering
{
	public class ComponentWiseVectorGizmo : MonoBehaviour, IFancyVectorGizmo, IEnumerable<IFancyVectorGizmo>
	{
#pragma warning disable 649
		[SerializeField]
		private VectorGizmo xGizmo;

		[SerializeField]
		private VectorGizmo yGizmo;

		[SerializeField]
		private VectorGizmo zGizmo;
#pragma warning restore 649

		[SerializeField]
		private Vector3 _value;
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
		/// <summary>
		/// Gets or sets the coordinate space in which to operate.
		/// This property determines how Value will be interpreted.
		/// </summary>
		public Space Space
		{
			get { return _space; }
			set
			{
				_space = value;
				UpdateEverything();
			}
		}

		[SerializeField]
		private float _originDiameter;
		public float OriginDiameter
		{
			get { return _originDiameter; }
			set
			{
				_originDiameter = value;
				UpdateEverything();
			}
		}

		[SerializeField]
		private float _shaftDiameter;
		public float ShaftDiameter
		{
			get { return _shaftDiameter; }
			set
			{
				_shaftDiameter = value;
				UpdateEverything();
			}
		}

		[SerializeField]
		private float _coneDiameter;
		public float ConeDiameter
		{
			get { return _coneDiameter; }
			set
			{
				_coneDiameter = value;
				UpdateEverything();
			}
		}

		[SerializeField]
		private float _coneLength;
		public float ConeLength
		{
			get { return _coneLength; }
			set
			{
				_coneLength = value;
				UpdateEverything();
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public IEnumerator<IFancyVectorGizmo> GetEnumerator()
		{
			yield return xGizmo;
			yield return yGizmo;
			yield return zGizmo;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public void UpdateEverything()
		{
			foreach (IFancyVectorGizmo gizmo in this)
			{
				if (gizmo == null) continue;
				gizmo.ConeDiameter = ConeDiameter;
				gizmo.ConeLength = ConeLength;
				gizmo.OriginDiameter = OriginDiameter;
				gizmo.ShaftDiameter = ShaftDiameter;
				gizmo.Space = Space;
			}

			if (xGizmo != null)
			{
				xGizmo.Value = Vector3.Project(Value, Vector3.right);
			}
			if (yGizmo != null)
			{
				yGizmo.Value = Vector3.Project(Value, Vector3.up);
			}
			if (zGizmo != null)
			{
				zGizmo.Value = Vector3.Project(Value, Vector3.forward);
			}
		}
	}
}
