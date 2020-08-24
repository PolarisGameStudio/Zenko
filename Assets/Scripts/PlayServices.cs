using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Text;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
//using GoogleMobileAds.Api;

public class PlayServices : MonoBehaviour
{
    //public GameObject teller;
    //public GameObject teller2;
    public static PlayServices instance;
    const string SAVE_NAME = "SaveFile";
    public static string mergedData;
    public static string curCloudData;
    public static string newMergedString;
    bool isSaving;
    bool isCloudDataLoaded = false;
    string stringToShow;
    bool finishedLoading;
    Loading loader;
    int read;
    //int dataString;
    //bool enableSaveGame = true;
    // Start is called before the first frame update
    void Awake()
    {
        read = 0;
        finishedLoading = false;

        #if UNITY_EDITOR
        //PlayerPrefs.DeleteAll();
        #endif
//        Debug.Log("cursavepref " + PlayerPrefs.GetString(SAVE_NAME));
        if(instance == null)
            {
                loader = GameObject.Find("Handler").GetComponent<Loading>();
                instance = this;
                DontDestroyOnLoad(this.gameObject);

                LevelStorer.PopulateFiveChapters(); //load level data (200) int levelstorer.leveldic
                LevelStorer.PopulatePotd500(); 

                if(!PlayerPrefs.HasKey("PoTD"))
                    PlayerPrefs.SetInt("PoTD", 0);

                if(!PlayerPrefs.HasKey(SAVE_NAME))
                    PlayerPrefs.SetString(SAVE_NAME, GameDataToString());


                if(!PlayerPrefs.HasKey("IsFirstTime"))
                    PlayerPrefs.SetInt("IsFirstTime", 1);
                    
                LoadLocal();              

                if(PlayerPrefs.GetString(SAVE_NAME).Length < GameDataToString().Length){
                    Debug.Log("NEW MAPS ARE IN EXTRA EXTRA NEW MAPS ARE IN");
                }

                //checks for first4chapters
                if (PlayerPrefs.HasKey ("Loaded")) {//has player prefs

                } 
                else {
                    LevelStorer.PopulatePlayerPrefs(); //initializeprefs
                    PlayerPrefs.SetInt("CurrentFirst", 1);
                    GameManager.mycurrency = 0;
                    PlayerPrefs.SetInt("Currency", 0);
                    PlayerPrefs.SetInt("Loaded",1);
                }
                loader.DaDebug("prepgp");
                return;
            }
        Destroy(this.gameObject);
    }
    void Start(){
        #if UNITY_ANDROID

        InitializePGP();
        SignIn();
        #endif        
    }
    IEnumerator LoadIn(int seconds){
        yield return new WaitForSeconds(seconds);
        loader.Loaded();
    }
    public string[] SplitString(string str){
        string[] strArray = new string[str.Length];
        for(int i=0; i<str.Length; i++){
            strArray[i] = str[i].ToString();
        }
        Debug.Log(strArray.Length + " is the datasize");
//        Debug.Log(strArray[0]);
        return strArray;

    }

   
    public void InitializePGP(){

        #if UNITY_ANDROID
        PlayGamesClientConfiguration.Builder builder = new PlayGamesClientConfiguration.Builder();
        builder.EnableSavedGames();
        PlayGamesPlatform.InitializeInstance(builder.Build());
        PlayGamesPlatform.Activate();    
        loader.DaDebug("postpgp");
        #endif
    }

    public void SignIn(){
        Social.localUser.Authenticate((bool success, string err) => {
            if(success){
                Debug.Log("Login success");
                loader.DaDebug("Login Success");
                finishedLoading = true;
                LoadData();
            }
            else
            {
                Debug.Log("Login failed");
                Debug.Log("Error : " + err);
                loader.DaDebug("Error : " + err);
                finishedLoading = true;
                loader.Loaded();
            }
        });        
    }
    public void SignOut() 
    {
        #if UNITY_ANDROID
        if (Social.localUser.authenticated)
            PlayGamesPlatform.Instance.SignOut();
        #endif
    }

    #region Saved Games

