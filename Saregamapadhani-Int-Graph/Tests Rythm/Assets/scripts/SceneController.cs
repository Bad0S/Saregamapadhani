using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    public List<GameObject> Blocs;
    public int Indexblocs;
    private GameObject BlocASpawner;
    private Vector3 BlocPos;
    private Transform PlayerTrans;
    public Transform ScrollCheck;
    public int MaxBlocs = 10;
    private List<GameObject> BlocsApparus;
    public int NbBlocsApparus;
    private Coroutine Intro;
    public float DelayIntro = 5f;
    public GameObject PlayerInit;
    private GameObject Player;
    public Vector3 InitPosPlayer;
    public bool PlayerHasSpawn = false;
    public GameObject BlocRien;
    public GameObject BlocBuddyIntroGauche;
    public GameObject BlocBuddyIntroDroite;
    public List<GameObject> BlocsDifficulté1;
    public List<GameObject> BlocsDifficulté2;
    public List<GameObject> BlocsDifficulté3;
    public List<GameObject> BlocsDifficulté4;
    public List<GameObject> BlocsDifficulté5;
    public List<GameObject> BlocsEnnemisRonde;
    public List<GameObject> BlocsEnnemisTeteChercheuse;
    public List<GameObject> BlocsFlameSpinner;
    public List<GameObject> BlocsDoubleLanceFlammes;
    public List<GameObject> BlocsTripleLanceFlammes;
    public List<GameObject> BlocsPiliers;
    public List<GameObject> BlocsBuddy;
    public List<GameObject> BlocsFakeBuddy;
    public GameObject BlocRivièreVanilla;

    private int countChunk = 0;

    // Use this for initialization
    void Awake()
    {
        BlocsApparus = new List<GameObject>();
        Blocs.AddRange(BlocsDifficulté1);
        Blocs.Add(BlocRien);
        Blocs.Add(BlocsBuddy[6]);
        Instantiate(BlocBuddyIntroGauche, new Vector3(0, 169, 0), Quaternion.identity);
        Instantiate(BlocBuddyIntroDroite, new Vector3(0, 313, 0), Quaternion.identity);
        Instantiate(BlocBuddyIntroGauche, new Vector3(0, 457, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHasSpawn)
        {

            Player = GameObject.FindGameObjectWithTag("Player");
            PlayerTrans = Player.GetComponent<Transform>();
            PlayerTrans = Player.transform;

            if (PlayerTrans.position.y > ScrollCheck.position.y)
            {
                Indexblocs = Random.Range(0, Blocs.Count);
                BlocASpawner = Blocs[Indexblocs];
                BlocPos = new Vector3(0, (int)transform.position.y + 288, 0);
                GameObject chunk = Instantiate(BlocASpawner, BlocPos, Quaternion.identity);
                BlocsApparus.Add(chunk);
                transform.position += new Vector3(0, 144, 0);
                NbBlocsApparus++;
                countChunk++;
            }

            if (BlocsApparus.Count > MaxBlocs)
            {
                var tempDestroy = BlocsApparus[0];
                BlocsApparus.Remove(tempDestroy);
                Destroy(tempDestroy);
            }
            if (NbBlocsApparus == 20)
            {
                Blocs.Clear();
                Blocs.Add(BlocRivièreVanilla);
            }
            if (NbBlocsApparus == 21)
            {
                Blocs.Clear();
                Blocs.AddRange(BlocsDifficulté1);
                Blocs.AddRange(BlocsDifficulté2);
                Blocs.Add(BlocBuddyIntroDroite);
                Blocs.Add(BlocBuddyIntroGauche);
                Blocs.Add(BlocRien);
                Blocs.Add(BlocsBuddy[6]);
                Blocs.Add(BlocsBuddy[0]);
                Blocs.Add(BlocsBuddy[1]);
            }
        }
    }
}
