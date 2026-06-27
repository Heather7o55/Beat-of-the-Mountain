using UnityEngine;

public class RmpDial : MonoBehaviour
{
    public RhythmManager rhythmManager;
    public double t;
    public float dialZero = 222f;
    public float dialMax = 0f;
    public float dialRest = 118f;
    public RectTransform rectTransform;
    private bool forward = false;
    private float tmpRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        t += .5d * RhythmManager.TimeSinceLastBeat;
        tmpRotation = rectTransform.rotation.z;
        RhythmManager.OnBeatEvent.AddListener(EnableTaco);
    }

    // Update is called once per frame
    void Update()
    {
        if(!RhythmManager.activeSong.active) return;
        UpdateTaco();
    }
    private void UpdateTaco()
    {
        if(forward)
        {
            t += .6d * RhythmManager.SongDeltaTime;
            rectTransform.eulerAngles = new Vector3(0,0, Mathf.Lerp(tmpRotation, dialMax, (float) t));
        }
        else
        {
            if(rectTransform.rotation.eulerAngles.z  == dialRest) return;
            t += 2.5d * RhythmManager.SongDeltaTime;
            rectTransform.eulerAngles = new Vector3(0,0, Mathf.Lerp(tmpRotation, dialRest, (float) t));
        }
        if(t >= 1)
        {
            tmpRotation = rectTransform.rotation.eulerAngles.z;
            forward = false;
            t = 0;
        }
    }
    private void EnableTaco()
    {
        if(RhythmManager.OnBeatPerfect(new Beat(rhythmManager.currentBeat + 5, 1)))
        {
            forward = true;
            t = 0;
        }
    }
}
