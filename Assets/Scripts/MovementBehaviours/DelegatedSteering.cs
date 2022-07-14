using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (Rigidbody))]
public class DelegatedSteering : MonoBehaviour {

	public float minLinearSpeed = 5f;
	public float maxLinearSpeed = 40f;
	public float maxAngularSpeed = 100f;
	
	[HideInInspector]
	public Vector3 lastPos;
	[HideInInspector]
	public int lap = 0;

	private PathFollowing pathStrategy = null;

	private MovementStatus status;

	private void Start () {
		status = new MovementStatus ();
		int i = Random.Range(0, 3);
		if (i == 0)
		{
			GetComponent<ChaseTheRabbit>().predictionOffset = 5;
			pathStrategy = GetComponent<ChaseTheRabbit>();
			Debug.Log(gameObject.name + " choose Chase the rabbit");
        }
        else if(i==1)
        {
			pathStrategy = GetComponent<PredictivePathFollowing>();
			Debug.Log(gameObject.name + " choose PPF");
        }
        else
        {
			GetComponent<ChaseTheRabbit>().predictionOffset = 1;
			pathStrategy = GetComponent<ChaseTheRabbit>();
			Debug.Log(gameObject.name + " choose Chase the rabbit (follow path strictly)");
		}
	}

	void FixedUpdate () {

		RaycastHit hit;
		if (Physics.Raycast(GetComponent<Rigidbody>().position, -transform.up, out hit))
		{
			GetComponent<Rigidbody>().useGravity = false;
			GetComponent<Rigidbody>().MovePosition(new Vector3(GetComponent<Rigidbody>().position.x, hit.point.y + 1, GetComponent<Rigidbody>().position.z));
			lastPos = hit.point;
        }
        else
        {
			GetComponent<Rigidbody>().useGravity = true;
		}

		status.movementDirection = transform.forward;

		// Get the destination point based on the behaviour attached to the object
		Vector3 destination = pathStrategy.GetDestination(status);

		// Contact al behaviours and build a list of directions
		List<Vector3> components = new List<Vector3> ();
		foreach (MovementBehaviour mb in GetComponents<MovementBehaviour> ())
			components.Add (mb.GetAcceleration (status, destination));

		// Blend the list to obtain a single acceleration to apply
		Vector3 blendedAcceleration = Blender.Blend (components);

		// if we have an acceleration, apply it
		if (blendedAcceleration.magnitude != 0f) {
			Driver.Steer (GetComponent<Rigidbody> (), status, blendedAcceleration,
				          minLinearSpeed, maxLinearSpeed, maxAngularSpeed);
		}
	}

	/*private void OnDrawGizmos () {
		if (status != null) {
			UnityEditor.Handles.Label (transform.position + 2f * transform.up, status.linearSpeed.ToString () + "\n" + status.angularSpeed.ToString());
		}
	}*/

}
