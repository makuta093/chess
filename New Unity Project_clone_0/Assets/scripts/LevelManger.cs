using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManger : MonoBehaviour
{
    private Text WinningStatus;
    // Start is called before the first frame update
    void Start()
    {
        WinningStatus = GameObject.Find("WinningStatus").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeStatuesText(string satus)
    {
        WinningStatus.text = satus;
    }

    public void GenerateRandomNumber()
    {
        int randomNumberSteps = Random.Range(1,4);

        //inform everone about this incuding myslef 
        NetworkLayer.MoveSteps(NetworkManger.myGamePlayerId, randomNumberSteps);
    }
}
