using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkLayer : MonoBehaviour
{
    public static PhotonView ScriptsPhotonView;
    // Start is called before the first frame update
    void Start()
    {
        ScriptsPhotonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void MoveSteps(string playerName, int numberOfSteps)
    {
        Debug.Log("reached");
        ScriptsPhotonView.RPC("MoveStepsRPC", PhotonTargets.All, playerName, numberOfSteps);
    }

    [PunRPC]
    void MoveStepsRPC(string playerName, int numberOfSteps)
    {
        print("playerName " + playerName);
        print("numberOfSteps " + numberOfSteps);
        GameObject.Find(playerName).GetComponent<playerController>().Move(numberOfSteps);
    }

    public static void WinningStatus(string playerName)
    {
        ScriptsPhotonView.RPC("WinningStatus", PhotonTargets.All, playerName);
    }
    [PunRPC]
    void WinningStatusRPC(string playerName)
    {
        Debug.Log("sent");
        GameObject.Find("Scripts").GetComponent<LevelManger>().changeStatuesText(playerName + " is the winner");
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
