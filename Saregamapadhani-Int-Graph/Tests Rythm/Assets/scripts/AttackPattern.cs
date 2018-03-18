using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPattern : MonoBehaviour {
	public float time;
	public bool offensive;
	public bool defensive;
	public float phase;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time <= 4*phase){
			if (time < phase) {
				offensive = true;
			}
			else if ( time < 3*phase){
				defensive = true;
			}
			else{
				offensive = false;
				defensive = false;
			}
		}
		else{
			time = 0;
		}
	}
}
