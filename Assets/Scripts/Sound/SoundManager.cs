using UnityEngine;
using System.Collections;



[RequireComponent (typeof(AudioSource))]
public class SoundManager : MonoBehaviour {
    private  static SoundManager instance;
    private AudioSource Audio;
    
    //effect Sound
    
    private AudioClip effectSound;
    
    public static SoundManager Instance{
         get{
            if(instance == null)
            {
                GameObject projectile = (GameObject)Instantiate((GameObject)Resources.Load("Sound/Effect"));
                instance = projectile.GetComponent<SoundManager>();
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
    
    public void Play_Effect_Sound (string Name) 
    {

        if (PreviewLabs.PlayerPrefs.GetBool ("FXSoundOff")) {
            effectSound = ( AudioClip)Resources.Load ("Sound/Effect/"+ Name);
            Audio.PlayOneShot(effectSound);
        }
//        Debug.Log (effectSound);
    }
    public void Play_Effect_SoundStop () {
            gameObject.audio.Stop ();
    }
    public void GameSoundPlayer () {
        if (PreviewLabs.PlayerPrefs.GetBool ("FXSoundOff")) {
            gameObject.audio.Play ();
        }
    }
    
    void Destroy() {
        effectSound = null;
        Resources.UnloadUnusedAssets();
        
    }
}

