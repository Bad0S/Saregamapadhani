using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
	//Sound
	public AudioSource audioSource;

	//Visual
	private Animator anim;
	private Animator animAttaques;
	private SpriteRenderer render;
	private Shader shaderDeBase;


	//Move
    private Vector2 déplacement;
	public bool canMove = true;
	public float MovSpeed = 0.13f;

	//Physics
	private Rigidbody2D body;
	private Collider2D playerColl;

	//Fight
	public bool canAttack = true;
	public GameObject attaqueSlash;
	public GameObject attaqueRepousse;
	public GameObject dashTranscendanceTargeter;
	private bool isAttacking = false;
	private float chargeAttaque;

	//FightEnemies
	public bool grabbed;
	public float speedMultiplicator=1;

	//Dash
	public bool canDash = true;
    public float DashSpeed = 4;
	public Transform dashTarget;

	//Rythm
	public bool transcendance = false;

    // Use this for initialization
    void Start () 
	{
		body = GetComponent<Rigidbody2D> ();
		audioSource = GetComponent<AudioSource>();
		anim = GetComponent<Animator> ();
		animAttaques = GetComponentInChildren<Animator> ();
		render = GetComponent<SpriteRenderer> ();
		playerColl = GetComponent<Collider2D> ();
		MovSpeed = 0.13f;
		shaderDeBase = Shader.Find("Sprites/Default"); 
	}

    // Update is called once per frame
    void Update () 
	{
		//Le dash en transcendance
		dashTranscendanceTargeter.transform.localPosition = déplacement;
		dashTranscendanceTargeter.transform.localRotation = Quaternion.Euler (0, 0, ((Mathf.Atan2 (Input.GetAxisRaw ("Horizontal"), (Input.GetAxisRaw ("Vertical"))) * -Mathf.Rad2Deg)+90));
        Attack ();
	}

	void FixedUpdate()
	{
		Move ();
	}

	void Move()
	{
		if (canMove == true) 
		{
			if (Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0) 
			{
				déplacement = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
				body.position += (déplacement*MovSpeed*speedMultiplicator);
				anim.SetBool ("IsIdle", false);
				anim.SetBool ("IsMoving", true);
				//float angle = (Mathf.Atan2(Input.GetAxisRaw("Horizontal"), (Input.GetAxisRaw("Vertical"))) * -Mathf.Rad2Deg);
				//body.transform.rotation = Quaternion.Euler(0, 0, angle);
			} else 
			{
				anim.SetBool ("IsIdle", true);
				anim.SetBool ("IsMoving", false);
			}
			anim.SetFloat ("XSpeed", Input.GetAxisRaw ("Horizontal"));
			anim.SetFloat ("YSpeed", Input.GetAxisRaw ("Vertical"));
		}
	}

    void Attack()
    {
		if (isAttacking == false) 
		{
			attaqueSlash.transform.localPosition = déplacement;
			attaqueSlash.transform.localRotation = Quaternion.Euler (0, 0, (Mathf.Atan2 (Input.GetAxisRaw ("Horizontal"), (Input.GetAxisRaw ("Vertical"))) * -Mathf.Rad2Deg));
			attaqueRepousse.transform.localPosition = déplacement;
			attaqueRepousse.transform.localRotation = Quaternion.Euler (0, 0, (Mathf.Atan2 (Input.GetAxisRaw ("Horizontal"), (Input.GetAxisRaw ("Vertical"))) * -Mathf.Rad2Deg));
		}
        if (canAttack == true)
        {
			if (Input.GetButton ("Fire1")) 
			{
				chargeAttaque += Time.deltaTime;
			}
			if (Input.GetButtonUp ("Fire1") && isAttacking == false) 
			{
				anim.SetTrigger ("Attack_Slash");
				StartCoroutine(slashCoroutine ());
			}
			if (Input.GetButtonDown ("Fire2") && isAttacking == false) 
			{
				StartCoroutine (repousseCoroutine ());
			}

			if (transcendance == true) 
			{
				/*for (float i = 0f; i < 1f; i += 0.1f) 
				{
					RaycastHit2D hit = Physics2D.Raycast (transform.position, new Vector2(déplacement.x - 0.5f + i,déplacement.y), 6f);
					if (hit.collider != null && hit.collider.tag == "Enemy") 
					{
						dashTarget = hit.transform;
					}
				}*/
			}
			if(canDash==true){
				dashTranscendanceTargeter.transform.localPosition = déplacement;
				dashTranscendanceTargeter.transform.localRotation = Quaternion.Euler (0, 0, ((Mathf.Atan2 (Input.GetAxisRaw ("Horizontal"), (Input.GetAxisRaw ("Vertical"))) * -Mathf.Rad2Deg)+90));

				if (Input.GetButtonDown ("Fire3")&& déplacement != new Vector2(0f,0f)) 
				{
					StartCoroutine (dashCoroutine ());
				}
			}
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
		if (other.name == "Enemy") 
		{
			backUpNormal ();
		}
    }

	public void GrabUngrab()
	{
		if (grabbed == true) 
		{
			speedMultiplicator = 0.6f;	
		}
		else{
			speedMultiplicator = 1;
		}
	}
	IEnumerator backUpNormal(){
		yield return new WaitForSeconds (0.12f);
		render.material.shader = shaderDeBase;
	}

	IEnumerator slashCoroutine()
	{
		GetComponent<health> ().damage = Mathf.CeilToInt (chargeAttaque);
		attaqueSlash.SetActive (true);
		isAttacking = true;
		yield return new WaitForSeconds (0.333f);
		attaqueSlash.SetActive (false);
		isAttacking = false;
		chargeAttaque = 0f;
	}
	IEnumerator repousseCoroutine()
	{
		attaqueRepousse.SetActive (true);
		isAttacking = true;
		yield return new WaitForSeconds (0.5f);
		attaqueRepousse.SetActive (false);
		isAttacking = false;
	}

	IEnumerator dashCoroutine()
	{
		canDash = false;
	playerColl.enabled = false;
		if (transcendance == false||GetComponentInChildren <DashTranscendance> ().enemyList.Count == 0) 
		{
			body.AddForce (déplacement * 20f, ForceMode2D.Impulse);
		}
		else
		{
			Vector2 vecTmp = GetComponentInChildren <DashTranscendance> ().SelectEnemy (GetComponentInChildren <DashTranscendance> ().enemyList);
			body.AddForce ( vecTmp * 4.5f, ForceMode2D.Impulse);
		}
		yield return new WaitForSeconds (0.1f);
		playerColl.enabled = true;
		yield return new WaitForSeconds (0.9f);
		canDash = true;
	}
}
