using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetection : MonoBehaviour {
	public List<GameObject> Denizens; 
	private GameObject Temp;

	private void OnTriggerEnter2D(Collider2D other)
	{
		//si player en collision avec collider, dit aux gameobjects de denizens que le player est dedans
		if (other.tag == "Player") {
			for (int i = 0;i < Denizens.Count; i++){
				Denizens [i].SendMessage ("Detection", other.gameObject) ;
			}
		}
	}
}
