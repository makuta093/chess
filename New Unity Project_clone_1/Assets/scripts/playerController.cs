using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    private List<GameObject> pathList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1;i < 4; i++)
        {
            pathList.Add(GameObject.Find(gameObject.name + "Path" + i));
        }
        //Move(1);

        print(pathList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(int numberofSteps)
    {
        StartCoroutine(MovetoPath(numberofSteps));
       
    }
    IEnumerator MovetoPath(int numberofSteps)
    {
        for(int i = 0; i < numberofSteps; i++)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject path = pathList[i];
            transform.position = path.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            print("FINISHED:" + gameObject.name);
            if(NetworkManger.myGamePlayerId == gameObject.name)
            {
                NetworkLayer.WinningStatus(NetworkManger.myGamePlayerId);
            }
        }
    }
}