    string GameDataToString(){
        string stringToSave = "";
        if(PlayerPrefs.HasKey("AdFree")){
            stringToSave = stringToSave + "1";
        }
        else{
            stringToSave = stringToSave + "0";
        }
        //200 from 4 chapters
        //feeds string with first four chapters
        for(int i=1; i< 160+1; i++){
            if(i == 160){
                //Debug.Log("160 is happening and " + LevelStorer.leveldic[i].rating + " is its rating");
            }
            if(LevelStorer.leveldic[i].rating == 0){
            	if(LevelStorer.leveldic[i].islocked == true){
            	stringToSave = stringToSave + "0";  
            	}
            	else{
            	stringToSave = stringToSave + "1"; 	
            	}
            }
            else{
                
                int rating = LevelStorer.leveldic[i].rating;
                stringToSave = stringToSave + "" + (rating+1).ToString() + "";
            }
        }

        for(int i=0; i<500; i++){
        	if(LevelStorer.potdDic[i].islocked == true){
        			stringToSave = stringToSave + "0";
        	}
        	else{
        		int rating = LevelStorer.potdDic[i].rating;
        		stringToSave = stringToSave + "" + (rating+1).ToString() + "";
        	}
        }

        for(int i=161; i < 201; i++){
            if(i == 200){
                //Debug.Log("200 is happening and " + LevelStorer.leveldic[i].rating + " is its rating");
            }
            if(LevelStorer.leveldic[i].rating == 0){
                if(LevelStorer.leveldic[i].islocked == true){
                stringToSave = stringToSave + "0";  
                }
                else{
                stringToSave = stringToSave + "1";  
                }
                 
            }
            else{
                int rating = LevelStorer.leveldic[i].rating;
                stringToSave = stringToSave + "" + (rating+1).ToString() + "";
            }            
        }
        return stringToSave;
    }

    void AssignData(string Data){
        Debug.Log("ASSIGNING DATA WITH STRING: " + Data);
        if(Data == null){
            
            Data = "0";
        }
        string[] dataArray = SplitString(Data);



        //assigns adfree with first digit
        if(int.Parse(dataArray[0])== 1)
            LevelManager.adFree = true;
        else
            LevelManager.adFree = false;

//        Debug.Log(dataArray.Length + " IS THE LENGTH");

        AssignFirstFourChapters(dataArray);
        AssignPotdData(dataArray);
        AssignChapterFive(dataArray);
        if(finishedLoading)
        loader.Loaded();


    }
    void AssignChapterFive(string[] dataArray){
        if(dataArray.Length>666){
            for(int i=161; i<201;i++){
                int rating = int.Parse(dataArray[i+500]);
                if(rating == 1){
                    LevelStorer.leveldic[i].rating = 0;
                    LevelStorer.leveldic[i].islocked = false;       
                }
                if(rating>1){
                UpdateImportantValue(i, rating-1);
                }
            }
        }
    }
    void AssignFirstFourChapters(string[] dataArray){
        for(int i=1; i<161;i++){
            int rating = int.Parse(dataArray[i]);
            if(rating == 1){
        		LevelStorer.leveldic[i].rating = 0;
       			LevelStorer.leveldic[i].islocked = false;   	
            }
            if(rating>1){
            UpdateImportantValue(i, rating-1);
            }
        }
    }
    void AssignPotdData(string[] dataArray){
    	for(int i=0; i<500; i++){
    		int rating = int.Parse(dataArray[i+161]);
    		if(rating == 0){
    			LevelStorer.potdDic[i].rating = 0;
    			LevelStorer.potdDic[i].islocked = true;
    		}
    		if(rating==1){
    			LevelStorer.potdDic[i].rating = 0;
    			LevelStorer.potdDic[i].islocked = false;
    		}
    		if(rating>1){
    			LevelStorer.potdDic[i].rating = rating-1;
    			LevelStorer.potdDic[i].islocked = false;
    		}
    	}
    }


