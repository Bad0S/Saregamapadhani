using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	//Axel's pathifinding 
	private Animator anim;
	[SerializeField]private float speed;
	//[SerializeField]private GameObject attackHitbox;
	[SerializeField]private float maxDetectionRange;
	[SerializeField]private float attackRangeMax;
	private ContactFilter2D cFilter; 
	private Collider2D[] resultings = new Collider2D[1];

	private Rigidbody2D rb2D;
	public GameObject target;
	Vector3 targetVector;
	private Vector3 targetVectorAttacking;
	public bool isFighting = false ;
	[SerializeField]private int health;

	//arrêt du saut et saut
	bool isJumping;
	public float vitesseBond =1;
	public float timerWaitRepousse=0;

	//Idle
	bool idleCanMove;
	public bool idling;

	int damage =1;

	//le grab
	bool canGrab;
	public float grabberPercentage;
	private SpriteRenderer enemyRenderer;
	public bool isGrabbing;
	public float speedBurst;
	public float timeBursting = 1;
	float timerGrabbing;
	public bool grabbed;

    public List<AudioClip> groupieClips;
    private AudioSource groupieSource;

	//ATTENTION IL VA ATTAQUER
	private Shader shaderDeCouleur;
	private Shader shaderDeBase;
	private Color couleurDeBase;
	private Color couleurRougeShader;
	private Color couleurBlancheShader;
	public float opacityShader=0f;
	private SpriteRenderer playerVictimeRend;
	private Rigidbody2D playerRB;

	public bool aEteRepousse;

	// le rythme
	public bool beatAllowAttack;
	public Rythme rythmeScript;
	private int rythmeRange;
	public int rythmeRangeMax;
	private int counterRythme;

    void Start ()
    {
		rythmeRange = Random.Range (1, rythmeRangeMax +1);
		print (rythmeRange); 
		rythmeScript = target.GetComponent <Rythme> ();
		anim = GetComponent<Animator> ();
		rb2D = GetComponent<Rigidbody2D> ();
		//target = GameObject.FindGameObjectWithTag ("Player");

		shaderDeCouleur = Shader.Find("GUI/Text Shader");
		shaderDeBase = Shader.Find("Sprites/Default"); // or whatever sprite shader is being used
		enemyRenderer = GetComponent<SpriteRenderer> ();

		if((Random.Range(0f,1f))*100< grabberPercentage){
			canGrab = true;
			enemyRenderer.color = Color.red;
			transform.localScale= new Vector3(1.2f,1.2f,1);
		}
        groupieSource = GetComponent<AudioSource>();
		couleurDeBase = enemyRenderer.color;
		playerVictimeRend= target.GetComponent <SpriteRenderer>();
		playerRB= target.GetComponent <Rigidbody2D>();

	}

	void Update () 
	{
		beatAllowAttack = rythmeScript.isBeating;
		/*if (beatAllowAttack == true){
			if(counterRythme <= rythmeRangeMax){

				if(counterRythme != rythmeRange){
					beatAllowAttack = false;
				}
				counterRythme++;

			}
			else{
				counterRythme = 1;
			}
		}*/

		if (grabbed == true) 
		{
			GetComponent<BoxCollider2D> ().isTrigger = true;
		}

		if(rb2D.velocity.y <0){
			anim.SetFloat ("YSpeed",1);
		}
		else if (rb2D.velocity.y>0){
			anim.SetFloat ("YSpeed",-1);

		}
		timerGrabbing += Time.deltaTime;
		timerWaitRepousse += Time.deltaTime;
		if (timerWaitRepousse>0.35f && aEteRepousse == true){
			isFighting = false;
			aEteRepousse = false;
		}
		targetVector = target.transform.position -transform.position;

		//GRAB
		if (grabbed ==false){
			
			if (targetVector.magnitude < attackRangeMax  && isFighting == false&&beatAllowAttack == true) {
				if (canGrab == true){
					if (isGrabbing == false&&timerGrabbing> timeBursting+ 2){
						StartCoroutine ("GrabSequence");

					}
				}
				else if(aEteRepousse == false){
					StartCoroutine ("FightSequence");
				}
			}
		}
		else{
			transform.position = target.transform.position;
		}
		//PATHFINDING
		if (targetVector.magnitude < maxDetectionRange && isFighting == false) 
		{
			if (idling == true) 
			{
				StopCoroutine("MoveAndWait" ); 
			}

			anim.SetBool ("IsMoving", true);
			if (timerWaitRepousse > 0.35f && aEteRepousse == false) {
				rb2D.velocity = Vector3.Normalize (targetVector) * speed;
			}
			else if(aEteRepousse = false){
				aEteRepousse = false;
				timerWaitRepousse = 0;
			}
			//anim.SetBool ("Walking", true);
			if (targetVector.x > 0) {
				GetComponent<SpriteRenderer> ().flipX = true;
			} else {
				GetComponent<SpriteRenderer> ().flipX = false;
			}

	} else {
			//anim.SetBool("Walking",false);
			if (idleCanMove == true && isFighting == false)
			{
				StartCoroutine(MoveAndWait(Random.Range(0.7f,1.2f),Random.Range(1f,2f)));
			}
			else if (idling == false)
			{
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
		speed*= speedBurst ;
		yield return new WaitForSeconds (timeBursting);
		speed /= speedBurst;
		isGrabbing = false;
		timerGrabbing = 0;
	}

	void WhiteSprite() {
		enemyRenderer.material.shader = shaderDeCouleur;
		enemyRenderer.color = Color.white;
	}

	void RedSprite() {
		enemyRenderer.material.shader = shaderDeCouleur;
		enemyRenderer.color = Color.red;

	}

	void NormalSprite() {
		enemyRenderer.material.shader = shaderDeBase;
		enemyRenderer.color = couleurDeBase;
	}

	IEnumerator FightSequence()
	{
		yield return new WaitForSeconds (0.20f);
		WhiteSprite ();
		isFighting = true;
		yield return new WaitForSeconds (0.16f);
		NormalSprite ();
		yield return new WaitForSeconds (0.14f);
		anim.SetBool ("IsAttacking", true);
		targetVectorAttacking = targetVector;
		//anim.SetTrigger ("Fighting");
		rb2D.velocity = new Vector3 (0,0,0);
		isJumping = true;
		rb2D.AddForce (targetVectorAttacking * vitesseBond, ForceMode2D.Impulse);
		//Instantiate (attackHitbox, transform);
		yield return new WaitForSeconds (0.36f);
		isJumping = false;
		yield return new WaitForSeconds (0.64f);
		isFighting = false;
		anim.SetBool ("IsAttacking", false);
	}

	private void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player") 
		{
			if(isJumping == true){
				other.gameObject.GetComponent<health>().Hurt(damage);
				StartCoroutine (PlayerDamage ());

			}
			else if(isGrabbing ==true){
				GetComponent<BoxCollider2D> ().isTrigger = true;
				grabbed = true;
				target.GetComponent <Player> ().grabbed=true;
				target.GetComponent<Player> ().GrabUngrab ();
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "PlayerAttack") 
		{
			StartCoroutine (Knockback ());
			if(grabbed == true)
			{
				GameObject.FindGameObjectWithTag ("Player").GetComponent <Player>().grabbed = false ;
				GameObject.FindGameObjectWithTag ("Player").GetComponent <Player>().GrabUngrab () ;

			}
		}

		if (other.name == "Attaque_Repousse") 
		{
			timerWaitRepousse = 0;
			aEteRepousse = true;
			StopCoroutine ("FightSequence");
			if(!canGrab)
				NormalSprite ();
			isJumping = false;
			if(isGrabbing){
				StopCoroutine ("GrabSequence");
				speed /= speedBurst;
				isGrabbing = false;
				timerGrabbing = 0;
			}
			anim.SetBool ("IsAttacking", false);
			GetComponent<BoxCollider2D> ().isTrigger = false;
			rb2D.velocity = Vector2.zero;
			rb2D.AddForce (new Vector2(-targetVector.x,-targetVector.y).normalized*60,ForceMode2D.Impulse);
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

	IEnumerator Knockback(){
		timerWaitRepousse = 0;
		aEteRepousse = true;
		StopCoroutine ("FightSequence");
		if(!canGrab)
			NormalSprite ();
		isJumping = false;
		if(isGrabbing){
			StopCoroutine ("GrabSequence");
			speed /= speedBurst;
			isGrabbing = false;
			timerGrabbing = 0;
		}
		RedSprite ();
		GetComponent<BoxCollider2D> ().isTrigger = false;
		rb2D.velocity = Vector2.zero;
		rb2D.AddForce (new Vector2(-targetVector.x,-targetVector.y).normalized*30f,ForceMode2D.Impulse);
		yield return new WaitForSeconds(0.07f);
		NormalSprite ();
		yield return new WaitForSeconds(0.07f);

	}

	IEnumerator PlayerDamage(){
		playerVictimeRend.material.shader = shaderDeCouleur;
		playerVictimeRend.color = Color.red;


		playerRB.velocity = Vector2.zero;
		playerRB.AddForce (new Vector2(targetVector.x,targetVector.y).normalized*18f,ForceMode2D.Impulse);
		yield return new WaitForSeconds(0.12f);
		playerVictimeRend.material.shader = shaderDeBase;
		playerVictimeRend.color = couleurDeBase;
		yield return new WaitForSeconds(0.07f);

	}

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
		
	//pour faire du wait prce que les timers ça va bien 5 minutes
	public void Wait(float seconds ){
		StartCoroutine(Waiting(seconds));
	}
	IEnumerator Waiting(float time){
		yield return new WaitForSeconds(time);
	}


}

