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



    [System.Serializable]
    public class Player
    {
        public string PlayerName;
        public bool isHisTurn;
        public string color;



    }
    [System.Serializable]
    public class marks
    {

        public int POSX;
        public int POSY;

        public string name;
        public string pieace;
        public bool color;



    }

    [System.Serializable]
    public class chees
    {
        public List<marks> team = new List<marks>();

        public chees(marks[] _allpeaces)
        {
            foreach (marks s in _allpeaces)
            {

                team.Add(s);
            }
        }
    }

    public int Bp1x = 1; public int Bp2x = 2; public int Bp3x = 3; public int Bp4x = 4; public int Bp5x = 5; public int Bp6x = 6; public int Bp7x = 7; public int Bp8x = 8;
    public int Bp1y = 2; public int Bp2y = 2; public int Bp3y = 2; public int Bp4y = 2; public int Bp5y = 2; public int Bp6y = 2; public int Bp7y = 2; public int Bp8y = 2;

    public int Wp1x = 1; public int Wp2x = 2; public int Wp3x = 3; public int Wp4x = 4; public int Wp5x = 5; public int Wp6x = 6; public int Wp7x = 7; public int Wp8x = 8;
    public int Wp1y = 2; public int Wp2y = 2; public int Wp3y = 2; public int Wp4y = 2; public int Wp5y = 2; public int Wp6y = 7; public int Wp7y = 7; public int Wp8y = 7;

    public int Br1x = 1; public int Br2x = 8; public int Wr1x = 1; public int Wr2x = 8;
    public int Br1y = 1; public int Br2y = 1; public int Wr1y = 8; public int Wr2y = 8;

    public int Bk1x = 2; public int Bk2x = 7; public int Wk1x = 2; public int Wk2x = 7;
    public int Bk1y = 1; public int Bk2y = 1; public int Wk1y = 8; public int Wk2y = 8;

    public int Bb1x = 3; public int Bb2x = 6; public int Wb1x = 3; public int Wb2x = 6;
    public int Bb1y = 1; public int Bb2y = 1; public int Wb1y = 8; public int Wb2y = 8;

    public int Bqx = 4; public int KingBx = 5; public int Wqx = 5; public int KingWx = 4;
    public int Bqy = 1; public int KingBy = 1; public int Wqy = 8; public int KingWy = 8;

    public bool fillenemy,fillmine;

    public string mycolor;

    public string checkcolor;

   public marks[] _allpeaces;
    
    public chees myBoard;

    public int turn;

    public string myname;

    public int pl;

    public string currentPlayerKey, enemyPlayerKey;

    bool starts = false;

    public bool star = false;

    public Player currentPlayer, otherPlayer;

    public marks Bp1, Bp2, Bp3, Bp4, Bp5, Bp6, Bp7, Bp8,
                 Wp1, Wp2, Wp3, Wp4, Wp5, Wp6, Wp7, Wp8,
                 Br1, Br2, Wr1, Wr2, Bk1, Bk2, Wk1, Wk2,
                 Bb1, Bb2, Wb1, Wb2, Bq, KingB, Wq, KingW;



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
        StartCoroutine(makePlayers());
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://chess-9c5ab-default-rtdb.europe-west1.firebasedatabase.app/");

        reference = FirebaseDatabase.DefaultInstance.RootReference;
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(signInToFirebase());
        //StartCoroutine(addPlayerToFirebase());


    }

    IEnumerator makePlayers()
    {
        Bp1.color = true; Bp1.name = "Bp1"; Bp1.POSX = Bp1x; Bp1.POSY = Bp1y; Bp1.pieace = "pawn";
        Bp2.color = true; Bp2.name = "Bp2"; Bp2.POSX = Bp2x; Bp2.POSY = Bp2y; Bp2.pieace = "pawn";
        Bp3.color = true; Bp3.name = "Bp3"; Bp3.POSX = Bp3x; Bp3.POSY = Bp3y; Bp3.pieace = "pawn";
        Bp4.color = true; Bp4.name = "Bp2"; Bp4.POSX = Bp4x; Bp2.POSY = Bp4y; Bp4.pieace = "pawn";
        Bp5.color = true; Bp5.name = "Bp5"; Bp5.POSX = Bp5x; Bp5.POSY = Bp5y; Bp5.pieace = "pawn";
        Bp6.color = true; Bp6.name = "Bp6"; Bp6.POSX = Bp6x; Bp6.POSY = Bp6y; Bp6.pieace = "pawn";
        Bp7.color = true; Bp7.name = "Bp7"; Bp7.POSX = Bp7x; Bp7.POSY = Bp7y; Bp7.pieace = "pawn";
        Bp8.color = true; Bp8.name = "Bp8"; Bp8.POSX = Bp8x; Bp8.POSY = Bp8y; Bp8.pieace = "pawn";

        Wp1.color = true; Wp1.name = "Wp1"; Wp1.POSX = Wp1x; Wp1.POSY = Wp1y; Wp1.pieace = "pawn";
        Wp2.color = true; Wp2.name = "Wp2"; Wp2.POSX = Wp2x; Wp2.POSY = Wp2y; Wp2.pieace = "pawn";
        Wp3.color = true; Wp3.name = "Wp3"; Wp3.POSX = Wp3x; Wp3.POSY = Wp3y; Wp3.pieace = "pawn";
        Wp4.color = true; Wp4.name = "Wp2"; Wp4.POSX = Wp4x; Wp2.POSY = Wp4y; Wp4.pieace = "pawn";
        Wp5.color = true; Wp5.name = "Wp5"; Wp5.POSX = Wp5x; Wp5.POSY = Wp5y; Wp5.pieace = "pawn";
        Wp6.color = true; Wp6.name = "Wp6"; Wp6.POSX = Wp6x; Wp6.POSY = Wp6y; Wp6.pieace = "pawn";
        Wp7.color = true; Wp7.name = "Wp7"; Wp7.POSX = Wp7x; Wp7.POSY = Wp7y; Wp7.pieace = "pawn";
        Wp8.color = true; Wp8.name = "Wp8"; Wp8.POSX = Wp8x; Wp8.POSY = Wp8y; Bp8.pieace = "pawn";

        Br1.color = true; Br1.name = "Br1"; Br1.POSX = Br1x; Br1.POSY = Br1y; Br1.pieace = "rook";
        Br2.color = true; Br2.name = "Br2"; Br2.POSX = Br2x; Br2.POSY = Br2y; Br2.pieace = "rook";
        Bk1.color = true; Bk1.name = "Bk1"; Bk1.POSX = Bk1x; Bk1.POSY = Bk1y; Bk1.pieace = "knight";
        Bk2.color = true; Bk2.name = "Bk2"; Bk2.POSX = Bk2x; Bk2.POSY = Bk2y; Bk2.pieace = "knight";
        Bb1.color = true; Bb1.name = "Bb1"; Bb1.POSX = Bb1x; Bb1.POSY = Bb1y; Bb1.pieace = "bishop";
        Bb2.color = true; Bb2.name = "Bb2"; Bb2.POSX = Bb2x; Bb2.POSY = Bb2y; Bb2.pieace = "bishop";
        Bq.color = true; Bq.name = "Bq"; Bq.POSX = Bqx; Bq.POSY = Bqy; Bq.pieace = "queen";
        KingB.color = true; KingB.name = "KingB"; KingB.POSX = KingBx; KingB.POSY = KingBy; KingB.pieace="king";

        Wr1.color = true; Wr1.name = "Wr1"; Wr1.POSX = Wr1x; Wr1.POSY = Wr1y; Wr1.pieace = "rook";
        Wr2.color = true; Wr2.name = "Wr2"; Wr2.POSX = Wr2x; Wr2.POSY = Wr2y; Wr2.pieace = "rook";
        Wk1.color = true; Wk1.name = "Wk1"; Wk1.POSX = Wk1x; Wk1.POSY = Wk1y; Wk1.pieace = "knight";
        Wk2.color = true; Wk2.name = "Wk2"; Wk2.POSX = Wk2x; Wk2.POSY = Wk2y; Wk2.pieace = "knight";
        Wb1.color = true; Wb1.name = "Wb1"; Wb1.POSX = Wb1x; Wb1.POSY = Wb1y; Wb1.pieace = "bishop";
        Wb2.color = true; Wb2.name = "Wb2"; Wb2.POSX = Wb2x; Wb2.POSY = Wb2y; Wb2.pieace = "bishop";
        Wq.color = true; Wq.name = "Wq"; Wq.POSX = Wqx; Wq.POSY = Wqy; Wq.pieace = "queen";
        KingW.color = true; KingW.name = "KingW"; KingW.POSX = KingWx; KingW.POSY = KingWy; KingW.pieace = "king";

        yield return null;
    }

   //sign in to the firebase instance so we can read some data
   //Coroutine Number 1
   IEnumerator signInToFirebase()
    {
        Debug.Log("sign in start" + logedin);
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

    IEnumerator getMineDataFromFirebase(string childLabel)
    {

        Task getdatatask = reference.Child(childLabel).GetValueAsync().ContinueWithOnMainThread(
            getValueTask =>
            {
                bool displaydata = false;
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
        yield return StartCoroutine(displayDataMine());
    }

    IEnumerator displayDataMine()
    {
        foreach (var element in myDataDictionary)
        {
            Debug.Log(element.Key.ToString() + "<->" + element.Value.ToString());
            if ((element.Key.ToString() == "PlayerName"))
            {
                Debug.Log("Reached");
                currentPlayer.PlayerName = element.Value.ToString();

            }
            if ((element.Key.ToString() == "color"))
            {
                Debug.Log("Reached");
                currentPlayer.color = element.Value.ToString();

            }
            if ((element.Key.ToString() == "isHisTurn"))
            {
                if (element.Value.ToString() == "true")
                {
                    currentPlayer.isHisTurn = true;
                }
                if (element.Value.ToString() == "false")
                {
                    currentPlayer.isHisTurn = false;
                }

            }
            yield return new WaitForSeconds(1f);
        }

        yield return null;
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

    IEnumerator updatecolorDataClass(chessfirebase childlabel, string newdata)
    {
        string jsonshot = JsonUtility.ToJson(newdata);
        Debug.Log(newdata);
        Debug.Log(jsonshot);


        //find the child of player4 that corresponds to playername and set the value to whatever is inside newdata
        Task updateJsonValueTask = reference.Child(childlabel.currentPlayerKey).Child("color").SetRawJsonValueAsync(jsonshot).ContinueWithOnMainThread(
            
        updJsonValueTask =>
            {
                
                if (updJsonValueTask.IsCompleted)
                {
                    // dataupdated = true;
                }

            });


        yield return new WaitUntil(() => updateJsonValueTask.IsCompleted);




    }

    IEnumerator getEnemyDataFromFirebase(string childLabel)
    {

        Task getdatatask = reference.Child(childLabel).GetValueAsync().ContinueWithOnMainThread(
            getValueTask =>
            {
                bool displaydata = false;
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
        yield return StartCoroutine(displayDataEnemy());
    }

    IEnumerator displayDataEnemy()
    {
        foreach (var element in myDataDictionary)
        {
            Debug.Log(element.Key.ToString() + "<->" + element.Value.ToString());
            if ((element.Key.ToString() == "PlayerName"))
            {
                Debug.Log("Reached");
                otherPlayer.PlayerName = element.Value.ToString();

            }
            if ((element.Key.ToString() == "color"))
            {
                Debug.Log("Reached");
                otherPlayer.color = element.Value.ToString();

            }
            if ((element.Key.ToString() == "isHisTurn"))
            {
                if (element.Value.ToString() == "true")
                {
                    otherPlayer.isHisTurn = true;
                }
                if (element.Value.ToString() == "false")
                {
                    otherPlayer.isHisTurn = false;
                }

            }
            yield return new WaitForSeconds(1f);
        }

        yield return null;
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
                    Debug.Log(snapshot.Value.ToString());

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
            //myBoard = new chees(_allpeaces);

            //yield return saveShips(this, myBoard);
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

                if (fillenemy == false)
                {
                    if (fillmine == false)
                    {
                        yield return getMineDataFromFirebase(currentPlayerKey);
                        fillmine = true;
                    }
                    if (fillmine == true)
                        yield return getEnemyDataFromFirebase(enemyPlayerKey);
                    fillenemy = true;
                }

                if (currentPlayer.isHisTurn)
                {
                    Debug.Log("my turn!");
                    turn = 1;
                    
                    //if (mycolor == "white")
                    //{

                    //}
                    //else if (mycolor == "black")
                    //{

                    //}
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
                    if (fillenemy == false)
                    {
                        if (fillmine == false) {
                            yield return getMineDataFromFirebase(currentPlayerKey);
                            fillmine = true;
                        } if(fillmine == true)
                        yield return getEnemyDataFromFirebase(enemyPlayerKey);
                        fillenemy = true;
                    }

                    Debug.Log("nmy turn!");
                    turn = 2;
                    //if(mycolor == "white")
                    //{

                    //}else if(mycolor == "black")
                    //{

                    //}
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
        if(pl == 0)
        {
            checkcolor ="white";
        }else if (pl == 1)
        {
            checkcolor = "black";
        }
        currentPlayer.color = checkcolor;
        




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


    //public IEnumerator saveShips(chessfirebase g, chees f)
    //{

    //    string jsonBOARD = JsonUtility.ToJson(f);


    //    Debug.Log(f.ToString());

    //    Debug.Log(jsonBOARD);


    //    //modified.  I don't really need a unique key for the ships.  Also makes it easier to get the list of ships from fleet.
    //    Task addfleettask = reference.Child(g.currentPlayerKey).Child("pieces").SetRawJsonValueAsync(jsonBOARD);

    //    yield return new WaitUntil(() => addfleettask.IsCompleted);
    //}

    IEnumerator addDataClass(string datatoinsert , string name)
    {
        //create a unique ID
        
        yield return StartCoroutine(updateDataClass(name, datatoinsert));

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