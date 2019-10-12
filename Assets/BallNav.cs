using UnityEngine;
using UnityEngine.AI;

public class BallNav : MonoBehaviour
{
    public Transform goal;
    public bool manual;
    public float manualSpeed = 5;
    void Update()
    {



        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * Time.deltaTime * h * manualSpeed);



        transform.Translate(Vector3.forward * Time.deltaTime * v * manualSpeed);

    }

    void Start()
    {
        if (!manual)
        {
            goal = GameObject.FindGameObjectWithTag("Goal").transform;
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = goal.position;
        }
    }
}
