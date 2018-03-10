using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttaqueSwing : MonoBehaviour
{
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (transform.rotation.z < 300)
        {
            transform.rotation *= Quaternion.Euler(0,0,-8);
        }
        if (transform.localEulerAngles.z == 300)
        { 
            Destroy(gameObject);
        }
    }
}
