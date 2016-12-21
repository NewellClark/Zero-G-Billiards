using UnityEngine;
using System.Collections;
using Codonbyte.SpaceBilliards;
using Codonbyte.SpaceBilliards.GameLogic;

namespace Codonbyte.SpaceBilliards.Arena.Balls
{
    public class CueBallCreator : MonoBehaviour
    {
#pragma warning disable 649
		[SerializeField]
		private GameObject ballPrefab;

        [SerializeField]
        private float ballRadius = .025f;

        [SerializeField]
        private Vector3 localOffset = Vector3.zero;

		[SerializeField]
		private Material backMaterial;

		[SerializeField]
		private Material overlayMaterial;

        //[SerializeField]
        //private bool makeCueBallChildOfSelf = false;
        [SerializeField]
        private Transform cueBallParent;
#pragma warning restore 649

		// Use this for initialization
		void Start()
        {
            var ball = (GameObject)GameObject.Instantiate(ballPrefab, transform.TransformPoint(localOffset), Quaternion.identity);
            if (cueBallParent != null) ball.transform.SetParent(cueBallParent);
            var comp = ball.GetComponent<BilliardBall>();
            comp.BallType = BallType.Cue;
            comp.BallAlliance = BallAlliance.Neutral;
            comp.BallNumber = 0;

            ball.transform.localScale = new Vector3(1, 1, 1) * ballRadius * 2;
            var mats = new Material[] { backMaterial, overlayMaterial };
            ball.GetComponent<MeshRenderer>().materials = mats;
        }

        // Update is called once per frame
        void Update()
        {

        }
    } 
}
