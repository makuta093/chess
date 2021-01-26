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
using UnityEngine.SceneManagement;

public class objects
{
    
    public int POSX;
    public int POSY;
    public string date;
    public string shape;
    public string name;
    
   
}

public class firebase : MonoBehaviour
{
    public GameObject player;
    public GameObject ty;
    FirebaseStorage storage;
    public string currentPlayerKey;
    public GameObject t;
    public Scene scen;
    gameManager gm;
    public int playerPOSX =0;
    public int playerPOSY =0;
    public List<string> index = new List<string>();
    public List<string> posx = new List<string>();
    public List<string> posy = new List<string>();
    public List<string> date = new List<string>();
    public List<string> shape = new List<string>();
    public List<string> name = new List<string>();
    public GameObject dbText, nameInput, xposInput, yposInput, shapeInput;
    public GameObject loginBUTTON;
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


    


    IEnumerator createUser()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        //this is not enough, because I have no guarantee of the user being created.
        //auth.CreateUserWithEmailAndPasswordAsync(email, password);



        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(
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
                     usercreated = true;
                     return;
                 }
                 //if anything goes wrong, otherwise
                 Firebase.Auth.FirebaseUser myNewUser = createUserTask.Result;
                 Debug.Log("Your nice new user is:" + myNewUser.DisplayName + " " + myNewUser.UserId);
                 //THIS IS WHAT HAPPENS AT THE END OF THE ASYNC TASK
                 usercreated = true;

             }
             );

        while (!usercreated)
        {

            yield return null;
        }
        //this is where my user has been created
        //Debug.Log("ready!!!");



    }

    IEnumerator addRecordFromForm()
    {
        DateTime dt = DateTime.Now;
        Debug.Log(dt.ToString("yyyy-MM-dd"));
        objects newPlayer = new objects();
        string t = shapeInput.GetComponent<InputField>().text;
        

        if (t.Equals("square"))
        {
            t = "square";
        }
        else
        {
            t = "circle";
        }

        
        newPlayer.POSX = Convert.ToInt32(xposInput.GetComponent<InputField>().text);
        newPlayer.POSY = Convert.ToInt32(yposInput.GetComponent<InputField>().text);
        newPlayer.name = nameInput.GetComponent<InputField>().text;
        newPlayer.shape = t;
        newPlayer.date = dt.ToString("yyyy-MM-dd");
        //first sign in
        yield return StartCoroutine(signInToFirebase());
        //I am done from signing in.
        Debug.Log("now signed in" + Time.time);

        string jsonNewPlayer = JsonUtility.ToJson(newPlayer);

        //start the process of adding a player
        yield return StartCoroutine(addDataClass(jsonNewPlayer));

        //clear the values that are already on screen
        

        //now I'm ready and can refresh the data
        yield return getDataFromFirebase();


    }

    IEnumerator addDataClass(string datatoinsert)
    {
        //create a unique ID
        string newkey = reference.Push().Key;
        Debug.Log(newkey);
        //Update the unique key with the data I want to insert
        yield return StartCoroutine(updateDataClass(newkey, datatoinsert));

    }

    IEnumerator updateDataClass(string childlabel, string newdata)
    {

        bool dataupdated = false;
        //find the child of player4 that corresponds to playername and set the value to whatever is inside newdata
        reference.Child(childlabel).SetRawJsonValueAsync(newdata).ContinueWithOnMainThread(
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

    public IEnumerator initFirebase()
    {
        if (!signedin)
        {
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://circle-and-sqaure-default-rtdb.firebaseio.com/");
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
    //get the parent data
    IEnumerator displayData()
    {
        foreach (var element in myDataDictionary)
        {

            Debug.Log(element.Key.ToString() + "<->" + element.Value.ToString());
            // Debug.Log(element.Key.Child("POSX").ToString());
            if ((element.Key.ToString() != "playerPOSX")&&(element.Key.ToString() != "playerPOSY"))
            {
                index.Add(element.Key.ToString());

                new WaitForSeconds(1f);
                StartCoroutine(getDataFromFirebase(element.Key.ToString()));
                
            }
            //  Debug.Log(showData(element.Value));
            if (element.Key.ToString() == "playerPOSX")
            {
                playerPOSX = int.Parse(element.Value.ToString());
            }
            if (element.Key.ToString() == "playerPOSY")
            {
                playerPOSY = int.Parse(element.Value.ToString());
            }

            new WaitForSeconds(1f);
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
        yield return StartCoroutine(shape_spawn());
    }

    IEnumerator shape_spawn()
    {
        foreach (var element in myDataDictionary)
        {

            Debug.Log(element.Key.ToString() + "<->" + element.Value.ToString());
            // Debug.Log(element.Key.Child("POSX").ToString());

            if(element.Key.ToString() == "POSX")
            {
                posx.Add(element.Value.ToString());
            }
            if (element.Key.ToString() == "POSY")
            {
                posy.Add(element.Value.ToString());
            }
            if (element.Key.ToString() == "date")
            {
                date.Add(element.Value.ToString());
            }
            if (element.Key.ToString() == "shape")
            {
                shape.Add(element.Value.ToString());
            }
            if (element.Key.ToString() == "name")
            {
                name.Add(element.Value.ToString());
            }
            //  Debug.Log(showData(element.Value));

            new WaitForSeconds(1f);

            yield return new WaitForSeconds(1f);
        }

        yield return null ;
    }

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<SpriteRenderer>().enabled = false;
        ty.SetActive(false);
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://circle-and-sqaure-default-rtdb.firebaseio.com/");

        reference = FirebaseDatabase.DefaultInstance.RootReference;
        DontDestroyOnLoad(this.gameObject);
    }
    IEnumerator getAllData()
    {
        //get all data has to wait until I have a number of records
        yield return getNumberOfRecords();
        Debug.Log(numberOfRecords);

        //wait for the getdata coroutine to finish and yield return null
        yield return getDataFromFirebase();

    }

    IEnumerator spawn_shapes()
    { string sharpe;
        int i = 0;
            while(i < index.Count)
        {
            GameObject go2 = new GameObject(name[i]+date[i]);
            go2.transform.position = new Vector3(int.Parse(posx[i]), int.Parse(posy[i]));
            go2.transform.localScale = new Vector3(0.05f, 0.05f);
            go2.AddComponent<SpriteRenderer>();
            if (shape[i].Equals("circle"))
            {
                sharpe = "c";
            }
            else
            {
                sharpe = "s";
            }
            StartCoroutine(downloadAndSaveImage(name[i] + date[i],sharpe));
            i++;
        }
        yield return null;
    }

    public IEnumerator downloadAndSaveImage(string obj, string shape)
    {

        string pathToSaveIn = Application.persistentDataPath;

        storage = FirebaseStorage.DefaultInstance;

        // Create local filesystem URL


        
            string filename = Application.persistentDataPath + "/"+ shape + ".png";

            StorageReference storage_ref = storage.GetReferenceFromUrl("gs://circle-and-sqaure.appspot.com/" + shape + ".png");
        
        // Start downloading a file
        Task task = storage_ref.GetFileAsync(filename,
          new Firebase.Storage.StorageProgress<DownloadState>((DownloadState state) =>
          {
              // called periodically during the download
              Debug.Log(String.Format(
                "Progress: {0} of {1} bytes transferred.",
                state.BytesTransferred,
                state.TotalByteCount
              ));
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


        Sprite warship = LoadSprite(filename);
        GameObject.Find(obj).GetComponent<SpriteRenderer>().sprite = warship;


        yield return null;
    }

    private Sprite LoadSprite(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;
        if (System.IO.File.Exists(path))
        {
            Debug.Log("hello");
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }
        return null;
    }

    public void show()
    {
        
        index.Clear();
        posx.Clear();
         posy.Clear();
         date.Clear();
        shape.Clear();
         name.Clear();
        
        StartCoroutine(getDataFromFirebase());
        
        
    }

    public void addDATA()
    {
        index.Clear();
        posx.Clear();
        posy.Clear();
        date.Clear();
        shape.Clear();
        name.Clear();
        StartCoroutine(addRecordFromForm());

       
    }

    public void tark()
    {
        ty.SetActive(false);
        player.GetComponent<SpriteRenderer>().enabled = true;
    }

        public void tar()
    {
        t.SetActive(false);
        StartCoroutine(spawn_shapes());
        ty.SetActive(true);
    }

    public void login()
    {
        StartCoroutine(signInToFirebase());
        
        StartCoroutine(getDataFromFirebase());

    }

    // Update is called once per frame
    void Update()
    {
        player.transform.position = new Vector3(playerPOSX, playerPOSY);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerPOSY++;
            StartCoroutine(updateDataClass("playerPOSY", playerPOSY.ToString()));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerPOSY--;
            StartCoroutine(updateDataClass("playerPOSY", playerPOSY.ToString()));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerPOSX++;
            StartCoroutine(updateDataClass("playerPOSX", playerPOSX.ToString()));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerPOSX--;
            StartCoroutine(updateDataClass("playerPOSX", playerPOSX.ToString()));
        }
    }
}
