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
    public bool MOVED = false;
    public string move ;
    public bool l = false;
    public string too;

    public string make;

    public bool mycollor;
    
    public List<string> locations = new List<string>();

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

     int Wp1x = 1;  int Wp2x = 2;  int Wp3x = 3;   int Wp5x = 5;  int Wp6x = 6;  int Wp7x = 7;  int Wp8x = 8; int Wp4y = 7;
    int Wp1y = 7;  int Wp2y = 7;  int Wp3y = 7;    int Wp5y = 7;  int Wp6y = 7;  int Wp7y = 7;  int Wp8y = 7; int Wp4x = 4;

     int Br1x = 1;  int Br2x = 8;  int Wr1x = 1;  int Wr2x = 8;
     int Br1y = 1;  int Br2y = 1;  int Wr1y = 8;  int Wr2y = 8;

     int Bk1x = 2;  int Bk2x = 7;  int Wk1x = 2;  int Wk2x = 7;
     int Bk1y = 1;  int Bk2y = 1;  int Wk1y = 8;  int Wk2y = 8;

     int Bb1x = 3;  int Bb2x = 6;  int Wb1x = 3;  int Wb2x = 6;
     int Bb1y = 1;  int Bb2y = 1;  int Wb1y = 8;  int Wb2y = 8;

     int Bqx = 4;  int KingBx = 5;  int Wqx = 5;  int KingWx = 4;
     int Bqy = 1;  int KingBy = 1;  int Wqy = 8;  int KingWy = 8;

    public bool fillenemy,fillmine;

    public bool myturn = false;

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
        


    }

    public IEnumerator downloadAndSaveImage( string shape)
    {
        Debug.Log("downloadAndSaveImage");
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
        
        yield return new WaitUntil(() => task.IsCompleted);
        
        
        
       

        yield return null;
    }

    private Sprite LoadSprite(string path)
    {
        Debug.Log("LoadSprite");
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
        Debug.Log("makePlayers");
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
            Wp4.color = false; Wp4.name = "Wp4"; Wp4.POSX = Wp4x; Wp4.POSY = Wp4y; Wp4.pieace = "Wp"; Wp4.live = true;
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
        Debug.Log("makeTeams");

        GameObject go1 = new GameObject();
        go1.name = a.name;
        go1.AddComponent<SpriteRenderer>();
        if (go1.name == "Wp4")
        {
            GameObject location = GameObject.Find(7 + "," + a.POSX);

            go1.transform.position = location.transform.position;
            go1.transform.localScale = new Vector3(0.5f, 0.5f);

            BoxCollider2D boxCollider2D = go1.AddComponent<BoxCollider2D>();
            boxCollider2D.size = new Vector2(1, 1);
        }
        else
        {
            GameObject location = GameObject.Find(a.POSY + "," + a.POSX);

            go1.transform.position = location.transform.position;
            go1.transform.localScale = new Vector3(0.5f, 0.5f);
            
            BoxCollider2D boxCollider2D = go1.AddComponent<BoxCollider2D>();
            boxCollider2D.size = new Vector2(1, 1);
            
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
        move = null;
        too = null;
        Debug.Log("addColorForm");

        co newPlayer = new co();
        
        


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


        newPlayer.turn = color;
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
        Debug.Log("addColor");
        //create a unique ID
        string newkey = "chess-9c5ab-default-rtdb";
        Debug.Log(newkey);
        //Update the unique key with the data I want to insert
        yield return StartCoroutine(updatecolor(newkey, datatoinsert));

    }

    IEnumerator updatecolor(string childlabel, string newdata)
    {
        Debug.Log("updatecolor");
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
        Debug.Log("CheckIfEmpty");
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
        Debug.Log("clearFirebase");
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
        Debug.Log("getNumberOfRecords");

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
        
        yield return new WaitUntil(() => numberofrecordstask.IsCompleted);


    }

    IEnumerator getMineDataFromFirebase(string childLabel)
    {
        Debug.Log("getMineDataFromFirebase");
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
        

        yield return new WaitUntil(() => getdatatask.IsCompleted);

        
        yield return StartCoroutine(displayDataMine());
    }

    IEnumerator displayDataMine()
    {
        Debug.Log("displayDataMine");
        foreach (var element in myDataDictionary)
        {
            Debug.Log(element.Key.ToString() + "<->" + element.Value.ToString());
            if ((element.Key.ToString() == "PlayerName"))
            {
                Debug.Log(element.Value.ToString());
                currentPlayer.PlayerName = element.Value.ToString();

            }
            if ((element.Key.ToString() == "color"))
            {
                Debug.Log(element.Value.ToString());
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

        Debug.Log("updateDataClass");
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
        Debug.Log("updatecolorDataClass");
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
        Debug.Log("getEnemyDataFromFirebase");
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
        

        yield return new WaitUntil(() => getdatatask.IsCompleted);

        //the data has been saved to snapshot here
        yield return StartCoroutine(displayDataEnemy());
    }

    IEnumerator displayDataEnemy()
    {
        Debug.Log("displayDataEnemy");
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
        Debug.Log("getDataFromFirebase");
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
        

        yield return new WaitUntil(() => getdatatask.IsCompleted);

        //the data has been saved to snapshot here
        yield return StartCoroutine(displayData());
    }

    IEnumerator displayData()
    {
        Debug.Log("displayData");
        foreach (var element in myDataDictionary)
        {
            Debug.Log(element.Key.ToString() + "<->" + element.Value.ToString());
            
            yield return new WaitForSeconds(1f);
        }

        yield return null;
    }

    public IEnumerator getOtherPlayerKey(Player otherPlayer, chessfirebase g)
    {
        Debug.Log("getOtherPlayerKey");
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

        

    }

    public IEnumerator check_if_turn()
    {
        Debug.Log("check_if_turn");
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
        
        yield return StartCoroutine(check_if_my_turn());
    }

    public IEnumerator check_if_my_turn()
    {
        Debug.Log("check_if_my_turn");
        foreach (var element in myDataDictionary)
        {
            Debug.Log(element.Key.ToString()+"--"+ element.Value.ToString());
            
            if (element.Key.ToString() == "Bb1")
            {
                Bb1.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bb1x")
            {
                Bb1.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bb1y")
            {
                Bb1.POSY = int.Parse(element.Value.ToString());
            }

            if (element.Key.ToString() == "Bb2")
            {
                Bb2.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bb2x")
            {
                Bb2.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bb2y")
            {
                Bb2.POSY = int.Parse(element.Value.ToString());
            }

            if (element.Key.ToString() == "Bk2")
            {
                Bk2.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bk2x")
            {
                Bk2.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bk2y")
            {
                Bk2.POSY = int.Parse(element.Value.ToString());
            }


            if (element.Key.ToString() == "Bk1")
            {
                Bk1.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bk1x")
            {
                Bk1.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bk1y")
            {
                Bk1.POSY = int.Parse(element.Value.ToString());
            }


            if (element.Key.ToString() == "Br2")
            {
                Br2.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Br2x")
            {
                Br2.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Br2y")
            {
                Br2.POSY = int.Parse(element.Value.ToString());
            }


            if (element.Key.ToString() == "Br1")
            {
                Br1.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Br1x")
            {
                Br1.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Br1y")
            {
                Br1.POSY = int.Parse(element.Value.ToString());
            }


            if (element.Key.ToString() == "Bq")
            {
                Bq.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bqx")
            {
                Bq.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bqy")
            {
                Bq.POSY = int.Parse(element.Value.ToString());
            }


            if (element.Key.ToString() == "KingB")
            {
                KingB.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "KingBx")
            {
                KingB.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "KingBy")
            {
                KingB.POSY = int.Parse(element.Value.ToString());
            }



            if (element.Key.ToString() == "Wb1")
            {
                Wb1.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wb1x")
            {
                Wb1.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wb1y")
            {
                Wb1.POSY = int.Parse(element.Value.ToString());
            }

            if (element.Key.ToString() == "Wb2")
            {
                Wb2.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wb2x")
            {
                Wb2.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wb2y")
            {
                Wb2.POSY = int.Parse(element.Value.ToString());
            }

            if (element.Key.ToString() == "Wk2")
            {
                Wk2.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wk2x")
            {
                Wk2.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wk2y")
            {
                Wk2.POSY = int.Parse(element.Value.ToString());
            }


            if (element.Key.ToString() == "Wk1")
            {
                Wk1.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wk1x")
            {
                Wk1.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wk1y")
            {
                Wk1.POSY = int.Parse(element.Value.ToString());
            }


            if (element.Key.ToString() == "Wr2")
            {
                Wr2.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wr2x")
            {
                Wr2.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wr2y")
            {
                Wr2.POSY = int.Parse(element.Value.ToString());
            }


            if (element.Key.ToString() == "Wr1")
            {
                Wr1.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wr1x")
            {
                Wr1.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wr1y")
            {
                Wr1.POSY = int.Parse(element.Value.ToString());
            }


            if (element.Key.ToString() == "Wq")
            {
                Wq.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wqx")
            {
                Wq.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wqy")
            {
                Wq.POSY = int.Parse(element.Value.ToString());
            }


            if (element.Key.ToString() == "KingW")
            {
                KingW.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "KingWx")
            {
                KingW.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "KingWy")
            {
                KingW.POSY = int.Parse(element.Value.ToString());
            }


            if (element.Key.ToString() == "Bp1")
            {
                Wp1.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bp1x")
            {
                Wp1.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bp1y")
            {
                Wp1.POSY = int.Parse(element.Value.ToString());
            }

            if (element.Key.ToString() == "Bp2")
            {
                Wp2.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bp2x")
            {
                Wp2.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bp2y")
            {
                Wp2.POSY = int.Parse(element.Value.ToString());
            }

            if (element.Key.ToString() == "Bp3")
            {
                Wp3.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bp3x")
            {
                Wp3.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bp3y")
            {
                Wp3.POSY = int.Parse(element.Value.ToString());
            }


            if (element.Key.ToString() == "Bp4")
            {
                Wp4.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bp4x")
            {
                Wp4.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bp4y")
            {
                Wp4.POSY = int.Parse(element.Value.ToString());
            }


            if (element.Key.ToString() == "Bp5")
            {
                Wp5.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bp5x")
            {
                Wp5.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bp5y")
            {
                Wp5.POSY = int.Parse(element.Value.ToString());
            }


            if (element.Key.ToString() == "Bp6")
            {
                Wp6.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bp6x")
            {
                Wp6.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bp6y")
            {
                Wp6.POSY = int.Parse(element.Value.ToString());
            }


            if (element.Key.ToString() == "Bp7")
            {
                Wp7.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bp7x")
            {
                Wp7.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bp7y")
            {
                Wp7.POSY = int.Parse(element.Value.ToString());
            }


            if (element.Key.ToString() == "Bp8")
            {
                Wp8.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bp8x")
            {
                Wp8.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Bp8y")
            {
                Wp8.POSY = int.Parse(element.Value.ToString());
            }



            if (element.Key.ToString() == "Wp1")
            {
                Wp1.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wp1x")
            {
                Wp1.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wp1y")
            {
                Wp1.POSY = int.Parse(element.Value.ToString());
            }

            if (element.Key.ToString() == "Wp2")
            {
                Wp2.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wp2x")
            {
                Wp2.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wp2y")
            {
                Wp2.POSY = int.Parse(element.Value.ToString());
            }

            if (element.Key.ToString() == "Wp3")
            {
                Wp3.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wp3x")
            {
                Wp3.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wp3y")
            {
                Wp3.POSY = int.Parse(element.Value.ToString());
            }


            if (element.Key.ToString() == "Wp4")
            {
                Wp4.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wp4x")
            {
                Wp4.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wp4y")
            {
                Wp4.POSY = int.Parse(element.Value.ToString());
            }


            if (element.Key.ToString() == "Wp5")
            {
                Wp5.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wp5x")
            {
                Wp5.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wp5y")
            {
                Wp5.POSY = int.Parse(element.Value.ToString());
            }


            if (element.Key.ToString() == "Wp6")
            {
                Wp6.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wp6x")
            {
                Wp6.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wp6y")
            {
                Wp6.POSY = int.Parse(element.Value.ToString());
            }


            if (element.Key.ToString() == "Wp7")
            {
                Wp7.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wp7x")
            {
                Wp7.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wp7y")
            {
                Wp7.POSY = int.Parse(element.Value.ToString());
            }


            if (element.Key.ToString() == "Wp8")
            {
                Wp8.live = bool.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wp8x")
            {
                Wp8.POSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "Wp8y")
            {
                Wp8.POSY = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "turn")
            {
                Debug.Log("trun");
                if (element.Value.ToString() == currentPlayer.color)
                {
                    Debug.Log("enter");
                    make = "eneter";
                    myturn = true;
                }
                else
                {
                    Debug.Log("false");
                    make = "false";
                    myturn = false;
                }
            }
            new WaitForSeconds(1f);
            yield return new WaitForSeconds(1f);
        }

        yield return null;
    }

    public IEnumerator BeginGame()
    {


        Debug.Log("game begin");

        {
            yield return addPlayerToFirebase();
            
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
                        testbutton.SetActive(true);
                        yield return getEnemyDataFromFirebase(enemyPlayerKey);
                        
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

                    StartCoroutine(check_if_turn());

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
                    StartCoroutine(check_if_turn());
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
        if (myturn == false)
        {
            Debug.Log("add Player To Firebase");
            yield return initFirebase();
            currentPlayer = new Player();



            currentPlayer.PlayerName = "P" + pl;
            Debug.Log(currentPlayer.PlayerName);
            currentPlayer.isHisTurn = true;
            Debug.Log(currentPlayer.isHisTurn);
            if (pl == 0)
            {
                checkcolor = "white";
            }
            else if (pl == 1)
            {
                checkcolor = "black";
            }
            currentPlayer.color = checkcolor;
            Debug.Log(currentPlayer.color);




            //yield return dbScript.clearFirebase();

            myturn = true;
            yield return addDataClassplayer(JsonUtility.ToJson(currentPlayer), this);
        }
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


    

    IEnumerator addDataClass(string datatoinsert , string name)
    {
        //create a unique ID
        
        yield return StartCoroutine(updateDataClass(name, datatoinsert));

    }



    



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
        Debug.Log(currentPlayer.color);
        if(currentPlayer.color == "white")
        {
            mycollor = false;
        }else if (currentPlayer.color == "black")
        {
            mycollor = true;
        }
        Debug.Log(currentPlayerKey);
        if ((myturn == true)&&(make == "eneter"))
        {
            if (MOVED == false)
            {
                move_boxes();
            }
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    string k = hit.collider.gameObject.name;
                    string tt = k.Substring(1);
                    bool jj;
                    Debug.Log(tt);
                    if ((tt == ",1")||(tt == ",2") || (tt == ",3") || (tt == ",4") || (tt == ",5") || (tt == ",6") || (tt == ",7") || (tt == ",8"))
                    {
                        if(l == true)
                        {
                            too = k;
                            jj = false;
                            make = "poggers";
                            movell(move, too, jj);
                        }
                    }
                    else if (l == false)
                    {
                        marks g = Get_peace_by_name(hit.collider.gameObject.name);
                        bool r = check_collor(g);
                        Debug.Log("r " + r);
                            Debug.Log("enter move");
                        if (g != null)
                        {
                            Debug.Log("enter g");
                            if (r == true)
                            {
                                Debug.Log("enter r");
                                Debug.Log(mycollor + " " + g.color);
                                move = g.name;
                                l = true;
                            }
                        }
                    }
                    else
                    {
                        marks g = Get_peace_by_name(hit.collider.gameObject.name);
                        bool r = check_collor(g);

                        if (r == false)
                            {
                                Debug.Log("enter r");
                                Debug.Log(mycollor + " " + g.color);
                                too = g.name;
                                l = true;
                            jj = true;
                            make = "poggers";
                            movell(move, too, jj);
                            }
                        
                    }
                }
            }
        }
    }

    public void move_boxes()
    {
        MOVED = true;

        //BLACK PAWNS
        GameObject Bp1OBJ = GameObject.Find("Bp1");
        GameObject Bp1_location = GameObject.Find(Bp1.POSY+","+ Bp1.POSX);
        Bp1OBJ.transform.position = Bp1_location.transform.position;

        GameObject Bp2OBJ = GameObject.Find("Bp2");
        GameObject Bp2_location = GameObject.Find(Bp2.POSY + "," + Bp2.POSX);
        Bp2OBJ.transform.position = Bp2_location.transform.position;

        GameObject Bp3OBJ = GameObject.Find("Bp3");
        GameObject Bp3_location = GameObject.Find(Bp3.POSY + "," + Bp3.POSX);
        Bp3OBJ.transform.position = Bp3_location.transform.position;

        GameObject Bp4OBJ = GameObject.Find("Bp4");
        GameObject Bp4_location = GameObject.Find(Bp4.POSY + "," + Bp4.POSX);
        Bp4OBJ.transform.position = Bp4_location.transform.position;

        GameObject Bp5OBJ = GameObject.Find("Bp5");
        GameObject Bp5_location = GameObject.Find(Bp5.POSY + "," + Bp5.POSX);
        Bp5OBJ.transform.position = Bp5_location.transform.position;

        GameObject Bp6OBJ = GameObject.Find("Bp6");
        GameObject Bp6_location = GameObject.Find(Bp6.POSY + "," + Bp6.POSX);
        Bp6OBJ.transform.position = Bp6_location.transform.position;

        GameObject Bp7OBJ = GameObject.Find("Bp7");
        GameObject Bp7_location = GameObject.Find(Bp7.POSY + "," + Bp7.POSX);
        Bp7OBJ.transform.position = Bp7_location.transform.position;

        GameObject Bp8OBJ = GameObject.Find("Bp8");
        GameObject Bp8_location = GameObject.Find(Bp8.POSY + "," + Bp8.POSX);
        Bp8OBJ.transform.position = Bp8_location.transform.position;


        //WHITE PAWNS
        GameObject Wp1OBJ = GameObject.Find("Wp1");
        GameObject Wp1_location = GameObject.Find(Wp1.POSY + "," + Wp1.POSX);
        Wp1OBJ.transform.position = Wp1_location.transform.position;

        GameObject Wp2OBJ = GameObject.Find("Wp2");
        GameObject Wp2_location = GameObject.Find(Wp2.POSY + "," + Wp2.POSX);
        Wp2OBJ.transform.position = Wp2_location.transform.position;

        GameObject Wp3OBJ = GameObject.Find("Wp3");
        GameObject Wp3_location = GameObject.Find(Wp3.POSY + "," + Wp3.POSX);
        Wp3OBJ.transform.position = Wp3_location.transform.position;

        GameObject Wp4OBJ = GameObject.Find("Wp4");
        GameObject Wp4_location = GameObject.Find(Wp4.POSY + "," + Wp4.POSX);
        Wp4OBJ.transform.position = Wp4_location.transform.position;

        GameObject Wp5OBJ = GameObject.Find("Wp5");
        GameObject Wp5_location = GameObject.Find(Wp5.POSY + "," + Wp5.POSX);
        Wp5OBJ.transform.position = Wp5_location.transform.position;

        GameObject Wp6OBJ = GameObject.Find("Wp6");
        GameObject Wp6_location = GameObject.Find(Wp6.POSY + "," + Wp6.POSX);
        Wp6OBJ.transform.position = Wp6_location.transform.position;

        GameObject Wp7OBJ = GameObject.Find("Wp7");
        GameObject Wp7_location = GameObject.Find(Wp7.POSY + "," + Wp7.POSX);
        Wp7OBJ.transform.position = Wp7_location.transform.position;

        GameObject Wp8OBJ = GameObject.Find("Wp8");
        GameObject Wp8_location = GameObject.Find(Wp8.POSY + "," + Wp8.POSX);
        Wp8OBJ.transform.position = Wp8_location.transform.position;

        //BLACK PEACES
        GameObject Br1OBJ = GameObject.Find("Br1");
        GameObject Br1_location = GameObject.Find(Br1.POSY + "," + Br1.POSX);
        Br1OBJ.transform.position = Br1_location.transform.position;

        GameObject Br2OBJ = GameObject.Find("Br2");
        GameObject Br2_location = GameObject.Find(Br2.POSY + "," + Br2.POSX);
        Br2OBJ.transform.position = Br2_location.transform.position;

        GameObject Bk1OBJ = GameObject.Find("Bk1");
        GameObject Bk1_location = GameObject.Find(Bk1.POSY + "," + Bk1.POSX);
        Bk1OBJ.transform.position = Bk1_location.transform.position;

        GameObject Bk2OBJ = GameObject.Find("Bk2");
        GameObject Bk2_location = GameObject.Find(Bk2.POSY + "," + Bk2.POSX);
        Bk2OBJ.transform.position = Bk2_location.transform.position;

        GameObject Bb1OBJ = GameObject.Find("Bb1");
        GameObject Bb1_location = GameObject.Find(Bb1.POSY + "," + Bb1.POSX);
        Bb1OBJ.transform.position = Bb1_location.transform.position;

        GameObject Bb2OBJ = GameObject.Find("Bb2");
        GameObject Bb2_location = GameObject.Find(Bb2.POSY + "," + Bb2.POSX);
        Bb2OBJ.transform.position = Bb2_location.transform.position;

        GameObject BqOBJ = GameObject.Find("Bq");
        GameObject Bq_location = GameObject.Find(Bq.POSY + "," + Bq.POSX);
        BqOBJ.transform.position = Bq_location.transform.position;

        GameObject KingBOBJ = GameObject.Find("KingB");
        GameObject KingB_location = GameObject.Find(KingB.POSY + "," + KingB.POSX);
        KingBOBJ.transform.position = KingB_location.transform.position;


        //WHITE PEACES
        GameObject Wr1OBJ = GameObject.Find("Wr1");
        GameObject Wr1_location = GameObject.Find(Wr1.POSY + "," + Wr1.POSX);
        Wr1OBJ.transform.position = Wr1_location.transform.position;

        GameObject Wr2OBJ = GameObject.Find("Wr2");
        GameObject Wr2_location = GameObject.Find(Wr2.POSY + "," + Wr2.POSX);
        Wr2OBJ.transform.position = Wr2_location.transform.position;

        GameObject Wk1OBJ = GameObject.Find("Wk1");
        GameObject Wk1_location = GameObject.Find(Wk1.POSY + "," + Wk1.POSX);
        Wk1OBJ.transform.position = Wk1_location.transform.position;

        GameObject Wk2OBJ = GameObject.Find("Wk2");
        GameObject Wk2_location = GameObject.Find(Wk2.POSY + "," + Wk2.POSX);
        Wk2OBJ.transform.position = Wk2_location.transform.position;

        GameObject Wb1OBJ = GameObject.Find("Wb1");
        GameObject Wb1_location = GameObject.Find(Wb1.POSY + "," + Wb1.POSX);
        Wb1OBJ.transform.position = Wb1_location.transform.position;

        GameObject Wb2OBJ = GameObject.Find("Wb2");
        GameObject Wb2_location = GameObject.Find(Wb2.POSY + "," + Wb2.POSX);
        Wb2OBJ.transform.position = Wb2_location.transform.position;

        GameObject WqOBJ = GameObject.Find("Wq");
        GameObject Wq_location = GameObject.Find(Wq.POSY + "," + Wq.POSX);
        WqOBJ.transform.position = Wq_location.transform.position;

        GameObject KingWOBJ = GameObject.Find("KingW");
        GameObject KingW_location = GameObject.Find(KingW.POSY + "," + KingW.POSX);
        KingWOBJ.transform.position = KingW_location.transform.position;
        
    }

    public void movell(string move, string too, bool jj)
    {
        Debug.Log("movell");
        if(jj == true)
        {
            Debug.Log("JJ "+jj);
            int tt = 0;
            int rr = 0;
            //TOO
            //BLACK PAWNS
            if (too == "Bp1")
            {
                rr = Bp1.POSX;
                tt = Bp1.POSY;
                Bp1.POSY = 0;
                Bp1.POSX = 0;
                Bp1.live = false;
                Debug.Log(Bp1.POSY);
                Debug.Log(Bp1.POSX);
            }
            else if (too == "Bp2")
            {
                rr = Bp2.POSX;
                tt = Bp2.POSY;
                Bp2.POSY = 0;
                Bp2.POSX = 0;
                Bp2.live = false;
                Debug.Log(Bp1.POSY);
                Debug.Log(Bp1.POSX);
            }
            else if (too == "Bp3")
            {
                rr = Bp3.POSX;
                tt = Bp3.POSY;
                Bp3.POSY = 0;
                Bp3.POSX = 0;
                Bp3.live = false;
                Debug.Log(Bp2.POSY);
                Debug.Log(Bp2.POSX);
            }
            else if (too == "Bp4")
            {
                rr = Bp4.POSX;
                tt = Bp4.POSY;
                Bp4.POSY = 0;
                Bp4.POSX = 0;
                Bp4.live = false;
                Debug.Log(Bp3.POSY);
                Debug.Log(Bp3.POSX);
            }
            else if (too == "Bp5")
            {
                rr = Bp5.POSX;
                tt = Bp5.POSY;
                Bp5.POSY = 0;
                Bp5.POSX = 0;
                Bp5.live = false;
                Debug.Log(Bp5.POSY);
                Debug.Log(Bp5.POSX);
            }
            else if (too == "Bp6")
            {
                rr = Bp6.POSX;
                tt = Bp6.POSY;
                Bp6.POSY = 0;
                Bp6.POSX = 0;
                Bp6.live = false;
                Debug.Log(Bp6.POSY);
                Debug.Log(Bp6.POSX);
            }
            else if (too == "Bp7")
            {
                rr = Bp7.POSX;
                tt = Bp7.POSY;
                Bp7.POSY = 0;
                Bp7.POSX = 0;
                Bp7.live = false;
                Debug.Log(Bp7.POSY);
                Debug.Log(Bp7.POSX);
            }
            else if (too == "Bp8")
            {
                rr = Bp8.POSX;
                tt = Bp8.POSY;
                Bp8.POSY = 0;
                Bp8.POSX = 0;
                Bp8.live = false;
                Debug.Log(Bp8.POSY);
                Debug.Log(Bp8.POSX);
            }

            //WHITE PAWNS
            else if (too == "Wp1")
            {
                rr = Wp1.POSX;
                tt = Wp1.POSY;
                Wp1.POSY = 0;
                Wp1.POSX = 0;
                Wp1.live = false;
                Debug.Log(Wp1.POSY);
                Debug.Log(Wp1.POSX);
            }
            else if (too == "Wp2")
            {
                rr = Wp2.POSX;
                tt = Wp2.POSY;
                Wp2.POSY = 0;
                Wp2.POSX = 0;
                Wp2.live = false;
                Debug.Log(Wp2.POSY);
                Debug.Log(Wp2.POSX);
            }
            else if (too == "Wp3")
            {
                rr = Wp3.POSX;
                tt = Wp3.POSY;
                Wp3.POSY = 0;
                Wp3.POSX = 0;
                Wp3.live = false;
                Debug.Log(Wp3.POSY);
                Debug.Log(Wp3.POSX);
            }
            else if (too == "Wp4")
            {
                rr = Wp4.POSX;
                tt = Wp4.POSY;
                Wp4.POSY = 0;
                Wp4.POSX = 0;
                Wp4.live = false;
                Debug.Log(Wp4.POSY);
                Debug.Log(Wp4.POSX);
            }
            else if (too == "Wp5")
            {
                rr = Wp5.POSX;
                tt = Wp5.POSY;
                Wp5.POSY = 0;
                Wp5.POSX = 0;
                Wp5.live = false;
                Debug.Log(Wp5.POSY);
                Debug.Log(Wp5.POSX);
            }
            else if (too == "Wp6")
            {
                rr = Wp6.POSX;
                tt = Wp6.POSY;
                Wp6.POSY = 0;
                Wp6.POSX = 0;
                Wp6.live = false;
                Debug.Log(Wp6.POSY);
                Debug.Log(Wp6.POSX);
            }
            else if (too == "Wp7")
            {
                rr = Wp7.POSX;
                tt = Wp7.POSY;
                Wp7.POSY = 0;
                Wp7.POSX = 0;
                Wp7.live = false;
                Debug.Log(Wp7.POSY);
                Debug.Log(Wp7.POSX);
            }
            else if (too == "Wp8")
            {
                rr = Wp8.POSX;
                tt = Wp8.POSY;
                Wp8.POSY = 0;
                Wp8.POSX = 0;
                Wp8.live = false;
                Debug.Log(Wp8.POSY);
                Debug.Log(Wp8.POSX);
            }


            //BLACK PEACES
            else if (too == "Br1")
            {
                Br1.POSY = 0;
                Br1.POSX = 0;
                Br1.live = false;
                Debug.Log(Br1.POSY);
                Debug.Log(Br1.POSX);
            }
            else if (too == "Br2")
            {
                Br2.POSY = 0;
                Br2.POSX = 0;
                Br2.live = false;
                Debug.Log(Br2.POSY);
                Debug.Log(Br2.POSX);
            }
            else if (too == "Bk1")
            {
                Bk1.POSY = 0;
                Bk1.POSX = 0;
                Bk1.live = false;
                Debug.Log(Bk1.POSY);
                Debug.Log(Bk1.POSX);
            }
            else if (too == "Bk2")
            {
                Bk2.POSY = 0;
                Bk2.POSX = 0;
                Bk2.live = false;
                Debug.Log(Bk2.POSY);
                Debug.Log(Bk2.POSX);
            }
            else if (too == "Bb1")
            {
                Bb1.POSY = 0;
                Bb1.POSX = 0;
                Bb1.live = false;
                Debug.Log(Bb1.POSY);
                Debug.Log(Bb1.POSX);
            }
            else if (too == "Bb2")
            {
                Bb2.POSY = 0;
                Bb2.POSX = 0;
                Bb2.live = false;
                Debug.Log(Bb2.POSY);
                Debug.Log(Bb2.POSX);
            }
            else if (too == "Bq")
            {
                Bq.POSY = 0;
                Bq.POSX = 0;
                Bq.live = false;
                Debug.Log(Bq.POSY);
                Debug.Log(Bq.POSX);
            }
            else if (too == "KingB")
            {
                KingB.POSY = 0;
                KingB.POSX = 0;
                KingB.live = false;
                Debug.Log(KingB.POSY);
                Debug.Log(KingB.POSX);
            }

            //BLACK PEACES
            else if (too == "Wr1")
            {
                Wr1.POSY = 0;
                Wr1.POSX = 0;
                Wr1.live = false;
                Debug.Log(Wr1.POSY);
                Debug.Log(Wr1.POSX);
            }
            else if (too == "Br2")
            {
                Br2.POSY = 0;
                Br2.POSX = 0;
                Br2.live = false;
                Debug.Log(Wr2.POSY);
                Debug.Log(Wr2.POSX);
            }
            else if (too == "Wk1")
            {
                Wk1.POSY = 0;
                Wk1.POSX = 0;
                Wk1.live = false;
                Debug.Log(Wk1.POSY);
                Debug.Log(Wk1.POSX);
            }
            else if (too == "Wk2")
            {
                Wk2.POSY = 0;
                Wk2.POSX = 0;
                Wk2.live = false;
                Debug.Log(Wk2.POSY);
                Debug.Log(Wk2.POSX);
            }
            else if (too == "Wb1")
            {
                Wb1.POSY = 0;
                Wb1.POSX = 0;
                Wb1.live = false;
                Debug.Log(Wb1.POSY);
                Debug.Log(Wb1.POSX);
            }
            else if (too == "Wb2")
            {
                Wb2.POSY = 0;
                Wb2.POSX = 0;
                Wb2.live = false;
                Debug.Log(Wb2.POSY);
                Debug.Log(Wb2.POSX);
            }
            else if (too == "Wq")
            {
                Wq.POSY = 0;
                Wq.POSX = 0;
                Wq.live = false;
                Debug.Log(Wq.POSY);
                Debug.Log(Wq.POSX);
            }
            else if (too == "KingW")
            {
                KingW.POSY = 0;
                KingW.POSX = 0;
                KingW.live = false;
                Debug.Log(KingW.POSY);
                Debug.Log(KingW.POSX);
            }



            //move
            if (move == "Bp1")
            {
                Bp1.POSY = tt;
                Bp1.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bp2")
            {
                Bp2.POSY = tt;
                Bp2.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bp3")
            {
                Bp3.POSY = tt;
                Bp3.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bp4")
            {
                Bp4.POSY = tt;
                Bp4.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bp5")
            {
                Bp5.POSY = tt;
                Bp5.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bp6")
            {
                Bp6.POSY = tt;
                Bp6.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bp7")
            {
                Bp7.POSY = tt;
                Bp7.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bp8")
            {
                Bp8.POSY = tt;
                Bp8.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }

            //WHITE PAWNS
            else if (move == "Wp1")
            {
                Wp1.POSY = tt;
                Wp1.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Wp2")
            {
                Wp2.POSY = tt;
                Wp2.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Wp3")
            {
                Wp3.POSY = tt;
                Wp3.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Wp4")
            {
                Wp4.POSY = tt;
                Wp4.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Wp5")
            {
                Wp5.POSY = tt;
                Wp5.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Wp6")
            {
                Wp6.POSY = tt;
                Wp6.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Wp7")
            {
                Wp7.POSY = tt;
                Wp7.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Wp8")
            {
                Wp8.POSY = tt;
                Wp8.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }


            //BLACK PEACES
            else if (move == "Br1")
            {
                Br1.POSY = tt;
                Br1.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Br2")
            {
                Br2.POSY = tt;
                Br2.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bk1")
            {
                Bk1.POSY = tt;
                Bk1.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bk2")
            {
                Bk2.POSY = tt;
                Bk2.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bb1")
            {
                Bb1.POSY = tt;
                Bb1.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bb2")
            {
                Bb2.POSY = tt;
                Bb2.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bq")
            {
                Bq.POSY = tt;
                Bq.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "KingB")
            {
                KingB.POSY = tt;
                KingB.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }

            //WHITE PEACES
            else if (move == "Wr1")
            {
                Wr1.POSY = tt;
                Wr1.POSX = rr;
            }
            else if (move == "Wr2")
            {
                Wr2.POSY = tt;
                Wr2.POSX = rr;
            }
            else if (move == "Wk1")
            {
                Wk1.POSY = tt;
                Wk1.POSX = rr;
            }
            else if (move == "Wk2")
            {
                Wk2.POSY = tt;
                Wk2.POSX = rr;
            }
            else if (move == "Wb1")
            {
                Wb1.POSY = tt;
                Wb1.POSX = rr;
            }
            else if (move == "Wb2")
            {
                Wb2.POSY = tt;
                Wb2.POSX = rr;
            }
            else if (move == "Wq")
            {
                Wq.POSY = tt;
                Wq.POSX = rr;
            }
            else if (move == "KingW")
            {
                KingW.POSY = tt;
                KingW.POSX = rr;
            }

            

            //WHITE PEACES
            else if (too == "Wr1")
            {
                Wr1.POSY = 0;
                Wr1.POSX = 0;
                Wr1.live = false;
            }
            else if (too == "Wr2")
            {
                Wr2.POSY = 0;
                Wr2.POSX = 0;
                Wr2.live = false;
            }
            else if (too == "Wk1")
            {
                Wk1.POSY = 0;
                Wk1.POSX = 0;
                Wk1.live = false;
            }
            else if (too == "Wk2")
            {
                Wk2.POSY = 0;
                Wk2.POSX = 0;
                Wk2.live = false;
            }
            else if (too == "Wb1")
            {
                Wb1.POSY = 0;
                Wb1.POSX = 0;
                Wb1.live = false;
            }
            else if (too == "Wb2")
            {
                Wb2.POSY = 0;
                Wb2.POSX = 0;
                Wb2.live = false;
            }
            else if (too == "Wq")
            {
                Wq.POSY = 0;
                Wq.POSX = 0;
                Wq.live = false;
            }
            else if (too == "KingW")
            {
                KingW.POSY = 0;
                KingW.POSX = 0;
                KingW.live = false;

            }
        }
        else if (jj == false)
        {
            Debug.Log("too " + jj);
            Debug.Log(too.Substring(0, 1));
            int tt = int.Parse(too.Substring(0, 1));
            Debug.Log(tt);
            Debug.Log(too.Substring(2, 1));
            int rr = int.Parse(too.Substring(2, 1));
            Debug.Log(rr);
            //BLACK PAWNS
            if (move == "Bp1")
            {
                Bp1.POSY = tt;
                Bp1.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bp2")
            {
                Bp2.POSY = tt;
                Bp2.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bp3")
            {
                Bp3.POSY = tt;
                Bp3.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bp4")
            {
                Bp4.POSY = tt;
                Bp4.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bp5")
            {
                Bp5.POSY = tt;
                Bp5.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bp6")
            {
                Bp6.POSY = tt;
                Bp6.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bp7")
            {
                Bp7.POSY = tt;
                Bp7.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bp8")
            {
                Bp8.POSY = tt;
                Bp8.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }

            //WHITE PAWNS
            else if (move == "Wp1")
            {
                Wp1.POSY = tt;
                Wp1.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Wp2")
            {
                Wp2.POSY = tt;
                Wp2.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Wp3")
            {
                Wp3.POSY = tt;
                Wp3.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Wp4")
            {
                Wp4.POSY = tt;
                Wp4.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Wp5")
            {
                Wp5.POSY = tt;
                Wp5.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Wp6")
            {
                Wp6.POSY = tt;
                Wp6.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Wp7")
            {
                Wp7.POSY = tt;
                Wp7.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Wp8")
            {
                Wp8.POSY = tt;
                Wp8.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }

            
            //BLACK PEACES
            else if (move == "Br1")
            {
                Br1.POSY = tt;
                Br1.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Br2")
            {
                Br2.POSY = tt;
                Br2.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bk1")
            {
                Bk1.POSY = tt;
                Bk1.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bk2")
            {
                Bk2.POSY = tt;
                Bk2.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bb1")
            {
                Bb1.POSY = tt;
                Bb1.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bb2")
            {
                Bb2.POSY = tt;
                Bb2.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Bq")
            {
                Bq.POSY = tt;
                Bq.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "KingB")
            {
                KingB.POSY = tt;
                KingB.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }

            //WHITE PEACES
            else if (move == "Wr1")
            {
                Wr1.POSY = tt;
                Wr1.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Wr2")
            {
                Wr2.POSY = tt;
                Wr2.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Wk1")
            {
                Wk1.POSY = tt;
                Wk1.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Wk2")
            {
                Wk2.POSY = tt;
                Wk2.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Wb1")
            {
                Wb1.POSY = tt;
                Wb1.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "Wb2")
            {
                Wb2.POSY = tt;
                Wb2.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }           
            else if (move == "Wq")
            {
                Wq.POSY = tt;
                Wq.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }
            else if (move == "KingW")
            {
                KingW.POSY = tt;
                KingW.POSX = rr;
                Debug.Log("SARTING COLOR");
                StartCoroutine(addColorForm(otherPlayer.color));
            }


            Debug.Log("SARTING COLOR");
            StartCoroutine( addColorForm(otherPlayer.color));
        }
    }

    public bool check_collor(marks a)
    {
        Debug.Log("check_collor");
        Debug.Log(a.color+ " a.color");
        Debug.Log(currentPlayer.color + " currentPlayer.color");
        bool t;
        string m;
        if(a.color == false)
        {
            m = "white";
        }
        else
        {
            m = "black";
        }
        Debug.Log(m);

        if(m == currentPlayer.color)
        {
            Debug.Log("return true");
            return true;
        }
        else
        {
            Debug.Log("return flase");
            return false;
        }
    }

    public bool check_Bp(marks a)
    {
        string check_l;
        if (a.POSY <= 8)
        {
            return false;
        }

        if (a.POSX - 1 > 0)
        {
            check_l = Check_box(a.POSX-1, a.POSY, a.color);
            if (check_l == "enemy")
            {
                locations.Add(a.POSY + "," + (a.POSX-1));
                return true;
            }
        }
        string check_r;
        if (a.POSX + 1 < 8)
        {
            check_r = Check_box(a.POSX+1, a.POSY, a.color);
            if (check_r == "enemy")
            {
                locations.Add(a.POSY + "," + (a.POSX + 1));
                return true;
            }
        }
        string check_f;
        check_f = Check_box(a.POSX , a.POSY+1, a.color);
        if(check_f == "open")
        {
            locations.Add((a.POSY + 1) + "," + a.POSX);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool check_Wp(marks a)
    {
        string check_l;
        if (a.POSY  <= 1)
        {
            return false;
        }

        if (a.POSX - 1 > 0)
        {
            check_l = Check_box(a.POSX - 1, a.POSY, a.color);
            if (check_l == "enemy")
            {
                locations.Add(a.POSY + "," + (a.POSX - 1));
                return true;
            }
        }
        string check_r;
        if (a.POSX + 1 < 8)
        {
            check_r = Check_box(a.POSX + 1, a.POSY, a.color);
            if (check_r == "enemy")
            {
                locations.Add(a.POSY + "," + (a.POSX + 1));
                return true;
            }
        }
        string check_f;
        check_f = Check_box(a.POSX, a.POSY - 1, a.color);
        if (check_f == "open")
        {
            locations.Add((a.POSY-1) + "," + a.POSX);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool check_KING(marks a)
    {
        return false;
    }

    public bool check_moves(marks a)
    {
        bool check;
        locations.Clear();
        if(a.pieace == "Bp")
        {
            check = check_Bp(a);
        }
        else if (a.pieace == "Wp")
        {
            check = check_Wp(a);
        }
        else if ((a.pieace == "Br")||(a.pieace == "Wr"))
        {

        }
        else if ((a.pieace == "Bk") || (a.pieace == "Wk"))
        {

        }
        else if ((a.pieace == "Bb") || (a.pieace == "Wb"))
        {

        }
        else if ((a.pieace == "Bq") || (a.pieace == "Wq"))
        {

        }
        else if ((a.pieace == "KINGb") || (a.pieace == "KINGw"))
        {

        }
        return false;
    }

    public string Check_box(int x, int y, bool colr)
    {
        Debug.Log("Check_box");
        string open = "open";
        

        if ((x == Bp1.POSX) && (y == Bp1.POSY))
        {
            if (Bp1.color == colr)
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
        else if ((x == Bp3.POSX) && (y == Bp3.POSY))
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
        else if ((x == Bp4.POSX) && (y == Bp4.POSY))
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
        else if ((x == Bp5.POSX) && (y == Bp5.POSY))
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
        else if ((x == Bp6.POSX) && (y == Bp6.POSY))
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
        else if ((x == Bp7.POSX) && (y == Bp7.POSY))
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
        else if ((x == Bp8.POSX) && (y == Bp8.POSY))
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
        Debug.Log("Get_peace");
        marks name = null;


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

    public marks Get_peace_by_name(string x)
    {
        Debug.Log("Get_peace");
        marks name = null;


        if ((x == Bp1.name))
        {
            name = Bp1;
        }
        else if ((x == Bp2.name))
        {
            name = Bp2;
        }
        else if ((x == Bp3.name))
        {
            name = Bp3;
        }
        else if ((x == Bp4.name))
        {
            name = Bp4;
        }
        else if ((x == Bp5.name))
        {
            name = Bp5;
        }
        else if ((x == Bp6.name))
        {
            name = Bp6;
        }
        else if ((x == Bp7.name))
        {
            name = Bp7;
        }
        else if ((x == Bp8.name))
        {
            name = Bp8;
        }

        // white pawns

        else if ((x == Wp1.name))
        {
            name = Wp1;
        }
        else if ((x == Wp2.name))
        {
            name = Wp2;
        }
        else if ((x == Wp3.name))
        {
            name = Wp3;
        }
        else if ((x == Wp4.name))
        {
            name = Wp4;
        }
        else if ((x == Wp5.name))
        {
            name = Wp5;
        }
        else if ((x == Wp6.name))
        {
            name = Wp6;
        }
        else if ((x == Wp7.name))
        {
            name = Wp7;
        }
        else if ((x == Wp8.name))
        {
            name = Wp8;
        }

        //black pices 

        else if ((x == Br1.name))
        {
            name = Br1;
        }
        else if ((x == Br2.name))
        {
            name = Br2;
        }
        else if ((x == Bk1.name))
        {
            name = Bk1;
        }
        else if ((x == Bk2.name))
        {
            name = Bk2;
        }
        else if ((x == Bb1.name))
        {
            name = Bb1;
        }
        else if ((x == Bb2.name))
        {
            name = Bb2;
        }
        else if ((x == Bq.name))
        {
            name = Bq;
        }
        else if ((x == KingB.name))
        {
            name = KingB;
        }

        //white pieces 

        else if ((x == Wr1.name))
        {
            name = Wr1;
        }
        else if ((x == Wr2.name))
        {
            name = Wr2;
        }
        else if ((x == Wk1.name))
        {
            name = Wk1;
        }
        else if ((x == Wk2.name))
        {
            name = Wk2;
        }
        else if ((x == Wb1.name))
        {
            name = Wb1;
        }
        else if ((x == Wb2.name))
        {
            name = Wb2;
        }
        else if ((x == Wq.name))
        {
            name = Wq;
        }
        else if ((x == KingW.name))
        {
            name = KingW;
        }



        return name;
    }
}
