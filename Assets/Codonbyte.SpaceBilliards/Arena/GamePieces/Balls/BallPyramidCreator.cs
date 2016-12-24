using UnityEngine;
using System.Collections.Generic;

namespace Codonbyte.SpaceBilliards.Arena.Balls
{
	
	[System.Serializable]
	public class BallPyramidCreator : MonoBehaviour
	{
		[SerializeField]
		private GameObject ballPrefab = null;

		[SerializeField]
		private float ballRadius = .025f;

		[SerializeField]
		private int ballPyramidLayers = 4;

		[Tooltip("The base material for the colored backgrounds of all balls. This material should be white, as it will be cloned and recolored for each ball.")]
		[SerializeField]
		private Material objectBallBaseBackgroundMaterial = null;

		[Tooltip("Background colors for all object balls, excluding death-balls. If each player has more balls than there are colors, then colors will be reused.")]
		[SerializeField]
		private Color[] objectBallBackgroundColors = new Color[]
		{
			new Color(.4667f, 0, 0),
			Color.red,
			new Color(1, 73f / 255f, 0),
			new Color(1, 192f / 255f, 0),
			new Color(0, 194f / 255f, 0),
			new Color(0, 98f / 255f, 0),
			new Color(0, 187f / 255f, 194f / 255f),
			new Color(0, 0, 197f / 255f),
			new Color(77f / 255, 0, 134f / 255),
			new Color(1, 34f / 255f, 166f / 255f)
		};
		private Material[] backMaterials;

		[SerializeField]
		private Color deathBallBackgroundColor = Color.black;
		private Material deathBallBackMaterial;

#pragma warning disable 649
		[SerializeField]
		private Material solidsOverlayMaterial;

		[SerializeField]
		private Material stripesOverlayMaterial;

		//	Not using these for now.
		//[SerializeField]
		//private Material deathBallSolidsMaterial;

		//[SerializeField]
		//private Material deathBallStripesMaterial;

		[SerializeField]
		private bool randomizeBallOrientation = true;

		[SerializeField]
		private bool randomizeBallArrangement = true;

		[SerializeField]
		private Transform ballParent;

		private List<GameObject> ballsList = new List<GameObject>();
		private VerticeTetrahedron pyramid;
#pragma warning restore 649


		private int NonDeathBallsPerPlayer
		{
			get
			{
				if (ballsList.Count % 2 > 0)
					return (ballsList.Count - 1) / 2;
				return ballsList.Count / 2 - 1;
			}
		}

		private int NumDeathBalls
		{
			get
			{
				if (ballsList.Count % 2 > 0)
					return 1;
				return 2;
			}
		}

		void Awake()
		{
			CreateBackColorMaterials();
			CreateBallPyramid();

			if (randomizeBallArrangement) RandomizeBallsList();
			SetBallAllianceProperties();
			SetBallNumbers();
			SetBallMaterials();
		}

		private void CreateBackColorMaterials()
		{
			var list = new List<Material>();
			foreach (Color color in objectBallBackgroundColors)
			{
				var mat = Instantiate(objectBallBaseBackgroundMaterial);
				mat.color = color;
				list.Add(mat);
			}
			backMaterials = list.ToArray();
			deathBallBackMaterial = Instantiate(objectBallBaseBackgroundMaterial);
			deathBallBackMaterial.color = deathBallBackgroundColor;
		}

		private void CreateBallPyramid()
		{
			pyramid = new VerticeTetrahedron(ballPyramidLayers, ballRadius, Vector3.zero);
			Vector3 offset = new Vector3(0, .5f * pyramid.Height, 0);
			pyramid = new VerticeTetrahedron(ballPyramidLayers, ballRadius, offset);

			foreach (var point in pyramid.Vertices)
			{
				var ball = CreateBall(transform.TransformPoint(point));
				ballsList.Add(ball);
			}
			if (randomizeBallOrientation)
			{
				foreach (var ball in ballsList)
				{
					ball.transform.rotation = Random.rotationUniform;
				}
			}
		}

