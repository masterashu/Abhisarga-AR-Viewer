using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class WelcomePageLoaderScript : MonoBehaviour
{
    public Text idText;
    protected int UserID = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("userid") == false){
            PlayerPrefs.SetInt("userid", UserID);
            Debug.Log("Player ID " + UserID.ToString());
        }
        else{
            UserID = PlayerPrefs.GetInt("userid");
            Debug.Log("Player ID : " + UserID.ToString());
        }
        idText.text = UserID.ToString();
    }
    public void continueToScanPage(int sceneNumber){
        UserID = PlayerPrefs.GetInt("userid");
        if(UserID == 0){
            SceneManager.LoadScene(13);
        }
        else{
            SceneManager.LoadScene(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(0);
        }
    }
}
