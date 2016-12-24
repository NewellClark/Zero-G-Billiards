using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;

namespace Codonbyte
{
	/// <summary>
	/// Script for a simple configurable crosshairs prefab.
	/// </summary>
	public class SimpleCrosshairsScript : MonoBehaviour
	{
		[SerializeField]
		[Range(0, .5f)]
		private float _hairDiameter = .05f;
		/// <summary>
		/// Gets or sets the diameter of each crosshair.
		/// </summary>
		public float HairDiameter
		{
			get { return _hairDiameter; }
			set
			{
				_hairDiameter = value;
				UpdateEverything();
			}
		}

		[SerializeField]
		private float _hairLength = 1;
		/// <summary>
		/// Gets or sets the length of each crosshair, from end to end.
		/// </summary>
		public float HairLength
		{
			get { return _hairLength; }
			set
			{
				_hairLength = value;
				UpdateEverything();
			}
		}

#pragma warning disable 649
		[SerializeField]
		private SimpleRayScript[] _rays;
#pragma warning restore 649

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
			if (_rays == null) return;
			foreach (var ray in _rays)
			{
				if (ray == null) continue;
				if (_material == null) _material = ray.Material;
				else ray.Material = _material;

				ray.RayDiameter = HairDiameter;
				ray.LocalRayVector = ray.LocalRayVector.normalized * HairLength / 2;
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
	} 
}
