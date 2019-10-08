using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyDrop : MonoBehaviour
{
    public float delay = 5f; 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(dropBall()); 
    }
    IEnumerator dropBall ( )
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Rigidbody>().useGravity = true; 
        yield return new WaitForSeconds(1);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
