using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour {

	static AudioSource SoundSource;
	static AudioSource[] MusicSource;
	static Slider Soundslider;
	static Slider Musicslider;
	// Use this for initialization
	void Awake () {
		SoundSource = GameObject.Find("Sfx Source").GetComponent<AudioSource>();
		MusicSource = GameObject.Find("Music Source").GetComponents<AudioSource>();
		Soundslider = GameObject.Find("SoundSlider").GetComponent<Slider>();
		Musicslider = GameObject.Find("MusicSlider").GetComponent<Slider>();
		//MusicSlider = GameObject.Find("Musicslider").GetComponent<Slider
		Soundslider.value = SoundSource.volume;
		Musicslider.value = MusicSource[0].volume;

	}
	
	// Update is called once per frame


	public void SlideSound(){
		SoundSource.volume = Soundslider.value;
	}
	public void SlideMusic(){
		MusicSource[0].volume = Musicslider.value;
		MusicSource[1].volume = Musicslider.value;
	}
}
