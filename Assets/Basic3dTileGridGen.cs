using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
using UnityEditor;
public enum SpawnDir {
 X,
 Y, 
 Z
}


// Based Off https://gist.github.com/mdomrach/a66602ee85ce45f8860c36b2ad31ea14

public class Basic3dTileGridGen : MonoBehaviour
{
    public List<BlockObject> blockObjectPrefabs; 
    public int GridWidthF;
    public int GridLengthF = 4;

    public float blockBuildTime = 0.3f; 
    public List<BlockObject> blocksInGame;
    public List< int> publicIndecies;
    public BlockObject defaultBlock;
    void OnValidate()
    {
        // Gen(); 
        blockObjectPrefabs.ForEach(blockTemp =>
       {
           if (blockTemp.name == "") {
               blockTemp.name = blockTemp.prefab.name; 
                }
       }); 

    }
    void Awake()
    {
       // defaultBlock.prefabSize
        blockObjectPrefabs = blockObjectPrefabs.OrderBy(o => o.spawnDivisibleFloat).ToList();
        blockObjectPrefabs.Reverse();
        Gen();      
    }
    void Gen()
    {
       
        var verticies = new List<Vector3>();

        var indicies = new List<int>();
        for (int i = 0; i < GridWidthF; i++)
        {
            verticies.Add(new Vector3(i, 0, 0));
            verticies.Add(new Vector3(i, 0, GridWidthF));

            indicies.Add(GridLengthF * i + 0);
            indicies.Add(GridLengthF * i + 1);

            verticies.Add(new Vector3(0, 0, i));
            verticies.Add(new Vector3(GridWidthF, 0, i));

            indicies.Add(GridLengthF * i + 2);
            indicies.Add(GridLengthF * i + 3);
        }
        //publicPoints = verticies; 
        /*
        MeshFilter filter = gameObject.GetComponent<MeshFilter>();
        var mesh = new Mesh();
        mesh.vertices = verticies.ToArray();
        mesh.SetIndices(indicies.ToArray(), MeshTopology.Lines, 0);
        filter.mesh = mesh;
      
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
        meshRenderer.material.color = Color.white;
        */
        publicIndecies = indicies;

        StartCoroutine( Gen3dTiles(verticies)); 
    }

    private IEnumerator Gen3dTiles( List <Vector3> verticies)
    {
        blocksInGame.Clear();
        foreach (Transform  oldBlock in  transform)
       {

           Destroy(oldBlock.gameObject); 

       }
       

        foreach (var indc in publicIndecies)
        {
            foreach (var vert in verticies)
            {

                if (blockObjectPrefabs.Count != 0)
                {



                    BlockObject blockObjectPrefab = null; 
                    var newVert = new Vector3(vert.x + indc, 0, vert.z);

                    foreach (var  blockPreFabThing in blockObjectPrefabs)
                    {
                        if(blockPreFabThing.canSpawn(newVert))
                        {

                            blockObjectPrefab = blockPreFabThing;
                           
                        }
                    }
                    if(blockObjectPrefab == null)
                    {
                        blockObjectPrefab = defaultBlock; 
                    }
                    bool build = true; 
                        foreach( var block in blocksInGame)
                        {
                            if(block.currentLocation == newVert)
                        {
                            build = false; 
                        }

                        }
                    if (build ) 
                    {
                        var newBlockOb = new BlockObject();

                        //vert.y = indc; 
                        newBlockOb.prefab = Instantiate(blockObjectPrefab.prefab, newVert, transform.rotation);
                        newBlockOb.prefab.transform.parent = transform;
                      
                        newBlockOb.name += newVert.ToString();
                        newBlockOb.currentLocation = newVert;
                        blocksInGame.Add(newBlockOb);
                    }
                        if (blockBuildTime > 0.1f)
                    {
                        yield return new WaitForSeconds(blockBuildTime);
                    } 
                }
            }

        }
         yield return new WaitForSeconds(5);
    }
}