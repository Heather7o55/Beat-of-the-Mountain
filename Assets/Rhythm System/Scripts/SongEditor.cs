using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using Unity.VisualScripting;
using JetBrains.Annotations;
// Beginnings of a custom song editor within unity, useless for now, but have begun outlining.
// Unity's docs in this area are a little vague, so might take a bit before its working.
// Following this tutorial https://youtu.be/34736DHWzaI but will need a lot of adapting and further research.
public class SongEditorWindow : RhythmManager
{
    public GameObject[] visuals;
    public bool playbackMode = false;
    public int lastBeat;
    /* This actually probably wants to be an engine thing, rather than a window? The unity custom tool docs are bad and 
    i don't know if it's best suited to it.*/
    void Start()
    {
        LoadSong();
    }
    void Update()
    {
        if(activeSong == null) return;
        UpdateSongPosition();
        Debug.Log(currentBeat);
        Debug.Log(songPosition);
        if(playbackMode) 
            Playback();
        else if(RhythmKeyPressed() && GetLaneKey() != 0) 
            AddBeat(GetLaneKey());
        
    }
    public void Playback()
    {
        if(lastBeat == currentBeat) return;
        lastBeat = currentBeat;
        for(int i = 1; i <=4; i++)
        {
            if(OnBeatPerfect(currentBeat, i)) 
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
    public void CalculateBLISTB()
    {
        activeSong.beatLengthInSeconds = 60f / activeSong.bpm;
        activeSong.totalBeats = (int) (activeSong.songAudio.length / activeSong.beatLengthInSeconds);
    }


}
