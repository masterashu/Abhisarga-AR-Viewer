using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.SceneManagement;
using Vuforia;

public class MainGameLauncher : MonoBehaviour, ITrackableEventHandler
{
    public TrackableBehaviour mTrackableBehaviour;
    public TrackableBehaviour.Status m_PreviousStatus;
    public TrackableBehaviour.Status m_NewStatus;

    public void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(0);
        }
    }

    public void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            if(mTrackableBehaviour.TrackableName == "spaceshooter"){
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                SceneManager.LoadScene(2);
            }
            
            if(mTrackableBehaviour.TrackableName == "trafficracer"){
                SceneManager.LoadScene(8);
            }

            if(mTrackableBehaviour.TrackableName == "Tanker3d"){
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                SceneManager.LoadScene(4);
            }

            if(mTrackableBehaviour.TrackableName == "TankerAR"){
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                SceneManager.LoadScene(6);
            }

            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }
    protected void OnTrackingFound()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);
        
        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = true;

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;
    }


    protected void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;
    }

}
