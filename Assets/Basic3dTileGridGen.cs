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
[Serializable]
public class titleHolderObject
{
    public GameObject tileHolder; 
    public Bounds  titleBounds ;
    public Vector3 targetRotation;  //= new Quaternion()
    public List<BlockObjectWithRandom> blocksInGame;
}
// Based Off https://gist.github.com/mdomrach/a66602ee85ce45f8860c36b2ad31ea14

public class Basic3dTileGridGen : MonoBehaviour
{
    public List<BlockObjectWithRandom> blockObjectPrefabs; 
    public int GridWidthF;
    public int GridLengthF = 4;
    public Vector3 targetRotation;
    public float blockBuildTime = 0.3f; 
  //  public List<BlockObject> blocksInGame;
    public List< int> publicIndecies;
    public BlockObjectWithRandom defaultBlock;
    public List<titleHolderObject> tileHolders;
    //public Vector3 titleHolderSiz
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

            for (int f = 0; f < GridLengthF; f++)
            {
                verticies.Add(new Vector3(i, 0, f));
            }
        }
       
        publicIndecies = indicies;

        StartCoroutine( Gen3dTiles(verticies)); 
    }

    public IEnumerator Gen3dTiles( List <Vector3> verticies, int tileHolderIndex = 0 )
    {

        if(tileHolders.Count -1  < tileHolderIndex)
        {
            tileHolders.Add(new titleHolderObject()); 

        }
        var currentTileHolderObject = tileHolders[tileHolderIndex];
      
        if(currentTileHolderObject.tileHolder)
        {
            Destroy(currentTileHolderObject.tileHolder); 
        }
        currentTileHolderObject.tileHolder = new GameObject("Tile Holder:" + currentTileHolderObject);
        currentTileHolderObject.tileHolder.transform.parent = transform;
        currentTileHolderObject.blocksInGame = new List<BlockObjectWithRandom>(); 

       

        
            foreach (var vert in verticies)
            {

                if (blockObjectPrefabs.Count != 0)
                {
                    BlockObjectWithRandom blockObjectPrefab;
                   // Vector3 newVert;
                    GetBlockPreb( vert, out blockObjectPrefab);
                    bool build = true;
                    foreach (var block in currentTileHolderObject.blocksInGame)
                    {
                        if (block.currentLocation == vert)
                        {
                            build = false;
                        }

                    }
                    if (build)
                    {
                        var newBlockOb = new BlockObjectWithRandom();

                        //vert.y = indc; 
                        newBlockOb.prefab = Instantiate(blockObjectPrefab.prefab, vert, transform.rotation);
                        newBlockOb.prefab.transform.parent = currentTileHolderObject.tileHolder.transform;

                        newBlockOb.name += vert.ToString();
                        newBlockOb.currentLocation = vert;
                        currentTileHolderObject.blocksInGame.Add(newBlockOb);
                    }
                    if (blockBuildTime > 0.1f)
                    {
                        yield return new WaitForSeconds(blockBuildTime);
                    }
                }
            }

        

        // calculate title holder size

        currentTileHolderObject.titleBounds = new Bounds(currentTileHolderObject.tileHolder. transform.position, Vector3.zero);

        foreach (Renderer r in currentTileHolderObject.tileHolder. GetComponentsInChildren<Renderer>())
        {
            currentTileHolderObject.titleBounds.Encapsulate(r.bounds);
        }

        currentTileHolderObject.tileHolder.transform.localPosition = new Vector3(0, 0, 0);
        currentTileHolderObject.targetRotation = targetRotation;
        //  cube1.transform.
        var newX = currentTileHolderObject.targetRotation.x;
        var newY = currentTileHolderObject.targetRotation.y;
        var newZ = currentTileHolderObject.targetRotation.z; 
        currentTileHolderObject.tileHolder.transform.Rotate(newX, newY,newZ,  Space.Self);
        yield return new WaitForSeconds(5);
    }

    public void GetBlockPreb( Vector3 vert, out BlockObjectWithRandom blockObjectPrefab)
    {
        blockObjectPrefab = null;
       // newVert = new Vector3(vert.x + indc, 0, vert.z);
        foreach (var blockPreFabThing in blockObjectPrefabs)
        {
            if (blockPreFabThing.canSpawn(vert))
            {

                blockObjectPrefab = blockPreFabThing;

            }
        }
        if (blockObjectPrefab == null)
        {
            blockObjectPrefab = defaultBlock;
        }
    }
}