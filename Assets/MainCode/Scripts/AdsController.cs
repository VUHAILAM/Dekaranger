using System;
using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System.Collections.Generic;

public class AdsController : MonoBehaviour {
	const string leaderboard_unlimit_highscore = "CgkIzamJ55MZEAIQCA";	
	public static AdsController instance = null;
		
	void Awake(){
		
		if(instance != null){
			Destroy(gameObject);

		}else{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		#if UNITY_ANDROID || UNITY_IOS
		Authenticate();
		#endif
	}

	public void Authenticate()
	{
		#if UNITY_ANDROID || UNITY_IOS
		if (Authenticated){
			Debug.LogWarning("Ignoring repeated call to Authenticate().");
			return;
		}
		
		// Enable/disable logs on the PlayGamesPlatform
		PlayGamesPlatform.DebugLogEnabled = true;
		
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
			//			.EnableSavedGames()
			.Build();
		PlayGamesPlatform.InitializeInstance(config);
		//		
		//		// Activate the Play Games platform. This will make it the default
		//		// implementation of Social.Active
		PlayGamesPlatform.Activate();
		
		// Set the default leaderboard for the leaderboards UI
		//((PlayGamesPlatform)Social.Active).SetDefaultLeaderboardForUI(LEADERBOARD_SPEEDTEST);
		
		SignIn();
		#endif
	}
	
	public void SignIn(){
		// Sign in to Google Play Games
		Social.localUser.Authenticate ((bool success) =>
		                               {
			if (success) {
				// if we signed in successfully, load data from cloud
				Debug.Log ("Login successful!");
			} else {
				// no need to show error message (error messages are shown automatically by plugin)
				Debug.LogWarning ("Failed to sign in with Google Play Games.");
			}
		});
	}
	
	public void ShowLeaderBoardUI(){
		#if UNITY_ANDROID || UNITY_IOS
		if (Authenticated)
		{
			Debug.Log("Show all LeaderBoard.");
			Social.ShowLeaderboardUI();
		}
		#endif
	}
	
	public void UpdateScoreToLeaderboard(int score){
		if (Authenticated){
				// post score to leaderboard ID LEADERBOARD_SPEEDTEST)
			Social.ReportScore(score, leaderboard_unlimit_highscore, (bool success) => {
					// handle success or failure
				});
		}else {
			Debug.Log("Not reporting score");
		}
	}
	
	public bool Authenticated
	{
		get
		{
			return Social.Active.localUser.authenticated;
		}
	}
}
