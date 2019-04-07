using UnityEngine;
using System.Collections;

public class collisionScript : MonoBehaviour {

  // Use this for initialization
  void Start () {

  }

  // Update is called once per frame
  void Update () {

  }

  //for this to work both need colliders, one must have rigid body (spaceship) the other must have is trigger checked.
  void OnTriggerEnter (Collider col)
  {
    GameObject explosion = Instantiate(Resources.Load("PlasmaExplosionEffect", typeof(GameObject))) as GameObject;
    explosion.transform.position = transform.position;
    if(scoreScript.enemiesLeft > 0){
      Destroy(col.gameObject);
      Destroy (explosion, 1);

      scoreScript.enemiesLeft -= 1;
      /*if (GameObject.FindGameObjectsWithTag("Player").Length == 0){
  
        GameObject enemy = Instantiate(Resources.Load("enemy", typeof(GameObject))) as GameObject;
        GameObject enemy2 = Instantiate(Resources.Load("enemy2", typeof(GameObject))) as GameObject;
        GameObject enemy3 = Instantiate(Resources.Load("enemy3", typeof(GameObject))) as GameObject;
        GameObject enemy4 = Instantiate(Resources.Load("enemy4", typeof(GameObject))) as GameObject;
        GameObject enemy5 = Instantiate(Resources.Load("enemy5", typeof(GameObject))) as GameObject;
        GameObject enemy6 = Instantiate(Resources.Load("enemy6", typeof(GameObject))) as GameObject;
      }*/

      Destroy (gameObject);
    }


  }

}