    void UpdateImportantValue(int place, int value){ //grabs a value from datastring and places  it in leveldic
        LevelStorer.leveldic[place].rating = value;
        LevelStorer.leveldic[place].islocked = false;
        if(place<LevelStorer.leveldic.Count){
            LevelStorer.leveldic[place+1].islocked = false; 
        }

    }
    string GameData(){
        return "";
    }
    string UpdateGameDataAtValue(){
        return "";
    }
    string WriteData(){
        return null;
    }

    string MergeData(string cloudData, string localData){
        string[] cloudArray = SplitString(cloudData);
        string[] localArray = SplitString(localData);
        Debug.Log("DATA TO MERGE IS " + cloudData + "which is cloud, and local is "+ localData);
        mergedData = "";
        //string[] mergedArray = new String[localData.Length];
        for(int i=0; i<localData.Length; i++){
        	if(i<cloudData.Length){
	            if (cloudData[i] > localData[i])
	            	mergedData = mergedData + "" +  cloudData[i] + "";
	            else
	            	mergedData = mergedData + "" +  localData[i] + "";        		
	        }
	        else{
	        	mergedData = mergedData + "" + localData[i] + "";
	        }

        }
        return mergedData;
    }


    void StringToGameData(string cloudData, string localData){/////////////////Merges both data strings and then assigns & saves to loud
        Debug.Log(cloudData + "cloud and local" + localData);
        mergedData = MergeData(cloudData, localData);
        PlayerPrefs.SetString(SAVE_NAME, mergedData);
        Debug.Log("GONNA ASSIGN DATA FROM MERGE");
        isCloudDataLoaded = true;
        AssignData(mergedData);
        loader.DaDebug("stringtogamedata2");
        SaveData();        
    }

    void StringToGameData(string localData){
        Debug.Log("GONNA ASSIGN DATA FROM LOCAL with size " + localData.Length);
        if(localData == null){
            localData = "0";
        }
        AssignData(localData);
        loader.DaDebug("stringtogamedata1");
    }

    public void LoadData(){
        #if UNITY_ANDROID
        if (Social.localUser.authenticated){
            isSaving = false;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME, 
                DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
            Debug.Log("LOADED DATA SUCCESFFULY");
        }
        else{
            LoadLocal();
        }
        #endif
    }
    private void LoadLocal(){
//        Debug.Log("LoadingLocal");
        StringToGameData(PlayerPrefs.GetString(SAVE_NAME));

    }
    public void SaveData(){
        #if UNITY_ANDROID
        if(!isCloudDataLoaded){
            curCloudData = "0";
            isCloudDataLoaded = true;
            //SaveLocal();
            //return;
        }
        if(Social.localUser.authenticated){
            Debug.Log("SAVINGTOCLOUD");
            isSaving = true;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME, 
                DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
        }
        else{
            SaveLocal();
        }
        #endif
    }

    public void SaveLocal(){
        PlayerPrefs.SetString(SAVE_NAME, GameDataToString());
    }

    private void ResolveConflict(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData, 
        ISavedGameMetadata unmerged, byte[] unmergedData){
        //Debug.Log(Encoding.ASCII.GetString(originalData)  +  " is original data");
        //Debug.Log(Encoding.ASCII.GetString(unmergedData) + " is UNMERGED DATA");
        //Debug.Log(originalData + "Orig" + unmergedData + "unmerged");
        if(originalData == null){
            Debug.Log("Chossing unmerged in conflict");
            resolver.ChooseMetadata(unmerged);
        }
        else if (unmergedData == null){
            Debug.Log("Choosing originalData");
            resolver.ChooseMetadata(original);
        }
        else{


            string originalStr = Encoding.ASCII.GetString(originalData);
            string unmergedStr = Encoding.ASCII.GetString(unmergedData);




            // Debug.Log("TRynna parse");
            int originalNum;
            int unmergedNum;

            int.TryParse(originalStr.Substring(0,4), out originalNum);
            int.TryParse(unmergedStr.Substring(0,4), out unmergedNum);

            if(originalStr.Length < unmergedStr.Length){
                resolver.ChooseMetadata(unmerged);
            }
            if (originalNum > unmergedNum){

                resolver.ChooseMetadata(original);
                return;
            }
            else if (unmergedNum > originalNum){
                resolver.ChooseMetadata(unmerged);
                return;
            }
            resolver.ChooseMetadata(original);
        }
    }

