using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxHandler : MonoBehaviour
{
	public AudioSource source;
    public AudioSource slideSource;
	public AudioClip[] fragile;
    public AudioClip[] icarus_Blow;
    public AudioClip pedro_Hit;
    public AudioClip[] wall_Hit;
    public AudioClip[] icarus_Sound;
    public AudioClip hole_Sound;
    public AudioClip victory_Sound;
    public AudioClip victory_Short;
    public AudioClip seed_Pop;
    static AudioSource[] MusicSource;
    public bool playingVictory;
    public bool playingVictory2;
    public bool inIt;
    public AudioClip grabSound;
    public AudioClip dropSound;
    public AudioClip draggingSound;
    public AudioClip[] slideSounds;
    public AudioClip[] flowerOpen;
    public AudioClip[] flowerClose;
    public AudioClip[] flowerChomp;

    public static bool isFlowerOpen;

    static public SfxHandler Instance;
    // Start is called before the first frame update
    void Awake(){
        if(Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this.gameObject);
            MusicSource = GameObject.Find("Music Source").GetComponents<AudioSource>();
            isFlowerOpen = false;
            return;
        }
        Destroy(this.gameObject);
    }

    public void PlayWallHit(int x, int y){
//        Debug.Log(x + " " + y);
        //source.PlayOneShot(pedro_Hit, 1f);

        string typeOfWall = FindType(x,y);
//        Debug.Log(typeOfWall + " IS THE HIT");
        switch(typeOfWall){
            case "Wall":
                source.PlayOneShot(pedro_Hit, .8f);
                break;
            case null:
//                Debug.Log("NULL");
                source.PlayOneShot(wall_Hit[Random.Range(0,wall_Hit.Length)], .65f);
                break;
            case "Right":
                if(Random.Range(0,10) > 7){
                    source.PlayOneShot(icarus_Sound[Random.Range(0,icarus_Sound.Length)], 1f);    
                }
                source.PlayOneShot(pedro_Hit, 1f);
                
                break;
            case "Left":
                if(Random.Range(0,10) > 7){
                    source.PlayOneShot(icarus_Sound[Random.Range(0,icarus_Sound.Length)], 1f);    
                }
                source.PlayOneShot(pedro_Hit, 1f);
                break;
            case "Up":
                if(Random.Range(0,10) > 7){
                    source.PlayOneShot(icarus_Sound[Random.Range(0,icarus_Sound.Length)], 1f);    
                }
                source.PlayOneShot(pedro_Hit, 1f);
                break;
            case "Down":
                if(Random.Range(0,10) > 7){
                    source.PlayOneShot(icarus_Sound[Random.Range(0,icarus_Sound.Length)], 1f);    
                }
                source.PlayOneShot(pedro_Hit, 1f);
                break;

        }


    }

    string FindType(int x, int y){

        if(x>=0 && x <LevelBuilder.totaldimension && 
            y>=0 && y < LevelBuilder.totaldimension){
            return LevelManager.placedPieces[x,y];
        }

        else
        return null;
    }

    public void PlayFlowerOpen(){
        if(!isFlowerOpen){
        source.PlayOneShot(flowerOpen[Random.Range(0,flowerOpen.Length)], .6f);
        isFlowerOpen = true;
        //StartCoroutine(OpenTimer());            
        }
        //Debug.Log("Open");
    }
    public void PlayFlowerClose(){
        if(isFlowerOpen){
            source.PlayOneShot(flowerClose[Random.Range(0,flowerClose.Length)], .6f);
            isFlowerOpen = false;
           //StartCoroutine(CloseTimer());
        }
        //Debug.Log("Close");
    
    }
    public void PlayFragile(){
        source.PlayOneShot(fragile[Random.Range(0,fragile.Length)], .7f);
    }
    public void PlayIcarus(){
        //Debug.Log("ICARIRIRI");
        source.PlayOneShot(icarus_Blow[Random.Range(0,icarus_Blow.Length)], 1);
    }
    public void PlayHole(){
        source.PlayOneShot(hole_Sound, .2f);

        //StartCoroutine(Duck(.1f, 1f));
    }
    public void PlayChomp(){
        source.PlayOneShot(flowerChomp[Random.Range(0 , flowerChomp.Length)], 1f);
    }
    public void PlaySeedPop(){
        source.PlayOneShot(seed_Pop, .6f);
    }

    // private IEnumerator CloseTimer(){
    //     yield return new WaitForSeconds(.2f);
    //     playingClose = false;
    // }

    // private IEnumerator OpenTimer(){
    //     yield return new WaitForSeconds(.2f);
    //     playingOpen = false;
    // }

    public void PlayVictory(){
        if(!playingVictory){
            // DuckMusic(.3f);
            // source.PlayOneShot(vicotry_Short,1f);
            // playingVictory = true;
            // StartCoroutine(Reseter(2));
            DuckMusic(1);
            if(Random.Range(0,10) > 8)
            source.PlayOneShot(victory_Sound,.6f);
            else
            source.PlayOneShot(victory_Short,1f);
            playingVictory = true;
            StartCoroutine(Reseter(6));
            inIt = true;
            StartCoroutine(InThisReseter(2));
            return;
        }
        else if(!playingVictory2 && !inIt){
            source.PlayOneShot(victory_Short,1f);
            playingVictory2 = true;
            StartCoroutine(Reseter2(2));
        }
    }
    public void PlaySlide(){
        AudioClip clip = slideSounds[Random.Range(0, slideSounds.Length)];
        slideSource.loop = false;
        slideSource.clip = clip;
        slideSource.Play();
    }
    public void PickUp(){
        source.PlayOneShot(grabSound, 1f);
    }
    public void Drop(){
        source.PlayOneShot(dropSound, 1f);
    }
    public void Drag(){
        source.PlayOneShot(draggingSound, 1f);
    }
    public void StopSlide(){
        //StartCoroutine(FadeOut(slideSource, .02f));
        slideSource.Stop();
    }
    public void StopSlideVictory(){
        StartCoroutine(DelayAndStop());
    }
    public IEnumerator DelayAndStop(){
        yield return new WaitForSeconds(.4f);
        //StartCoroutine(FadeOut(slideSource, .02f));
        slideSource.Stop();
    }
    private void DuckMusic(float time){
        StartCoroutine(Duck(time,time*2.5f));
    }
    private IEnumerator FadeOut(AudioSource source, float fadeouttime){
        float initvalue = source.volume;
        for(float i=0; i<fadeouttime; i+=Time.deltaTime){
            source.volume = Mathf.Lerp(initvalue, 0, i/fadeouttime);
            yield return null;
        }        
        source.volume = 0;
        source.Stop();
        source.volume = initvalue;
        yield return null;
    }

    private IEnumerator Duck(float fadeouttime, float fadeintime){
        //yield return new WaitForSeconds(.4f);
        float initvalue = MusicSource[0].volume;
        for(float i=0; i<fadeouttime; i+=Time.deltaTime){
            MusicSource[0].volume = Mathf.Lerp(initvalue, .7f*initvalue, i/fadeouttime);
            MusicSource[1].volume = Mathf.Lerp(initvalue, .7f*initvalue, i/fadeouttime);
            yield return null;
        }
        MusicSource[0].volume = 0;
        MusicSource[1].volume = 0;
        for(float i = 0; i<fadeintime; i+= Time.deltaTime){
            MusicSource[0].volume = Mathf.Lerp(.7f*initvalue, initvalue, i/fadeintime);
            MusicSource[1].volume = Mathf.Lerp(.7f*initvalue, initvalue, i/fadeintime);  
            yield return null;          
        }
            MusicSource[0].volume = initvalue;
            MusicSource[1].volume = initvalue;         
        yield return null;
    }
    public IEnumerator Reseter(int time){
        yield return new WaitForSeconds(time);
        playingVictory = false;
    }
    public IEnumerator Reseter2(int time){
        yield return new WaitForSeconds(time);
        playingVictory2 = false;
    }
    public IEnumerator InThisReseter(int time){
        yield return new WaitForSeconds(time);
        inIt = false;
    }
}
