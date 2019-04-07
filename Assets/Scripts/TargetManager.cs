// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Vuforia;
// using System.Linq;

// public class TargetManager : MonoBehaviour
// {
//     public string mDatabaseName = "";


//     private List<TrackableBehaviour> mAllTargets = new List<TrackableBehaviour>();


//     public void Awake(){
//         VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
//     }
//     public void OnDestroy(){
//         VuforiaARController.Instance.UnregisterVuforiaStartedCallback(OnVuforiaStarted);
//     }
//     public void OnVuforiaStarted(){
//         LoadDatabase(mDatabaseName);

//         mAllTargets = GetTargets();

//         SetupTargets(mAllTargets);

//     }

//     private void LoadDatabase(string setName){

//         ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
//         objectTracker.Stop();
//         if(DataSet.Exists(setName)){
//             DataSet dataSet = objectTracker.CreateDataSet();
//             objectTracker.ActivateDataSet(dataSet);
//         }
//         objectTracker.Start();

//     }

//     private List<TrackableBehaviour> GetTargets(){
//         List<TrackableBehaviour> allTrackables = new List<TrackableBehaviour>();
//         allTrackables = TrackerManager.Instance.GetStateManager().GetTrackableBehaviours().ToList();
//         // later
//         return null;
//     }

//     private void SetupTargets(List<TrackableBehaviour> AllTargets){
//         foreach (TrackableBehaviour target in allTargets)
//         {
//             target.gameObject.transform.parent = transform;

//             target.gameObject.name = target.TrackableName;

//             Debug.log(target.TrackableName + " " + "Created!");
//         }
//     }

//     public void Update()
//     {
//     }
// }
