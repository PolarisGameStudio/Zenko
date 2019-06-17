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

	// Use this for initialization
	void Start () {
		myanim = GetComponentInChildren<Animator>();
		readytomove = false;
		isstatic =true;
		goaling = false;
		active = true;
//		Debug.Log(transform.rotation.y);
//		Debug.Log(newx);
	}
	public static void restartGoal(){
		readytomove = false;
		isstatic =true;
		goaling = false;
	}
	// Update is called once per frame
	void Update () {
		//if(myplayer.GetComponent<PlayerMovement>().currenttile != myplayer.transform.position){
		//	Debug.Log("Moving");
		//}

		if(LevelBuilder.playertransform!= null){
		newx = LevelBuilder.playertransform.position.x;
		newy = -LevelBuilder.playertransform.position.z;
		xdif = Mathf.Abs(newx-transform.position.x);
		ydif = Mathf.Abs(newy+transform.position.z);			
		}

//		Debug.Log(lastphase);

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
			//Debug.Log(newx + "" + oldx +newy+oldy );
//			Debug.Log(lastphase);
			xdif = Mathf.Abs(newx-transform.position.x);
			ydif = Mathf.Abs(newy+transform.position.z);
			//if moving towards value
			//if moving away from value
			//Debug.Log(xdif + "" + ydif);
			if(isvertical){//if plant is facing down/up
				if(newx>oldx){//moving right
					if(xdif<1.5){
						if (transform.position.x>newx){//getting closer since im at its right.
							myanim.SetInteger("Phase",1);
			//				Debug.Log("orthis");
							lastphase = 1;
						}							
					}

					if (transform.position.x<newx){//getting away since im at its left.
						myanim.SetInteger("Phase",0);
						lastphase = 0;
					}

				}
				if(newx<oldx){//moving left
					if(xdif<1.5){
						if (transform.position.x<newx){//getting closer because im at it's left
							myanim.SetInteger("Phase",1);
//							Debug.Log("this");
							lastphase = 1;
						}								
					}
					if (transform.position.x>newx){//getting away since im at its right
						myanim.SetInteger("Phase",0);
						lastphase = 0;
					}					
			
					
				}
				if(newy<oldy){//moving up
//					Debug.Log("Moving where");
					if(xdif<.9){
						//Debug.Log(ydif);
						if(ydif<1.5){
							if(-transform.position.z<newy){
								Debug.Log("Doing this");
								goaling = true;
								myanim.SetInteger("Phase",2);
								AnimateFoxGoal();
								lastphase = 2;								
							}

						}
						else{
							if (-transform.position.z>newy){//getting closer
								myanim.SetInteger("Phase",1);
								//Debug.Log("orthis");
								lastphase = 1;
							}							
						}
						
					}
				}
				if(newy>oldy){//moving down
					if(xdif<.9){
						if(ydif<1.5){
							if(-transform.position.z>newy){

//								Debug.Log(newy);
								goaling = true;
//								Debug.Log("Doing this too");
								myanim.SetInteger("Phase",2);
								AnimateFoxGoal();
								lastphase = 2;
							}
						}
						else{
							if (transform.position.x>newx){//getting closer
								myanim.SetInteger("Phase",1);
								//Debug.Log("orthis");
								lastphase = 1;
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
							//Debug.Log("orthis");
							lastphase = 1;
						}							
						
					}
						if (-transform.position.z<newy){//getting away
							myanim.SetInteger("Phase",0);
							lastphase = 0;					
					}


				}
				if(newy<oldy){//moving down
					if(ydif<1.5){
						if (-transform.position.z<newy){//getting closer
							myanim.SetInteger("Phase",1);
							//Debug.Log("this");
							lastphase = 1;
						}								
					}
						if (-transform.position.z>newy){//getting away
							myanim.SetInteger("Phase",0);
							lastphase = 0;
						}							

				}
				if(newx>oldx){//moving right
					if(ydif<.9){
						if(xdif<1.5){
							if(transform.position.x > newx){
								goaling = true;
								myanim.SetInteger("Phase",2);
								AnimateFoxGoal();
								lastphase = 2;								
							}
						}
						else{
							if (-transform.position.z>newy){
								myanim.SetInteger("Phase",1);
							//	Debug.Log("orthis");
								lastphase = 1;
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
								AnimateFoxGoal();
								lastphase = 2;								
							}
						}
						else{
							if (-transform.position.z>newy){
								Debug.Log("orthis");
								lastphase = 1;
							}							
						}
						
					}
				}
			}
			//myanim.SetInteger("Phase", lastphase);	

			oldx = newx;
			oldy = newy;
		}
		//if(!readytomove){
		//	isstatic = true;
		//}
//		Debug.Log("Is vertical" + isvertical);
	}

	void OpenFlower(){

	}

	void EatFox(){

	}
	void AnimateFoxGoal(){
		StartCoroutine(LevelBuilder.playertransform.GetComponent<PlayerAnimation>().Disappear(.8f));
		LevelBuilder.playertransform.GetComponent<PlayerMovement>().speed = 3;
		LevelBuilder.playertransform.GetChild(3).GetComponent<Animator>().SetInteger("Phase", 2);
		//LevelBuilder.playertransform.GetComponent<PlayerAnimation>().Disappear(1);
	}


}
