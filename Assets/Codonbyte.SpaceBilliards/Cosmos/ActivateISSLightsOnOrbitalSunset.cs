using UnityEngine;
using System.Collections;

namespace Codonbyte.SpaceBilliards.Cosmos
{
	public class ActivateISSLightsOnOrbitalSunset : MonoBehaviour
	{
#pragma warning disable 649
		[SerializeField]
		private Collider sun;

		[SerializeField]
		private GameObject lightRig;
#pragma warning restore 649

		[SerializeField]
		private string sunLayerMask = "Planets";

		[SerializeField]
		private float raycastRange = float.PositiveInfinity;

		// Update is called once per frame
		void Update()
		{
			Ray ray = new Ray(transform.position, (sun.transform.position - transform.position).normalized);
			RaycastHit hit;
			bool lightsOn = true;
			if (Physics.Raycast(ray, out hit, raycastRange, LayerMask.NameToLayer(sunLayerMask)))
			{
				lightsOn = hit.collider != sun;
			}

			lightRig.SetActive(lightsOn);
			
		}
	} 
}
