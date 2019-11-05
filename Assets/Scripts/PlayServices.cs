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
    //int dataString;
    //bool enableSaveGame = true;
    // Start is called before the first frame update
    void Awake()
    {
        //PlayerPrefs.DeleteAll();
        //Debug.Log(instance);
        Debug.Log("cursavepref " + PlayerPrefs.GetString(SAVE_NAME));
        if(instance == null)
            {
        //PlayerPrefs.DeleteAll();

                LevelManager.adFree = false;

                instance = this;
                DontDestroyOnLoad(this.gameObject);

                //Debug.Log("Doingit");
                //Debug.Log(instance);
                LevelStorer.PopulateLevelDictionary(); //load level data int levelstorer.leveldic
               // Debug.Log(PlayerPrefs.GetString(SAVE_NAME));
                if(!PlayerPrefs.HasKey(SAVE_NAME))
                    PlayerPrefs.SetString(SAVE_NAME, GameDataToString());
                if(!PlayerPrefs.HasKey("IsFirstTime"))
                    PlayerPrefs.SetInt("IsFirstTime", 1);
                    
                LoadLocal();              
                if (PlayerPrefs.HasKey ("Loaded")) {
                    Debug.Log ("Has playerpref");
                    //LevelStorer.AddRatingsToDictionary(); //Playerprefs into leveldic.
                    Debug.Log(PlayerPrefs.GetInt("hintCurrency"));


                } 
                else {
                    LevelStorer.PopulatePlayerPrefs(); //initializeprefs
                    PlayerPrefs.SetInt("CurrentFirst", 1);
                    GameManager.mycurrency = 0;
                    PlayerPrefs.SetInt("Currency", 0);
                    //PopulateRatings();
                    PlayerPrefs.SetInt("Loaded",1);
                }
                InitializePGP();
                SignIn();
                //Debug.Log(int.Parse("1 1 1 1"));
//                SplitString("1 2 3 4 5");
                //byte[] dataToSave = Encoding.ASCII.GetBytes("1 23 4 5");
                //Debug.Log(dataToSave[0]);

                return;
            }
        Destroy(this.gameObject);

    }

    public string[] SplitString(string str){

        //string[] strArray = Regex.Split(str, "");
        string[] strArray = new string[str.Length];
        for(int i=0; i<str.Length; i++){
            strArray[i] = str[i].ToString();
        }
        Debug.Log(strArray.Length + " is the datasize");
        Debug.Log(strArray[0]);
        Debug.Log(strArray[1]);
        return strArray;

    }
    // string InitializeSavePref(){
    //     string stringToSave = "";
    //     if(SceneLoading.adFree = true){
    //         stringToSave = stringToSave + "1 ";
    //     }
    //     else{
    //         stringToSave = stringToSave + "0 ";
    //     }
    //     for
    //     return stringToSave;        
    // }
    public void InitializePGP(){
        PlayGamesClientConfiguration.Builder builder = new PlayGamesClientConfiguration.Builder();


        builder.EnableSavedGames();


        PlayGamesPlatform.InitializeInstance(builder.Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();        
    }

    public void SignIn(){
        Social.localUser.Authenticate((bool success, string err) => {
            if(success){
                Debug.Log("Login success");
                //teller.SetActive(true);
                LoadData();
                //MobileAds.Initialize("ca-app-pub-3301322474937909~4906291296");
            }
            else
            {
                //teller2.SetActive(true);
                Debug.Log("Login failed");
                Debug.Log("Error : " + err);

            }
        });        
    }
    public void SignOut() 
    {
        if (Social.localUser.authenticated)
            PlayGamesPlatform.Instance.SignOut();
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
        for(int i=1; i< LevelStorer.leveldic.Count+1; i++){
            if(LevelStorer.leveldic[i].islocked == true && LevelStorer.leveldic[i].rating ==0){
                stringToSave = stringToSave + "0";   
            }
            else{
                
                int rating = LevelStorer.leveldic[i].rating;
                //Debug.Log("place " + i + " is " + (rating+1).ToString());
                stringToSave = stringToSave + "" + (rating+1).ToString() + "";
                //Debug.Log(stringToSave);
            }

        }
        Debug.Log("GameData is " + stringToSave);
        return stringToSave;
    }

    // string StringWithValueAdded(string value){
    //     string newString = value + 
    // }

    void AssignData(string Data){
        if(Data == null){
            
            Data = "0";
        }
        string[] dataArray = SplitString(Data);

        //string[] dataValues = 

        Debug.Log(Data);
        Debug.Log("ASSIGNED");

        //Debug.Log(LevelStorer.leveldic.Count);
        // LevelMenu.highestLevelSolved = int.Parse(Data);
        // int highestSolved = LevelMenu.highestLevelSolved;
        Debug.Log(dataArray[0]);
        if(int.Parse(dataArray[0])== 1)
            LevelManager.adFree = true;
        else
            LevelManager.adFree = false;

        //GameObject.Find("highestsolved").GetComponent<Text>().text = dataArray[0]; //this currently displays 1 or 0 for paid or not
        for(int i=1; i<dataArray.Length-1;i++){
            int rating = int.Parse(dataArray[i]);
            if(rating>1){
            UpdateImportantValue(i, rating-1);
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
        mergedData = "";
        //string[] mergedArray = new String[localData.Length];
        for(int i=0; i<localData.Length; i++){
            if (cloudData[i] > localData[i])
            mergedData = mergedData + "" +  cloudData[i] + "";
            else
            mergedData = mergedData + "" +  localData[i] + "";
        }
        return mergedData;
    }


    void StringToGameData(string cloudData, string localData){/////////////////Merges both data strings and then assigns & saves to loud
        Debug.Log(cloudData + "cloud and local" + localData);
        mergedData = MergeData(cloudData, localData);
        PlayerPrefs.SetString(SAVE_NAME, mergedData);
        AssignData(mergedData);
        isCloudDataLoaded = true;
        SaveData();

        // if (PlayerPrefs.GetInt("IsFirstTime") == 1){
        //     PlayerPrefs.SetInt("IsFirstTime", 0);
        //     if (int.Parse(cloudData) > int.Parse(localData)){
        //         Debug.Log("StringTogameData goes cloud");
        //         PlayerPrefs.SetString(SAVE_NAME, cloudData);
        //         AssignData(cloudData);
        //         //SaveDataLocally
        //         //LOADFROMCLOUD
        //     }
        // }
        // else{
        //     if(int.Parse(localData) > int.Parse(cloudData)){
        //         //uselocaldata
        //         Debug.Log("StringTogameData goes local");
        //         PlayerPrefs.SetString(SAVE_NAME, localData);
        //         AssignData(localData);

        //         isCloudDataLoaded = true;
        //         SaveData();
        //         return;
        //     }
        // }


        //useclouddata
        isCloudDataLoaded = true;
    }

    void StringToGameData(string localData){
        Debug.Log(localData);
        if(localData == null){
            localData = "0";
        }
        AssignData(localData);
        //uselocaldata
    }

    public void LoadData(){
        if (Social.localUser.authenticated){
            isSaving = false;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME, 
                DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
            Debug.Log("LOADED DATA SUCCESFFULY");
        }
        else{
            LoadLocal();
        }
    }

    private void LoadLocal(){
        Debug.Log("LoadingLocal");
        StringToGameData(PlayerPrefs.GetString(SAVE_NAME));
    }

    public void SaveData(){
        if(!isCloudDataLoaded){
            SaveLocal();
            return;
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
    }

    public void SaveLocal(){
        PlayerPrefs.SetString(SAVE_NAME, GameDataToString());
    }

    private void ResolveConflict(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData, 
        ISavedGameMetadata unmerged, byte[] unmergedData){
        Debug.Log(originalData + "Orig" + unmergedData + "unmerged");
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

            int originalNum = int.Parse(originalStr);
            int unmergedNum = int.Parse(unmergedStr);

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
        //Debug.Log(game + " "+ "game");
        //Debug.Log(status + " "+ "status");
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
        ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
    }

    private void SaveGame(ISavedGameMetadata game){
        newMergedString = MergeData(curCloudData, GameDataToString());

        SaveLocal();

        byte[] dataToSave = Encoding.ASCII.GetBytes(newMergedString);//////////needtofixthis

        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();

        //Debug.Log("DataToSave length is " + dataToSave.Length);

        ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, dataToSave, OnSavedGameDataWritten);

    }

    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] savedData){
        Debug.Log("Saved Data byte is " + savedData.Length + " long, the first ones are " + savedData[0]);
        if(status == SavedGameRequestStatus.Success){
            string cloudDataString;
            if(savedData.Length == 0 || savedData.Length == 1)
                cloudDataString = GameDataToString();
            else
                cloudDataString = Encoding.ASCII.GetString(savedData);

            string localDataString = PlayerPrefs.GetString(SAVE_NAME);

            StringToGameData(cloudDataString, localDataString);
            curCloudData = cloudDataString;
            //curCloudData = mergedData;

        }
    } 

    private void OnSavedGameDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game){
        curCloudData = newMergedString;
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
        }
    }

    public static void IncrementAchievement(string id, int stepsToIncrement) 
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
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
