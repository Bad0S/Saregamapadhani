using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveBehaviour : MonoBehaviour {
	public static bool detected;
	public GameObject Target;
	float xEnemyDirection;
	float yEnemyDirection;
	Vector3 enemyDirection;
	private float angle;
	public float moveSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (detected == true){
			Vector3 enemyDirection = Target.transform.InverseTransformPoint (transform.position);
			xEnemyDirection = enemyDirection.x;
			yEnemyDirection = enemyDirection.y;
			if (xEnemyDirection != 0.0f || yEnemyDirection != 0.0f) 
			{
				angle = Mathf.Atan2 (yEnemyDirection, xEnemyDirection) * Mathf.Rad2Deg;
				if (angle <= 0f)
					angle = 360 + angle;
			}

			transform.rotation = Quaternion.Euler(0, 0, angle);
			float step = moveSpeed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, step);
			//enemyDirection.Normalize ();
			//float step = moveSpeed * Time.deltaTime;
			//transform.position = Vector3.MoveTowards(transform.position, enemyDirection, step);
			//transform.position = new Vector3 (xEnemyDirection*moveSpeed/100,yEnemyDirection *moveSpeed/100, 0);
			//transform.Translate (Vector3.left* Time.deltaTime*moveSpeed);
			Debug.Log (angle); 
			//print (enemyDirection);

		}
	}
}
