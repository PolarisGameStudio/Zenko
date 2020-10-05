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
using VoxelBusters.NativePlugins;
//using GoogleMobileAds.Api;

public class PlayServices : MonoBehaviour
{
    public static PlayServices instance;
    const string SAVE_NAME = "SaveFile";
    const string TEST_NAME = "TestFile";
    public static string mergedData;
    public static string curCloudData;
    public static string newMergedString;
    bool isSaving;
    bool isCloudDataLoaded = false;
    bool finishedLoading;
    Loading loader;
    int read;
    bool isGameServicesInitialized;

    void Awake()
    {
        read = 0;
        finishedLoading = false;
        if(instance == null)
        {
            #if UNITY_EDITOR
            //PlayerPrefs.DeleteAll();
            #endif
            loader = GameObject.Find("Handler").GetComponent<Loading>();
            Debug.Log("loader is " + loader);
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            //initiateDictionarys
            LevelStorer.PopulateFiveChapters(); //load level data (200) int levelstorer.leveldic
            LevelStorer.PopulatePotd500(); //loads data into levelstorer.potddic

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
        #if UNITY_IOS
        NPBinding.CloudServices.Initialise();
        isGameServicesInitialized = InitializeGameServices();
        //finishedLoading = true;
        //loader.Loaded();
        #endif  
    }
    #if UNITY_IOS
    void OnEnable(){
        CloudServices.KeyValueStoreDidInitialiseEvent += OnKeyValueStoreInitialised;
        CloudServices.KeyValueStoreDidChangeExternallyEvent     += OnKeyValueStoreChanged;
        CloudServices.KeyValueStoreDidSynchroniseEvent     += OnKeyValueStoreDidSynchronise;
    }

    void OnDisable(){
        CloudServices.KeyValueStoreDidInitialiseEvent -= OnKeyValueStoreInitialised;
        CloudServices.KeyValueStoreDidChangeExternallyEvent -= OnKeyValueStoreChanged;
        CloudServices.KeyValueStoreDidSynchroniseEvent     -= OnKeyValueStoreDidSynchronise;
    }

    void SaveToCloud(bool signedin){
        SaveLocal();
        if(signedin){
            NPBinding.CloudServices.SetString(SAVE_NAME, GameDataToString());
            NPBinding.CloudServices.Synchronise();
            Debug.Log("new cloud is " + NPBinding.CloudServices.GetString(SAVE_NAME));
        }
    }
    void OnKeyValueStoreInitialised(bool success){
        Debug.Log("INITIALIZED WITH SUCCESS BEING " + success);
        Debug.Log(loader);
        loader.DaDebug("ios init is " + success + " " + NPBinding.CloudServices.GetString(SAVE_NAME));
        Debug.Log(NPBinding.CloudServices.GetString(SAVE_NAME));
        Debug.Log("post loader.dadebug");
        if(success){
            isCloudDataLoaded = true;
            Debug.Log("BOUTTAGETSTRING");
            string stringValueOnCloud = NPBinding.CloudServices.GetString(SAVE_NAME);
            string testValueOnCloud = NPBinding.CloudServices.GetString(TEST_NAME);
            NPBinding.CloudServices.SetString(TEST_NAME, "test");
            NPBinding.CloudServices.Synchronise();
            Debug.Log("Stringvalueoncloud is " + stringValueOnCloud);
            Debug.Log("gamedatatostring is " + GameDataToString());
            Debug.Log("TEST DATA WAS " + testValueOnCloud);
            Debug.Log("TEST DATA IS NOW " + NPBinding.CloudServices.GetString(TEST_NAME));
            if(stringValueOnCloud == null){
                NPBinding.CloudServices.SetString(SAVE_NAME, GameDataToString());
                NPBinding.CloudServices.Synchronise();
                loader.Loaded();
                Debug.Log(NPBinding.CloudServices.GetString(SAVE_NAME));
            }
            else{

                string mergedValue = MergeData(stringValueOnCloud, GameDataToString());
                NPBinding.CloudServices.SetString(SAVE_NAME, mergedValue);
                NPBinding.CloudServices.Synchronise();
                finishedLoading = true;
                AssignData(mergedValue);
            }
        }
        else{
            
        }
    }

    private void OnKeyValueStoreChanged (eCloudDataStoreValueChangeReason _reason, string[] _changedKeys)
    {
        Debug.Log("Cloud key-value store has been changed.");
        Debug.Log(string.Format("Reason: {0}.", _reason));

        if (_changedKeys != null)
        {
            Debug.Log(string.Format("Total keys changed: {0}.", _changedKeys.Length));
            Debug.Log(string.Format("Pick a value from old and new and set the value to cloud for resolving conflict."));

            foreach (string _changedKey in _changedKeys)
            {
                string stringValueOnCloud = NPBinding.CloudServices.GetString(_changedKey);
                if(stringValueOnCloud == null){
                    NPBinding.CloudServices.SetString(_changedKey, GameDataToString());
                }
                else{
                    string mergedValue = MergeData(stringValueOnCloud, GameDataToString());
                    NPBinding.CloudServices.SetString(_changedKey, mergedValue);
                    AssignData(mergedValue);
                }               
            }
        }
    }

    void OnKeyValueStoreDidSynchronise(bool _success){
        if (_success)
        {
            Debug.Log("Successfully synchronised in-memory keys and values.");
            Debug.Log("TEST DATA POST SYNC IS NOW " + NPBinding.CloudServices.GetString(TEST_NAME));
            Debug.Log("SAVE DATA POST SYNC IS NOW " + NPBinding.CloudServices.GetString(SAVE_NAME));
        }
        else
        {
            Debug.Log("Failed to synchronise in-memory keys and values.");
        }
    }

    bool InitializeGameServices(){
        bool _isAvailable = NPBinding.GameServices.IsAvailable();
        bool success = false;
        Debug.Log("is available is " + _isAvailable);
        if(_isAvailable){
            bool _isAuthenticated = NPBinding.GameServices.LocalUser.IsAuthenticated;
            Debug.Log("is authenticated is " + _isAuthenticated);
            if(!_isAuthenticated){

                NPBinding.GameServices.LocalUser.Authenticate((bool _success, string _error)=>{
                    if (_success)
                    {
                        Debug.Log("Sign-In Successfully");
                        Debug.Log("Local User Details : " + NPBinding.GameServices.LocalUser.ToString());
                        success = true;
                    }
                    else
                    {
                        Debug.Log("Sign-In Failed with error " + _error);
                        success = false;
                    }
                });
                return success;
            }
            else
                return true;
        }
        else
            return false;
    }

    static void iOSAchievement(string achievementID){
        NPBinding.GameServices.ReportProgressWithGlobalID(achievementID, 100, (bool _status, string _error)=>{
            if (_status)
            {
                Debug.Log(achievementID + " " + "successfully reported");
            }
            else
            {
                Debug.Log(achievementID + " " + "couldnt be reported");
                Debug.Log("Achievement error is " + _error);
            }
        });
    }
    #endif
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

        AssignFirstFourChapters(dataArray);
        AssignPotdData(dataArray);
        AssignChapterFive(dataArray);
        LevelStorer.highestSolved = LevelMenu.FindHighestSolved();
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
        #if UNITY_IOS
            LoadLocal();
        #endif
    }

