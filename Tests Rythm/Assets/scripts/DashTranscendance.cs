using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTranscendance : MonoBehaviour {
	public List<Transform> enemyList;
	private Vector2 selection;
	private Vector2 temp;
	private Transform self;

	void Start(){
		self = gameObject.transform;
		enemyList = new List<Transform> ();
		selection = new Vector2 ();
	}
	void OnTriggerEnter2D (Collider2D enemy){
		if (enemy.tag=="Enemy"&& !enemyList.Contains (enemy.transform)){
			enemyList.Add (enemy.transform);
			//GetComponentInParent <Player> ().dashTarget = enemy.transform;
		}

		
	}
	void OnTriggerExit2D(Collider2D enemy){
		if(enemy.tag == "Enemy"){
			enemyList.Remove (enemy.transform);
			//GetComponentInParent <Player> ().dashTarget = null;
		}
	}

	public Vector2 SelectEnemy(List<Transform> sorted){
		selection = new Vector2 (Mathf.Infinity, Mathf.Infinity);
		for (int i = 0; i < sorted.Count; i++) {
			temp = new Vector2 (sorted [i].position.x - self.position.x, sorted [i].position.y - self.position.y);
				if(selection.sqrMagnitude > temp.sqrMagnitude){
				print (selection);

				selection = temp;
			}
		}
		print (selection);
		return selection;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
