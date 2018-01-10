using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPattern : MonoBehaviour {
	private float time;
	public static bool offensive;
	public static bool defensive;
	public float phase;
	private SpriteRenderer enemyRenderer;

	// Use this for initialization
	void Start () {
		enemyRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time <= 4*phase){
			if (time < phase) {
				offensive = true;
				enemyRenderer.color = Color.red;
				health.invincible = false;
			}
			else if ( time < 3*phase&& time > 2*phase){
				defensive = true;
				enemyRenderer.color = Color.cyan;
				health.invincible = true;
			}
			else{
				offensive = false;
				defensive = false;
				enemyRenderer.color = Color.white;
				health.invincible = false;
			}
		}
		else{
			time = 0;
		}
	}
}
