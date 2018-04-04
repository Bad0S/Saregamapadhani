using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Rythme : MonoBehaviour
{
    public float bpmInitial = 110;
    public float bpm;
    private AudioSource sourceSon;
    private float timeBetweenBeatsInSeconds;
    private float timeRBetweenBeats;
    private float musicTime;
    public int beats = 1;
    public float combo;
    public PostProcessingProfile initial;
    public PostProcessingProfile transe;
	public PostProcessingProfile Transcendance;
    // Use this for initialization
    void Start()
    {
        sourceSon = GetComponent<AudioSource>();
        bpm = bpmInitial;
    }

    // Update is called once per frame
    void Update()
    {
        musicTime += Time.deltaTime;
        timeRBetweenBeats += Time.deltaTime;
        bpm = bpmInitial * sourceSon.pitch;
        timeBetweenBeatsInSeconds = 60 / bpm;
		if (musicTime >= timeBetweenBeatsInSeconds * beats) 
		{
			beats += 1;
			timeRBetweenBeats = 0;
		}
		if (combo <= 0) 
		{
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingBehaviour>().profile = initial;
			//GameObject.FindGameObjectWithTag ("MainCamera").GetComponentInChildren<SpriteRenderer> ().enabled = false;
		}
		if (combo < 30 && combo > 0)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingBehaviour>().profile = transe;
			//GameObject.FindGameObjectWithTag ("MainCamera").GetComponentInChildren<SpriteRenderer> ().enabled = false;
			GetComponent<Player> ().MovSpeed = 0.13f + combo/600;
			sourceSon.pitch = 1 + combo / 300;
        }
		if (combo >= 30)
		{
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingBehaviour>().profile = Transcendance;
			//GameObject.FindGameObjectWithTag ("MainCamera").GetComponentInChildren<SpriteRenderer> ().enabled= true;
			GetComponent<Player> ().MovSpeed = 0.2f;
			GetComponent<Player> ().transcendance = true;
		}
    }
}