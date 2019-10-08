using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpin : MonoBehaviour
{

    public float rotateSpeed = 3;
    public Vector3 rotateVector = new Vector3(1, 0, 0); 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateVector * Time.deltaTime * rotateSpeed);
    }
}
