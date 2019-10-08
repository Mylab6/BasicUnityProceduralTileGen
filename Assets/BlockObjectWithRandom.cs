using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BlockObjectWithRandom : BlockObjectBase
{
    public Vector3 currentLocation;
    // public Color altColor;
    [Tooltip("Determine what vector points , when divided by this , will allow spaning. So 1,1,1, is always allowed to spawn, 5,1,1 will only spawn when x =  5,10, 15 etc ")]
    public Vector3 spawnDivisible = new Vector3(1, 1, 1);
    // public SpawnDir spawnDir; 
    [Tooltip("How likely the block is to spawn")]
    [Range(0.01f, 1f)]
    public float probability = 1;

    public float spawnDivisibleFloat
    {

        get { return this.spawnDivisible.x + spawnDivisible.y + spawnDivisible.z; }
    }
    public bool canSpawn(Vector3 vert)
    {
     
        if (UnityEngine.Random.Range(0.01f, 1f) <= probability)
        {



            return (vert.x % spawnDivisible.x == 0) && (vert.y % spawnDivisible.y == 0) && (vert.z % spawnDivisible.z == 0);
        }
        return false;
             }
}
