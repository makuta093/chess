using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManger : MonoBehaviour
{
    public GUIStyle textStyle = new GUIStyle();

    public static string myGamePlayerId = "";
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnJoinedLobby()
    {

        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        print("Connected to Room");
        int numberOfPlayers = PhotonNetwork.playerList.Length;
        print("number of players" + numberOfPlayers);

        myGamePlayerId = "Player" + numberOfPlayers;
    }

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString()+":"+myGamePlayerId, textStyle);
    }
}
