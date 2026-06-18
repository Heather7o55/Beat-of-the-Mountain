using UnityEngine;

public class RmpDial : MonoBehaviour
{
    public RhythmManager rhythmManager;
    public double lastSongPosition;
    public double songDeltaTime;
    public double t;
    public float dialZero = 222f;
    public float dialMax = 0f;
    public float dialRest = 118f;
    public RectTransform rectTransform;
    public int lastBeat;
    private bool forward = false;
    private bool backward = false;
    private float tmpRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastSongPosition = rhythmManager.songPosition;
        t += .5d * (rhythmManager.songPosition - (rhythmManager.activeSong.beatLengthInSeconds * rhythmManager.currentBeat));
        
        tmpRotation = rectTransform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        if(!rhythmManager.activeSong.active) return;
        songDeltaTime = (float) (rhythmManager.songPosition - lastSongPosition);
        EnableTaco();
        UpdateTaco();
        lastSongPosition = rhythmManager.songPosition;
    }
    private void UpdateTaco()
    {
        if(forward)
        {
            t += .6d * songDeltaTime;
            rectTransform.eulerAngles = new Vector3(0,0, Mathf.Lerp(tmpRotation, dialMax, (float) t));
        }
        else
        {
            if(rectTransform.rotation.eulerAngles.z  == dialRest) return;
            t += 2.5d * songDeltaTime;
            rectTransform.eulerAngles = new Vector3(0,0, Mathf.Lerp(tmpRotation, dialRest, (float) t));
        }
        if(t >= 1)
        {
            tmpRotation = rectTransform.rotation.eulerAngles.z;
            backward = false;
            forward = false;
            t = 0;
        }
    }
    private void EnableTaco()
    {
        if(lastBeat == rhythmManager.currentBeat) return;
        if(rhythmManager.OnBeatPerfect(new Beat(rhythmManager.currentBeat + 5, 1)))
        {
            forward = true;
            t = 0;
        }
        lastBeat = rhythmManager.currentBeat;
    }
}
