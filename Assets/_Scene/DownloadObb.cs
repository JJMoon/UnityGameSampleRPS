using UnityEngine;
using System.Collections;

public class DownloadObb : MonoBehaviour {
	
	#if UNITY_ANDROID
	private string expPath;
	private string logtxt;
	private bool alreadyLogged = false;
	private string nextScene = "Title";
	private bool downloadStarted;

	public Texture2D background;
	public GUISkin mySkin;

	void log( string t )
	{
		logtxt += t + "\n";
		print("MYLOG " + t);
	}
	void OnGUI()
	{

//		if (!GooglePlayDownloader.RunningOnAndroid())
//		{
//			GUI.Label(new Rect(10, 10, Screen.width-10, 20), "Use GooglePlayDownloader only on Android device!");
//			return;
//		}
//        GUI.Label(new Rect(10, 10, 2000, 20), "    MainPath" + GooglePlayDownloader.GetMainOBBPath(expPath));
//        GUI.Label(new Rect(10, 50, 2000, 20), "    ExpPath" + GooglePlayDownloader.GetExpansionFilePath());
//        if (GUI.Button (new Rect (100, 100, 2000, 50), "    ExpPath " + GooglePlayDownloader.GetExpansionFilePath())) {
//            StartCoroutine(loadLevel());
//        }






	}

	void Start () {
		expPath = GooglePlayDownloader.GetExpansionFilePath();
		string mainPath = GooglePlayDownloader.GetMainOBBPath(expPath);
		string patchPath = GooglePlayDownloader.GetPatchOBBPath(expPath);
        StartCoroutine(loadLevel());

	}

	protected IEnumerator loadLevel() 
	{ 
		string mainPath;
		do
		{
			yield return new WaitForSeconds(0.5f);
			mainPath = GooglePlayDownloader.GetMainOBBPath(expPath);	
			log("waiting mainPath "+mainPath);
		}
		while( mainPath == null);

		downloadStarted = true;

		string uri = "file://" + mainPath;
		log("downloading " + uri);
		WWW www = WWW.LoadFromCacheOrDownload(uri , 0);		

		// Wait for download to complete
		yield return www;

		if (www.error != null)
		{
			log ("wwww error " + www.error);
		}
		else
		{
			Application.LoadLevel(nextScene);
		}

	}
		#endif
		
}
