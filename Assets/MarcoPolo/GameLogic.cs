using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {
	
	public static int playerWhoIsIt;
	private static PhotonView ScenePhotonView;
	 
	void Start()
	{
	    ScenePhotonView = this.GetComponent<PhotonView>();
	}
	 
	void OnPhotonPlayerConnected(PhotonPlayer player)
	{
	    Debug.Log("OnPhotonPlayerConnected: " + player);
	 
	    // when new players join, we send "who's it" to let them know
	    // only one player will do this: the "master"
	 
	    if (PhotonNetwork.isMasterClient)
	    {
	        TagPlayer(playerWhoIsIt);
	    }
	}
	 
	// Will call the RPC to communicate who is the "it" 
	public static void TagPlayer(int playerID)
	{
	    Debug.Log("TagPlayer: " + playerID);
	    ScenePhotonView.RPC("TaggedPlayer", PhotonTargets.All, playerID);
	}

	// When a player DC, check that he was not the "it". If this was the case, the current room master is the new "it".
	void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
	    Debug.Log("OnPhotonPlayerDisconnected: " + player);
	 
	    if (PhotonNetwork.isMasterClient)
	    {
	        if (player.ID == playerWhoIsIt)
	        {
	            // if the player who left was "it", the "master" is the new "it"
	            TagPlayer(PhotonNetwork.player.ID);
	        }
	    }
	}

	void OnJoinedRoom()
	{
	    // game logic: if this is the only player, we're "it"
	    if (PhotonNetwork.playerList.Length == 1)
	    {
	        playerWhoIsIt = PhotonNetwork.player.ID;
	    }
	 
	    Debug.Log("playerWhoIsIt: " + playerWhoIsIt);
	}

	[PunRPC]
	void TaggedPlayer(int playerID)
	{
	    playerWhoIsIt = playerID;
	    Debug.Log("TaggedPlayer: " + playerID);
	}
}
