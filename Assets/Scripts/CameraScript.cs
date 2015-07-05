using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform BallRef;
	public float UpOffset;
	public float FrontOffset;

	Vector3 MoveToLocation;
	public float MoveSpeed;
	public float DistanceToTarget;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if(BallRef)
		{
			//DISTANCE
			DistanceToTarget = Vector3.Distance(transform.position, BallRef.transform.position);

			//GET MOVE TO LOCATION
			if(BallRef.GetComponent<Rigidbody>().velocity.normalized == Vector3.zero)
			{
				MoveToLocation = BallRef.position + new Vector3(0f,0f,-2.5f);
			}
			else
			{
				MoveToLocation = BallRef.position + ((BallRef.GetComponent<Rigidbody>().velocity.normalized) * -2.5f);
			}
			MoveToLocation += (Vector3.up * 2);

			//MOVE
			transform.position = Vector3.Lerp(transform.position, MoveToLocation, Time.deltaTime * (DistanceToTarget/3));

			//LOOK AT
			Vector3 relativePos = (BallRef.transform.position + Vector3.up) - transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			transform.rotation = rotation;

		}

	}
}
