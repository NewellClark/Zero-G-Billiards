﻿using UnityEngine;
using System.Collections;
using System.ComponentModel;

namespace Codonbyte.SpaceBilliards.Arena.GamePieces
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [ExecuteInEditMode]
    internal class CueTipLaserScript : MonoBehaviour
    {
#pragma warning disable 649
		[SerializeField]
        private SimpleRayScript _laserBeam;
#pragma warning restore 649

		[SerializeField]
        private float _laserRange = 1;

        void Update()
        {
            if (Application.isEditor && _laserBeam == null) return;

            RaycastHit result;
            if (Physics.Raycast(new Ray(_laserBeam.transform.position, _laserBeam.RayVector), out result, _laserRange))
            {
                _laserBeam.RayVector = result.point - _laserBeam.transform.position;
            }
        }
    } 
}
