using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour {

	static AudioSource SoundSource;
	static AudioSource MusicSource;
	static Slider Soundslider;
	static Slider Musicslider;
	// Use this for initialization
	void Awake () {
		SoundSource = GameObject.Find("Sfx Source").GetComponent<AudioSource>();
		MusicSource = GameObject.Find("Music Source").GetComponent<AudioSource>();
		Soundslider = GameObject.Find("SoundSlider").GetComponent<Slider>();
		Musicslider = GameObject.Find("MusicSlider").GetComponent<Slider>();
		Soundslider.value = SoundSource.volume;
		Musicslider.value = MusicSource.volume;
	}
	
	// Update is called once per frame


	public void SlideSound(){
		SoundSource.volume = Soundslider.value;
	}
	public void SlideMusic(){
		MusicSource.volume = Musicslider.value;
	}
}
