using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Unity.Editor;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class loadFirebase : MonoBehaviour
{
    public GameObject dbText, nameInput, xposInput, yposInput, shapeInput;

    int numberOfRecords = 0;
    bool usercreated = false;
    bool userloggedin = false;

    DatabaseReference reference;

    //main data dictionary
    Dictionary<string, object> myDataDictionary;

    //a reference to the firebase authentication scheme
    FirebaseAuth auth;

    string email = "makuta093@gmail.com";
    string password = "Tankore093";

    //get the number of records for a child
    IEnumerator getNumberOfRecords()
    {

        bool numberofrecordsretreived = false;

        reference.GetValueAsync().ContinueWithOnMainThread(
            getValueTask =>
            {
                if (getValueTask.IsFaulted)
                {
                    Debug.Log("Error getting data " + getValueTask.Exception);
                }

                if (getValueTask.IsCompleted)
                {
                    DataSnapshot snapshot = getValueTask.Result;
                    Debug.Log(snapshot.ChildrenCount);
                    numberOfRecords = (int)snapshot.ChildrenCount;
                    numberofrecordsretreived = true;
                }


            }
            );

        while (!numberofrecordsretreived)
            yield return null;


    }

    IEnumerator getDataFromFirebase()
    {
        bool displaydata = false;

        reference.GetValueAsync().ContinueWithOnMainThread(
            getValueTask =>
            {


                if (getValueTask.IsFaulted)
                {
                    Debug.Log("Error getting data " + getValueTask.Exception);
                }

                if (getValueTask.IsCompleted)
                {
                    DataSnapshot snapshot = getValueTask.Result;
                    Debug.Log(snapshot.Value.ToString());

                    //snapshot object is casted to an instance of its type


                    myDataDictionary = (Dictionary<string, object>)snapshot.Value;


                    Debug.Log("Data received" + Time.time);
                    displaydata = true;
                }


            }
            );
        //shock absorber
        while (!displaydata)
        {
            //the data has NOT YET been saved to snapshot
            yield return null;

        }
        //Debug.Log("got some data");
        //the data has been saved to snapshot here
        yield return StartCoroutine(displayData());
    }

    //get the parent data
    IEnumerator displayData()
    {
        foreach (var element in myDataDictionary)
        {

            Debug.Log(element.Key.ToString() + "<->" + element.Value.ToString());

            //  Debug.Log(showData(element.Value));
            yield return new WaitForSeconds(1f);
        }

        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
