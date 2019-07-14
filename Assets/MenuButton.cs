using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MenuButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{
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


    void Awake()
    {
        thisMB = this;
    }
	// Use this for initialization
	void Start () {
		open = false;	
        LevelBuilder.settingsBoard = ConfigMenu;
	}
	
	// Update is called once per frame
// 	void Update () {
// //		StartCoroutine(buttonAction());
// 	}
    public static void CloseMenu(){
        if(open){
            thisMB.toggleMenu();
           
        }
    }

	public void toggleMenu(){
        Debug.Log("menu");
            MenuButton.open = !MenuButton.open;
            Debug.Log(levelText.GetComponent<RectTransform>());
            RectTransform myrt = levelText.GetComponent<RectTransform>();
            
            for(int i=0; i<buttons.Count; i++){
                buttons[i].SetActive(open);
            }
            if(MenuButton.open){
                myrt.localPosition = new Vector3 (myrt.localPosition.x,-410,0);
                StartCoroutine(RotateRing(-30, .2f));
            }
            else{
                myrt.localPosition = new Vector3 (myrt.localPosition.x,0,0);
                StartCoroutine(RotateRing(30, .2f));
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

    }
/*	public IENumerator buttonAction(){

	}*/
    public void OnPointerDown(PointerEventData data)
    {
        //OnPress.Invoke();
 
        // do any custom "OnPress" behavior here
 
        //StartCoroutine(LoopWhileHolding());
    }
        public void OnPointerUp(PointerEventData data)
    {
        /*OnRelease.Invoke();
 		done = true;

 		if(!restarted){
  		Debug.Log("menu");
  			open = !open;
  			for(int i=0; i<buttons.Count; i++){
  				buttons[i].SetActive(open);
  			}
 		}*/

        // do any custom "OnRelease" behavior here
 
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

        // do any custom "OnHold" behavior here
 
            yield return null; // makes the loop wait until next frame to continue
    }
    public void toggleCongifMenu(){
        ConfigMenu.SetActive(true);
        Swiping.canswipe = false;
        if(open){
            toggleMenu();
        }
        LevelManager.isdragging = true;

        //set menu to back only.
    }
    public void closeConfigMenu(){
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
        LevelManager.isdragging = false;

    }
    public void CloseSettings(){
        ConfigMenu.SetActive(false);
                LevelManager.isdragging = false;

    }
}
