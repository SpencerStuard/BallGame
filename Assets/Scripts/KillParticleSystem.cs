using UnityEngine;
using System.Collections;

public class KillParticleSystem : MonoBehaviour 
{
	
	void FixedUpdate () 
	{
		if(GetComponent<ParticleSystem>() != null && GetComponent<ParticleSystem>().particleCount <= 0)
			Destroy (gameObject);
	}
}
