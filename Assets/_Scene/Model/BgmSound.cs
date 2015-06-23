using UnityEngine;
using System.Collections;



[RequireComponent (typeof(AudioSource))]
public class BgmSound : MonoBehaviour {
	private  static BgmSound instance;
	private AudioSource Audio;

	//effect Sound

	private AudioClip effectSound;

	public static BgmSound Instance{
		get{
			if(instance == null)
			{
				GameObject projectile = (GameObject)Instantiate((GameObject)Resources.Load("Sound/BgmSound"));
				instance = projectile.GetComponent<BgmSound>();
                projectile.name = "BGMSOUND";
                DontDestroyOnLoad (projectile);
			}
			return instance;
		}
	}

	// Use this for initialization
	void Start () { 


	}

	// Update is called once per frame
	void Update () {

	}

	void Awake () {
		Audio = gameObject.GetComponent<AudioSource>();
	}

	//effect Sound
    public void Play () {
		if (!gameObject.audio.isPlaying)
        	gameObject.audio.Play ();
    }

    public void Stop () {
        gameObject.audio.Stop ();
    }


    public void Pause () {
        gameObject.audio.Pause ();
    }

	public void Play_Effect_Sound (string Name) 
	{

		effectSound = ( AudioClip)Resources.Load ("Sound/Effect/"+ Name);
		Audio.PlayOneShot(effectSound);
//        Debug.Log (effectSound);
	}
	public void Play_Effect_Sound () {

	}

	void Destroy() {
		effectSound = null;
		Resources.UnloadUnusedAssets();

	}
}
