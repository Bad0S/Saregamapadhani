using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialTutoManager : MonoBehaviour {
	public List<GameObject> dialogues;
	public bool secondDial=false;
	public bool thirdDial=false;
	public bool fourthDial=false;
	public bool fifthDial=false;
	public int dialCounter=0;
	public GameObject player;
	public GameObject dialTester;
	private bool isTalking;
	private float tempRange;
	private float tempRangeAttack;
	// Use this for initialization
	void Start () {
		dialogues[0].GetComponent<DialogueComponent>().StartDialogue();
		dialCounter++;
		GetComponent <Animator>().speed = 0.55f;
		tempRange = GetComponent <EnemyBehaviourTuto> ().maxDetectionRange;
		tempRangeAttack= GetComponent <EnemyBehaviourTuto> ().attackRangeMax;
	}

	// Update is called once per frame
	void Update () {
		isTalking = dialTester.activeSelf;
		if (isTalking == true){
			GetComponent <EnemyBehaviourTuto> ().maxDetectionRange = 0;
			GetComponent<EnemyBehaviourTuto> ().attackRangeMax = 0;
		}
		else{
			GetComponent <EnemyBehaviourTuto> ().maxDetectionRange = tempRange;
			GetComponent <EnemyBehaviourTuto> ().attackRangeMax = tempRangeAttack;

		}
		GetComponent <EnemyBehaviourTuto> ().idleCanMove = false;
		//les activations des attaques
		if(GetComponent<health>().life <8 && GetComponent<health>().life >6){
			thirdDial = true;
		}
		else if(GetComponent<health>().life <7){
			
			fourthDial = true;
		}

		if (fifthDial == false){
			GetComponent<health>().life = 8;
		}
		//l'activation de la repousse
		if(player.GetComponent <PlayerTuto>().attaqueRepousse.activeSelf == true && fifthDial == false && fourthDial == true){
			fifthDial = true;
		}



		//les dials
		if(secondDial == true && dialCounter == 1)
		{
			dialogues[1].GetComponent<DialogueComponent>().StartDialogue();
			dialCounter++;
		}
		else if(thirdDial == true && dialCounter == 2)
		{
			dialogues[2].GetComponent<DialogueComponent>().StartDialogue();
			dialCounter++;
		}
		else if(fourthDial == true && dialCounter == 3)
		{
			dialogues[3].GetComponent<DialogueComponent>().StartDialogue();
			dialCounter++;

		}
		else if(fifthDial == true && dialCounter == 4)
		{
			dialogues[4].GetComponent<DialogueComponent>().StartDialogue();
			dialCounter++;

		}
	}
}
