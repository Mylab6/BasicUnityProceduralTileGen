using UnityEngine;
using System.Collections;

public class BlockObjectBase
{
    public string name;
    [Tooltip("In general only use blocks which are 1x1x1 , anything else is not supported  ")]

    public GameObject prefab;

    public Vector3 prefabSize
    {

        get { return this.prefab.transform.GetComponent<Renderer>().bounds.size; }
    }

}
