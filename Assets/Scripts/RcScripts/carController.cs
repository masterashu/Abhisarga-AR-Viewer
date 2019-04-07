using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class carController : MonoBehaviour
{
    public float carSpeed = 60f;
    public float maxPos = 2.3f;
    Vector3 position;
    public UI ui;
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        position.x += CrossPlatformInputManager.GetAxis("Horizontal") * carSpeed * Time.deltaTime;
        position.x = Mathf.Clamp(position.x,-2.3f,2.3f);
        transform.position = position;
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Enemy Car"){
            Destroy(gameObject);
            ui.GameOver();
        }
    }
}
