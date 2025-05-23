using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCameraq : MonoBehaviour
{
    public float turnspeed=50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hz = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, hz * turnspeed * Time.deltaTime);
            }
}
