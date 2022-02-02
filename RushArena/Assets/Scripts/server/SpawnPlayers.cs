using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    //let's setup the random pos to start for the player

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Start()
    {
        Vector2 randPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        PhotonNetwork.Instantiate(playerPrefab.name, randPos, Quaternion.identity);
    }
}
