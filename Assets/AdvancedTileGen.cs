using System;
using System.Collections.Generic;
using UnityEngine;
public class AdvancedTileGen : Basic3dTileGridGen
{
    public bool allowForcedEndAndStart = true;
    public List<PatternBlock> paternBlocks;
    public int indexToCreatePatternOn = 0;
    public int genRetries = 5;
    public int attempts = 0;
    public int forceLimit;
    public List<GameObject> newBlocks;


    //public NavMeshSurface[] surfaces;
    public Transform[] objectsToRotate;
    public override void PostGenActions()

    {


        try
        {
            createBlocksPattern();
            BuildNavMesh();
        }
        catch (Exception err)
        {

            if (attempts < genRetries)
            {
                attempts++;
                PostGenActions();
            }
        }

        //Debug.Log("Post Gen Actions Not defined on " + gameObject.name);
    }

    private void BuildNavMesh()
    {







        for (int j = 0; j < objectsToRotate.Length; j++)
        {
            objectsToRotate[j].localRotation = Quaternion.Euler(new Vector3(0, System.Random.Range(0, 360), 0));
        }

        // for (int i = 0; i < surfaces.Length; i++)
        // {
        //   surfaces[i].BuildNavMesh();
        // }
    }




    public void createBlocksPattern()
    {

        var currentTileHolder = tileHolders[indexToCreatePatternOn];
        paternBlocks.ForEach(patternBlock =>
       {
           var blocksToReplace = patternBlock.GeneratePattern(currentTileHolder.blocksInGame);



           blocksToReplace.ForEach(replaceableBlock =>
           {
               SpawnNewBlock(patternBlock, replaceableBlock, currentTileHolder);
           });


           if (allowForcedEndAndStart)
           {
               if (patternBlock.forceStartBlock != null)
               {

                   var startBlockForce = newBlocks.Find(x => x.transform.localPosition == patternBlock.randomlySelectedStart.currentLocation);
                   Destroy(startBlockForce);
                   SpawnNewBlock(patternBlock.forceStartBlock, patternBlock.randomlySelectedStart, currentTileHolder, "Start Block");
               }

               if (patternBlock.forceEndBlock != null)
               {
                   var endBlockForce = newBlocks.Find(x => x.transform.localPosition == patternBlock.randomlySelectedEnd.currentLocation);
                   Destroy(endBlockForce);
                   SpawnNewBlock(patternBlock.forceEndBlock, patternBlock.randomlySelectedEnd, currentTileHolder, "EndBlock ");
               }
           }
           //  patternBlock.randomlySelectedEnd;
           //  blocksToReplace.Add(randomlySelectedStart);
           // blocksToReplace.Add(randomlySelectedEnd);

       });
    }

    private void SpawnNewBlock(BlockObjectWithRandom patternBlock, BlockObjectWithRandom replaceableBlock, titleHolderObject currentTileHolder, String newBlockName = "Pattern Block ")
    {
        var savedLocation = replaceableBlock.currentLocation;
        Destroy(replaceableBlock.prefab);
        var newBlock = Instantiate(patternBlock.prefab, currentTileHolder.tileHolder.transform);
        newBlock.transform.localPosition = savedLocation;
        newBlock.name = newBlockName + savedLocation;
        newBlocks.Add(newBlock);
    }
}
