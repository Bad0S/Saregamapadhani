using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour {
	public static bool kill;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (kill)
			Destroy (gameObject);

	}
}
