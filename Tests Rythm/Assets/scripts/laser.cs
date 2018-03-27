using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour {
	//GameObject caster;
	public float beat = 1;

	void Start () {
		Destroy (gameObject, beat);


	}

	// Update is called once per frame
	void Update () {
		
	}
}
