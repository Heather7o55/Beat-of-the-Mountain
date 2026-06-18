using System.Collections.Generic;
using UnityEngine;

public class SignSpawner : MonoBehaviour
{
    public RhythmManager rhythmManager;
    public int lastBeat;
    public GameObject leftTurn;
    public GameObject rightTurn;
    public GameObject stop;
    public GameObject spawn;
    public GameObject destroy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!rhythmManager.activeSong.active) return;
        SpawnSigns();
    }
    private void SpawnSigns()
    {
        if(lastBeat == rhythmManager.currentBeat) return;
        if(rhythmManager.OnBeatPerfect(new Beat(rhythmManager.currentBeat + 5, 2)))
        {
            var i = Instantiate(leftTurn);
            i.GetComponent<SignRenderer>().rhythmManager = rhythmManager;
            i.GetComponent<SignRenderer>().spawn = spawn;
            i.GetComponent<SignRenderer>().destroy = destroy;
        }
        if(rhythmManager.OnBeatPerfect(new Beat(rhythmManager.currentBeat + 5, 4)))
        {
            var i = Instantiate(rightTurn);
            i.GetComponent<SignRenderer>().rhythmManager = rhythmManager;
            i.GetComponent<SignRenderer>().spawn = spawn;
            i.GetComponent<SignRenderer>().destroy = destroy;
        }
        if(rhythmManager.OnBeatPerfect(new Beat(rhythmManager.currentBeat + 5, 3)))
        {
            var i = Instantiate(stop);
            i.GetComponent<SignRenderer>().rhythmManager = rhythmManager;
            i.GetComponent<SignRenderer>().spawn = spawn;
            i.GetComponent<SignRenderer>().destroy = destroy;
        }
        lastBeat = rhythmManager.currentBeat;
    }
}
