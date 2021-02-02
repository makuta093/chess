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
    

    public class Player
    {
        public string PlayerName;
        public bool isHisTurn;



    }

    public class objects
    {

        public int POSX;
        public int POSY;
        
        public int shape;
        public bool team;
        


    }

    public string myname;

    public int pl;

    public string currentPlayerKey, enemyPlayerKey;

    bool starts = false;

    public bool star = false;

    public Player currentPlayer, otherPlayer;

    bool logedin = false;

    int playercounter = 1;

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

    string email = "sec.syn.02@gmail.com";
    string password = "mcast71699";
    // Start is called before the first frame update
    void Start()
    {
        
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://chess-9c5ab-default-rtdb.europe-west1.firebasedatabase.app/");

        reference = FirebaseDatabase.DefaultInstance.RootReference;
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(signInToFirebase());
        //StartCoroutine(addPlayerToFirebase());
        
        
    }


    //sign in to the firebase instance so we can read some data
    //Coroutine Number 1
    IEnumerator signInToFirebase()
    {
        Debug.Log("sign in start"+logedin);
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
                     Debug.Log("Win");
                     logedin = true;
                     StartCoroutine(getNumberOfRecords());
                     StartCoroutine(BeginGame());
                     
                 }
                );



            yield return new WaitUntil(() => signintask.IsCompleted);
        
        Debug.Log("User has signed in");
        


    }


    IEnumerator createUser()
    {
        Debug.Log("create user");
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
                     pl = numberOfRecords;
                     
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

    public IEnumerator getOtherPlayerKey(Player otherPlayer, chessfirebase g)
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

        //enemyshotReference.ChildAdded += (sender, args) => handleEnemyShot(sender, args, g);
        //myshotReference.ChildChanged += (sender, args) => handleMyShot(sender, args, g);


        //Debug.Log(myDataDictionary.Keys.ToList());

    }


    public IEnumerator BeginGame()
    {
        
        
        Debug.Log("game begin");
        
        {
            yield return addPlayerToFirebase();
        }
            //timerText.GetComponentInChildren<Text>().text = "00:00";
        //while (true)
        //{
        //    if (session.areAllShipsPlaced())
        //    {
        //        break;
        //    }
        //    yield return null;
        //}
        ////this will happen only when all ships are placed.

        //session.startGame();
        //StartCoroutine(updateTimer());

        //yield return addPlayerToFirebase();

        //battlefleet = new Fleet(allships);

        //yield return dbScript.saveShips(this, battlefleet);

        //start the turns. 
        if (starts)
        {
            currentPlayer.isHisTurn = true;
            while (true)
            {


                if (currentPlayer.isHisTurn)
                {
                             Debug.Log("my turn!");
                    yield return null;
                }
                else
                {
                              Debug.Log("their turn!");
                    yield return null;
                }


            }
        }
        else
        {
            while (true)
            {

                if (currentPlayer.isHisTurn)
                {
                              Debug.Log("nmy turn!");
                    yield return null;
                }
                else
                {
                               Debug.Log("ntheir turn!");
                    yield return null;
                }


            }
        }



        yield return null;

    }

    IEnumerator waitForOtherPlayer()
    {
        Debug.Log("wait for other player");
        yield return getNumberOfRecords();
        playercounter = numberOfRecords;

        while (playercounter < 2)
        {
            //first player to join starts
            starts = true;

            Debug.Log("Waiting for other player" + Time.time);
            yield return getNumberOfRecords();
            playercounter = numberOfRecords;


            //lobby
        }
        //if another player joins the game can begin
        Player otherPlayer = new Player();
        //I need to get the key of the OTHER player

        yield return getOtherPlayerKey(otherPlayer, this);

        //I now have the other player's key.  Let's randomly choose whose turn is next. 
        Debug.Log("other player has joined");

        // session.isMyTurn = true;






        yield return null;
    }

    IEnumerator addPlayerToFirebase()
    {

        Debug.Log("add Player To Firebase");
        yield return initFirebase();
        currentPlayer = new Player();



        currentPlayer.PlayerName = "P" + pl;
        currentPlayer.isHisTurn = true;
        



        //yield return dbScript.clearFirebase();
        

        yield return addDataClassplayer(JsonUtility.ToJson(currentPlayer), this);

        yield return waitForOtherPlayer();



    }


    public IEnumerator addDataClass(string datatoinsert, chessfirebase g)
    {
        if (star == false)
        {
            Debug.Log("add DataClass");
            //create a unique ID
            string newkey = reference.Push().Key;
            Debug.Log(newkey);
            //the key for the current player
            g.currentPlayerKey = newkey;
            //Update the unique key with the data I want to insert
            star = true;
            yield return StartCoroutine(updateDataClass(newkey, datatoinsert));
        }
    }

    public IEnumerator addDataClassplayer(string datatoinsert, chessfirebase g)
    {
        if (star == false)
        {
            Debug.Log("add DataClass");
            //create a unique ID
            string newkey = reference.Push().Key;
            Debug.Log(newkey);
            myname = newkey;
            //the key for the current player
            g.currentPlayerKey = newkey;
            //Update the unique key with the data I want to insert
            star = true;
            yield return StartCoroutine(updateDataClass(newkey, datatoinsert));
        }
    }


    



    //public IEnumerator saveShips(gameManager g, Fleet f)
    //{

    //    string jsonfleet = JsonUtility.ToJson(f);


    //    Debug.Log(f.ToString());

    //    Debug.Log(jsonfleet);


    //    //modified.  I don't really need a unique key for the ships.  Also makes it easier to get the list of ships from fleet.
    //    Task addfleettask = reference.Child(g.currentPlayerKey).Child("Ships").SetRawJsonValueAsync(jsonfleet);

    //    yield return new WaitUntil(() => addfleettask.IsCompleted);
    //}



    public IEnumerator getAllDataFromFirebase()
    {
        Debug.Log("get All Data From Firebase");
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
        Debug.Log("getAllData");
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
        Debug.Log(currentPlayerKey);
    }
}
