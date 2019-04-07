using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

    public class GameManager : MonoBehaviour
    {
        public float LastScore = 100;

        public int m_NumRoundsToWin = 3;        
        public float m_StartDelay = 2f;         
        public float m_EndDelay = 3f;           
        public CameraControl m_CameraControl;   
        public Text m_MessageText;              
        public GameObject m_TankPrefab;         
        public TankManager[] m_Tanks;           
        public GameObject m_TankPlayer;

        public TankPlay m_Player;

        public Text timing;
        private int m_RoundNumber;              
        private WaitForSeconds m_StartWait;     
        private WaitForSeconds m_EndWait;       
        private TankManager m_RoundWinner;
        private TankPlay m_GameWinner;     
        
        private float time = 0;  
        private bool alive;
        private bool is_game_on = false;

        public void Start()
        {
            m_StartWait = new WaitForSeconds(m_StartDelay);
            m_EndWait = new WaitForSeconds(m_EndDelay);
            SpawnAllTanks();
            SetCameraTargets();
            is_game_on = true;
            StartCoroutine(GameLoop());

        }
        private void Update()
        {
            if(is_game_on){
                time += Time.deltaTime;
                timing.text = "Time Left : " + (93-time).ToString("0");
            }
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Screen.orientation = ScreenOrientation.Portrait;
                SceneManager.LoadScene(4);
            }
        }
        private void SpawnAllTanks()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[i].m_Instance =
                    Instantiate(m_TankPrefab, m_Tanks[i].m_SpawnPoint.position, m_Tanks[i].m_SpawnPoint.rotation) as GameObject;
                m_Tanks[i].m_PlayerNumber = i + 1;
                m_Tanks[i].Setup();
            }
            
                m_Player.m_Instance =
                    Instantiate(m_TankPlayer, m_Player.m_SpawnPoint.position, m_Player.m_SpawnPoint.rotation) as GameObject;
                m_Player.m_PlayerNumber =4;
                m_Player.Setup();
        }


        private void SetCameraTargets()
        {
            Transform[] targets = new Transform[(m_Tanks.Length)+1];

            for (int i = 0; i < (targets.Length)-1; i++)
            {
                targets[i] = m_Tanks[i].m_Instance.transform;
            }
            targets[(targets.Length)-1] = m_Player.m_Instance.transform;

            m_CameraControl.m_Targets = targets;
        }


        private IEnumerator GameLoop()
        {
            yield return StartCoroutine(RoundStarting());
            yield return StartCoroutine(RoundPlaying());
            yield return StartCoroutine(RoundEnding());

            if (m_GameWinner != null || time < 93)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            else
            {
                StartCoroutine(GameLoop());
            }
        }


    private IEnumerator RoundStarting ()
        {
            // As soon as the round starts reset the tanks and make sure they can't move.
            ResetAllTanks ();
            DisableTankControl ();

            // Snap the camera's zoom and position to something appropriate for the reset tanks.
            m_CameraControl.SetStartPositionAndSize ();

            // Increment the round number and display text showing the players what round it is.
            m_RoundNumber++;
            m_MessageText.text = "Game Begins! ";// + m_RoundNumber;

            // Wait for the specified length of time until yielding control back to the game loop.
            yield return m_StartWait;
        }


        private IEnumerator RoundPlaying ()
        {
            // As soon as the round begins playing let the players control the tanks.
            EnableTankControl ();

            // Clear the text from the screen.
            m_MessageText.text = string.Empty;

            // While there is not one tank left...
            while (!OneTankLeft())
            {
                // ... return on the next frame.
                yield return null;
            }
        }


        private IEnumerator RoundEnding ()
        {
            // Stop tanks from moving.
            DisableTankControl ();

            // Clear the winner from the previous round.
            m_GameWinner = null;

            // See if there is a winner now the round is over.
            // If there is a winner, increment their score.
                m_GameWinner = GetRoundWinner();

            // Now the winner's score has been incremented, see if someone has one the game.
            //m_GameWinner = GetGameWinner ();

            // Get a message based on the scores and whether or not there is a game winner and display it.
            string message = EndMessage ();
            if(m_GameWinner != null || time > 93){
                m_MessageText.text = message;
                SceneManager.LoadScene(15);
                time = 0f;
            }

            // Wait for the specified length of time until yielding control back to the game loop.
            yield return m_EndWait;
        }


        // This is used to check if there is one or fewer tanks remaining and thus the round should end.
        private bool OneTankLeft()
        {
            // Start the count of tanks left at zero.
            int numTanksLeft = 0;
            
            // Go through all the tanks...
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                // ... and if they are active, increment the counter.
                if (m_Tanks[i].m_Instance.activeSelf)
                    numTanksLeft++;
            }
            if(time > 93){
                m_MessageText.text = "TIME UP!";
                return true;
                }
            Debug.Log(numTanksLeft);
            // If there are one or fewer tanks remaining return true, otherwise return false.
            return numTanksLeft == 0;
        }
        
        
        // This function is to find out if there is a winner of the round.
        // This function is called with the assumption that 1 or fewer tanks are currently active.
        private bool GetRoundWinner1()
        {
            int alive = 0;
            // Go through all the tanks...
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                // ... and if one of them is active, it is the winner so return it.
                if (m_Tanks[i].m_Instance.activeSelf)
                    alive += 1;
            }
            if(alive == 0) return false;
            return true;
            // If none of the tanks are active it is a draw so return null.
        }

        private TankPlay GetRoundWinner()
        {
                if(!GetRoundWinner1())
                    return m_Player;
                return null;
        }


        // This function is to find out if there is a winner of the game.
        /*private TankManager GetGameWinner()
        {
            // Go through all the tanks...
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                // ... and if one of them has enough rounds to win the game, return it.
                if (m_Tanks[i].m_Wins == m_NumRoundsToWin)
                    return m_TankPlayer;
            }

            // If no tanks have enough rounds to win, return null.
            return null;
        }*/


        // Returns a string message to display at the end of each round.
        private string EndMessage()
        {
            int myScore = (int)LastScore - (int)time;
            if(myScore < PlayerPrefs.GetInt("Game2Score") && myScore != 0){
                PlayerPrefs.SetInt("Game2Score", myScore);
            }
            // By default when a round ends there are no winners so the default end message is a draw.
            string message = "TIME UP!";

            // Go through all the tanks and add each of their scores to the message.
            /*for (int i = 0; i < m_Tanks.Length; i++)
            {
                message += m_Tanks[i].m_ColoredPlayerText + ": " + m_Tanks[i].m_Wins + " WINS\n";
            }*/
            
            // If there is a game winner, change the entire message to reflect that.
            if (m_GameWinner != null){
                message ="YOU WON THE GAME!";   
            }

            return message;
        }


        // This function is used to turn all the tanks back on and reset their positions and properties.
        private void ResetAllTanks()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[i].Reset();
            }
            m_Player.Reset();
        }


        private void EnableTankControl()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[i].EnableControl();
            }
            m_Player.EnableControl();
        }


        private void DisableTankControl()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[i].DisableControl();
            }
            m_Player.DisableControl();
        }
    }
