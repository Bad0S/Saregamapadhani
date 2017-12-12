using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerAnnex : MonoBehaviour {
	public static bool detectionAnnex = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (detectionAnnex == true){
			EnemyMoveBehaviour.detected = true;
		}
	}
}
