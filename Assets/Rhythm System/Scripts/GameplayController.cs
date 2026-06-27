using UnityEngine;
// To be honest this is kind of a pathetic class, its tiny, doesnt do much, 
// and because of time crunch a bunch of functions and variables which should be here, 
// have been put in places they just dont belong. just looking at this class kinda annoys me
public class GameplayController : RhythmManager
{
    public TMPro.TextMeshProUGUI text;
    public int score;
    // I put this in awake as for a period of time i was debugging what i thought was a race condition
    // but turned out not to be, this could be in start, but there isn't any major point in changing it
    void Awake()
    {
        LoadSong();
    }
    // The code here should be self explanatory
    void Update()
    {
        if(activeSong == null) return;
        UpdateSongPosition();
        if(RhythmKeyPressed() != 0)
        {
            if(OnBeatPerfect(new Beat(currentBeat,RhythmKeyPressed())))
            {
                score += 100;
            }
            else if(OnBeat(new Beat(currentBeat,RhythmKeyPressed())))
            {
                score += 50;
            }

        }
        text.text = $"Score: {score}";
    }
}
