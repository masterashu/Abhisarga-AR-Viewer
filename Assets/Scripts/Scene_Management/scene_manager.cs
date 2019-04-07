using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Used to Load a Scene Using it's name
using Vuforia;   // Used to connect to Vuforia Tasks as to view an Overlay over an image
using UnityEngine.Events;  // Used for detection of events i.e. tracking objects

namespace scene_manager
{
    public class scene_manager : MonoBehaviour
    {
        int current_scene = 0;

        public void SceneLoader(int SceneIndex)
        {
            SceneManager.LoadScene(SceneIndex);
            current_scene = SceneIndex;
        }
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
