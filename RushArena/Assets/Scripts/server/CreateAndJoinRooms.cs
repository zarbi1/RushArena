using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createRoomInput;
    public InputField joinRoomInput;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoomInput.text); //name of the room 
        //note: when a player create a room he is also automaticly joining it.
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoomInput.text); //room code
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("SceneTest"); //when we want a "sync" scene we need to use the LoadLevele methode from photon, here the player is redirected to the player slecetion menu while wating for an other player
    }
}
