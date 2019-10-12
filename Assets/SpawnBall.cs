using UnityEngine;

public class SpawnBall : MonoBehaviour
{

    public Vector3 posOffSet;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {

        var gameOb = Instantiate(prefab);
        gameOb.transform.position = transform.position + posOffSet;


    }

    // Update is called once per frame
    void Update()
    {

    }
}
