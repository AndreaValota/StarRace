using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent (typeof (Rigidbody))]
public class DelegatedSteering : MonoBehaviour {

	public float minLinearSpeed = 5f;
	public float maxLinearSpeed = 40f;
	public float maxAngularSpeed = 100f;
	public int rLower = 0;
	public int rUpper = 11;

	[HideInInspector]
	public Vector3 lastPos;
	[HideInInspector]
	public int lap = 0;
	[HideInInspector]
	public static bool CTR = false;
	[HideInInspector]
	public static bool PPF = false;
	[HideInInspector]
	public static bool CTRStrict = false;
	[HideInInspector]
	public static bool[] possibleStrats = { true, true, true };

	private PathFollowing pathStrategy = null;

	private MovementStatus status;

	private Rigidbody rb;

	private void Start () {
		status = new MovementStatus ();

		int scene = SceneManager.GetActiveScene().buildIndex;
		int i = 0;
		if (scene == 2)
		{
			//i = Random.Range(0, 2);
			i = 1;
			//Debug.Log("range");
		}
        else if(scene == 3)
        {
			i = Random.Range(0, 2);
        }
        else{
			i = Random.Range(0, 3);
			if (CTR)
            {
				possibleStrats[0] = false;
            }
			if (PPF)
            {
				possibleStrats[1] = false;
			}
			if (CTRStrict)
            {
				possibleStrats[2] = false;
			}


			if (CTR || PPF || CTRStrict)
			{
				for (i = 0; i < possibleStrats.Length; i++)
				{
					if (possibleStrats[i])
					{
						break;
					}
				}
			}
        }


		if (i == 0)
		{
			pathStrategy = GetComponent<ChaseTheRabbit>();
			CTR = true;
			Debug.Log(gameObject.name + " choose Chase the rabbit");
        }
        else if(i==1)
        {
			pathStrategy = GetComponent<PredictivePathFollowing>();
			PPF = true;
			maxAngularSpeed = 0;
			Debug.Log(gameObject.name + " choose PPF");
        }
        else
        {
			GetComponent<ChaseTheRabbit>().predictionOffset = 1;
			CTRStrict = true;
			pathStrategy = GetComponent<ChaseTheRabbit>();
			Debug.Log(gameObject.name + " choose Chase the rabbit (follow path strictly)");
		}

		rb = GetComponent<Rigidbody>();

		int r = Random.Range(rLower, rUpper);
		maxLinearSpeed = maxLinearSpeed + r;
		Debug.Log(gameObject.name + " has gas = " + maxLinearSpeed);
	}

	void FixedUpdate () {

		RaycastHit hit;
		if (Physics.Raycast(rb.position, -transform.up, out hit) && hit.transform.tag != "IgnoreSurface")
		{
			rb.useGravity = false;
			rb.MovePosition(hit.point + hit.normal); 
			rb.MoveRotation(Quaternion.FromToRotation(rb.transform.up, hit.normal) * rb.rotation);
			lastPos = hit.point;
        }
        else
        {
			rb.useGravity = true;
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
