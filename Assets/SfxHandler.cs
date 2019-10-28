using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxHandler : MonoBehaviour
{
	public AudioSource source;
	public AudioClip[] fragile;
    public AudioClip[] icarus_Blow;
    public AudioClip pedro_Hit;
    public AudioClip hole_Sound;
    public AudioClip victory_Sound;
    public AudioClip seed_Pop;
    static AudioSource[] MusicSource;
    public bool playingVictory;

    static public SfxHandler Instance;
    // Start is called before the first frame update
    void Awake(){
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            MusicSource = GameObject.Find("Music Source").GetComponents<AudioSource>();
            return;
        }
        Destroy(this.gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayWallHit(){
        source.PlayOneShot(pedro_Hit, 1f);
    }
    public void PlayFragile(){
        source.PlayOneShot(fragile[Random.Range(0,fragile.Length)], .2f);
    }
    public void PlayIcarus(){
        //Debug.Log("ICARIRIRI");
        source.PlayOneShot(icarus_Blow[Random.Range(0,icarus_Blow.Length)], 1);
    }
    public void PlayHole(){
        source.PlayOneShot(hole_Sound, .4f);

        StartCoroutine(Duck(.5f, 1f));
    }
    public void PlaySeedPop(){
        source.PlayOneShot(seed_Pop, .8f);
    }
    public void PlayVictory(){
        if(!playingVictory){
            DuckMusic(2);
            source.PlayOneShot(victory_Sound,.9f);
            playingVictory = true;
            StartCoroutine(Reseter());
        }
    }
    private void DuckMusic(float time){
        StartCoroutine(Duck(time,time*2));
    }
    private IEnumerator Duck(float fadeouttime, float fadeintime){
        float initvalue = MusicSource[0].volume;
        for(float i=0; i<fadeouttime; i+=Time.deltaTime){
            MusicSource[0].volume = Mathf.Lerp(initvalue, 0, i/fadeouttime);
            MusicSource[1].volume = Mathf.Lerp(initvalue, 0, i/fadeouttime);
            yield return null;
        }
        MusicSource[0].volume = 0;
        MusicSource[1].volume = 0;
        for(float i = 0; i<fadeintime; i+= Time.deltaTime){
            MusicSource[0].volume = Mathf.Lerp(0, initvalue, i/fadeintime);
            MusicSource[1].volume = Mathf.Lerp(0, initvalue, i/fadeintime);  
            yield return null;          
        }
            MusicSource[0].volume = initvalue;
            MusicSource[1].volume = initvalue;         
        yield return null;
    }
    public IEnumerator Reseter(){
        yield return new WaitForSeconds(6);
        playingVictory = false;
    }
}
