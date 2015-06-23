using UnityEngine;
using System.Collections;

public class CloseThisObject : MonoBehaviour
{
    public string mTimestamp, mUrl;

    //public JceTextNotice JceImgNotice  JceTxtNotiObj;
    public AmNotification JceNotiObj;

    void DestoryObj ()
    {
        //Debug.Log ("Clicked");
        if (this.gameObject.transform.FindChild ("check_today").GetComponent<UICheckbox> ().isChecked) {
            PreviewLabs.PlayerPrefs.SetString ("JoyCityImageBanner" + JceNotiObj.idx , mTimestamp);
            //Debug.Log (this.gameObject.transform.FindChild ("check_today").GetComponent<UICheckbox> ().isChecked +"checked" + this.gameObject.name);
        }
        FreqencySetting ();

        DestroyObject (this.gameObject);
    }


    void FreqencySetting () {
        if (JceNotiObj == null)
            Ag.LogIntenseWord (" Error ");
        else {
            if (JceNotiObj.IsFreqency) {
            
                PreviewLabs.PlayerPrefs.SetString ("JoyCityImageBannerFreq" + JceNotiObj.idx, (JceNotiObj.AlreadySeenNum + 1).ToString ());
                Ag.LogIntenseWord (this.gameObject.name + " ::::  FrequencyEa :" +(JceNotiObj.AlreadySeenNum + 1).ToString());

            } 
        }

        PreviewLabs.PlayerPrefs.Flush();
    }
    void FreqencySettingTextBanner () {
        if (JceNotiObj == null)
            Ag.LogIntenseWord (" Error ");
        else {
            if (JceNotiObj.IsFreqency) {

                PreviewLabs.PlayerPrefs.SetString ("JoyCityTextBannerTextFreq" + JceNotiObj.idx, (JceNotiObj.AlreadySeenNum + 1).ToString ());
                Ag.LogIntenseWord (this.gameObject.name + " ::::  FrequencyEa :" +(JceNotiObj.AlreadySeenNum + 1).ToString());

            } 
        }

        PreviewLabs.PlayerPrefs.Flush();
    }

    void OpenUrl ()
    {
        Application.OpenURL (mUrl);
    }

    void DestoryTextObj ()
    {
        FreqencySettingTextBanner ();
        DestroyObject (this.gameObject);
    }
}
