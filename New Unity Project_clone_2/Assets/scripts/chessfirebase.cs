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

     
    // Start is called before the first frame update

    public class co
    {
        public string turn;
        public int Bp1x; public int Bp2x; public int Bp3x ; public int Bp4x ; public int Bp5x ; public int Bp6x ; public int Bp7x ; public int Bp8x ;
        public int Bp1y ; public int Bp2y; public int Bp3y ; public int Bp4y ; public int Bp5y ; public int Bp6y ; public int Bp7y; public int Bp8y ;
        public bool Bp1; public bool Bp2; public bool Bp3; public bool Bp4; public bool Bp5; public bool Bp6; public bool Bp7; public bool Bp8;

        public int Wp1x ; public int Wp2x ; public int Wp3x ; public int Wp4x ; public int Wp5x ; public int Wp6x ; public int Wp7x ; public int Wp8x ;
        public int Wp1y ; public int Wp2y ; public int Wp3y ; public int Wp4y ; public int Wp5y ; public int Wp6y ; public int Wp7y ; public int Wp8y ;
        public bool Wp1; public bool Wp2; public bool Wp3; public bool Wp4; public bool Wp5; public bool Wp6; public bool Wp7; public bool Wp8;

        public int Br1x ; public int Br2x ; public int Wr1x ; public int Wr2x ;
        public int Br1y ; public int Br2y ; public int Wr1y ; public int Wr2y ;
        public bool Br1; public bool Br2; public bool Wr1; public bool Wr2;

        public int Bk1x ; public int Bk2x ; public int Wk1x ; public int Wk2x ;
        public int Bk1y ; public int Bk2y ; public int Wk1y ; public int Wk2y ;
        public bool Bk1; public bool Bk2; public bool Wk1; public bool Wk2;

        public int Bb1x ; public int Bb2x ; public int Wb1x ; public int Wb2x ;
        public int Bb1y ; public int Bb2y ; public int Wb1y ; public int Wb2y ;
        public bool Bb1; public bool Bb2; public bool Wb1; public bool Wb2;

        public int Bqx ; public int KingBx ; public int Wqx ; public int KingWx ;
        public int Bqy ; public int KingBy ; public int Wqy ; public int KingWy ;
        public bool Bq; public bool KingB; public bool Wq; public bool KingW;
    }

    

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
        public bool live;



    }

    [System.Serializable]
    public class black
    {
        public List<marks> team = new List<marks>();

        public black(marks[] _bpeaces)
        {
            foreach (marks s in _bpeaces)
            {

                team.Add(s);
            }
        }
    }
    [System.Serializable]
    public class white
    {
        public List<marks> team = new List<marks>();

        public white(marks[] _wpeaces)
        {
            foreach (marks s in _wpeaces)
            {

                team.Add(s);
            }
        }
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

    

     int Bp1x = 1;  int Bp2x = 2;  int Bp3x = 3;  int Bp4x = 4;  int Bp5x = 5;  int Bp6x = 6;  int Bp7x = 7;  int Bp8x = 8;
     int Bp1y = 2;  int Bp2y = 2;  int Bp3y = 2;  int Bp4y = 2;  int Bp5y = 2;  int Bp6y = 2;  int Bp7y = 2;  int Bp8y = 2;

     int Wp1x = 1;  int Wp2x = 2;  int Wp3x = 3;  int Wp4x = 4;  int Wp5x = 5;  int Wp6x = 6;  int Wp7x = 7;  int Wp8x = 8;
     int Wp1y = 7;  int Wp2y = 7;  int Wp3y = 7;  int Wp4y = 7;  int Wp5y = 7;  int Wp6y = 7;  int Wp7y = 7;  int Wp8y = 7;

     int Br1x = 1;  int Br2x = 8;  int Wr1x = 1;  int Wr2x = 8;
     int Br1y = 1;  int Br2y = 1;  int Wr1y = 8;  int Wr2y = 8;

     int Bk1x = 2;  int Bk2x = 7;  int Wk1x = 2;  int Wk2x = 7;
     int Bk1y = 1;  int Bk2y = 1;  int Wk1y = 8;  int Wk2y = 8;

     int Bb1x = 3;  int Bb2x = 6;  int Wb1x = 3;  int Wb2x = 6;
     int Bb1y = 1;  int Bb2y = 1;  int Wb1y = 8;  int Wb2y = 8;

     int Bqx = 4;  int KingBx = 5;  int Wqx = 5;  int KingWx = 4;
     int Bqy = 1;  int KingBy = 1;  int Wqy = 8;  int KingWy = 8;

    public bool fillenemy,fillmine;

    public string mycolor;

    public string checkcolor;

   public marks[] _allpeaces,_bpeaces,_wpeaces;
    
    public chees myBoard,WTEAM,BTEAM;

    public int turn;

    public string myname;

    public GameObject testbutton;

    public int pl;
    bool makeing = false;
    public string currentPlayerKey, enemyPlayerKey;

    bool starts = false;

    public bool star = false;

    public Player currentPlayer, otherPlayer;

    public marks Bp1, Bp2, Bp3, Bp4, Bp5, Bp6, Bp7, Bp8,
                 Wp1, Wp2, Wp3, Wp4, Wp5, Wp6, Wp7, Wp8,
                 Br1, Br2, Wr1, Wr2, Bk1, Bk2, Wk1, Wk2,
                 Bb1, Bb2, Wb1, Wb2, Bq, KingB, Wq, KingW;



    bool logedin = false;

    bool coloradd = false;

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
        testbutton.SetActive(false);
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://chess-9c5ab-default-rtdb.europe-west1.firebasedatabase.app/");

        makeBoard.chant();
        

        reference = FirebaseDatabase.DefaultInstance.RootReference;
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(signInToFirebase());
        //StartCoroutine(addPlayerToFirebase());


    }

    public IEnumerator downloadAndSaveImage( string shape)
    {
        
        Debug.Log(shape);
        string pathToSaveIn = Application.persistentDataPath;

        storage = FirebaseStorage.DefaultInstance;

        // Create local filesystem URL



        string filename = Application.persistentDataPath + "/" + shape + ".png";

        StorageReference storage_ref = storage.GetReferenceFromUrl("gs://chess-9c5ab.appspot.com/" + shape + ".png");

        // Start downloading a file
        Task task = storage_ref.GetFileAsync(filename,
          new Firebase.Storage.StorageProgress<DownloadState>((DownloadState state) =>
          {
              // called periodically during the download
              //Debug.Log(String.Format(
              //  "Progress: {0} of {1} bytes transferred.",
              //  state.BytesTransferred,
              //  state.TotalByteCount
              //));
          }), CancellationToken.None);

        task.ContinueWith(resultTask =>
        {
            if (!resultTask.IsFaulted && !resultTask.IsCanceled)
            {
                Debug.Log("Download finished.");
            }
        });

        Debug.Log(filename);
        //if (obj == "Wp8")
        //{
        //    Sprite warship = LoadSprite(filename);

        //    GameObject.Find(obj).GetComponent<SpriteRenderer>().sprite = warship;
        //}
        //else
        //{
        yield return new WaitUntil(() => task.IsCompleted);
        
        
        
        //}

        yield return null;
    }

    private Sprite LoadSprite(string path)
    {
        
        if (System.IO.File.Exists(path))
        {            
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }
        return null;
    }

    public void makePlayers()
    {
        if (makeing == false)
        {
            Bp1.color = true; Bp1.name = "Bp1"; Bp1.POSX = Bp1x; Bp1.POSY = Bp1y; Bp1.pieace = "Bp"; Bp1.live = true;
            makeTeams(Bp1);
            Bp2.color = true; Bp2.name = "Bp2"; Bp2.POSX = Bp2x; Bp2.POSY = Bp2y; Bp2.pieace = "Bp"; Bp2.live = true;
            makeTeams(Bp2);
            Bp3.color = true; Bp3.name = "Bp3"; Bp3.POSX = Bp3x; Bp3.POSY = Bp3y; Bp3.pieace = "Bp"; Bp3.live = true;
            makeTeams(Bp3);
            Bp4.color = true; Bp4.name = "Bp4"; Bp4.POSX = Bp4x; Bp4.POSY = Bp4y; Bp4.pieace = "Bp"; Bp4.live = true;
            makeTeams(Bp4);
            Bp5.color = true; Bp5.name = "Bp5"; Bp5.POSX = Bp5x; Bp5.POSY = Bp5y; Bp5.pieace = "Bp"; Bp5.live = true;
            makeTeams(Bp5);
            Bp6.color = true; Bp6.name = "Bp6"; Bp6.POSX = Bp6x; Bp6.POSY = Bp6y; Bp6.pieace = "Bp"; Bp6.live = true;
            makeTeams(Bp6);
            Bp7.color = true; Bp7.name = "Bp7"; Bp7.POSX = Bp7x; Bp7.POSY = Bp7y; Bp7.pieace = "Bp"; Bp7.live = true;
            makeTeams(Bp7);
            Bp8.color = true; Bp8.name = "Bp8"; Bp8.POSX = Bp8x; Bp8.POSY = Bp8y; Bp8.pieace = "Bp"; Bp8.live = true;
            makeTeams(Bp8);

            Wp1.color = false; Wp1.name = "Wp1"; Wp1.POSX = Wp1x; Wp1.POSY = Wp1y; Wp1.pieace = "Wp"; Wp1.live = true;
            makeTeams(Wp1);
            Wp2.color = false; Wp2.name = "Wp2"; Wp2.POSX = Wp2x; Wp2.POSY = Wp2y; Wp2.pieace = "Wp"; Wp2.live = true;
            makeTeams(Wp2);
            Wp3.color = false; Wp3.name = "Wp3"; Wp3.POSX = Wp3x; Wp3.POSY = Wp3y; Wp3.pieace = "Wp"; Wp3.live = true;
            makeTeams(Wp3);
            Wp4.color = false; Wp4.name = "Wp4"; Wp4.POSX = Wp4x; Wp2.POSY = Wp4y; Wp4.pieace = "Wp"; Wp4.live = true;
            makeTeams(Wp4);
            Wp5.color = false; Wp5.name = "Wp5"; Wp5.POSX = Wp5x; Wp5.POSY = Wp5y; Wp5.pieace = "Wp"; Wp5.live = true;
            makeTeams(Wp5);
            Wp6.color = false; Wp6.name = "Wp6"; Wp6.POSX = Wp6x; Wp6.POSY = Wp6y; Wp6.pieace = "Wp"; Wp6.live = true;
            makeTeams(Wp6);
            Wp7.color = false; Wp7.name = "Wp7"; Wp7.POSX = Wp7x; Wp7.POSY = Wp7y; Wp7.pieace = "Wp"; Wp7.live = true;
            makeTeams(Wp7);
            Wp8.color = false; Wp8.name = "Wp8"; Wp8.POSX = Wp8x; Wp8.POSY = Wp8y; Wp8.pieace = "Wp"; Wp8.live = true;
            makeTeams(Wp8);

            Br1.color = true; Br1.name = "Br1"; Br1.POSX = Br1x; Br1.POSY = Br1y; Br1.pieace = "Br"; Br1.live = true;
            makeTeams(Br1);
            Br2.color = true; Br2.name = "Br2"; Br2.POSX = Br2x; Br2.POSY = Br2y; Br2.pieace = "Br"; Br2.live = true;
            makeTeams(Br2);
            Bk1.color = true; Bk1.name = "Bk1"; Bk1.POSX = Bk1x; Bk1.POSY = Bk1y; Bk1.pieace = "Bk"; Bk1.live = true;
            makeTeams(Bk1);
            Bk2.color = true; Bk2.name = "Bk2"; Bk2.POSX = Bk2x; Bk2.POSY = Bk2y; Bk2.pieace = "Bk"; Bk2.live = true;
            makeTeams(Bk2);
            Bb1.color = true; Bb1.name = "Bb1"; Bb1.POSX = Bb1x; Bb1.POSY = Bb1y; Bb1.pieace = "Bb"; Bb1.live = true;
            makeTeams(Bb1);
            Bb2.color = true; Bb2.name = "Bb2"; Bb2.POSX = Bb2x; Bb2.POSY = Bb2y; Bb2.pieace = "Bb"; Bb2.live = true;
            makeTeams(Bb2);
            Bq.color = true; Bq.name = "Bq"; Bq.POSX = Bqx; Bq.POSY = Bqy; Bq.pieace = "Bq"; Bq.live = true;
            makeTeams(Bq);
            KingB.color = true; KingB.name = "KingB"; KingB.POSX = KingBx; KingB.POSY = KingBy; KingB.pieace = "KINGb"; KingB.live = true;
            makeTeams(KingB);

            Wr1.color = false; Wr1.name = "Wr1"; Wr1.POSX = Wr1x; Wr1.POSY = Wr1y; Wr1.pieace = "Wr"; Wr1.live = true;
            makeTeams(Wr1);
            Wr2.color = false; Wr2.name = "Wr2"; Wr2.POSX = Wr2x; Wr2.POSY = Wr2y; Wr2.pieace = "Wr"; Wr2.live = true;
            makeTeams(Wr2);
            Wk1.color = false; Wk1.name = "Wk1"; Wk1.POSX = Wk1x; Wk1.POSY = Wk1y; Wk1.pieace = "Wk"; Wk1.live = true;
            makeTeams(Wk1);
            Wk2.color = false; Wk2.name = "Wk2"; Wk2.POSX = Wk2x; Wk2.POSY = Wk2y; Wk2.pieace = "Wk"; Wk2.live = true;
            makeTeams(Wk2);
            Wb1.color = false; Wb1.name = "Wb1"; Wb1.POSX = Wb1x; Wb1.POSY = Wb1y; Wb1.pieace = "Wb"; Wb1.live = true;
            makeTeams(Wb1);
            Wb2.color = false; Wb2.name = "Wb2"; Wb2.POSX = Wb2x; Wb2.POSY = Wb2y; Wb2.pieace = "Wb"; Wb2.live = true;
            makeTeams(Wb2);
            Wq.color = false; Wq.name = "Wq"; Wq.POSX = Wqx; Wq.POSY = Wqy; Wq.pieace = "Wq"; Wq.live = true;
            makeTeams(Wq);
            KingW.color = false; KingW.name = "KingW"; KingW.POSX = KingWx; KingW.POSY = KingWy; KingW.pieace = "KINGw"; KingW.live = true;
            makeTeams(KingW);
            makeing = true;
            
        }

        
    }
    public void makeTeams(marks a)
    {
        

        GameObject go1 = new GameObject();
        go1.name = a.name;
        go1.AddComponent<SpriteRenderer>();
        if (go1.name == "Wp4")
        {
            GameObject location = GameObject.Find(7 + "," + a.POSX);

            go1.transform.position = location.transform.position;
            go1.transform.localScale = new Vector3(0.5f, 0.5f);
        }
        else
        {
            GameObject location = GameObject.Find(a.POSY + "," + a.POSX);

            go1.transform.position = location.transform.position;
            go1.transform.localScale = new Vector3(0.5f, 0.5f);
        }

        if (go1.name == "Wp8")
        {
            Sprite warship = LoadSprite("C:/Users/Keith/AppData/LocalLow/DefaultCompany/New Unity Project/"+"Wp"+".png");
            go1.GetComponent<SpriteRenderer>().sprite = warship;
        }
        else
        {
            Sprite warship = LoadSprite("C:/Users/Keith/AppData/LocalLow/DefaultCompany/New Unity Project/" + a.pieace + ".png");
            go1.GetComponent<SpriteRenderer>().sprite = warship;
        }
    }


    IEnumerator addColorForm(string color)
    {
        
        co newPlayer = new co();
        
        newPlayer.turn = color;


        newPlayer.Bp1x = Bp1.POSX; newPlayer.Bp2x = Bp2.POSX; newPlayer.Bp3x= Bp3.POSX; newPlayer.Bp4x= Bp4.POSX; newPlayer.Bp5x= Bp5.POSX; newPlayer.Bp6x= Bp6.POSX; newPlayer.Bp7x= Bp7.POSX; newPlayer.Bp8x= Bp8.POSX;
        newPlayer.Bp1y = Bp1y; newPlayer.Bp2y = Bp2y; newPlayer.Bp3y=Bp3y; newPlayer.Bp4y=Bp4y; newPlayer.Bp5y=Bp5y; newPlayer.Bp6y=Bp6y; newPlayer.Bp7y=Bp7y; newPlayer.Bp8y=Bp8y;
        newPlayer.Bp1=Bp1.live; newPlayer.Bp2 = Bp2.live; newPlayer.Bp3 = Bp3.live; newPlayer.Bp4 = Bp4.live; newPlayer.Bp5 = Bp5.live; newPlayer.Bp6 = Bp6.live; newPlayer.Bp7 = Bp7.live; newPlayer.Bp8 = Bp8.live;

        newPlayer.Wp1x = Wp1.POSX; newPlayer.Wp2x = Wp2.POSX; newPlayer.Wp3x = Wp3.POSX; newPlayer.Wp4x = Wp4.POSX; newPlayer.Wp5x = Wp5.POSX; newPlayer.Wp6x = Wp6.POSX; newPlayer.Wp7x = Wp7.POSX; newPlayer.Wp8x = Wp8.POSX;
        newPlayer.Wp1y = Wp1.POSY; newPlayer.Wp2y = Wp2.POSY; newPlayer.Wp3y = Wp3.POSY; newPlayer.Wp4y = Wp4.POSY; newPlayer.Wp5y = Wp5.POSY; newPlayer.Wp6y = Wp6.POSY; newPlayer.Wp7y = Wp7.POSY; newPlayer.Wp8y = Wp8.POSY;
        newPlayer.Wp1 = Wp1.live; newPlayer.Wp2 = Wp2.live; newPlayer.Wp3 = Wp3.live; newPlayer.Wp4 = Wp4.live; newPlayer.Wp5 = Wp5.live; newPlayer.Wp6 = Wp6.live; newPlayer.Wp7 = Wp7.live; newPlayer.Wp8 = Wp8.live;

        newPlayer.Br1x = Br1.POSX; newPlayer.Br2x = Br2.POSX; newPlayer.Wr1x = Wr1.POSX; newPlayer.Wr2x = Wr2.POSX; 
        newPlayer.Br1y = Br1.POSY; newPlayer.Br2y = Br2.POSY; newPlayer.Wr1y = Wr1.POSY; newPlayer.Wr2y = Wr2.POSY;
        newPlayer.Br1 = Br1.live; newPlayer.Br2 = Br2.live; newPlayer.Wr1 = Wr1.live; newPlayer.Wr2 = Wr2.live;

        newPlayer.Bk1x = Bk1.POSX; newPlayer.Bk2x = Bk2.POSX; newPlayer.Wk1x = Wk1.POSX; newPlayer.Wk2x = Wk2.POSX;
        newPlayer.Bk1y = Bk1.POSY; newPlayer.Bk2y = Bk2.POSY; newPlayer.Wk1y = Wk1.POSY; newPlayer.Wk2y = Wk2.POSY;
        newPlayer.Bk1 = Bk1.live; newPlayer.Bk2 = Bk2.live; newPlayer.Wk1 = Wk1.live; newPlayer.Wk2 = Wk2.live;

        newPlayer.Bb1x = Bb1.POSX; newPlayer.Bb2x = Bb2.POSX; newPlayer.Wb1x = Wb1.POSX; newPlayer.Wb2x = Wb2.POSX;
        newPlayer.Bb1y = Bb1.POSY; newPlayer.Bb2y = Bb2.POSY; newPlayer.Wb1y = Wb1.POSY; newPlayer.Wb2y = Wb2.POSY;
        newPlayer.Bb1 = Bb1.live; newPlayer.Bb2 = Bb2.live; newPlayer.Wb1 = Wb1.live; newPlayer.Wb2 = Wb2.live;

        newPlayer.Bqx = Bq.POSX; newPlayer.KingBx = KingB.POSX; newPlayer.Wqx = Wq.POSX; newPlayer.KingWx = KingW.POSX;
        newPlayer.Bqy = Bq.POSY; newPlayer.KingBy = KingB.POSY; newPlayer.Wqy = Wq.POSY; newPlayer.KingWy = KingW.POSY;
        newPlayer.Bq = Bq.live; newPlayer.KingB = KingB.live; newPlayer.Wq = Wq.live; newPlayer.KingW = KingW.live;

    //first sign in
    yield return StartCoroutine(signInToFirebase());
        //I am done from signing in.
        Debug.Log("now signed in" + Time.time);

        string jsonNewPlayer = JsonUtility.ToJson(newPlayer);

        //start the process of adding a player
        yield return StartCoroutine(addColor(jsonNewPlayer));

        //clear the values that are already on screen


        


    }

    IEnumerator addColor(string datatoinsert)
    {
        //create a unique ID
        string newkey = "chess-9c5ab-default-rtdb";
        Debug.Log(newkey);
        //Update the unique key with the data I want to insert
        yield return StartCoroutine(updatecolor(newkey, datatoinsert));

    }

    IEnumerator updatecolor(string childlabel, string newdata)
    {

        bool dataupdated = false;
        //find the child of player4 that corresponds to playername and set the value to whatever is inside newdata
        reference.Root.SetRawJsonValueAsync(newdata).ContinueWithOnMainThread(
            updateJsonValueTask =>
            {
                if (updateJsonValueTask.IsCompleted)
                {
                    dataupdated = true;
                }

            });



        while (!dataupdated)
        {
            //the data has NOT YET been saved to snapshot
            yield return null;

        }
        Debug.Log("Data updated");
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
                 
                 logedin = true;
                 

             }
            );



        yield return new WaitUntil(() => signintask.IsCompleted);

        Debug.Log("User has signed in");

        yield return StartCoroutine(CheckIfEmpty());
        yield return StartCoroutine(downloadAndSaveImage("Wp"));
        yield return StartCoroutine(downloadAndSaveImage("Bp"));
        yield return StartCoroutine(downloadAndSaveImage("Wr"));
        yield return StartCoroutine(downloadAndSaveImage("Br"));
        yield return StartCoroutine(downloadAndSaveImage("Wk"));
        yield return StartCoroutine(downloadAndSaveImage("Bk"));
        yield return StartCoroutine(downloadAndSaveImage("Wb"));
        yield return StartCoroutine(downloadAndSaveImage("Bb"));
        yield return StartCoroutine(downloadAndSaveImage("Wq"));
        yield return StartCoroutine(downloadAndSaveImage("Bq"));
        yield return StartCoroutine(downloadAndSaveImage("KINGw"));
        yield return StartCoroutine(downloadAndSaveImage("KINGb"));
        makePlayers();
        StartCoroutine(getNumberOfRecords());
        StartCoroutine(BeginGame());

    }

    public string Check_box(int x, int y,string color)
    {
        string open = "open";
        bool colr= false;
        if(color == "white")
        {
            colr = false;
        }
        else if(color == "black")
        {
            colr = true;
        }
        
        if((x == Bp1.POSX) &&(y == Bp1.POSY))
        {
            if(Bp1.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if ((x == Bp2.POSX) && (y == Bp2.POSY))
        {
            if (Bp2.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if((x == Bp3.POSX) && (y == Bp3.POSY))
        {
            if (Bp3.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if((x == Bp4.POSX) && (y == Bp4.POSY))
        {
            if (Bp4.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if((x == Bp5.POSX) && (y == Bp5.POSY))
        {
            if (Bp5.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if((x == Bp6.POSX) && (y == Bp6.POSY))
        {
            if (Bp6.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if((x == Bp7.POSX) && (y == Bp7.POSY))
        {
            if (Bp7.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if((x == Bp8.POSX) && (y == Bp8.POSY))
        {
            if (Bp8.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }

        // white pawns

        else if ((x == Wp1.POSX) && (y == Wp1.POSY))
        {
            if (Wp1.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if ((x == Wp2.POSX) && (y == Wp2.POSY))
        {
            if (Wp2.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if ((x == Wp3.POSX) && (y == Wp3.POSY))
        {
            if (Wp3.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if ((x == Wp4.POSX) && (y == Wp4.POSY))
        {
            if (Wp4.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if ((x == Wp5.POSX) && (y == Wp5.POSY))
        {
            if (Wp5.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if ((x == Wp6.POSX) && (y == Wp6.POSY))
        {
            if (Wp6.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if ((x == Wp7.POSX) && (y == Wp7.POSY))
        {
            if (Wp7.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if ((x == Wp8.POSX) && (y == Wp8.POSY))
        {
            if (Wp8.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }

        //black pices 

        else if ((x == Br1.POSX) && (y == Br1.POSY))
        {
            if (Br1.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if ((x == Br2.POSX) && (y == Br2.POSY))
        {
            if (Br2.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if ((x == Bk1.POSX) && (y == Bk1.POSY))
        {
            if (Bk1.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if ((x == Bk2.POSX) && (y == Bk2.POSY))
        {
            if (Bk2.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if ((x == Bb1.POSX) && (y == Bb1.POSY))
        {
            if (Bb1.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if ((x == Bb2.POSX) && (y == Bb2.POSY))
        {
            if (Bb2.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if ((x == Bq.POSX) && (y == Bq.POSY))
        {
            if (Bq.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if ((x == KingB.POSX) && (y == KingB.POSY))
        {
            if (KingB.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }

        //white pieces 

        else if ((x == Wr1.POSX) && (y == Wr1.POSY))
        {
            if (Wr1.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if ((x == Wr2.POSX) && (y == Wr2.POSY))
        {
            if (Wr2.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if ((x == Wk1.POSX) && (y == Wk1.POSY))
        {
            if (Wk1.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if ((x == Wk2.POSX) && (y == Wk2.POSY))
        {
            if (Wk2.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if ((x == Wb1.POSX) && (y == Wb1.POSY))
        {
            if (Wb1.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if ((x == Wb2.POSX) && (y == Wb2.POSY))
        {
            if (Wb2.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if ((x == Wq.POSX) && (y == Wq.POSY))
        {
            if (Wq.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }
        else if ((x == KingW.POSX) && (y == KingW.POSY))
        {
            if (KingW.color == colr)
            {
                open = "close";
            }
            else
            {
                open = "enemy";
            }
        }



        return open;
    }

    public marks Get_peace(int x, int y)
    {
        marks name = null ;
        

        if ((x == Bp1.POSX) && (y == Bp1.POSY))
        {
            name = Bp1;
        }
        else if ((x == Bp2.POSX) && (y == Bp2.POSY))
        {
            name = Bp2;
        }
        else if ((x == Bp3.POSX) && (y == Bp3.POSY))
        {
            name = Bp3;
        }
        else if ((x == Bp4.POSX) && (y == Bp4.POSY))
        {
            name = Bp4;
        }
        else if ((x == Bp5.POSX) && (y == Bp5.POSY))
        {
            name = Bp5;
        }
        else if ((x == Bp6.POSX) && (y == Bp6.POSY))
        {
            name = Bp6;
        }
        else if ((x == Bp7.POSX) && (y == Bp7.POSY))
        {
            name = Bp7;
        }
        else if ((x == Bp8.POSX) && (y == Bp8.POSY))
        {
            name = Bp8;
        }

        // white pawns

        else if ((x == Wp1.POSX) && (y == Wp1.POSY))
        {
            name = Wp1;
        }
        else if ((x == Wp2.POSX) && (y == Wp2.POSY))
        {
            name = Wp2;
        }
        else if ((x == Wp3.POSX) && (y == Wp3.POSY))
        {
            name = Wp3;
        }
        else if ((x == Wp4.POSX) && (y == Wp4.POSY))
        {
            name = Wp4;
        }
        else if ((x == Wp5.POSX) && (y == Wp5.POSY))
        {
            name = Wp5;
        }
        else if ((x == Wp6.POSX) && (y == Wp6.POSY))
        {
            name = Wp6;
        }
        else if ((x == Wp7.POSX) && (y == Wp7.POSY))
        {
            name = Wp7;
        }
        else if ((x == Wp8.POSX) && (y == Wp8.POSY))
        {
            name = Wp8;
        }

        //black pices 

        else if ((x == Br1.POSX) && (y == Br1.POSY))
        {
            name = Br1;
        }
        else if ((x == Br2.POSX) && (y == Br2.POSY))
        {
            name = Br2;
        }
        else if ((x == Bk1.POSX) && (y == Bk1.POSY))
        {
            name = Bk1;
        }
        else if ((x == Bk2.POSX) && (y == Bk2.POSY))
        {
            name = Bk2;
        }
        else if ((x == Bb1.POSX) && (y == Bb1.POSY))
        {
            name = Bb1;
        }
        else if ((x == Bb2.POSX) && (y == Bb2.POSY))
        {
            name = Bb2;
        }
        else if ((x == Bq.POSX) && (y == Bq.POSY))
        {
            name = Bq;
        }
        else if ((x == KingB.POSX) && (y == KingB.POSY))
        {
            name = KingB;
        }

        //white pieces 

        else if ((x == Wr1.POSX) && (y == Wr1.POSY))
        {
            name = Wr1;
        }
        else if ((x == Wr2.POSX) && (y == Wr2.POSY))
        {
            name = Wr2;
        }
        else if ((x == Wk1.POSX) && (y == Wk1.POSY))
        {
            name = Wk1;
        }
        else if ((x == Wk2.POSX) && (y == Wk2.POSY))
        {
            name = Wk2;
        }
        else if ((x == Wb1.POSX) && (y == Wb1.POSY))
        {
            name = Wb1;
        }
        else if ((x == Wb2.POSX) && (y == Wb2.POSY))
        {
            name = Wb2;
        }
        else if ((x == Wq.POSX) && (y == Wq.POSY))
        {
            name = Wq;
        }
        else if ((x == KingW.POSX) && (y == KingW.POSY))
        {
            name = KingW;
        }



        return name;
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

    public IEnumerator CheckIfEmpty()
    {
        int num = 1;
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
                     num = (int)snapshot.ChildrenCount;
                     Debug.Log("total "+num);
                     

                 }


             }
             );
       

        

        yield return new WaitUntil(() => numberofrecordstask.IsCompleted);
        if (num > 2)
        {
            yield return StartCoroutine(clearFirebase());
        }

    }

    public IEnumerator clearFirebase()
    {
        Task removeAllRecords = reference.RemoveValueAsync().ContinueWithOnMainThread(
            rmAllRecords =>
            {
                if (rmAllRecords.IsCompleted)
                {
                    Debug.Log("Database clear");
                }
            });

        yield return new WaitUntil(() => removeAllRecords.IsCompleted);

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
                        testbutton.SetActive(true);
                    fillenemy = true;
                }

                if (currentPlayer.isHisTurn)
                {
                    Debug.Log("my turn!");
                    turn = 1;
                    if(coloradd == false)
                    {
                        yield return addColorForm("white");
                        coloradd = true;
                    }
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

            Debug.Log("Waiting for other player");
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