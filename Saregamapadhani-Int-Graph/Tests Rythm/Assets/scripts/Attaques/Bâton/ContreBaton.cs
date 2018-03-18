using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContreBaton : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        {
            if (transform.localScale.y < 1.5)
            {
                transform.localScale += Vector3.up / 3;
            }
            if (transform.localScale.y >= 1.5)
            {
                Destroy(gameObject);
            }
        }
    }
}
