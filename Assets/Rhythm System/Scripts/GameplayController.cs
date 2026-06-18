using UnityEngine;

public class GameplayController : RhythmManager
{
    void Awake()
    {
        LoadSong();
    }

    // This function keeps the primary beat variables updated and in time/sync, it only runs when the song is active, and if the song is completed it sets the song to be inactive
    void Update()
    {
        if(activeSong == null) return;
        UpdateSongPosition();
        if(RhythmKeyPressed() && GetLaneKey() != 0)
        {
            if(OnBeatPerfect(new Beat(currentBeat,GetLaneKey())))
            {
                score += 100;
            }
            else if(OnBeat(new Beat(currentBeat,GetLaneKey())))
            {
                score += 50;
            }

        }
        text.text = $"Score: {score}";
    }
}
