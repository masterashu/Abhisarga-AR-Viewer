using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public Text Hs1;
    public Text Hs2;
    public Text Hs3;
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        MyScript.UpdateScores();
        Hs1.text = PlayerPrefs.GetInt("Game1Score").ToString();
        Hs2.text = PlayerPrefs.GetInt("Game2Score").ToString();
        Hs3.text = PlayerPrefs.GetInt("Game3Score").ToString();
    }

    public void MainMenu(){
        SceneManager.LoadScene(1);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(1);
        }
    }
}
