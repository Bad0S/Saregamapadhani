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
    public int combo;
    public PostProcessingProfile initial;
    public PostProcessingProfile transe;
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
        if (Input.anyKeyDown)
        {
            float tempSous = timeBetweenBeatsInSeconds - timeRBetweenBeats;
            if ((0f < tempSous && tempSous < 0.2f) || (0.54f < tempSous && tempSous < timeBetweenBeatsInSeconds))
            {
                print(tempSous);
                combo += Mathf.RoundToInt((1-tempSous)*100);
            }
        }
        if (combo > 500)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingBehaviour>().profile = transe;
        }
    }
}