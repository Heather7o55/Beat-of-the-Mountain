using System.Collections.Generic;
using UnityEngine;

public class SignSpawner : MonoBehaviour
{
    public RhythmManager rhythmManager;
    public int lastBeat;
    // prefabs for the 3 types on signs, all assigned in editor
    public GameObject leftTurn;
    public GameObject rightTurn;
    public GameObject stop;
    // These aren't actually used here in the spawner, we just assign them to the sign renderer class when we instantiate the prefabs, 
    // they're used as position data for beat timing maths. 
    public GameObject spawn;
    public GameObject destroy;
    void Start()
    {
        RhythmManager.OnBeatEvent.AddListener(SpawnSigns);
    }
    void Update()
    {
        if(!RhythmManager.activeSong.active) return;
        SpawnSigns();
    }
    // This function spawns the signs on beat, with the left, right and stop signs being spawned when beats corresponding to the lanes are found
    // its worth noting it does this checks 5 beats ahead of the current beat, so it spawns them before those beats happen,
    // so we can move the signs into place on beat so the player knows which beats are coming up
    private void SpawnSigns()
    {
        if(RhythmManager.OnBeatPerfect(new Beat(rhythmManager.currentBeat + 5, 2)))
        {
            var i = Instantiate(leftTurn).GetComponent<SignRenderer>();
            i.spawn = spawn.transform.position.z;
            i.destroy = destroy.transform.position.z;
        }
        if(RhythmManager.OnBeatPerfect(new Beat(rhythmManager.currentBeat + 5, 4)))
        {
            var i = Instantiate(rightTurn).GetComponent<SignRenderer>();
            i.spawn = spawn.transform.position.z;
            i.destroy = destroy.transform.position.z;
        }
        if(RhythmManager.OnBeatPerfect(new Beat(rhythmManager.currentBeat + 5, 3)))
        {
            var i = Instantiate(stop).GetComponent<SignRenderer>();
            i.spawn = spawn.transform.position.z;
            i.destroy = destroy.transform.position.z;
        }
    }
}
