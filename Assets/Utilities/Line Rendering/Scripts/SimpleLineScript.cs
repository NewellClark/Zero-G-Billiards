using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;

namespace Codonbyte
{
	public class SimpleLineScript : MonoBehaviour
	{
#pragma warning disable 649
		[SerializeField]
		private SimpleRayScript _rayPrefab;
#pragma warning restore 649

		private SimpleRayScript _rayInstance;

		[SerializeField]
		private Vector3 _localStartPoint = Vector3.zero;
		public Vector3 LocalStartPoint
		{
			get { return _localStartPoint; }
			set
			{
				_localStartPoint = value;
				UpdateEverything();
			}
		}

		[SerializeField]
		private Vector3 _localEndPoint = Vector3.forward;
		public Vector3 LocalEndPoint
		{
			get { return _localEndPoint; }
			set
			{
				_localEndPoint = value;
				UpdateEverything();
			}
		}

		[SerializeField]
		private float _lineDiameter = .05f;
		public float LineDiameter
		{
			get { return _lineDiameter; }
			set
			{
				_lineDiameter = value;
				UpdateEverything();
			}
		}

		[SerializeField]
		private Material _material;
		public Material Material
		{
			get { return _material; }
			set
			{
				_material = value;
				UpdateEverything();
			}
		}

		private void UpdateEverything()
		{
			if (_rayInstance == null)
			{
				_rayInstance = Instantiate(_rayPrefab);
				_rayInstance.transform.SetParent(transform);
			}
			if (_material == null) _material = _rayInstance.Material;
			else _rayInstance.Material = _material;

			_rayInstance.transform.localPosition = LocalStartPoint;
			_rayInstance.LocalRayVector = LocalEndPoint - LocalStartPoint;

			_rayInstance.RayDiameter = LineDiameter;
		}

		void OnValidate()
		{
			UpdateEverything();
		}

		void Start()
		{
			UpdateEverything();
		}
	} 
}
