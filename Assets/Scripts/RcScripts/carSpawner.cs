using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carSpawner : MonoBehaviour
{
    public GameObject[] cars;
    int carNo;
    public float maxPos = 2.3f;
    public float delayTimer = 4f;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = delayTimer;
        
        Vector3 carPos = new Vector3 (Random.Range(-2.3f,2.3f),transform.position.y,transform.position.z);
        Instantiate (cars[carNo],carPos, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {   if(delayTimer > 0.5f)
            delayTimer -= Time.deltaTime/7;
         
        timer -= Time.deltaTime;
        if(timer <= 0){
            Vector3 carPos = new Vector3 (Random.Range(-2.3f,2.3f),transform.position.y,transform.position.z);
            carNo = Random.Range(0,6);
            Instantiate (cars[carNo],carPos, transform.rotation);
            timer = delayTimer;
        }   
    }
}
