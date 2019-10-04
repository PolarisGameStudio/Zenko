using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxHandler : MonoBehaviour
{
	public AudioSource source;
	public AudioClip[] fragile;
	public AudioClip boop;
    public AudioClip icarus_Blow;
    public AudioClip pedro_Hit;

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

    public void PlayFragile(){
        source.PlayOneShot(fragile[Random.Range(0,fragile.Length)], .6f);
    }
    public void PlayIcarus(){
        source.PlayOneShot(icarus_Blow, .6f);
    }
}
