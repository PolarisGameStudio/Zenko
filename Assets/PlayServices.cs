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

public class PlayServices : MonoBehaviour
{
    public GameObject teller;
    public GameObject teller2;
    public static PlayServices instance;
    const string SAVE_NAME = "SaveFile";
    bool isSaving;
    bool isCloudDataLoaded = false;
    //bool enableSaveGame = true;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log(instance);
        if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);

                Debug.Log("Doingit");
                Debug.Log(instance);
                LevelStorer.PopulateLevelDictionary(); //load level data
                if(!PlayerPrefs.HasKey(SAVE_NAME))
                    PlayerPrefs.SetString(SAVE_NAME, "0");
                if(!PlayerPrefs.HasKey("IsFirstTime"))
                    PlayerPrefs.SetInt("IsFirstTime", 1);
               
                if (PlayerPrefs.HasKey ("Loaded")) {
                    Debug.Log ("Has playerpref");
                    LevelStorer.PopulateRatings(); //takes old playerprefs and populates current ratings
                    Debug.Log(PlayerPrefs.GetInt("hintCurrency"));
                    LoadLocal();

                } 
                else {
                    LevelStorer.PopulatePlayerPrefs();
                    PlayerPrefs.SetInt("CurrentFirst", 1);
                    GameManager.mycurrency = 0;
                    PlayerPrefs.SetInt("Currency", 0);
                    //PopulateRatings();
                    PlayerPrefs.SetInt("Loaded",1);
                }
                InitializePGP();
                SignIn();


                return;
            }
        Destroy(this.gameObject);

    }

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
                teller.SetActive(true);
                LoadData();
            }
            else
            {
                teller2.SetActive(true);
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

        return LevelMenu.highestLevelSolved.ToString();
    }

    void AssignData(string Data){
        if(Data == null){
            
            Data = "0";
        }
        Debug.Log(Data);
        Debug.Log("ASSIGNED");
        LevelMenu.highestLevelSolved = int.Parse(Data);
        int highestSolved = LevelMenu.highestLevelSolved;
        //Debug.Log(LevelMenu.Instance);
        GameObject.Find("highestsolved").GetComponent<Text>().text = Data;
        //LevelMenu.Instance.highestMarker.GetComponent<Text>().text = Data;


        // for(int i=0; i<highestSolved; i++){

        // }
    }

    void StringToGameData(string cloudData, string localData){/////////////////
        Debug.Log(cloudData + "cloud and local" + localData);
        if (PlayerPrefs.GetInt("IsFirstTime") == 1){
            PlayerPrefs.SetInt("IsFirstTime", 0);
            if (int.Parse(cloudData) > int.Parse(localData)){
                PlayerPrefs.SetString(SAVE_NAME, cloudData);
                AssignData(cloudData);
                //SaveDataLocally
                //LOADFROMCLOUD
            }
        }
        else{
            if(int.Parse(localData) > int.Parse(cloudData)){
                //uselocaldata
                AssignData(localData);

                isCloudDataLoaded = true;
                SaveData();
                return;
            }
        }
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
            isSaving = true;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME, 
                DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
        }
        else{
            SaveLocal();
        }
    }

    private void SaveLocal(){
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
        Debug.Log(game + " "+ "game");
        Debug.Log(status + " "+ "status");
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
        string stringToSave = GameDataToString();
        SaveLocal();

        byte[] dataToSave = Encoding.ASCII.GetBytes(stringToSave);

        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();

        ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, dataToSave, OnSavedGameDataWritten);

    }

    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] savedData){
        if(status == SavedGameRequestStatus.Success){
            string cloudDataString;
            if(savedData.Length == 0)
                cloudDataString = "0";
            else
                cloudDataString = Encoding.ASCII.GetString(savedData);

            string localDataString = PlayerPrefs.GetString(SAVE_NAME);

            StringToGameData(cloudDataString, localDataString);

        }
    } 

    private void OnSavedGameDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game){

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
