using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caster : MonoBehaviour {
	float timerCasting;
	bool canShoot;
	bool casting;
	public float timeCast; // ATTENTION, si on change le timeCast faut aussi changer le speedGrowth proportionnellement
	Transform player;
	public int casterFrom;
	public int casterTo;
	int casterTemp;
	bool decasting;
	public float speedGrowth=1;
	public GameObject laser;
	Vector3 hitV3;
	public float angleShoot;
	Vector3 playerDirection;
	RaycastHit2D hit;
	float laserLength;
	public float frame = 0.5f;
	public LayerMask layerLaser;

	void Start(){
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Fire1")){
			casting = true;
			decasting = false;
			print ("test");
		}

		if(casting==true){
			timerCasting += Time.deltaTime;
		}
		else if(decasting == true){
			timerCasting -= Time.deltaTime;
		}
		transform.localScale = new Vector3(Mathf.Lerp(casterFrom,casterTo, timerCasting*speedGrowth), Mathf.Lerp(casterFrom,casterTo, timerCasting*speedGrowth), 0);
		if(timerCasting<=timeCast - frame)
			playerDirection = new Vector3 (player.position.x - transform.position.x, player.position.y - transform.position.y, 0);
		if (timerCasting >= timeCast&& casting ==true) {
			timerCasting = 1;
			casting = false;
			canShoot = true;
			decasting = true;
			speedGrowth *= 2;
		}
		if (timerCasting <= 0 &&decasting == true){
			timerCasting = 0;
			decasting = false;
			speedGrowth /= 2;
		}
		// créé le cast
		if (canShoot == true){
			hit = Physics2D.Raycast(transform.position, playerDirection,Mathf.Infinity, layerLaser);
			laserLength = hit.distance;
			hitV3 = new Vector3 (hit.point.x, hit.point.y, 0);
			angleShoot = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
			if (hit.collider != null)
			{
				print ("yes");
				GameObject laserInstance = (GameObject)Instantiate (laser, (transform.position+hitV3)/2, Quaternion.Euler(0, 0, angleShoot));
				laserInstance.transform.localScale = new Vector3 (laserLength,1,1);
			}
			canShoot = false;
		}

	}
}
