using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;
		// Audio - Start Music
		if (!Fabric.EventManager.Instance.IsEventActive("Music/Switch", gameObject)) {
			Fabric.EventManager.Instance.PostEvent("Music/Switch");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