    private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game){
        read++;
        Debug.Log("ISSAVING IS " + isSaving);
        loader.DaDebug("SavedGameOpened + " + isSaving  + " + "+ SavedGameRequestStatus.Success + " + " + read);

        if (status == SavedGameRequestStatus.Success){
            if (!isSaving){

                LoadGame(game);
            }
            else{
                SaveGame(game);
            }
        }
        else{
            if(!isSaving)
                LoadLocal();
            else
                SaveLocal();
        }
    }

    private void LoadGame(ISavedGameMetadata game){
        #if UNITY_ANDROID
        Debug.Log("Gonna load game");
        loader.DaDebug("loadgame" + game.IsOpen);
        ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
        #endif
    }

    private void SaveGame(ISavedGameMetadata game){
        #if UNITY_ANDROID
        Debug.Log("GONNA SAVE DATA TO CLOUD");
        newMergedString = MergeData(curCloudData, GameDataToString());

        SaveLocal();

        byte[] dataToSave = Encoding.ASCII.GetBytes(newMergedString);//////////needtofixthis

        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();

        //Debug.Log("DataToSave length is " + dataToSave.Length);

        ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, dataToSave, OnSavedGameDataWritten);
        #endif
    }

    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] savedData){
        Debug.Log("Saved Data byte is " + savedData.Length + " long, the first ones are " + savedData[0]);
        Debug.Log(Encoding.ASCII.GetString(savedData) + " IS THE DATA READ");
        loader.DaDebug("SavedGameDataRead");
        int parsed;
        //int.TryParse(Encoding.ASCII.GetString(savedData), out parsed);
        //Debug.Log(parsed + " IS PARSED");
        if(!int.TryParse(Encoding.ASCII.GetString(savedData).Substring(0,4), out parsed)){
            savedData = Encoding.ASCII.GetBytes("0");
        }
        Debug.Log(parsed + " is parsed");

        if(status == SavedGameRequestStatus.Success){
            string cloudDataString;
            if(savedData.Length == 0 || savedData.Length == 1)
                cloudDataString = GameDataToString();
            else
                cloudDataString = Encoding.ASCII.GetString(savedData);

            string localDataString = PlayerPrefs.GetString(SAVE_NAME);
            Debug.Log("STRING FROM GAME DATA POST PROCESSING IS " + cloudDataString);
            curCloudData = cloudDataString;
            StringToGameData(cloudDataString, localDataString);
            
            //isCloudDataLoaded = true;
            //curCloudData = mergedData;

        }
    } 

    private void OnSavedGameDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game){
        curCloudData = newMergedString;
        Debug.Log("New cloud data is " + curCloudData);
    }

    #endregion /Saved Games


    #region Achievements

    public static void UnlockAchievement(string id, int points)
    {
        Debug.Log(id);
        Social.ReportProgress(id, points, success => { });
    }

    public static void UnlockWorldAchievement(int worldnumber){
        switch(worldnumber){
            case 1:
                IncrementAchievement("CgkIl8nVhZgLEAIQAQ", 50);
                break;
            case 2:
                IncrementAchievement("CgkIl8nVhZgLEAIQBQ", 50);
                break;
            case 3:
                IncrementAchievement("CgkIl8nVhZgLEAIQBg", 50);
                break;
            case 4:
                IncrementAchievement("CgkIl8nVhZgLEAIQBw", 50);
                break;
            case 5:
                IncrementAchievement("CgkIl8nVhZgLEAIQCA", 50);
                break;
        }
    }

    public static void IncrementAchievement(string id, int stepsToIncrement) 
    {
        #if UNITY_ANDROID
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
        #endif
    }

    public static void ShowAchievementsUI()
    {
        Social.ShowAchievementsUI();        
    }

    #endregion /Achievements




    #region Leaderboards

    public static void AddScoreToLeaderboard(string leaderboardID, long score){
        Social.ReportScore(score, leaderboardID, success => {});
    }

    #endregion /Leaderboards

}
