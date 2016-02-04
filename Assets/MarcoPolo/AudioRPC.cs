using UnityEngine;
 
public class AudioRPC : MonoBehaviour {
 
    public AudioClip marco;
    public AudioClip polo;
 
    [PunRPC]
    void Marco()
    {
        Debug.Log("Marco");
 
        this.GetComponent<AudioSource>().clip = marco;
        this.GetComponent<AudioSource>().Play();
    }
 
    [PunRPC]
    void Polo()
    {
    	// As RPC are not disabled even when the script is, we manually check it and return nothing if this is the case
    	if (!this.enabled)
	    {
	        return;
	    }
	 
        Debug.Log("Polo");
 
        this.GetComponent<AudioSource>().clip = polo;
        this.GetComponent<AudioSource>().Play();
    }


    // Allow to disable the audio script when the app loses focus. But RPC are not disabled !
    void OnApplicationFocus(bool focus)
	{
	    this.enabled = focus;
	}
}