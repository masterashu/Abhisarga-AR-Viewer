using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public Button playagain;
    public Button pauseb;
    public Button Play;
    public Text ScoreText;
    public AudioSource crash;
    int score;
    bool gameover;
    // Start is called before the first frame update
    void Start()
    {
        pause();
        
        gameover = false;
        score = 0;
        InvokeRepeating("ScoreUpdate",1.0f,1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Screen.orientation = ScreenOrientation.Portrait;
            SceneManager.LoadScene(2);
        }
        ScoreText.text = "Time : "+ score;
    }

    void ScoreUpdate()
    {
        if(!gameover)
        {   score += 1;
        }
    }

    public void GameOver(){
        crash.Play();
        gameover = true;
        playagain.gameObject.SetActive(true);
        pauseb.gameObject.SetActive(false);
        pause();
        if(score > PlayerPrefs.GetInt("Game3score")){
            PlayerPrefs.SetInt("Game3Score", score);
        }
        Screen.orientation = ScreenOrientation.Portrait;
        MyScript.UpdateScores();
        SceneManager.LoadScene(15);
    }

    public void pause()
    {
        if(Time.timeScale == 1)
            Time.timeScale = 0;
        else if(Time.timeScale == 0)
            Time.timeScale = 1;
    }


    public void PlayAgain(){
        Application.LoadLevel("SampleScene");
        pause();
    }

}
