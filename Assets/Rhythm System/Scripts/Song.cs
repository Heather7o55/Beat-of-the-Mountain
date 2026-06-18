using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
// Declaring this struct here outside the class allows it to be used across all scripts, quite useful.
[Serializable]
public struct Beat
{
    public int Position;
    public int Lane;
    // This is the struct constructor allowing you to do Beat tmp = new Beat(1,3) for example, making a bunch of code cleaner and more intuitive
    public Beat(int position, int lane)
    {
        Position = position;
        Lane = lane;
    }
    // These operators allow me to do simple comparison operators on a beat struct, making life way easier
    public static bool operator ==(Beat i, Beat j)
    {
        return i.Equals(j);
    }
    public static bool operator !=(Beat i, Beat j)
    {
        return !i.Equals(j);
    }
}
/* This is a public class, which is how we'll construct songs, classes can be serialized to json. 
Technically if we were being sensible this would be a scriptable object, however we want to learn to use json. 
(actually i don't know if scriptable object would be more applicable here, as they don't let your have helper functions within them i don't think, which would make the structure a little uglier) */
[Serializable]
public class Song
{
    // Honestly this active bool shouldnt be here, it makes no sense to have it within the song class, it should be within the rhythm manager, but extracting it out isn't worth it
    public bool active;
    public string songName;
    // Previously i was serializing the audio clip itself out to json, but the way unity does that is by giving a reference id, which is editor specific and broke down during compilation, 
    // to fix this i switched to a filepath, loaded from the unity resources folder which is baked in to compiled games, so i can load this from a local filepath at runtime
    public string audioFilepath;
    // There is normally a slight offset between the song and it actually starting beats, is worth having even if its normally 0.
    public float songAudioOffset;
    public int bpm;
    // This stores the length of a beat, this is useful for some visual timing calculations and a few other things, 
    // this was a concept taken from the original blog i based this system off
    public float beatLengthInSeconds;
    /* This might seem pointless as in theory you can just calculate this at runtime, but i figure having cached within the song data is just kinda useful, 
    also depending on how we use this we could stop the rhythm system of iterating over the song when we've got the last active beat. 
    (need to think about specific impl details as this could be interpreted in two ways, total song beats, or total beatmap beats) */
    public int totalBeats;
    /* This is actually how the beats are stored. The beat struct contains a lane and position int, these denote those things, 
    in the rhythm manager we "iterate" over the beat map and instantiate the visuals using this beat map, and we use it to ensure the player is hitting the correct beats. */
    public List<Beat> beatMap;
    
    // This is the song constructor, most of it is self explanatory
    public Song()
    {
        songName = "New Song";
        audioFilepath = "";
        songAudioOffset = 0f;
        bpm = 0;
        beatLengthInSeconds = 0f;
        totalBeats = 0;
        beatMap = new List<Beat>();
        active = false;
    }

    /* This function loads the song, pretty self explanatory. You pass a C# filepath, which is basically just a string, 
    and it loads the json string, converts it into a song object, then returns the song object. */
    public static Song LoadSong(string filepath)
    {
        string json;
        try
        {
            json = File.ReadAllText(filepath);
        }
        catch (IOException e)
        {
            Console.WriteLine("The song could not be read:");
            Console.WriteLine(e.Message);
            return null;
        }
        Song song = new Song();
        return song = JsonUtility.FromJson<Song>(json);
    }

    // This functions saves a song, you pass in the song object and a string filepath, converts the song data into json string then saves the json to that filepath.
    public static void SaveSong(Song song, string filepath)
    {
        string json = JsonUtility.ToJson(song, true);
        try
        {
            File.WriteAllText(filepath, json);
        }
        catch(IOException e)
        {
            Console.WriteLine("The song could not be saved:");
            Console.WriteLine(e.Message);
            return;
        }
    }
}