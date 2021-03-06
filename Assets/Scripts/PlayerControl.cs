﻿using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float TorqueAmount;
	public Rigidbody rb;
	public float HorizontalForce;
	public float VerticalForce;

	Vector3 LastMousePos = Vector3.zero;
	float XMouseDelta = 0f;
	float YMouseDelta = 0f;

	float ballPitch = 1f;
	float ballVolume = 1f;
	float pitchScale = 1f;

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

		if(rb.velocity.sqrMagnitude > 0f) {
			if(!Fabric.EventManager.Instance.IsEventActive("SFX/Ball/Roll", gameObject)) {
				Fabric.EventManager.Instance.PostEvent("SFX/Ball/Roll", gameObject);
			}
		} else {
			Fabric.EventManager.Instance.PostEvent("SFX/Ball/Roll", Fabric.EventAction.StopSound, gameObject);
		}
		//Debug.Log("Velocity: " + rb.velocity.sqrMagnitude);
		var squaredNumber = 10f;
		pitchScale = rb.velocity.sqrMagnitude / (squaredNumber*squaredNumber);
		ballPitch = Mathf.Clamp ( 0.5f + (pitchScale), 0.5f, 3.5f);
		//Debug.Log("Pitch: " + ballPitch);
		//ballVolume = Mathf.Clamp ( 0.5f + ((rb.velocity.sqrMagnitude / (squaredNumber*squaredNumber)) * (rb.velocity.sqrMagnitude / (squaredNumber*squaredNumber))), 0.5f, 10f);
		//Debug.Log("Volume: " +ballVolume);
		Fabric.EventManager.Instance.PostEvent("SFX/Ball/Roll", Fabric.EventAction.SetPitch, ballPitch , gameObject);
		//Fabric.EventManager.Instance.PostEvent("SFX/Ball/Roll", Fabric.EventAction.SetVolume, ballVolume , gameObject);
	
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
		//var magnitudeThreshold = 0f;
		//Debug.Log ("Ball Velocity is: " + rb.velocity.sqrMagnitude);
		//if (rb.velocity.sqrMagnitude > magnitudeThreshold)
			Fabric.EventManager.Instance.PostEvent("SFX/Ball/Bounce");
	}
}
