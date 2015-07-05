using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float TorqueAmount;
	public Rigidbody rb;
	public float HorizontalForce;
	public float VerticalForce;

	Vector3 LastMousePos = Vector3.zero;
	float XMouseDelta = 0f;
	float YMouseDelta = 0f;

	// Use this for initialization
	void Start () {
		rb = transform.GetComponent<Rigidbody>();
		rb.maxAngularVelocity = 100f;
	}
	
	// Update is called once per frame
	void Update () {

		//UsingKeyboardInput ();
		UsingMouseInput ();

		if(Input.GetMouseButton(0))
		{
			rb.AddTorque(Camera.main.transform.right * VerticalForce);
			rb.AddTorque(Camera.main.transform.forward * HorizontalForce);

			/*
			rb.AddTorque(Vector3.right * VerticalForce);
			rb.AddTorque(Vector3.forward * HorizontalForce);
			*/
		}

		//Debug.Log(rb.velocity);
	
	}

	void UsingKeyboardInput ()
	{
		if(Input.GetKey(KeyCode.UpArrow))
		{
			VerticalForce = 1;
		}
		else if(Input.GetKey(KeyCode.DownArrow))
		{
			VerticalForce = -1;
		}
		else
		{
			VerticalForce = 0;
		}
		
		if(Input.GetKey(KeyCode.RightArrow))
		{
			HorizontalForce = -1;
		}
		else if(Input.GetKey(KeyCode.LeftArrow))
		{
			HorizontalForce = 1;
		}
		else
		{
			HorizontalForce = 0;
		}
	}

	void UsingMouseInput ()
	{
		//Debug.Log(Input.mousePosition);


		if(Input.GetMouseButtonDown(0))
		{
			LastMousePos = Input.mousePosition;
		}

		if(Input.GetMouseButton(0))
		{
			XMouseDelta = Input.mousePosition.x - LastMousePos.x;
			YMouseDelta = Input.mousePosition.y - LastMousePos.y;

			HorizontalForce = -1 * XMouseDelta * TorqueAmount;
			VerticalForce = YMouseDelta * TorqueAmount;
		}
		LastMousePos = Input.mousePosition;
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Reset")
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	void OnCollisionEnter(Collision other) {
		var magnitudeThreshold = 0f;
		Debug.Log ("Ball Velocity is: " + rb.velocity.sqrMagnitude);
		//if (rb.velocity.sqrMagnitude > magnitudeThreshold)
			Fabric.EventManager.Instance.PostEvent("SFX/Ball/Bounce");
	}
}
