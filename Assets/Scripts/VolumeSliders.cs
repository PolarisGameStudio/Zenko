using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour {

	static AudioSource[] SoundSource;
	static AudioSource[] MusicSource;
	static Slider Soundslider;
	static Slider Musicslider;
	// Use this for initialization
	void Awake () {
		SoundSource = GameObject.Find("Sfx Source").GetComponents<AudioSource>();

		MusicSource = GameObject.Find("Music Source").GetComponents<AudioSource>();
		Soundslider = GameObject.Find("SoundSlider").GetComponent<Slider>();
		Musicslider = GameObject.Find("MusicSlider").GetComponent<Slider>();
		//MusicSlider = GameObject.Find("Musicslider").GetComponent<Slider
		Soundslider.value = SoundSource[0].volume/.38f;
		Musicslider.value = MusicSource[0].volume/.38f;
	}
	
	// Update is called once per frame


	public void SlideSound(){
		SoundSource[0].volume = Soundslider.value;
		SoundSource[1].volume = Soundslider.value;
	}
	public void SlideMusic(){
		MusicSource[0].volume = Musicslider.value*.38f;
		MusicSource[1].volume = Musicslider.value*.38f;
	}
}
