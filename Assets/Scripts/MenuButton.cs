using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour{
	public Sprite menuImage;
	public Sprite restartImage;
	public List<GameObject> buttons;
	bool holding;
	public UnityEvent OnPress;
    public UnityEvent OnHold;
    public UnityEvent OnRelease;
    bool done;
    bool restarted;
    public static bool open;
    public GameObject ConfigMenu;
    public GameObject levelText;
    public GameObject ringObject;
    public static MenuButton thisMB;
    public Image settingsSprite;

    public GameObject[] settingsToColor;

    void Awake()
    {
        thisMB = this;
    }

	// Use this for initialization
	void Start () {
		open = false;	
        LevelBuilder.settingsBoard = ConfigMenu;
	}
	
    public static void CloseMenu(){
        if(open){
            thisMB.toggleMenu();
        }
    }

	public void toggleMenu(){
        if(Swiping.canswipe){
            RectTransform myrt = levelText.GetComponent<RectTransform>();
            if(MenuButton.open){
                if(!LevelManager.configging){
                    LevelManager.isdragging = false;
                    Swiping.canswipe = true;
                    LevelManager.configging = false;
                    LevelManager.isdragging = false;                   
                }
                StartCoroutine(RotateRing(30, .2f));
            }
            else{
                StartCoroutine(RotateRing(-30, .2f));
            }
            MenuButton.open = !MenuButton.open;
            for(int i=0; i<buttons.Count; i++){
                buttons[i].SetActive(open);
            }            
        }
	}

	public void toggleMenuWorld(){
        MenuButton.open = !MenuButton.open;
        for(int i=0; i<buttons.Count; i++){
            buttons[i].SetActive(open);
        }	
    }

    public IEnumerator RotateRing(float target, float fadetime){
        for(float t = 0.0f; t<fadetime; t+= Time.deltaTime){
            float normalizedTime = t/fadetime;
            ringObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, target*Mathf.Lerp(0,1, normalizedTime));
            yield return null;
        }
        ringObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, target);
    }

    public void closeMenu(){
        open = false;
        for(int i=0; i<buttons.Count; i++){
            buttons[i].SetActive(open);
        }       
    }

    private IEnumerator LoopWhileHolding()
    {
    	done = false;
		yield return new WaitForSeconds(3);
        OnHold.Invoke();
        if(!done & open){
		Debug.Log("restart");
		done = true;
		restarted = true;        	
        } 
        yield return null; // makes the loop wait until next frame to continue
    }

    public void toggleCongifMenu(){
        ConfigMenu.SetActive(true);
        LevelManager.configging = true;
        Swiping.canswipe = false;
        LevelManager.isdragging = true;
        VolumeSliders.ColorStuff();
        if(!SceneLoading.Instance.isMenu){
            closeMenu();
        }
        if(open){
            toggleMenu();
        }
    }

    public void closeConfigMenu(){
        LevelManager.isdragging = false;
        ConfigMenu.SetActive(false);
        Swiping.canswipe = true;
        Swiping.mydirection = "Null";
        if(Input.touchCount>0){
            Touch t = Input.GetTouch(0);
            Swiping.firstPressPos = new Vector2(t.position.x,t.position.y);
        }
        else{
            Swiping.firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        LevelManager.configging = false;
    }

    public void CloseSettings(){
        ConfigMenu.SetActive(false);
                LevelManager.isdragging = false;
    }
}
