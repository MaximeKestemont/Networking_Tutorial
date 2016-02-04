using UnityEngine;
using Photon;
 
public class RandomMatchMaker : Photon.PunBehaviour
{
	private PhotonView myPhotonView;


    // Use this for initialization
    void Start()
    {
    	//PhotonNetwork.logLevel = PhotonLogLevel.Full;
        PhotonNetwork.ConnectUsingSettings("0.1");
    }
 

    public override void OnJoinedLobby()
    {
    	PhotonNetwork.JoinRandomRoom();
	}

	public override void OnJoinedRoom(){
		GameObject monster = PhotonNetwork.Instantiate("monsterprefab", Vector3.zero, Quaternion.identity, 0);
		monster.GetComponent<myThirdPersonController>().isControllable = true;
		monster.GetComponent<CharacterCamera>().enabled = true;

		// Store the view
		myPhotonView = monster.GetComponent<PhotonView>();
		
		/*
		CharacterControl controller = monster.GetComponent<CharacterControl>();
	    controller.enabled = true;
	    CharacterCamera camera = monster.GetComponent<CharacterCamera>();
	    camera.enabled = true;*/
	}

	void OnPhotonRandomJoinFailed()
	{
	    Debug.Log("Can't join random room!");
	    PhotonNetwork.CreateRoom(null);
	}

	void OnGUI()
	{
	    GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	 
	    if (PhotonNetwork.connectionStateDetailed == PeerState.Joined)
	    {
	        bool shoutMarco = GameLogic.playerWhoIsIt == PhotonNetwork.player.ID;
	 
	        if (shoutMarco && GUILayout.Button("Marco!"))
	        {
	            this.myPhotonView.RPC("Marco", PhotonTargets.All);
	        }
	        if (!shoutMarco && GUILayout.Button("Polo!"))
	        {
	            this.myPhotonView.RPC("Polo", PhotonTargets.All);
	        }
	    // and so on...
	}
}
}