		private GameObject CreateBall(Vector3 point)
		{
			GameObject ball = (GameObject)Instantiate(ballPrefab, point, Quaternion.identity);
			if (ballParent != null)
			{
				ball.transform.SetParent(ballParent);
			}
			ball.transform.localScale = 2f * ballRadius * new Vector3(1, 1, 1);
			return ball;
		}

		private void SetBallNumbers()
		{
			for (int index = 0; index < ballsList.Count; index++)
			{
				var comp = ballsList[index].GetComponent<BilliardBall>();
				comp.BallNumber = index + 1;
				comp.BallType = BallType.Object;
			}
		}

		private void SetBallMaterials()
		{
			var solids = GetSolidsToBe();
			var stripes = GetStripesToBe();
			var deaths = GetDeathBalls();

			for (int index = 0; index < solids.Length; index++)
			{
				var rend = solids[index].GetComponent<MeshRenderer>();
				Material back = backMaterials[index % backMaterials.Length];
				rend.materials = new Material[] { back, solidsOverlayMaterial };
			}

			for (int index = 0; index < stripes.Length; index++)
			{
				var rend = stripes[index].GetComponent<MeshRenderer>();
				Material back = backMaterials[index % backMaterials.Length];
				rend.materials = new Material[] { back, stripesOverlayMaterial };
			}

			foreach (var ball in deaths)
			{
				var rend = ball.GetComponent<MeshRenderer>();
				var info = ball.GetComponent<BilliardBall>();
				Material overlay;
				if (info.BallAlliance == BallAlliance.Stripes) overlay = stripesOverlayMaterial;
				else overlay = solidsOverlayMaterial;

				rend.materials = new Material[] { deathBallBackMaterial, overlay };
			}
		}

		private void SetBallAllianceProperties()
		{
			foreach (var ball in GetSolidsToBe())
			{
				var comp = ball.GetComponent<BilliardBall>();
				comp.BallAlliance = BallAlliance.Solids;
			}

			foreach (var ball in GetStripesToBe())
			{
				var comp = ball.GetComponent<BilliardBall>();
				comp.BallAlliance = BallAlliance.Stripes;
			}

			var deaths = GetDeathBalls();
			if (deaths.Length > 1)
			{
				deaths[0].GetComponent<BilliardBall>().BallAlliance = BallAlliance.Solids;
				deaths[1].GetComponent<BilliardBall>().BallAlliance = BallAlliance.Stripes;
			}
			else
			{
				deaths[0].GetComponent<BilliardBall>().BallAlliance = BallAlliance.Neutral;
			}

			foreach (var ball in deaths)
			{
				ball.GetComponent<BilliardBall>().IsDeathBall = true;
			}
		}

		private GameObject[] GetSolidsToBe()
		{
			var result = new List<GameObject>();
			for (int index = 0; index < NonDeathBallsPerPlayer; index++)
				result.Add(ballsList[index]);
			return result.ToArray();
		}
		private GameObject[] GetStripesToBe()
		{
			var result = new List<GameObject>();
			for (int index = NonDeathBallsPerPlayer + NumDeathBalls - 1; index < ballsList.Count; index++)
				result.Add(ballsList[index]);
			return result.ToArray();
		}
		private GameObject[] GetDeathBalls()
		{
			var result = new List<GameObject>();
			for (int index = NonDeathBallsPerPlayer; index < NonDeathBallsPerPlayer + NumDeathBalls; index++)
			{
				result.Add(ballsList[index]);
			}
			return result.ToArray();
		}
		private void RandomizeBallsList()
		{
			var list = new List<GameObject>();
			list.AddRange(ballsList);
			var rng = new System.Random();
			ballsList.Clear();
			while (list.Count > 0)
			{
				int index = rng.Next(0, list.Count - 1);
				ballsList.Add(list[index]);
				list.RemoveAt(index);
			}
		}
	}
}
