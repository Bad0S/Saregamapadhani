using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private Rigidbody2D body;
	public float MovSpeed;
	public PlayerAttack Swing;
	public PlayerAttack AttaqueBase;
	public AudioClip BaseAttackSound;
	public AudioClip HalfCircleAttackSound;
	public AudioClip DashAttackSound;
    private float PlayerRot;
    public float DashSpeed;
    public bool isDashing;
	public AudioSource[] audioSource;
	private Animator anim;
	private SpriteRenderer render;
	public float dashTimer;
	public float dashCooldown;
	public float damageDash = 0.35f;

	// Use this for initialization
	void Start () 
	{
		body = GetComponent<Rigidbody2D> ();
		audioSource = GetComponents<AudioSource>();
		anim = GetComponent<Animator> ();
		render = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
        body.transform.rotation =Quaternion.Euler (0,0,PlayerRot);
		float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");
		body.velocity = new Vector2 (horizontal * MovSpeed, vertical * MovSpeed);
        if (body.velocity.x > 0) { PlayerRot = 270; }
        if (body.velocity.x < 0) { PlayerRot = 90; }
        if (body.velocity.y > 0) { PlayerRot = 0; }
        if (body.velocity.y < 0) { PlayerRot = 180; }
        Attack ();
		dashTimer += Time.deltaTime;
		if ((body.velocity.x != 0) || (body.velocity.y != 0))
		{
			foreach (AudioSource source in audioSource)
			{
				if (source.priority == 128)
				{
					source.mute = false;
				}
			}
		}
		else
		{
			foreach (AudioSource source in audioSource)
			{
				if (source.priority == 128)
				{
					source.mute = true;
				}
			}
		}
		if (Input.GetButton ("Jump") == true) 
		{

		}

		if (dashTimer >= dashCooldown) {render.color = Color.white;}
	}
	void Attack()
	{
		if (Input.GetButton ("Fire1") == true) 
		{
			foreach (AudioSource source in audioSource)
			{
				if (source.priority == 129)
				{
					source.clip = BaseAttackSound;
					source.Play();
				}
			}
			AttaqueBase.gameObject.SetActive(true);
			StartCoroutine(DisableObject(AttaqueBase.gameObject, .1f));
			return;
		}

		if (Input.GetButton ("Fire2") == true)
		{
			foreach (AudioSource source in audioSource)
			{
				if (source.priority == 129)
				{
					source.clip = HalfCircleAttackSound;
					anim.SetTrigger ("MakeItPan");
					source.Play();
				}
			}
			Swing.gameObject.SetActive(true);
			StartCoroutine(DisableObject(Swing.gameObject, .1f));
			return;
		}
		if (Input.GetButtonUp ("Fire2") == true)
		{
			foreach (AudioSource source in audioSource)
			{
				if (source.priority == 129)
				{
					source.panStereo = 0.0f;
				}
			}
		}

		if (Input.GetButtonDown ("Fire3") == true && dashTimer >= dashCooldown)
		{
			foreach (AudioSource source in audioSource)
			{
				if (source.priority == 129)
				{
					source.clip = DashAttackSound;
					source.Play();
				}
			}
			render.color = Color.yellow;
			transform.Translate(Vector3.up * Time.deltaTime * DashSpeed);
			isDashing = true;
			dashTimer = 0f;
		}
		if (Input.GetButtonUp ("Fire3") == true) { isDashing = false; }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag =="Enemy" && isDashing == true)
        {
			other.GetComponent <health>().Hurt(damageDash);
        }
		if (other.name == "EnigmaRoom")
			enigme.entered = true;
		else if (other.name == "EnemyMainRoom")
			EnemyMoveBehaviour.detected = true;
		else if(other.name == "EnemyAnnexRoom")
			EnemyMoveBehaviourAnnex.detectedAnnex = true;
		
    }
    IEnumerator DisableObject(GameObject obj, float time)
	{
		yield return new WaitForSeconds(time);
		obj.SetActive (false);
	}

}
