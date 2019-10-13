using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BallNav : MonoBehaviour
{
    public Transform goal;
    public bool manual;
    public float manualSpeed = 5;
    public float delay = 6;
    void Update()
    {



        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * Time.deltaTime * h * manualSpeed);



        transform.Translate(Vector3.forward * Time.deltaTime * v * manualSpeed);

    }


    IEnumerator delayToStart()
    {
        yield return new WaitForSeconds(delay);
        goal = GameObject.FindGameObjectWithTag("Goal").transform;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        agent.destination = goal.position;

    }
    void Start()
    {
        if (!manual)
        {
            StartCoroutine(delayToStart());


        }
    }
}
