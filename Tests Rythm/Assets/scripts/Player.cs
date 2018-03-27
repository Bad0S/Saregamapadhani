using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	private Rigidbody2D body;
	public float MovSpeed;
	public GameObject Attaque1;
	public GameObject Attaque2;
	public GameObject Attaque3;
    public GameObject Esquive;
	public AudioSource audioSource;
	private Animator anim;
	private SpriteRenderer render;
    private Vector2 déplacement;
	public bool canAttack;
    public int indexAttackSounds;
    public AudioClip attaque1;
    public AudioClip attaque2;
    public AudioClip attaqueUlt;
    public AudioClip evasion;
    public float dashTimer = 3f;
    public float DashSpeed = 4;
    public float dashCooldown = 3f;

	public bool grabbed;

    // Use this for initialization
    void Start () 
	{
		body = GetComponent<Rigidbody2D> ();
		audioSource = GetComponent<AudioSource>();
		anim = GetComponent<Animator> ();
		render = GetComponent<SpriteRenderer> ();
		canAttack = true;
	}

    // Update is called once per frame
    void Update () 
	{
        dashTimer += Time.deltaTime;
		if (Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0) {
			déplacement = new Vector2 (Input.GetAxisRaw ("Horizontal") * MovSpeed, Input.GetAxisRaw ("Vertical") * MovSpeed);
			body.position += (déplacement);
			anim.SetBool ("IsIdle", false);
			anim.SetBool ("IsMoving", true);
			//float angle = (Mathf.Atan2(Input.GetAxisRaw("Horizontal"), (Input.GetAxisRaw("Vertical"))) * -Mathf.Rad2Deg);
			//body.transform.rotation = Quaternion.Euler(0, 0, angle);
		} 
		else 
		{
			anim.SetBool ("IsIdle", true);
			anim.SetBool ("IsMoving", false);

		}
        Attack ();
		anim.SetFloat ("XSpeed", Input.GetAxisRaw ("Horizontal"));
		anim.SetFloat ("YSpeed", Input.GetAxisRaw ("Vertical"));
	}
    void Attack()
    {
        if (canAttack == true)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            if (Input.GetButtonDown("Fire1") == true)
            {
                audioSource.clip = attaque1;
                audioSource.Play();
                Instantiate(Attaque1, transform);
                StartCoroutine(CDAttack());
            }
            //si le joueur appuie sur le bouton d'attaque 1, il joue le son de cette attaque, et fait apparaître la hitbox de l'attaque
            if (Input.GetButtonDown("Fire2") == true)
            {
                audioSource.clip = attaque2;
                //anim.SetTrigger ("MakeItPan");
                audioSource.Play();
                Instantiate(Attaque2, transform);
                StartCoroutine(CDAttack());
                anim.SetTrigger("Attack_Slash");
            }
            if (Input.GetButtonUp("Fire2") == true)
            {
                audioSource.panStereo = 0.0f;
            }
            //si le joueur appuie sur le bouton d'attaque 2, il joue le son de cette attaque, le fait passer de gauche à droite dans les oreilles avec une animation, et fait apparaître la hitbox de l'attaque

            if (Input.GetButtonDown("Fire3") == true)
            {
                audioSource.clip = attaqueUlt;
                audioSource.Play();
                Instantiate(Attaque3, transform);
                StartCoroutine(CDAttack());
            }
            if (Input.GetButtonDown("Jump") == true && dashTimer >= dashCooldown)
            {
                audioSource.clip = evasion;
                audioSource.Play();
				if (xDashDirectionStick != 0.0f || yDashDirectionStick != 0.0f) 
				{
					directionDash = new Vector2 (xDashDirectionStick, yDashDirectionStick);
					body.AddForce (directionDash * forceDash, ForceMode2D.Impulse);
				}
				else{
					body.AddForce (Vector2.up * forceDash, ForceMode2D.Impulse);
				}
                dashTimer = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
		if (other.name == "EnigmaRoom") 
		{
			enigme.entered = true;
		}
    }

	public void GrabUngrab(){
		if (grabbed == true) {
			MovSpeed *= 0.6f;	
		}
		else{
			MovSpeed *= 1.66666f;
		}
	}

	IEnumerator CDAttack()
	{
		canAttack = false;
		yield return new WaitForSeconds (0.3f);
		canAttack = true;
	}
}
