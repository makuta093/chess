using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Storage;
using Firebase.Extensions;
using Firebase.Unity.Editor;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.IO;

public class chessfirebase : MonoBehaviour
{
    public class objects
    {

        public int POSX;
        public int POSY;
        
        public string shape;
        


    }

    DatabaseReference reference;

    //reference to the storage bucket
    FirebaseStorage storage;

    string output = "";

    int counter = 0;
    public int numberOfRecords = 0;
    

    //main data dictionary
    Dictionary<string, object> myDataDictionary;

    //a reference to the firebase authentication scheme
    FirebaseAuth auth;

    string email = "makuta093@gmail.com";
    string password = "Tankore093";
    // Start is called before the first frame update
    void Start()
    {
        
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://chess-9c5ab-default-rtdb.europe-west1.firebasedatabase.app/");

        reference = FirebaseDatabase.DefaultInstance.RootReference;
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(signInToFirebase());
    }


    //sign in to the firebase instance so we can read some data
    //Coroutine Number 1
    IEnumerator signInToFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;

        //the outside task is a DIFFERENT NAME to the anonymous inner class
        Task signintask = auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(
             signInTask =>
             {
                 if (signInTask.IsCanceled)
                 {
                     //write cancelled in the console
                     Debug.Log("Cancelled!");
                     return;
                 }
                 if (signInTask.IsFaulted)
                 {
                     //write the actual exception in the console
                     Debug.Log("Something went wrong!" + signInTask.Exception);
                     return;
                 }

                 Firebase.Auth.FirebaseUser loggedInUser = signInTask.Result;
                 Debug.Log("User " + loggedInUser.DisplayName + " has logged in!");

             }
            );


        yield return new WaitUntil(() => signintask.IsCompleted);

