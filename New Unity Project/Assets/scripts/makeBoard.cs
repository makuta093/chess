using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeBoard : MonoBehaviour
{
    public float starty = 4.25f;
    public float startx = -4.25f;
    public int y = 1;
    public int x = 1;
    public bool colorR = false;
    public GameObject whiteR, blackR;
    // Start is called before the first frame update

    public static void MakeBoard()
    { 
         float starty = 4.25f;
     float startx = -4.25f;
     int y = 0;
     int x = 0;
     bool colorR = false;
     GameObject whiteR, blackR, b;

    b = GameObject.Find("board");
    whiteR = GameObject.Find("white");
    blackR = GameObject.Find("black");
        Debug.Log("reacherd");
        while ((x <= 9) && (y <= 9))
        {
            if (x == 9)
            {
                Debug.Log("reached x");
                if ((y <= 9))
                {
                    Debug.Log("reached y");
                    x = 1;
                    starty = -4.25f;
                    y++;
                    startx = startx - 1.25f;
                    if (colorR == false)
                    {
                        colorR = true;
                    }
                    else if (colorR == true)
                    {
                        colorR = false;
                    }

                }
            }

            
            if (colorR == false)
            {
                
                GameObject go1 = Instantiate(whiteR, Vector3.zero, Quaternion.identity) as GameObject;
                go1.name = (y + "," + x);
                go1.transform.position = new Vector3(starty, startx,2);
                go1.transform.parent = b.transform;
                colorR = true;
                x++;
                starty = starty + 1.25f;
            }
            else if (colorR == true)
            {
                
                GameObject go1 = Instantiate(blackR, Vector3.zero, Quaternion.identity) as GameObject;
                go1.name = (y + "," + x);
                go1.transform.position = new Vector3(starty, startx, 2);
                go1.transform.parent = b.transform;
                colorR = false;
                x++;
                starty = starty + 1.25f;
            }

            
            
        }
    }

    public static void chant()
    {
        MakeBoard();
    }
            void Start()
            {
                
            }

            // Update is called once per frame
            void Update()
            {

            }
        }
    

