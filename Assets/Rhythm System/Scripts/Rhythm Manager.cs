using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Linq;
// https://fizzd.me/posts/how-to-make-a-rhythm-game-a-quick-and-dirty-guide-to-setting-up-your-project FNF team and other's used this basic guide.
// Worth us both looking through I think.
public class RhythmManager : MonoBehaviour
{

    public int score;
    public Song activeSong = new Song();
    public float songPosition;
    public int currentBeat;
    public float dspDelay;
    public AudioSource speaker;
    public string LoadSongFilepath;
    public string SaveSongFilepath;

    public void UpdateSongPosition()
    {
        if(currentBeat >= activeSong.totalBeats) activeSong.active = false;
        if(!activeSong.active) return;
        songPosition = (float) (AudioSettings.dspTime -dspDelay) -activeSong.songAudioOffset;
        currentBeat = (int) (songPosition / activeSong.beatLengthInSeconds);
    }
    public void StartSong()
    {
        activeSong.active = true;
        speaker.clip = activeSong.songAudio;
        speaker.Play();
        dspDelay = (float) AudioSettings.dspTime;
    }
    public bool OnBeatPerfect(int position, int lane)
    {
        Beat beat = new Beat(position, lane);
        if(activeSong.beatMap.Any(tmp => tmp == beat))
        {
            return true;
        }
        return false;
    }
    public bool OnBeat(int position, int lane)
    {
        Beat beat = new Beat(position, lane);
        if(activeSong.beatMap.Any(tmp => tmp == new Beat(beat.Position +1, beat.Lane) || 
        activeSong.beatMap.Any(tmp => tmp == new Beat(beat.Position -1, beat.Lane))))
        {
            return true;
        }
        return false;
    }
    public bool BeatPulse(float lastBeat)
    {
        return songPosition > lastBeat + activeSong.beatLengthInSeconds;
    }
    public void LoadSong()
    {
        activeSong = Song.LoadSong(LoadSongFilepath);
    }
    public void SaveSong()
    {
        Song.SaveSong(activeSong,SaveSongFilepath);
    }
//ALL OF THIS IS BAD CODE, PLEASE REWRITE IT IN THE MORNING JESUS CHRIST
    public bool RhythmKeyPressed()
    {
        if(!activeSong.active) return false;
        if(Input.GetKeyDown("w")) return true;
        if(Input.GetKeyDown("a")) return true;
        if(Input.GetKeyDown("s")) return true;
        if(Input.GetKeyDown("d")) return true;
        return false;
    }
    public int GetLaneKey()
    {
        if(!activeSong.active) return 0;
        if(Input.GetKeyDown("w")) return 1;
        if(Input.GetKeyDown("a")) return 2;
        if(Input.GetKeyDown("s")) return 3;
        if(Input.GetKeyDown("d")) return 4;
        return 0;
    }
}