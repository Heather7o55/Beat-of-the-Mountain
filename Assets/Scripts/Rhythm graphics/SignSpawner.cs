using System.Collections.Generic;
using UnityEngine;

public class SignSpawner : MonoBehaviour
{
    public RhythmManager rhythmManager;
    public int lastBeat;
    public Transform spawn;
    public Transform destroy;
    public GameObject leftTurn;
    public GameObject rightTurn;
    public List<GameObject> signs = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!rhythmManager.activeSong.active) return;
        if(rhythmManager.OnBeatPerfect(new Beat(rhythmManager.currentBeat + 5, 2))) signs.Add(Instantiate(leftTurn));
        if(rhythmManager.OnBeatPerfect(new Beat(rhythmManager.currentBeat + 5, 4))) signs.Add(Instantiate(rightTurn));
        foreach(GameObject i in signs)
        {
            
        }
    }
}
