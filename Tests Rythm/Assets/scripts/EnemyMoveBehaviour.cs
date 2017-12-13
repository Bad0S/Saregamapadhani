using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


public class EnemyMoveBehaviour : MonoBehaviour {
	public bool detected;
	public static GameObject Target;
	float xEnemyDirection;
	float yEnemyDirection;
	Vector3 enemyDirection;
	private float angle;
	public float moveSpeed;
	public static bool detectionMain = false;

	// Use this for initialization
	void Start () {
	}
	//est appelée par RoomDétection, lui donne sa cible etl droit de la suivre
	public void Detection(GameObject Player){
		Target = Player;
		detected = true;
	} 
	// Update is called once per frame
	void Update () {
		if (detected == true){
			//récup la position et l'angle de la cible
			Vector3 enemyDirection = Target.transform.InverseTransformPoint (transform.position);
			xEnemyDirection = enemyDirection.x;
			yEnemyDirection = enemyDirection.y;
			if (xEnemyDirection != 0.0f || yEnemyDirection != 0.0f) 
			{
				angle = Mathf.Atan2 (yEnemyDirection, xEnemyDirection) * Mathf.Rad2Deg;
				if (angle <= 0f)
					angle = 360 + angle;
			}
			//se tourne vers la cible
			transform.rotation = Quaternion.Euler(0, 0, angle);
			//va vers la cible
			float step = moveSpeed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, step);
		}
	}
}
