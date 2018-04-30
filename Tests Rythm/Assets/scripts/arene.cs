using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arene : MonoBehaviour {

	public GameObject door;
	public GameObject talker;
	public GameObject secondDial;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (talker.GetComponent <dialTutoManager>().dialCounter ==1&&secondDial.GetComponent <DialogueComponent>().inDialogue ==false&&talker.GetComponent <dialTutoManager>().secondDial == false )
		{
			door.SetActive ((false));
		}
	
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {

			door.SetActive ((true));
			talker.GetComponent <dialTutoManager>().secondDial = true;
		}
	}
}