        Debug.Log("User has signed in");



    }


    IEnumerator createUser()
    {
        auth = FirebaseAuth.DefaultInstance;





        Task createusertask = auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(
             //created an anonymous inner class inside continueonmainthread which is of type Task
             createUserTask =>
             {
                 //if anything goes wrong
                 if (createUserTask.IsCanceled)
                 {
                     //I pressed escape or cancelled the task
                     Debug.Log("Sorry, user was not created!");
                     return;
                 }
                 if (createUserTask.IsFaulted)
                 {
                     //my internet exploded or firebase exploded or some other error happened here
                     Debug.Log("Sorry, user was not created!" + createUserTask.Exception);

                     return;
                 }
                 //if anything goes wrong, otherwise
                 Firebase.Auth.FirebaseUser myNewUser = createUserTask.Result;
                 Debug.Log("Your nice new user is:" + myNewUser.DisplayName + " " + myNewUser.UserId);
                 //THIS IS WHAT HAPPENS AT THE END OF THE ASYNC TASK


             }
             );



        //*a better way to wait until the end of the coroutine*//
        yield return new WaitUntil(() => createusertask.IsCompleted);



    }

    //get the number of records for a child
    public IEnumerator getNumberOfRecords()
    {
        Task numberofrecordstask = reference.GetValueAsync().ContinueWithOnMainThread(
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

                 }


             }
             );
        /*
        while (!numberofrecordsretreived)
            yield return null;*/

        yield return new WaitUntil(() => numberofrecordstask.IsCompleted);


    }


    IEnumerator updateDataClass(string childlabel, string newdata)
    {


        //find the child of player4 that corresponds to playername and set the value to whatever is inside newdata
        Task updateJsonValueTask = reference.Child(childlabel).SetRawJsonValueAsync(newdata).ContinueWithOnMainThread(
            updJsonValueTask =>
            {
                if (updJsonValueTask.IsCompleted)
                {
                    // dataupdated = true;
                }

            });


        yield return new WaitUntil(() => updateJsonValueTask.IsCompleted);




    }

   

    IEnumerator getDataFromFirebase(string childLabel)
    {

        Task getdatatask = reference.Child(childLabel).GetValueAsync().ContinueWithOnMainThread(
            getValueTask =>
            {
                if (getValueTask.IsFaulted)
                {
                    Debug.Log("Error getting data " + getValueTask.Exception);
                }

                if (getValueTask.IsCompleted)
                {
                    DataSnapshot snapshot = getValueTask.Result;
                    //Debug.Log(snapshot.Value.ToString());

                    //snapshot object is casted to an instance of its type
                    myDataDictionary = (Dictionary<string, object>)snapshot.Value;


                    //    Debug.Log("Data received");

                }


            }
            );
        //shock absorber
        /* while (!displaydata)
         {
             //the data has NOT YET been saved to snapshot
             yield return null;

         }*/

        yield return new WaitUntil(() => getdatatask.IsCompleted);

        //the data has been saved to snapshot here
        yield return StartCoroutine(displayData());
    }

    IEnumerator displayData()
    {
        foreach (var element in myDataDictionary)
        {
            Debug.Log(element.Key.ToString() + "<->" + element.Value.ToString());
            yield return new WaitForSeconds(1f);
        }

        yield return null;
    }

    public IEnumerator getOtherPlayerKey(Player otherPlayer, gameManager g)
    {
        Task getotherplayer = reference.GetValueAsync().ContinueWithOnMainThread(
            getOtherPlayerTask =>
            {
                if (getOtherPlayerTask.IsFaulted)
                {
                    Debug.Log("Error getting data " + getOtherPlayerTask.Exception);
                }
                if (getOtherPlayerTask.IsCompleted)
                {
                    DataSnapshot snapshot = getOtherPlayerTask.Result;
                    //Debug.Log(snapshot.Value.ToString());

                    //snapshot object is casted to an instance of its type
                    myDataDictionary = (Dictionary<string, object>)snapshot.Value;


                }
            }

            );

        //wait until I'm done.
        yield return new WaitUntil(() => getotherplayer.IsCompleted);
        //all the data inside mydatadictionary
        //this gets me the second key (which is what I want)

        foreach (var element in myDataDictionary)
        {
            if (!(g.currentPlayerKey == element.Key.ToString()))
            {

                g.enemyPlayerKey = element.Key.ToString();
            }
        }


        //register shots listener


        DatabaseReference enemyshotReference = reference.Child(g.enemyPlayerKey).Child("Shots");
        DatabaseReference myshotReference = reference.Child(g.currentPlayerKey).Child("Shots");

        enemyshotReference.ChildAdded += (sender, args) => handleEnemyShot(sender, args, g);
        myshotReference.ChildChanged += (sender, args) => handleMyShot(sender, args, g);


        //Debug.Log(myDataDictionary.Keys.ToList());

    }

    void handleMyShot(object sender, ChildChangedEventArgs args, gameManager g)
    {
        DataSnapshot snapshot = args.Snapshot;

        Dictionary<string, object> myshotdata = (Dictionary<string, object>)snapshot.Value;

        Shot myshot = new Shot(Convert.ToInt32(myshotdata["x"]), Convert.ToInt32(myshotdata["y"]));

        foreach (Block b in g.enemyGrid.blocks)
        {
            if (b.indexX == myshot.x && b.indexY == myshot.y)
            {
                b.toptile.GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }

    }





    void handleEnemyShot(object sender, ChildChangedEventArgs args, gameManager g)
    {


        DataSnapshot snapshot = args.Snapshot;

        Dictionary<string, object> enemyshotdata = (Dictionary<string, object>)snapshot.Value;

        //selecting the value from the key, in this case the key is X and the value is the value of the enemy shot. 

        //let us highlight the shot on the playergrid. 
        Shot enemyshot = new Shot(Convert.ToInt32(enemyshotdata["x"]), Convert.ToInt32(enemyshotdata["y"]));


        foreach (Block b in g.playerGrid.blocks)
        {
            if (b.indexX == enemyshot.x && b.indexY == enemyshot.y)
            {
                b.toptile.GetComponent<playerBoxController>().flipColor();
            }
        }

        Debug.Log(enemyshot.ToString());

        //get player ships
        foreach (Ship s in g.battlefleet.allships)
        {
            //ship in your fleet has been hit!  this is added to the list of hits in the ship
            if (s.checkHit(enemyshot, g.playerGrid))
            {

                Debug.Log(s.shipname + "Has been hit!");


                enemyshot.hit = true;

                string jsonshot = JsonUtility.ToJson(enemyshot);

                Debug.Log(jsonshot);


                //update the enemy shot to tell the enemy that the shot has hit.
                reference.Child(g.enemyPlayerKey).Child("Shots").Child(snapshot.Key).SetRawJsonValueAsync(jsonshot);



                //update ships in db
                StartCoroutine(saveShips(g, g.battlefleet));


            }

        }



        g.session.isMyTurn = true;



    }

    public IEnumerator saveShips(gameManager g, Fleet f)
    {

        string jsonfleet = JsonUtility.ToJson(f);


        Debug.Log(f.ToString());

        Debug.Log(jsonfleet);


        //modified.  I don't really need a unique key for the ships.  Also makes it easier to get the list of ships from fleet.
        Task addfleettask = reference.Child(g.currentPlayerKey).Child("Ships").SetRawJsonValueAsync(jsonfleet);

        yield return new WaitUntil(() => addfleettask.IsCompleted);
    }

    public IEnumerator getAllDataFromFirebase()
    {

        Task getdatatask = reference.GetValueAsync().ContinueWithOnMainThread(
            getValueTask =>
            {
                if (getValueTask.IsFaulted)
                {
                    Debug.Log("Error getting data " + getValueTask.Exception);
                }

                if (getValueTask.IsCompleted)
                {
                    DataSnapshot snapshot = getValueTask.Result;
                    //Debug.Log(snapshot.Value.ToString());

                    //snapshot object is casted to an instance of its type
                    myDataDictionary = (Dictionary<string, object>)snapshot.Value;


                }


            }
            );


        yield return new WaitUntil(() => getdatatask.IsCompleted);

        //the data has been saved to snapshot here
        yield return StartCoroutine(displayData());
    }

    IEnumerator getAllData()
    {
        yield return getNumberOfRecords();
        Debug.Log(numberOfRecords);



        yield return getAllDataFromFirebase();

        Debug.Log("All records retreived");

        yield return null;
    }

    public IEnumerator initFirebase()
    {
        if (!signedin)
        {
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://chess-9c5ab-default-rtdb.europe-west1.firebasedatabase.app/");
            reference = FirebaseDatabase.DefaultInstance.RootReference;
            yield return signInToFirebase();
            Debug.Log("Firebase Initialized!");
            yield return true;
            signedin = true;
        }
        else
        {
            yield return null;
        }
    }
    public bool signedin = false;
    // Update is called once per frame
    void Update()
    {
        
    }
}
