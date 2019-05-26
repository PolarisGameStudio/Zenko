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
	// Use this for initialization
	void Start () {
		open = false;	
	}
	
	// Update is called once per frame
	void Update () {
//		StartCoroutine(buttonAction());
	}

	public void toggleMenu(){
        Debug.Log("menu");
            MenuButton.open = !MenuButton.open;
            for(int i=0; i<buttons.Count; i++){
                buttons[i].SetActive(open);
            }
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
        //set menu to back only.
    }
    public void closeConfigMenu(){
        ConfigMenu.SetActive(false);
    }
}
