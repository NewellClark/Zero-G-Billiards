using UnityEngine;
using System.Collections.Generic;
using Codonbyte;
using Codonbyte.Development;
using Codonbyte.SpaceBilliards;
using Codonbyte.SpaceBilliards.Arena;
using Codonbyte.SpaceBilliards.Arena.GamePieces;
using Codonbyte.Development.TransformRendering;

public class MobileTouchCueAimerVariableSpeed : MonoBehaviour {
#pragma warning disable 649
	[SerializeField]
	private BilliardCueScript cue;

	[SerializeField]
	private Collider raycastTarget;

	[SerializeField]
	private Camera shotCam;

	[SerializeField]
	private AnimationCurve powerCurve;

	[SerializeField]
	private VectorGizmo aimingArrow;
#pragma warning restore 649

	/*
	[SerializeField]
	private float minSpeed = .01f;

	[SerializeField]
	private float maxSpeed = 2f;

	[SerializeField]
	private float minTargetRadius = .1f;*/

	[SerializeField]
	[Range(0, .3f)]
	private float targetRadius = .2f;

	[SerializeField]
	private float raycastRange = 10;

	[SerializeField]
	private Vector3 horizontalDirection = Vector3.right;

	[SerializeField]
	private Vector3 verticalDirection = Vector3.up;


	private void ResetAimingArrow()
	{
		if (aimingArrow == null) return;
		aimingArrow.Value = Vector3.zero;
	}

	void Update()
	{
		if (!Input.GetMouseButton(0))
		{
			ResetAimingArrow();
			return;
		}
		var tangent = TryGetTangent(Input.mousePosition);
		if (tangent == null)
		{
			ResetAimingArrow();
			return;
		}
		//RotateCueByTangent(tangent.Value);
		RotateCueByTangentUsingCurve(tangent.Value);
	}

	void OnValidate()
	{
		raycastTarget.transform.localScale = new Vector3(
			targetRadius * 2, raycastTarget.transform.localScale.y, targetRadius * 2);
	}

	private Vector2? TryGetTangent(Vector3 screenPoint)
	{
		Ray ray = shotCam.ScreenPointToRay(screenPoint);
		RaycastHit hit;
		if (!raycastTarget.Raycast(ray, out hit, raycastRange)) return null;

		var offset = cue.transform.InverseTransformPoint(hit.point);
		if (aimingArrow != null) aimingArrow.Value = offset;
		Vector2 tangent = new Vector2(
			offset.ScalarProjection(horizontalDirection),
			offset.ScalarProjection(verticalDirection));

		return tangent;
	}

	/*
	private void RotateCueByTangent(Vector2 tangent)
	{
		float distanceMag = Mathf.Clamp(tangent.magnitude, minTargetRadius, targetRadius);
		float normalizedMag = (distanceMag - minTargetRadius) / (targetRadius - minTargetRadius);
		float speed = minSpeed + (maxSpeed - minSpeed) * normalizedMag;
		Vector2 rotate = tangent.normalized * speed * Time.deltaTime;
		cue.RotateAroundCueBall(rotate);
	}*/

	private void RotateCueByTangentUsingCurve(Vector2 tangent)
	{
		float normalizedRadius = tangent.magnitude / targetRadius;
		float speed = powerCurve.Evaluate(normalizedRadius);
		Vector2 rotate = tangent.normalized * speed * Time.deltaTime;
		cue.RotateAroundCueBall(rotate);
	}
}
