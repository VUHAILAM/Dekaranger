﻿using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;

public class GeneralSharing : MonoBehaviour
{
	#region PUBLIC_VARIABLES
	//public Texture2D MyImage;
	#endregion
	
	#region UNITY_DEFAULT_CALLBACKS
	public void OnEnable ()
	{
		ScreenshotHandler.ScreenshotFinishedSaving += ScreenshotSaved;
	}
	
	void OnDisable ()
	{
		ScreenshotHandler.ScreenshotFinishedSaving -= ScreenshotSaved;
	}
	#endregion

	#region DELEGATE_EVENT_LISTENER
	void ScreenshotSaved ()
	{
		#if UNITY_IPHONE || UNITY_IPAD
		GeneralSharingiOSBridge.ShareTextWithImage (ScreenshotHandler.savedImagePath, "#RoadFighter");
		#endif
	}
	#endregion
	
	#region CO_ROUTINES
	IEnumerator ShareAndroidText ()
	{
		yield return new WaitForEndOfFrame ();
		#if UNITY_ANDROID

		var width = Screen.width;
		var height = Screen.height;
		var tex = new Texture2D (width, height, TextureFormat.RGB24, false);
		// Read screen contents into the texture
		tex.ReadPixels (new Rect (0, 0, width, height), 0, 0);
		tex.Apply ();
		
		// Encode texture into PNG
		var bytes = tex.EncodeToPNG ();
		Destroy (tex);
		string path = Application.persistentDataPath + "/SavedScreen.png";
		File.WriteAllBytes (path, bytes);

//		byte[] bytes = MyImage.EncodeToPNG();
//		string path = Application.persistentDataPath + "/MyImage.png";
//		File.WriteAllBytes(path, bytes);

		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
		
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
		intentObject.Call<AndroidJavaObject>("setType", "image/*");
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "Text Sharing ");
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "Text Sharing ");
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "Text Sharing Android Demo");
		
		AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
		
		AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", path);// Set Image Path Here
		
		AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("fromFile", fileObject);
		
		//			string uriPath =  uriObject.Call<string>("getPath");
		bool fileExist = fileObject.Call<bool>("exists");
		Debug.Log("File exist : " + fileExist);
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		currentActivity.Call("startActivity", intentObject);
		
		#endif
	}
	
	IEnumerator SaveAndShare ()
	{
		yield return new WaitForEndOfFrame ();
		#if UNITY_ANDROID
		RenderTexture.active = Camera.main.targetTexture;
		var width = Screen.width;
		var height = Screen.height;
		var tex = new Texture2D (width,height, TextureFormat.RGB24, false);
		// Read screen contents into the texture1
		tex.ReadPixels (new Rect (0, 0, width, height), 0, 0);
		tex.Apply ();
		
		// Encode texture into PNG
		var bytes = tex.EncodeToPNG ();
		Destroy (tex);
		string path = Application.persistentDataPath + "/SavedScreen.png";
		File.WriteAllBytes (path, bytes);


//		byte[] bytes = MyImage.EncodeToPNG();
//		string path = Application.persistentDataPath + "/MyImage.png";
//		File.WriteAllBytes(path, bytes);

		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
		
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
		intentObject.Call<AndroidJavaObject>("setType", "image/*");
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "Media Sharing ");
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "Media Sharing ");
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "Media Sharing Android Demo");
		
		AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
		AndroidJavaClass fileClass = new AndroidJavaClass("java.io.File");
		
		AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", path);// Set Image Path Here
		
		AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("fromFile", fileObject);
		
		//			string uriPath =  uriObject.Call<string>("getPath");
		bool fileExist = fileObject.Call<bool>("exists");
		Debug.Log("File exist : " + fileExist);
		if (fileExist)
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
		
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		currentActivity.Call("startActivity", intentObject);
		#endif
		
	}
	#endregion
	
	#region BUTTON_CLICK_LISTENER
	
	public void OnShareSimpleText ()
	{
		#if UNITY_ANDROID
		StartCoroutine (ShareAndroidText ());
		#elif UNITY_IPHONE || UNITY_IPAD
		GeneralSharingiOSBridge.ShareSimpleText ("");
		#endif
	}
	
	public void OnShareTextWithImage ()
	{
		Debug.Log ("Media Share");
		#if UNITY_ANDROID
		StartCoroutine (SaveAndShare ());
		#elif UNITY_IPHONE || UNITY_IPAD
		//string fileName = "ShareScreenShot.png";
		//Application.CaptureScreenshot(fileName);

//		byte[] bytes = MyImage.EncodeToPNG ();
//		string path = Application.persistentDataPath + "/MyImage.png";
//		File.WriteAllBytes (path, bytes);
		//string path_ = fileName;
		StartCoroutine(captureScreen());
		//StartCoroutine (ScreenshotHandler.Save (path_, "Media Share", true));
		#endif
	}
	public IEnumerator captureScreen ()
	{
		yield return new WaitForEndOfFrame ();
		var width = Screen.width;
		var height = Screen.height;
		var tex = new Texture2D (width, height, TextureFormat.RGB24, false);
		// Read screen contents into the texture
		tex.ReadPixels (new Rect (0, 0, width, height), 0, 0);
		tex.Apply ();
		
		// Encode texture into PNG
		var bytes = tex.EncodeToPNG ();
		Destroy (tex);
		string path = Application.persistentDataPath + "/SavedScreen.png";
		File.WriteAllBytes (path, bytes);
		string path_ = "SavedScreen.png";
		StartCoroutine (ScreenshotHandler.Save (path_, "Media Share", true));
	}
	#endregion
	
}
