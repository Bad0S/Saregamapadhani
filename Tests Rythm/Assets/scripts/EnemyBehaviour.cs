using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	//Axel's pathifinding 
	private Animator anim;
	[SerializeField]private float speed;
	[SerializeField]private GameObject attackHitbox;
	[SerializeField]private float maxDetectionRange;
	[SerializeField]private float attackRangeMax;
	private ContactFilter2D cFilter; 
	private Collider2D[] resultings = new Collider2D[1];

	private Rigidbody2D rb2D;
	private GameObject target;
	Vector3 targetVector;
	private Vector3 targetVectorAttacking;
	bool isFighting = false ;
	[SerializeField]private int health;

	//arrêt du saut et saut
	float closenessComparison;
	float timerResetDrag;
	float timerComparison;
	bool isJumping;
	public float vitesseBond =1;

	//Idle
	bool idleCanMove;
	private bool idling;

	float damage =10f;

	//le grab
	bool canGrab;
	public float grabberPercentage;
	private SpriteRenderer enemyRenderer;
	bool isGrabbing;
	public float speedBurst;
	public float timeBursting = 1;
	float timerGrabbing;
	public bool grabbed;

	void Start () {
		anim = GetComponent<Animator> ();
		rb2D = GetComponent<Rigidbody2D> ();
		target = GameObject.FindGameObjectWithTag ("Player");

		if((Random.Range(0f,1f))*100< grabberPercentage){
			canGrab = true;
			enemyRenderer = GetComponent<SpriteRenderer> ();
			enemyRenderer.color = Color.red;
			transform.localScale= new Vector3(1.2f,1.2f,1);
		}

	}

	void Update () {
		//freine pour éviter de dépasser le joueur
		if(rb2D.velocity.y <0){
			anim.SetFloat ("YSpeed",1);
		}
		else if (rb2D.velocity.y>0){
			anim.SetFloat ("YSpeed",-1);

		}
		timerComparison += Time.deltaTime;
		timerGrabbing += Time.deltaTime;
		if(isJumping == true){
			if (targetVector.magnitude >closenessComparison&& targetVector.magnitude<2.5f){
				if (isFighting == true) {
					GetComponent <Rigidbody2D> ().drag = 20;
					timerResetDrag = 0;
				}
			}
		}
		if(timerComparison>0.12f){
			closenessComparison = targetVector.magnitude;
			timerComparison = 0;
		}

		//print (closenessComparison);

		timerResetDrag += Time.deltaTime;
		if (timerResetDrag>0.2f&& isFighting ==true){
			GetComponent <Rigidbody2D>().drag = 5;// ajouter un bool Stopfighting si nécessaire
		}


		//GRAB
		if (grabbed ==false){
			
			if (targetVector.magnitude < attackRangeMax  && isFighting == false) {
				//isFighting = true;
				//print (targetVector.magnitude);
				if (canGrab == true){
					if (isGrabbing == false&&timerGrabbing> timeBursting+ 2){
						StartCoroutine ("GrabSequence");

					}
				}
				else{
					StartCoroutine ("FightSequence");
				}
			}
		}
		else{
			transform.position = target.transform.position;
		}
		//PATHFINDING
		targetVector = target.transform.position -transform.position;
		if (targetVector.magnitude < maxDetectionRange && isFighting == false) 
		{
			if (idling == true) {
				StopCoroutine("MoveAndWait" ); 
			}

			anim.SetBool ("IsMoving", true);
			rb2D.velocity = Vector3.Normalize(targetVector)*speed;
			//anim.SetBool ("Walking", true);
			if (targetVector.x > 0) {
				GetComponent<SpriteRenderer> ().flipX = true;
			} else {
				GetComponent<SpriteRenderer> ().flipX = false;
			}

		} else {
			//anim.SetBool("Walking",false);
			if (idleCanMove == true && isFighting == false){
				StartCoroutine(MoveAndWait(Random.Range(0.7f,1.2f),Random.Range(1f,2f)));
			}
			else if (idling == false){
				idleCanMove = true;
			}
		}
		/*if (Physics2D.OverlapCollider (GetComponent<Collider2D> (), cFilter, resultings) > 0) {
			if (resultings [0].tag == "AttackHitbox") {
				Vector3 KnockbackVector = Vector3.Normalize(transform.position - resultings[0].transform.position);
				StartCoroutine (Hurt(KnockbackVector,resultings[0].GetComponent<AttackEffect>())); 
			}
		}*/			

	}

	IEnumerator GrabSequence(){
		isGrabbing = true;
		GetComponent<BoxCollider2D> ().isTrigger = true;
		speed*= speedBurst ;
		yield return new WaitForSeconds (timeBursting);
		speed /= speedBurst;
		isGrabbing = false;
		timerGrabbing = 0;
		if(grabbed ==false){
			GetComponent<BoxCollider2D> ().isTrigger = false;
		}
	}

	IEnumerator FightSequence()
	{
		isFighting = true;
		anim.SetBool ("IsAttacking", true);
		targetVectorAttacking = targetVector;
		closenessComparison = targetVector.magnitude;
		//anim.SetTrigger ("Fighting");
		//yield return new WaitForSeconds (0.001f);
		rb2D.velocity = new Vector3 (0,0,0);
		isJumping = true;
		rb2D.AddForce (targetVectorAttacking.normalized * vitesseBond, ForceMode2D.Impulse);
		//Instantiate (attackHitbox, transform);
		yield return new WaitForSeconds (1f);
		isFighting = false;
		isJumping = false;
		anim.SetBool ("IsAttacking", false);
		GetComponent <Rigidbody2D>().drag = 5;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			if(isJumping == true){
				other.gameObject.GetComponent<health>().Hurt(damage);

			}
			else if(isGrabbing ==true){
				grabbed = true;
				target.GetComponent <Player> ().grabbed=true;
				target.GetComponent<Player> ().GrabUngrab ();
				//other.gameObject.GetComponent <Player> ().MovSpeed =other.gameObject.GetComponent <Player> ().MovSpeed *0.7f;
			}
		}
		if (other.tag == "PlayerAttack") {
			if(grabbed == true){
				GameObject.FindGameObjectWithTag ("Player").GetComponent <Player>().grabbed = false ;
				GameObject.FindGameObjectWithTag ("Player").GetComponent <Player>().GrabUngrab () ;

			}
			Destroy (gameObject);
		}
	}

	/*IEnumerator Hurt(Vector3 KnockDirection, AttackEffect effects)
	{
		health-= effects.damages;
		StartCoroutine (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().ScreenShake (effects.screenShakeDuration, effects.screenShakeMagnitude));
		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (KnockDirection.x, KnockDirection.y) * effects.knockbackIntensity ,ForceMode2D.Impulse);
		if (effects.screenFlash == true)
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponentInChildren<Vignette> ().ScreenFlash();
		Destroy (resultings [0]);
		GetComponent<SpriteRenderer> ().color = Color.red;
		yield return new WaitForSeconds (0.1f);
		GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		GetComponent<SpriteRenderer> ().color = Color.white;
		if (health == 0) {
			Death();
		}
	}*/

	IEnumerator MoveAndWait(float secMove,float secWait) // l'idle
	{
		idleCanMove = false;
		idling = true;
		rb2D.velocity = (new Vector2 (Random.Range(-5,5),Random.Range(-5,5)));
		anim.SetBool ("IsMoving", true);
		yield return new WaitForSeconds(secMove);
		rb2D.velocity = (new Vector2 (0,0));
		anim.SetBool ("IsMoving", false);
		yield return new WaitForSeconds (secWait);
		idleCanMove = true;
		idling = false;
	}

	void Death()
	{
		Destroy (gameObject);
		//Particles confettis !
	}
}
