using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerMain : MonoBehaviour {
	public static bool detectionMain = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
		void Update () {
			if (detectionMain == true){
				EnemyMoveBehaviour.detected = true;
			}
		}
}
