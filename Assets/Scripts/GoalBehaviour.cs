using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBehaviour : MonoBehaviour {
	Animator myanim;
	public Transform myplayer;
	//public bool moving;
	public static bool readytomove;
	public static bool isvertical;
	public static float newx;
	public static float oldx;
	public static float newy;
	public static float oldy;
	public static bool isstatic;
	public static float xdif;
	public static float ydif;
	public static bool movingtowards;
	public static bool movingaway;
	public static int lastphase;
	public static bool goaling;
	public static bool active;
	bool chomping;

	// Use this for initialization
	void Start () {
		myanim = GetComponentInChildren<Animator>();
		readytomove = false;
		isstatic =true;
		goaling = false;
		active = true;
		SfxHandler.isFlowerOpen = false;
	}
	public static void restartGoal(){
		readytomove = false;
		isstatic =true;
		goaling = false;
		SfxHandler.isFlowerOpen = false;
	}

	void FixedUpdate () {
		if(LevelBuilder.playertransform!= null){
		newx = LevelBuilder.playertransform.position.x;
		newy = -LevelBuilder.playertransform.position.z;
		xdif = Mathf.Abs(newx-transform.position.x);
		ydif = Mathf.Abs(newy+transform.position.z);			
		}

		if(readytomove && active){
			if(isstatic == true){
				newx = LevelBuilder.playertransform.position.x;
				oldx = LevelBuilder.playertransform.position.x;
				newy = -LevelBuilder.playertransform.position.z;
				oldy = -LevelBuilder.playertransform.position.z;
				isstatic = false;
			}
			else{
				newx = LevelBuilder.playertransform.position.x;
				newy = -LevelBuilder.playertransform.position.z;
			}
			xdif = Mathf.Abs(newx-transform.position.x);
			ydif = Mathf.Abs(newy+transform.position.z);
			if(isvertical){//if plant is facing down/up
				if(newx>oldx){//moving right
					if(xdif<1.5){
						if (transform.position.x>newx){//getting closer since im at its right.
							myanim.SetInteger("Phase",1);
							lastphase = 1;
							SfxHandler.Instance.PlayFlowerOpen();
						}							
					}
					if (transform.position.x<newx){//getting away since im at its left.
						myanim.SetInteger("Phase",0);
						lastphase = 0;
						SfxHandler.Instance.PlayFlowerClose();
					}

				}
				if(newx<oldx){//moving left
					if(xdif<1.5){
						if (transform.position.x<newx){//getting closer because im at it's left
							myanim.SetInteger("Phase",1);
							lastphase = 1;
							SfxHandler.Instance.PlayFlowerOpen();
						}								
					}
					if (transform.position.x>newx){//getting away since im at its right
						myanim.SetInteger("Phase",0);
						lastphase = 0;
						SfxHandler.Instance.PlayFlowerClose();
					}							
				}
				if(newy<oldy){//moving up
					if(xdif<.9){
						if(ydif<1.5){
							if(-transform.position.z<newy){
								SfxHandler.Instance.StopSlideVictory();
								SfxHandler.Instance.PlayVictory();
								Debug.Log("Doing this");
								goaling = true;
								myanim.SetInteger("Phase",2);
								StartCoroutine(PlayChompAfterDelay());
								AnimateFoxGoal();
								lastphase = 2;								
							}
						}
						else{
							if (-transform.position.z>newy){//getting closer
								myanim.SetInteger("Phase",1);
								//Debug.Log("orthis");
								lastphase = 1;
								SfxHandler.Instance.PlayFlowerOpen();
							}							
						}
					}
				}
				if(newy>oldy){//moving down
					if(xdif<.9){
						if(ydif<1.5){
							if(-transform.position.z>newy){
								goaling = true;
								myanim.SetInteger("Phase",2);
								StartCoroutine(PlayChompAfterDelay());
								SfxHandler.Instance.StopSlideVictory();
								SfxHandler.Instance.PlayVictory();
								AnimateFoxGoal();
								lastphase = 2;
							}
						}
						else{
							if (transform.position.x>newx){//getting closer
								myanim.SetInteger("Phase",1);
								lastphase = 1;
								SfxHandler.Instance.PlayFlowerOpen();
							}							
						}
						
					}

				}
			//check x
			}
			if(!isvertical){
				if(newy>oldy){//moving up
					if(ydif<1.5){
						if (-transform.position.z>newy){//getting closer
							myanim.SetInteger("Phase",1);
							lastphase = 1;
							SfxHandler.Instance.PlayFlowerOpen();
						}							
					}
						if (-transform.position.z<newy){//getting away
							myanim.SetInteger("Phase",0);
							lastphase = 0;
							SfxHandler.Instance.PlayFlowerClose();					
					}
				}
				if(newy<oldy){//moving down
					if(ydif<1.5){
						if (-transform.position.z<newy){//getting closer
							myanim.SetInteger("Phase",1);
							lastphase = 1;
							SfxHandler.Instance.PlayFlowerOpen();
						}								
					}
						if (-transform.position.z>newy){//getting away
							myanim.SetInteger("Phase",0);
							lastphase = 0;
							SfxHandler.Instance.PlayFlowerClose();
						}							
				}
				if(newx>oldx){//moving right
					if(ydif<.9){
						if(xdif<1.5){
							if(transform.position.x > newx){
								goaling = true;
								myanim.SetInteger("Phase",2);
								StartCoroutine(PlayChompAfterDelay());
								SfxHandler.Instance.StopSlideVictory();
								SfxHandler.Instance.PlayVictory();
								AnimateFoxGoal();
								lastphase = 2;								
							}
						}
						else{
							if (-transform.position.z>newy){
								myanim.SetInteger("Phase",1);
								lastphase = 1;
								SfxHandler.Instance.PlayFlowerOpen();
							}							
						}
					}
				}
				if(newx<oldx){//moving left
					if(ydif<.9){
						if(xdif<1.5){
							if(transform.position.x < newx){
								goaling = true;
								myanim.SetInteger("Phase",2);
								StartCoroutine(PlayChompAfterDelay());
								SfxHandler.Instance.StopSlideVictory();
								SfxHandler.Instance.PlayVictory();
								AnimateFoxGoal();
								lastphase = 2;								
							}
						}
						else{
							if (-transform.position.z>newy){
								Debug.Log("orthis");
								lastphase = 1;
								SfxHandler.Instance.PlayFlowerOpen();
							}							
						}
					}
				}
			}
			oldx = newx;
			oldy = newy;
		}
	}

	IEnumerator PlayChompAfterDelay(){
		if(!chomping){
			chomping = true;
			yield return new WaitForSeconds(.66f);
			SfxHandler.Instance.PlayChomp();	
			yield return new WaitForSeconds(.4f);
			chomping = false;		
		}
	}

	void AnimateFoxGoal(){
		StartCoroutine(LevelBuilder.playertransform.GetComponent<PlayerAnimation>().Disappear(.8f));
		LevelBuilder.playertransform.GetComponent<PlayerMovement>().speed = 3;
		LevelBuilder.playertransform.GetChild(0).GetComponent<Animator>().SetInteger("Phase", 2);
	}
}
