using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMove : MonoBehaviour
{
    public float speed = 0.5f;
    Vector2 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(speed < 2.5f)
            speed += Time.deltaTime/30;
        offset = new Vector2(0,Time.time *speed);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
}
