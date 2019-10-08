using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BlockObject
{
    public string name;
    [Tooltip("In general only use blocks which are 1x1x1 , anything else is not supported  ")]

    public GameObject prefab;
    public Vector3 currentLocation;
    // public Color altColor;
    [Tooltip("Determine what vector points , when divided by this , will allow spaning. So 1,1,1, is always allowed to spawn, 5,1,1 will only spawn when x =  5,10, 15 etc ")]
    public Vector3 spawnDivisible = new Vector3(1, 1, 1);
    // public SpawnDir spawnDir; 
    [Tooltip("How likely the block is to spawn")]

    [Range(0.01f, 1f)]

    public float probability = 1;

    public Vector3 prefabSize
    {

        get { return this.prefab.transform.GetComponent<Renderer>().bounds.size; }
    }
    public float spawnDivisibleFloat
    {

        get { return this.spawnDivisible.x + spawnDivisible.y + spawnDivisible.z; }
    }
    public bool canSpawn(Vector3 vert)
    {
        //   System.Random rand = new System.Random();
        //int yes = 0;

        if (UnityEngine.Random.Range(0.01f, 1f) <= probability)
        {



            return (vert.x % spawnDivisible.x == 0) && (vert.y % spawnDivisible.y == 0) && (vert.z % spawnDivisible.z == 0);
        }
        return false;

        //return false; 
    }
}
