using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using MirzaBeig.ParticleSystems.Demos;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {
	static public Vector3 cameraorigin;
	public static Vector3 cameraposition;
	public static Vector3 eulerangles;
	public GameObject topDownCam;
	public CameraShake shaker;
	bool topdown;
	public Text screenText;
	public static CameraController Instance;
	public GameObject gameModeBackground;
	public Sprite World1;
	public Sprite World2;
	public Sprite World3;
	public Sprite World4;
	public Sprite World5;
	public Sprite[] WorldTitles;
	public Sprite[] WorldNumber;
	public Sprite[] WorldName;
	public Sprite[] WorldTitlesSpanish;
	public Sprite[] WorldNameSpanish;
	public GameObject WorldTitle;
	public GameObject World;
	// Use this for initialization
	void Awake () {
		topdown = false;
		cameraposition = transform.localPosition;
		eulerangles = transform.localEulerAngles;
		cameraorigin = new Vector3(Camera.main.gameObject.transform.position.x, Camera.main.gameObject.transform.position.y, Camera.main.gameObject.transform.position.z);
		Instance = GameObject.Find("Main Canvas").GetComponent<CameraController>();
	}
	void Start(){
		Instance = GameObject.Find("Main Canvas").GetComponent<CameraController>();
		//Application.targetFrameRate = 30;
	}

	public IEnumerator FadeBackgroundTo(float fadetime, float alphaPercentage){
		Image image = gameModeBackground.GetComponent<Image>();
		float initValue = image.color.a;
		Color tempColor = image.color;
		for(float t = 0.0f; t<fadetime; t+= Time.deltaTime){
			float normalizedTime = t/fadetime;
			tempColor.a = (float)Mathf.Lerp(initValue, alphaPercentage, normalizedTime);
			image.color = tempColor;
			yield return null;
		}
		tempColor.a = alphaPercentage;
		image.color = tempColor;
	}
	public static void Fade(float time, float alphaPercentage, int place){
		if(Instance == null)
		Instance = GameObject.Find("Main Canvas").GetComponent<CameraController>();
		int world = Mathf.FloorToInt((place-1)/40)  + 1;
		if(world>5)
		world=5;
		Instance.gameModeBackground.GetComponent<Image>().sprite = Instance.CheckBackground(world);
		if(SceneLoading.Instance.isMenu){
			Instance.WorldTitle.GetComponent<Image>().sprite = Instance.CheckWorldName(world);
			Instance.World.GetComponent<Image>().sprite = Instance.CheckWorldSprite(world);
			Instance.World.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Instance.CheckWorldNumber(world);

		}
		Instance.gameModeBackground.GetComponent<Image>().enabled = true;
		Instance.StartCoroutine(Instance.FadeBackgroundTo(time,alphaPercentage));

	}
	public static void Fade(float time, float alphaPercentage){
		Instance.StartCoroutine(Instance.FadeBackgroundTo(time,alphaPercentage));

	}
	private Sprite CheckWorldNumber(int world){
		return WorldNumber[world-1];		
	}	
	private Sprite CheckWorldSprite(int world){
		if(LanguageHandler.IsEnglish())
		return WorldName[world-1];		
		else return WorldNameSpanish[world-1];
	}
	private Sprite CheckWorldName(int world){
		if(LanguageHandler.IsEnglish())
		return WorldTitles[world-1];
		else
		return WorldTitlesSpanish[world-1];
	}
	private Sprite CheckBackground(int world){
		switch(world){
			case 1:
				return World1;
			break;

			case 2:
				return World2;
			break;

			case 3:
				return World3;
			break;

			case 4:
				return World4;
			break;

			case 5:
				return World5;
			break;
		}
		return World5;
	}
	public void ChangeResolution(){
		Screen.SetResolution(1280,720,true);
	}
	static public void changePosition(int shiftx, int shifty){
		Camera.main.gameObject.transform.position = new Vector3(cameraorigin.x+shiftx, cameraorigin.y, cameraorigin.z-shifty);
	}
	static public void changeFovAndRot(int fov, float rot){
		Camera.main.fieldOfView = fov;
		Camera.main.gameObject.transform.rotation = Quaternion.Euler(rot,0,0);
		cameraposition = Camera.main.transform.localPosition;
		eulerangles = Camera.main.transform.localEulerAngles;
	}

}
	