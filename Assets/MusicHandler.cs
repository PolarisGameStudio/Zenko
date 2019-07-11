using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
	public AudioClip titleTheme;
	public AudioSource source;
	public List<AudioClip> levelLoops;
    public int lastAudio;
	public static MusicHandler thisMH;
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
        //StartCoroutine(Count());
    }

    // Update is called once per frame
    void Update()
    {
//        Debug.Log(source.timeSamples);
  //      Debug.Log(titleTheme.frequency);
    }
    public static void PlayTitleTheme(){
    	thisMH.source.clip = thisMH.titleTheme;
    	thisMH.source.Play();
    }
    public static void PlayInitialLoop(int num){
    	thisMH.source.clip = thisMH.levelLoops[num];
    	thisMH.source.Play();

    }
    public IEnumerator Count(float time){

        for(float t = 0.0f; t<1.0f; t+= Time.deltaTime / 50){
            Debug.Log(t);
          yield return null;
        }

    }
    public void FindNewLoopAndTime(){

    }
}
