using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;
using Proyecto26;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable]
public class UserDetails
{
    public string username;
    public string email;
    public int playerid;
    public int game1score;
    public int game2score;
    public int game3score;
}

public class UsersCount
{
    public int UserIdCount;
}
public class MyScript : MonoBehaviour {
    public Text usernameText;
    public Text emailText;
    protected static DatabaseReference reference;

    void Start() {
        if(PlayerPrefs.HasKey("Game1Score") == false){
            PlayerPrefs.SetInt("Game1Score", 0);
        }
        if(PlayerPrefs.HasKey("Game2Score") == false){
            PlayerPrefs.SetInt("Game2Score", 0);
        }
        if(PlayerPrefs.HasKey("Game3Score") == false){
            PlayerPrefs.SetInt("Game3Score", 0);
        }
        if(PlayerPrefs.HasKey("username") == false){
            PlayerPrefs.SetString("username", "user");
        }
        if(PlayerPrefs.HasKey("email") == false){
            PlayerPrefs.SetString("email", "no_email");
        }
        if(PlayerPrefs.HasKey("userid") == false){
            PlayerPrefs.SetInt("userid", 0);
        }
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://iiits-ar-lens.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        if(PlayerPrefs.GetInt("userid") != 0){
            SceneManager.LoadScene(1);
        }
        else{
            Debug.Log("Creating new user");
            int nextId = 0;
            Debug.Log("Getting Next Id ");
            UsersCount usercount;
            RestClient.Get<UsersCount>("https://iiits-ar-lens.firebaseio.com/" + "UsersCount" + ".json").Then(responce => {
                usercount =  responce;
                Debug.Log(responce);
                nextId = usercount.UserIdCount;
                nextId += 1;
                Debug.Log("Next id is  " + nextId);
                PlayerPrefs.SetInt("userid", nextId);
            });
        }
    }

    public static int GetNextPlayerId(){
        int nextId = 0;
        Debug.Log("Getting Next Id ");
        UsersCount usercount;
        RestClient.Get<UsersCount>("https://iiits-ar-lens.firebaseio.com/" + "UsersCount" + ".json").Then(responce => {
            usercount =  responce;
            Debug.Log(responce);
            nextId = usercount.UserIdCount;
            nextId += 1;
            Debug.Log("Next id is  " + nextId);
        });
        return nextId + 1;
    }

    public static void UpdateScores(){
        Debug.Log("Creating a User object ");
        UserDetails user = new UserDetails();
        user.username = PlayerPrefs.GetString("username");
        user.email = PlayerPrefs.GetString("email");
        user.playerid = PlayerPrefs.GetInt("userid");
        user.game1score = PlayerPrefs.GetInt("Game1Score");
        user.game2score = PlayerPrefs.GetInt("Game2Score");
        user.game3score = PlayerPrefs.GetInt("Game3Score");

        string json = JsonUtility.ToJson(user);
        Debug.Log("Publishing to Firebase");
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://iiits-ar-lens.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        Debug.Log("Details " + user.username + " " + user.email + " " + user.playerid);
        Debug.Log("Scores " + user.game1score + " " + user.game2score + " " + user.game3score);
        reference.Child("Leaders").Child(user.playerid.ToString()).SetRawJsonValueAsync(json);

    }
    

    public void CreateNewUser(){

        PlayerPrefs.SetString("username", usernameText.text);
        PlayerPrefs.SetString("email", emailText.text);
        UpdateScores();

        Debug.Log(PlayerPrefs.GetInt("userid"));
        if(PlayerPrefs.GetInt("userid") > 1){
            UsersCount usercount = new UsersCount();
            usercount.UserIdCount = PlayerPrefs.GetInt("userid");
        
            string json = JsonUtility.ToJson(usercount);
            Debug.Log(json);
            reference.Child("UsersCount").SetRawJsonValueAsync(json);
        }
        SceneManager.LoadScene(1);
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(0);
        }
    }


  }
