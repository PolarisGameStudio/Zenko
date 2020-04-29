using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinMessage : MonoBehaviour
{
	public Sprite[] World1Messages;
	public Sprite[] World2Messages;
	public Sprite[] World3Messages;
	public Sprite[] World4Messages;
    public Sprite[] World5Messages;

    public Sprite[] World1MessagesSprite;
    public Sprite[] World2MessagesSprite;
    public Sprite[] World4MessagesSprite;
    public Sprite[] World3MessagesSprite;
    public Sprite[] World5MessagesSprite;

    public GameObject Next;
    public GameObject Home;
	public static WinMessage Instance;
	public static List<Sprite[]> worldMessageStorer = new List<Sprite[]>();
    public static List<Sprite[]> worldMessageStorerSpanish = new List<Sprite[]>();
	public static float size;
	public bool movingUpwards;
	private float timer;
    private int mystars;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        worldMessageStorer.Add(World1Messages);
        worldMessageStorer.Add(World2Messages);
        worldMessageStorer.Add(World3Messages);
        worldMessageStorer.Add(World4Messages);
        worldMessageStorer.Add(World5Messages);

        worldMessageStorerSpanish.Add(World1MessagesSprite);
        worldMessageStorerSpanish.Add(World2MessagesSprite);
        worldMessageStorerSpanish.Add(World3MessagesSprite);
        worldMessageStorerSpanish.Add(World4MessagesSprite);
        worldMessageStorerSpanish.Add(World5MessagesSprite);
        Debug.Log("SizeSIZESZIEZIZISISISIDISDIAISDIASIDIASDIAISDIASIDIASDIASA " + size);
        movingUpwards = true;
        timer = 0;
        mystars = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //timer += Time.deltaTime;
		//this.GetComponent<RectTransform>().sizeDelta = new Vector2 (size + oscillate(timer,7,(25-((25/3)*(3-mystars)))), 70);
    }

    public void AssignMessage(int world, int stars){
        //if(world >4)
        //world = 4;
        mystars = stars;
        Sprite cursprite;
        if(LanguageHandler.IsEnglish())
        {
            if(LevelManager.ispotd)
                cursprite = worldMessageStorer[0][stars];
            else
                cursprite = worldMessageStorer[world-1][stars];
        }
        else
        {
            if(LevelManager.ispotd)
                cursprite = worldMessageStorerSpanish[0][stars];
            else
                cursprite = worldMessageStorerSpanish[world-1][stars];
        }
    	this.GetComponent<Image>().sprite = cursprite;
    	this.GetComponent<RectTransform>().sizeDelta = new Vector2 (cursprite.bounds.size.x*50 , 100);
    	size = cursprite.bounds.size.x*50;
        CheckNext();
    }
    private bool TooHigh(float value){
    	if(value > 1.2*size){
    		return true;
    	}
    	return false;
    }
    private bool TooLow(float value){
    	if(value < .8*size){
    		return true;
    	}
    	return false;
    }
    private void SizeUp(){

    }

    private void SizeDown(){

    }
    float oscillate(float time, float speed, float scale)
    {
        return Mathf.Sin(time * speed / Mathf.PI) * scale;
    }
    public void CheckNext(){
        if(LevelManager.levelnum == 200 && !LevelManager.ispotd){
            Next.SetActive(false);
            Home.SetActive(true);
            return;
        }
        if(LevelManager.ispotd){
            if(LevelManager.adFree && DateChecker.Instance.currentIndex < 
            DateChecker.Instance.todayIndex){

            }
            else{
                Next.SetActive(false);
                Home.SetActive(true);
                return;                
            }
        }
    }
}
