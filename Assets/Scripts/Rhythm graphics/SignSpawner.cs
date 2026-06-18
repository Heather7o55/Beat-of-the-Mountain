using System.Collections.Generic;
using UnityEngine;

public class SignSpawner : MonoBehaviour
{
    public RhythmManager rhythmManager;
    // This is so we dont endlessly spawn the same sign for the same beat
    public int lastBeat;
    // prefabs for the 3 types on signs, all assigned in editor
    public GameObject leftTurn;
    public GameObject rightTurn;
    public GameObject stop;
    // These aren't actually used here in the spawner, we just assign them to the sign renderer class when we instantiate the prefabs, they're used as position data for beat timing maths. 
    // These could just be floats as we only end up using the z value, but its too late now
    public GameObject spawn;
    public GameObject destroy;
    void Update()
    {
        if(!rhythmManager.activeSong.active) return;
        SpawnSigns();
    }
    // This function spawns the signs on beat, with the left, right and stop signs being spawned when beats corresponding to the lanes are found
    // its worth noting it does this checks 5 beats ahead of the current beat, so it spawns them before those beats happen,
    // so we can move the signs into place on beat so the player knows which beats are coming up
    private void SpawnSigns()
    {
        // This is so we dont endlessly spawn the same sign for the same beat
        if(lastBeat == rhythmManager.currentBeat) return;
        if(rhythmManager.OnBeatPerfect(new Beat(rhythmManager.currentBeat + 5, 2)))
        {
            // i know that doing get component here is a little expensive, but because of the way unity prefabs work, 
            // i couldnt assign these in editor, and this was the least expensive was i could think to do it at the time. 
            // however what i could be doing it creating another var and caching the sign renderer, then doing my operations (also it shouldnt be a var, but i didnt wanna type out GameObject)
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
