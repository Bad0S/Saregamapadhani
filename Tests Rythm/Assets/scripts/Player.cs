using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	private Rigidbody2D body;
	public float MovSpeed;
	public GameObject Attaque1;
	public GameObject Attaque2;
	public GameObject Attaque3;
    public GameObject Esquive;
    public AudioClip BaseAttackSound;
	public AudioClip HalfCircleAttackSound;
	public AudioClip DashAttackSound;
	public AudioSource[] audioSource;
	private Animator anim;
	private SpriteRenderer render;
    private Vector2 déplacement;
	public bool canAttack;
	public Image LifeBar;

    // Use this for initialization
    void Start () 
	{
		body = GetComponent<Rigidbody2D> ();
		audioSource = GetComponents<AudioSource>();
		anim = GetComponent<Animator> ();
		render = GetComponent<SpriteRenderer> ();
		canAttack = true;
	}

    // Update is called once per frame
    void Update () 
	{
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            déplacement = new Vector2(Input.GetAxisRaw("Horizontal") * MovSpeed, Input.GetAxisRaw("Vertical") * MovSpeed);
            body.position += (déplacement);
            float angle = (Mathf.Atan2(Input.GetAxisRaw("Horizontal"), (Input.GetAxisRaw("Vertical"))) * -Mathf.Rad2Deg);
            body.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        Attack ();
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
		LifeBar.fillAmount = gameObject.GetComponent<health> ().life / 100;
	}
	void Attack()
	{
		if (canAttack == true) 
		{
			if (Input.GetButtonDown ("Fire1") == true) 
			{
				foreach (AudioSource source in audioSource) 
				{
					if (source.priority == 129) 
					{
						source.clip = BaseAttackSound;
						source.Play ();
					}
				}
				Instantiate (Attaque1, transform);
				StartCoroutine(CDAttack());
			}
			//si le joueur appuie sur le bouton d'attaque 1, il joue le son de cette attaque, et fait apparaître la hitbox de l'attaque
			if (Input.GetButtonDown ("Fire2") == true) 
			{
				foreach (AudioSource source in audioSource) 
				{
					if (source.priority == 129) 
					{
						source.clip = HalfCircleAttackSound;
						anim.SetTrigger ("MakeItPan");
						source.Play ();
					}
				}
				Instantiate (Attaque2, transform);
				StartCoroutine(CDAttack());
			}
			if (Input.GetButtonUp ("Fire2") == true) 
			{
				foreach (AudioSource source in audioSource) 
				{
					if (source.priority == 129) {
						source.panStereo = 0.0f;
					}
				}
			}
			//si le joueur appuie sur le bouton d'attaque 2, il joue le son de cette attaque, le fait passer de gauche à droite dans les oreilles avec une animation, et fait apparaître la hitbox de l'attaque

			if (Input.GetButtonDown ("Fire3") == true) 
			{
				foreach (AudioSource source in audioSource) 
				{
					if (source.priority == 129) 
					{
						source.clip = DashAttackSound;
						source.Play ();
					}
				}
				Instantiate (Attaque3, transform);
				StartCoroutine(CDAttack());
			}
			if (Input.GetButtonDown ("Jump") == true) 
			{
				Instantiate (Esquive, transform);
			}
		}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
		if (other.name == "EnigmaRoom") 
		{
			enigme.entered = true;
		}
		if (other.tag == "Enemy") 
		{
			gameObject.GetComponent<health>().Hurt(other.gameObject.GetComponent<health>().damage);
		}
    }

	IEnumerator CDAttack()
	{
		canAttack = false;
		yield return new WaitForSeconds (0.5f);
		canAttack = true;
	}
}
