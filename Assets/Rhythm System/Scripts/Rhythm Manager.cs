using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Linq;
// https://fizzd.me/posts/how-to-make-a-rhythm-game-a-quick-and-dirty-guide-to-setting-up-your-project FNF team and other's used this basic guide.
// Worth us both looking through I think.
public class RhythmManager : MonoBehaviour
{
    /* This shouldn't really be here, but as it stands, it was the easiest place to put it.
    This is the text for the score UI, it being here in the rhythm manager is a complete structural failure on my part
    but this game needed to be done like 2 weeks ago so here it is */
    public TMPro.TextMeshProUGUI text;
    // Another thing which shouldnt be here, this should be in gameplay controller... name is self explanatory, its the score of the player
    public int score;
    public Song activeSong = new Song();
    // Song position is seconds, but actually accurate unlike Unity
    public double songPosition;
    // Current beat in out of total beats
    public int currentBeat;
    // Delay between the audio system and Unity
    public double dspDelay;
    public AudioSource speaker;
    /* C# stores filepaths as strings, these allow me to set those filepaths from within the unity editor 
    this filepaths make use of the streaming assets system within unity, to allow them to be packaged within the compiled game. 
    Due to this i've changed the folder structure and these paramaters only need the file name, rather than path */
    public string LoadSongFileName;
    public string SaveSongFileName;
    // This double stores the previous song position for allowing songs to continue after pausing the game
    private double oldSongPosition;
    public void UpdateSongPosition()
    {
        if(currentBeat >= activeSong.totalBeats) activeSong.active = false;
        if(!activeSong.active) return;
        /* this might look a little odd, but because of old song position is 0 by default, always having it here in the equation makes sense. 
        as it has no impact if its not in use and saves having an ugly if else or ternary here*/
        songPosition = oldSongPosition + AudioSettings.dspTime -dspDelay -activeSong.songAudioOffset;
        currentBeat = (int) (songPosition / activeSong.beatLengthInSeconds);
        // Debug.LogError(songPosition);
        // Debug.LogError((AudioSettings.dspTime -dspDelay) -activeSong.songAudioOffset);
    }
    // This function Sets the song to active, starts playing the audio, and records the dsp-delay, which is the delay between the song playing and unity updating
    public void StartSong()
    {
        // This is mentioned in the song class, but i moved the song files into the resources folder so they can be loaded in at runtime, 
        // the json now just stores the path to the song rather than an audio clip, so the audio clip is loaded at runtime

        speaker.Play();
        dspDelay = (float) AudioSettings.dspTime;
        activeSong.active = true;
    }
    // This is used to pause the song, it pauses the active speaker, deactivates the song, and logs the current song position to be used when the song is unpaused, so it doesnt lose tracking
    public void PauseSong()
    {
        speaker.Pause();
        activeSong.active = false;
        oldSongPosition = songPosition;
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
    // Wrapper functions around the json load/save song utilities, due to folder structure changes needed for compiler file paths, 
    // these now use the streaming assets path, which unity stores, so the editor facing variable is just the json file name
    public void LoadSong()
    {
        activeSong = Song.LoadSong(System.IO.Path.Combine(Application.streamingAssetsPath, LoadSongFileName));
        speaker.clip = Resources.Load<AudioClip>(activeSong.audioFilepath);
    }
    public void SaveSong()
    {
        Song.SaveSong(activeSong,System.IO.Path.Combine(Application.streamingAssetsPath, SaveSongFileName));
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