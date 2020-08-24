using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
	public string type;
	public int level;
	public int potdFirst;
    // Start is called before the first frame update

    public void Click(){
        if(SceneLoading.Instance.isMenu){
            if(type == "Adventure"){
                SceneLoading.Instance.LoadLevel(level);
            }
            else if(type == "Potd"){
                SceneLoading.Instance.LoadPotdMap(level);
            }
            else if(type == "PotdUnlock"){
                LevelMenu.Instance.OpenUnlockMenu(level, potdFirst);
            }
        }
        else{
            if(type == "Potd"){
                SceneLoading.Instance.PotdSpecific(level);
                
            }
            else if(type == "PotdUnlock"){
                LevelMenu.Instance.OpenUnlockMenu(level, potdFirst);
            }
        }
    }
}
