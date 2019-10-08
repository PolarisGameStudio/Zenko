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

    public bool playingVictory;

    static public SfxHandler Instance;
    // Start is called before the first frame update
    void Awake(){
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
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
        source.PlayOneShot(pedro_Hit, .7f);
    }
    public void PlayFragile(){
        source.PlayOneShot(fragile[Random.Range(0,fragile.Length)], .2f);
    }
    public void PlayIcarus(){
        source.PlayOneShot(icarus_Blow[Random.Range(0,icarus_Blow.Length)], 1);
    }
    public void PlayHole(){
        source.PlayOneShot(hole_Sound, .2f);

        DuckMusic(1);
    }
    public void PlaySeedPop(){
        source.PlayOneShot(seed_Pop, .4f);
    }
    public void PlayVictory(){
        if(!playingVictory){
            playingVictory = true;
            source.PlayOneShot(victory_Sound,.2f);
            StartCoroutine(Reseter());
            DuckMusic(2);

        }
    }
    private void DuckMusic(float time){

    }
    public IEnumerator Reseter(){
        yield return new WaitForSeconds(2);
        playingVictory = false;
    }
}
