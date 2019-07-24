// using System.Collections;
// using GooglePlayGames;
// using GooglePlayGames.BasicApi;
// using UnityEngine;

// public class PlayServices : MonoBehaviour
// {
//     // Start is called before the first frame update
//     void Start()
//     {
//         PlayGamesClientConfiguration.Builder builder = new PlayGamesClientConfiguration.Builder();
//         PlayGamesPlatform.InitializeInstance(builder.Build());
//         PlayGamesPlatform.DebugLogEnabled = true;
//         PlayGamesPlatform.Activate();

//         Social.localUser.Authenticate((bool success, string err) => {
//         	if(success){
//         		Debug.Log("Login success");
//         	}
//         	else
//         	{
//         		Debug.Log("Login failed");
//         		Debug.Log("Error : " + err);

//         	}
//         });
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