    private void LoadLocal(){
        StringToGameData(PlayerPrefs.GetString(SAVE_NAME));
    }

    public void SaveData(){
        #if UNITY_ANDROID
        if(!isCloudDataLoaded){
            curCloudData = "0";
            isCloudDataLoaded = true;
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

        #if UNITY_IOS
            SaveToCloud(isCloudDataLoaded);
        #endif
    }

    public void SaveLocal(){
        PlayerPrefs.SetString(SAVE_NAME, GameDataToString());
    }

    private void ResolveConflict(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData, ISavedGameMetadata unmerged, byte[] unmergedData){
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
        ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, dataToSave, OnSavedGameDataWritten);
        #endif
        #if UNITY_IOS
        SaveLocal();
        #endif
    }

    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] savedData){
        Debug.Log("Saved Data byte is " + savedData.Length + " long, the first ones are " + savedData[0]);
        Debug.Log(Encoding.ASCII.GetString(savedData) + " IS THE DATA READ");
        loader.DaDebug("SavedGameDataRead");
        int parsed;
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
            #if UNITY_ANDROID
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
            #endif
            #if UNITY_IOS
            case 1:
                iOSAchievement("world1complete");
                break;
            case 2:
                iOSAchievement("world2complete");
                break;
            case 3:
                iOSAchievement("world3complete");
                break;
            case 4:
                iOSAchievement("world4complete");
                break;
            case 5:
                iOSAchievement("world5complete");
                break;            
            #endif
        }
    }

    public static void IncrementAchievement(string id, int stepsToIncrement) 
    {
        #if UNITY_ANDROID
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
        #endif
    }

    #endregion /Achievements
}
