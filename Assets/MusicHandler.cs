using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
	//public AudioClip titleTheme;
	public AudioSource source1;
    public AudioSource source2;
	public List<AudioClip> levelLoops;
    public AudioClip titleTheme;
    AudioSource lastSource;
    AudioSource newSource;
	public static MusicHandler thisMH;
    static bool calculatedNextTrack;
    static int entranceSample;
    static double lastBeginning;
    static int currentLoop;
    static double nextEventTime;
    static int flip;
    static int nextLoop;
    static bool running;
    // Start is called before the first frame update
    void Awake(){
		if(thisMH == null)
		{
			thisMH = this;
			DontDestroyOnLoad(this.gameObject);
			return;
		}
		Destroy(this.gameObject);
    }
    void Start()
    {
        flip = 0;
        running = false;
        //StartCoroutine(Count());
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(AudioSettings.dspTime);
        /*if(lastSource!= null || lastSource!= titleSource)
        WaitForTrigger(lastSource); //calculates nextTrack and sets bool on.

        if(calculatedNextTrack)
        PlayWhenReady(entranceSample, newSource);//plays nextrack in time and sets bool off.

        if(!lastSource.isPlaying && lastSource!=titleSource){// once previous audio ends last/new source are updated
            lastSource = newSource;
            newSource = null;
        }
        if(lastSource!= null && newSource!= null)
        Debug.Log(lastSource.clip + " " + newSource.clip);*/
        if(!running){
            return;
        }
        double time  = AudioSettings.dspTime;
        if(time +1.0f > nextEventTime){
           // Debug.Log("Time " + time  + " nextevent at " + nextEventTime);
            if(flip == 0){
                source2.clip = levelLoops[nextLoop-1];
                source2.PlayScheduled(nextEventTime);
                lastBeginning = nextEventTime;
                currentLoop = nextLoop;
                FindNewLoopAndTime();

            }
            if(flip == 1){
                source1.clip = levelLoops[nextLoop-1];
                source1.PlayScheduled(nextEventTime);
                lastBeginning = nextEventTime;
                currentLoop = nextLoop;
                FindNewLoopAndTime();
            }
            flip = 1 -flip;
        }

    }

    /*public AudioSource AssignNewSource(int num){
        switch(num){
            case 1:
                return loop1Source;
                break;
            case 2:
                return loop2Source;
                break;
            case 3:
                return loop3Source;
                break;
            default:
                return null;
                break;
        }
    }*/
    public void ScheduleNextLoop(){

    }
    public static void PlayTitleTheme(){
//    	thisMH.titleSource.clip = thisMH.titleTheme;
        thisMH.source1.Stop();
        thisMH.source2.Stop();
        thisMH.source1.clip = thisMH.titleTheme;
        thisMH.source1.loop = true;
    	thisMH.source1.Play();
        //thisMH.lastSource = thisMH.titleSource;
    }
    public static void PlayInitialLoop(){
    	//thisMH.loop1Source.clip = thisMH.levelLoops[num];
        thisMH.source1.loop =false;
        thisMH.source1.Stop();
        thisMH.source2.Stop();
    	thisMH.source1.clip = thisMH.levelLoops[0];
        thisMH.source1.Play();
        lastBeginning = AudioSettings.dspTime;
        //thisMH.lastSource = thisMH.source1;
        //lastBeginning = AudioSettings.dspTime;
        currentLoop = 1;
        thisMH.FindNewLoopAndTime();
        running = true;
    }
    public void FindNewLoopAndTime(){
        nextLoop = Random.Range(1,4);
        int measureToEnter = 64;
        Debug.Log(currentLoop + " is current and " + nextLoop + " is next.");
        if(currentLoop!=1){
            measureToEnter = measureToEnter +2;
        }
        if(nextLoop!= 1){
            measureToEnter = measureToEnter -2;
        }
        nextEventTime = lastBeginning+ ((60f/140f)*(measureToEnter*3));
        Debug.Log(lastBeginning);
        Debug.Log(nextEventTime);
        //double otherNextEventTime = (double)lastBeginning+((double)measureToEnter*(double)1.28571428571);
       // Debug.Log(nextEventTime +  otherNextEventTime);
    }
    /*public void PlayWhenReady(int entrance, AudioSource audioToPlay){
        Debug.Log("Waiting to play");
//        Debug.Log(lastSource.timeSamples + " " + entranceSample);
        if(lastSource.timeSamples>entranceSample && calculatedNextTrack){
            calculatedNextTrack =false;
            Debug.Log("Gonna play new");
            audioToPlay.Play();
        }
    }
    public int IndexCurrent(){
        if(loop1Source.isPlaying){
            return 1;
        }
        if(loop2Source.isPlaying){
            return 2;
        }
        if(loop3Source.isPlaying){
            return 3;
        }
        else{
            return 0;
        }
    }
    public void WaitForTrigger(AudioSource currentPlaying){
        Debug.Log(lastSource.clip);
        if(currentPlaying.timeSamples>(48000*60) && !calculatedNextTrack){
            calculatedNextTrack = true;
            Debug.Log("ABOVE");
            FindNewLoopAndTime();
        }        
    }*/
    public IEnumerator Count(float time){
        for(float t = 0.0f; t<1.0f; t+= Time.deltaTime / 50){
            Debug.Log(t);
          yield return null;
        }
    }
}
