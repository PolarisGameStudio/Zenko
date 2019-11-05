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
    public static bool running;
    public static bool hasFocus;
    double time;

    void Awake()
    {
		if(thisMH == null)
		{
			thisMH = this;
			//DontDestroyOnLoad(this.gameObject);
            source1.volume = source1.volume*.38f;
            source2.volume = source2.volume*.38f;
			return;  
		}
		Destroy(this.gameObject);
    }

    void Start()
    {
        hasFocus = true;
        flip = 0;
        running = false;
    }
    void OnApplicationFocus(){
        hasFocus = !hasFocus;
        if(!hasFocus) 
        AudioListener.pause = true;



    }
    void Update()
    {
//        Debug.Log(AudioSettings.dspTime);
        if(AudioListener.pause == true){
            AudioListener.pause = false;
        }
        if(!running){
            return;
        }

        time  = AudioSettings.dspTime;

        if(time +1.0f > nextEventTime){
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
    public static void PlayTitleTheme()
    {
        thisMH.source1.Stop();
        thisMH.source2.Stop();
        thisMH.source1.clip = thisMH.titleTheme;
        thisMH.source1.loop = true;
    	thisMH.source1.Play();
        running = false;
    }
    public static void FindNewTiTle(){

        
    }
    public static void PlayInitialLoop()
    {
        thisMH.source1.loop =false;
        thisMH.source1.Stop();
        thisMH.source2.Stop();
        currentLoop = Random.Range(0,3);
    	thisMH.source1.clip = thisMH.levelLoops[currentLoop];
        thisMH.source1.Play();
        lastBeginning = AudioSettings.dspTime;
        thisMH.FindNewLoopAndTime();
        running = true;
    }
    public void FindNewLoopAndTime()
    {
        nextLoop = Random.Range(1,4);
        int measureToEnter = 64;
        Debug.Log(currentLoop + " is current and " + nextLoop + " is next.");
        if(currentLoop!=1)
        {
            measureToEnter = measureToEnter +2;
        }
        if(nextLoop!= 1)
        {
            measureToEnter = measureToEnter -2;
        }
        nextEventTime = lastBeginning+ ((60f/140f)*(measureToEnter*3));
    }
}
