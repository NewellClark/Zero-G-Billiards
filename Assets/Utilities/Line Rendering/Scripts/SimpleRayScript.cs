using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;

namespace Codonbyte
{
	/// <summary>
	/// Script for a prefab that renders a ray from its origin.
	/// </summary>
	public class SimpleRayScript : MonoBehaviour
	{
#pragma warning disable 649
		[SerializeField]
		private Transform _cylinder;
#pragma warning restore 649

		[SerializeField]
		private Vector3 _localRayVector = Vector3.forward;
		/// <summary>
		/// Gets or sets a vector specifying both the direction and the length of the ray, in local-space.
		/// </summary>
		public Vector3 LocalRayVector
		{
			get { return _localRayVector; }
			set
			{
				_localRayVector = value;
				UpdateEverything();
			}
		}

		/// <summary>
		/// Gets or sets a vector specifying both the direction and the length of the ray, in world-space.
		/// </summary>
		public Vector3 RayVector
		{
			get { return transform.TransformVector(LocalRayVector); }
			set
			{
				LocalRayVector = transform.InverseTransformVector(value);
			}
		}

		[SerializeField]
		[Range(0, .5f)]
		private float _rayDiameter = .02f;
		public float RayDiameter
		{
			get { return _rayDiameter; }
			set
			{
				_rayDiameter = value;
				UpdateEverything();
			}
		}

		[SerializeField]
		private Material _material;
		public Material Material
		{
			get
			{
				return _material;
			}
			set
			{
				_material = value;
				UpdateEverything();
			}
		}

		void OnValidate()
		{
			UpdateEverything();
		}

		void Start()
		{
			UpdateEverything();
		}

		private Renderer _renderer; 
		private void UpdateEverything()
		{
			if (_cylinder == null) return;

			if (_renderer == null) _renderer = _cylinder.GetComponent<Renderer>();
			if (_material == null) Material = _renderer.sharedMaterial;
			else _renderer.sharedMaterial = Material;

			_cylinder.localScale = new Vector3(RayDiameter, LocalRayVector.magnitude / 2, RayDiameter);
			_cylinder.localPosition = new Vector3(0, 0, _cylinder.localScale.y);

			transform.localRotation = Quaternion.FromToRotation(Vector3.forward, LocalRayVector);

			_renderer.enabled = (LocalRayVector.magnitude != 0);
		}
	} 
}
