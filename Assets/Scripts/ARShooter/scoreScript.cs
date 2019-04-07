using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scoreScript : MonoBehaviour
{
    public static int enemiesLeft = 6;
    public Text enemies;
    public Text time;
    int TIME;
    // Start is called before the first frame update
    void Start()
    {
        TIME = 0;
        InvokeRepeating("ScoreUpdate",1.0f,1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesLeft <= 0)
        {    
            if(Time.timeScale == 1)
                Time.timeScale = 0;
            else if(Time.timeScale == 0)
                Time.timeScale = 1;
        }    
        enemies.text = "Enemies Left : " + enemiesLeft;
        time.text = "Time : "+ TIME;
    }

    void ScoreUpdate()
    {
        if(enemiesLeft != 0){
            TIME += 1;
        }
        else{
            Screen.orientation = ScreenOrientation.Portrait;
            if(PlayerPrefs.GetInt("Game1Score") > TIME){
                PlayerPrefs.SetInt("Game1Score", TIME);
            }
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            SceneManager.LoadScene(15);
        }
    }

}
