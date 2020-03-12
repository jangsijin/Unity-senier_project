using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GooglePlayServiceManager : MonoBehaviour {

    static private GooglePlayServiceManager instance;

    public static GooglePlayServiceManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GooglePlayServiceManager>();

                if(instance == null)
                {
                    instance = new GameObject("Google Play Service")
                        .AddComponent<GooglePlayServiceManager>();
                }
                
            }
            return instance;
        }
    }

	// Use this for initialization
	void Awake () {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames().Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = false;
        PlayGamesPlatform.Activate();
	}

    public bool isAuthenticated
    {
        get
        {
            return Social.localUser.authenticated;
        }
    }

    public void Login()
    {
        Social.localUser.Authenticate((bool success) => { if (!success) { Debug.Log("Login Fail"); } });
    }
	
	// Update is called once per frame
	public void CompleteThankYouForStart ()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ReportProgress(GPGSIds.achievement_thank_you_for_start, 100.0, (bool success) => { if (!success) { Debug.Log("Report Fail"); } });
	}

    public void CompletePleasePlayMore()
    {

        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ReportProgress(GPGSIds.achievement_please_play_more, 100.0, (bool success) => { if (success) { PlayerPrefs.SetInt("PleasePlayMore", 1); } });
    }

    public void ShowAchivementUI()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ShowAchievementsUI();
    }
}
