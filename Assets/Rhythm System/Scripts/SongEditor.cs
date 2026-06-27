using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using Unity.VisualScripting;
using JetBrains.Annotations;
// This class is an extension of rhythm manager, as most of the functions it needs to work are contained within that class, because i know how to write good fucking code.
public class SongEditorWindow : RhythmManager
{
    // This is for the visuals which represents, when beat's happen in playback mode.
    public GameObject[] visuals;
    public bool playbackMode = false;
    public int lastBeat;
    /* This actually probably wants to be an engine thing, rather than a window? The unity custom tool docs are bad and 
    i don't know if it's best suited to it.*/
    void Update()
    {
        if(activeSong == null) return;
        UpdateSongPosition();
        Debug.Log(currentBeat);
        Debug.Log(songPosition);
        if(playbackMode) 
            Playback();
        else if(RhythmKeyPressed()!= 0) 
            AddBeat(RhythmKeyPressed());
        
    }
    // This allows the rhythm manager to playback the charted song, so your can sure your chart is on beat
    public void Playback()
    {
        if(lastBeat == currentBeat) return;
        lastBeat = currentBeat;
        for(int i = 1; i <=4; i++)
        {
            if(OnBeatPerfect(new Beat(currentBeat, i))) 
            {
                var tmp = Instantiate(visuals[i], gameObject.transform);
                tmp.GetComponent<Tmpvisual>().destory(activeSong.beatLengthInSeconds * 0.7f);
            }
        }
    }
    public void AddBeat(int lane)
    {
        activeSong.beatMap.Add(new Beat(currentBeat, lane));
        Debug.Log("added beat");
    }
    // This function calculates the Beat Length In Seconds & Total Beats. I know its a confusing name, but i wasn't gonna type all that out
    public void CalculateBLISTB()
    {
        activeSong.beatLengthInSeconds = 60f / activeSong.bpm;
        activeSong.totalBeats = (int) (speaker.clip.length / activeSong.beatLengthInSeconds);
    }


}
