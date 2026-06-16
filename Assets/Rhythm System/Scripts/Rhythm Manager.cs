using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Linq;
// https://fizzd.me/posts/how-to-make-a-rhythm-game-a-quick-and-dirty-guide-to-setting-up-your-project FNF team and other's used this basic guide.
// Worth us both looking through I think.
public class RhythmManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    public int score;
    public Song activeSong = new Song();
    // Song position is seconds, but actually accurate unlike Unity accurate
    public float songPosition;
    // Current beat in out of total beats
    public int currentBeat;
    // Delay between the audio system and Unity
    public float dspDelay;
    public AudioSource speaker;
    // C# stores filepaths as strings, these allow me to set those filepaths from within the unity editor
    public string LoadSongFilepath;
    public string SaveSongFilepath;

    // This function keeps the primary beat variables updated and in time/sync, it only runs when the song is active, and if the song is completed it sets the song to be inactive
    void Update()
    {
        if(activeSong == null) return;
        UpdateSongPosition();
        Debug.Log(currentBeat);
        Debug.Log(songPosition);
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
    public void UpdateSongPosition()
    {
        if(currentBeat >= activeSong.totalBeats) activeSong.active = false;
        if(!activeSong.active) return;
        songPosition = (float) (AudioSettings.dspTime -dspDelay) -activeSong.songAudioOffset;
        currentBeat = (int) (songPosition / activeSong.beatLengthInSeconds);
    }
    // This function Sets the song to active, starts playing the audio, and records the dsp-delay, which is the delay between the song playing and unity updating
    public void StartSong()
    {
        activeSong.active = true;
        speaker.clip = activeSong.songAudio;
        speaker.Play();
        dspDelay = (float) AudioSettings.dspTime;
    }
    // This detects if a beat passed to it is "the same" as the current beat in the beatmap, this is intended to be used for scoring, however it has uses in other places.
    public bool OnBeatPerfect(Beat beat)
    {
        if(activeSong.beatMap.Any(tmp => tmp == beat))
        {
            return true;
        }
        return false;
    }
    // This detects if a beat passed to it is "the same" as the current beat in the beatmap with a +1-1 margin of error, this is used for scoring
    public bool OnBeat(Beat beat)
    {
        if(activeSong.beatMap.Any(tmp => tmp == new Beat(beat.Position +1, beat.Lane) || 
        activeSong.beatMap.Any(tmp => tmp == new Beat(beat.Position -1, beat.Lane))))
        {
            return true;
        }
        return false;
    }
    // This function is intended to be used to ensure you can keep visuals and other things in time with the music/beat, this is an idea taken from the blog
    public bool BeatPulse(float lastBeat)
    {
        return songPosition > lastBeat + activeSong.beatLengthInSeconds;
    }
    // Wrapper functions around the json load/save song utilities
    public void LoadSong()
    {
        activeSong = Song.LoadSong(LoadSongFilepath);
    }
    public void SaveSong()
    {
        Song.SaveSong(activeSong,SaveSongFilepath);
    }
    //ALL OF THIS IS BAD CODE, PLEASE REWRITE IT IN THE MORNING JESUS CHRIST, its currently handling player input, but there has to be a cleaner way i swear
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