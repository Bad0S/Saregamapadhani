using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour {
	public GameObject onde;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.Space) == true){
			GameObject clone = (GameObject)Instantiate (onde, transform.position,Quaternion.Euler(0f,0f,90f));
			//clone.transform.
	}
}
}
