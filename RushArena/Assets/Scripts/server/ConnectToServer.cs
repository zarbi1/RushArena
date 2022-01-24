using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //auto connect to server from loading scene where the script is automaticly launched
    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {

        PhotonNetwork.JoinLobby(); //join the lobby scene as soon as the player is connected to the server
       // base.OnConnectedToMaster();
    }

    public override void OnJoinedLobby()
    {
        //succeffully joined the lobby scene
        SceneManager.LoadScene("Lobby"); //load the lobby scene
       // base.OnJoinedLobby();
    }
}
