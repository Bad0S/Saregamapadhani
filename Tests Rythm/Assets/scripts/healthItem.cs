using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthItem : MonoBehaviour {
	public float lifeGained = 0.2f;// la vie que tu gagnes
	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter2D( Collider2D  other)
	{
		if (other.tag == "Player") 
		{
			other.GetComponent<health> ().Heal (lifeGained);
			Destroy (gameObject);
		}
		//dans l'autre, on récupère dans player la fonction heal
	}
	// Update is called once per frame
	void Update () {
		
	}
}
