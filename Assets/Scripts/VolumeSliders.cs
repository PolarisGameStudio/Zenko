using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour {

	static AudioSource[] SoundSource;
	static AudioSource[] MusicSource;
	static Slider Soundslider;
	static Slider Musicslider;
	public GameObject[] textsToColor;
	public GameObject[] spritesToColor;
	public static VolumeSliders Instance;
	// Use this for initialization
	void Awake () {
		SoundSource = GameObject.Find("Sfx Source").GetComponents<AudioSource>();

		MusicSource = GameObject.Find("Music Source").GetComponents<AudioSource>();
		Soundslider = GameObject.Find("SoundSlider").GetComponent<Slider>();
		Musicslider = GameObject.Find("MusicSlider").GetComponent<Slider>();
		//MusicSlider = GameObject.Find("Musicslider").GetComponent<Slider
		Soundslider.value = SoundSource[0].volume/.3f;
		Musicslider.value = MusicSource[0].volume/.3f;

		Instance = this;
	}
	
	// Update is called once per frame


	public void SlideSound(){
		SoundSource[0].volume = Soundslider.value;
		SoundSource[1].volume = Soundslider.value;
	}
	public void SlideMusic(){
		MusicSource[0].volume = Musicslider.value*.3f;
		MusicSource[1].volume = Musicslider.value*.3f;
	}
	public static void ColorStuff(){
        foreach(GameObject holder in Instance.spritesToColor){

            holder.GetComponent<Image>().color = Color.HSVToRGB((float)TextModulator.hue/359,(float)TextModulator.s/99, (float)TextModulator.v/99);
        }
        foreach(GameObject holder in Instance.textsToColor){

            holder.GetComponent<Text>().color = Color.HSVToRGB((float)TextModulator.hue/359,(float)TextModulator.s/99, (float)TextModulator.v/99);
        }
	}
}
