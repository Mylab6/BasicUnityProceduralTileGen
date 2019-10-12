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
    public GameObject reffNavMesh;


    //public NavMeshSurface[] surfaces;
    public Transform[] objectsToRotate;
    public override void PostGenActions()

    {


        try
        {
            createBlocksPattern();
            // BuildNavMesh();
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



           //  patternBlock.randomlySelectedEnd;
           //  blocksToReplace.Add(randomlySelectedStart);
           // blocksToReplace.Add(randomlySelectedEnd);

       });

        if (allowForcedEndAndStart)
        {
            var locPatternBlock = paternBlocks[0];

            if (locPatternBlock.forceStartBlock != null)
            {

                var startBlockForceArray = newBlocks.FindAll(x => x.transform.localPosition == locPatternBlock.randomlySelectedStart.currentLocation);
                startBlockForceArray.ForEach(endForce => Destroy(endForce));
                SpawnNewBlock(locPatternBlock.forceStartBlock, locPatternBlock.randomlySelectedStart, currentTileHolder, "Start Block");
            }
            if (locPatternBlock.forceEndBlock != null)
            {
                var endBlockForceArray = newBlocks.FindAll(x => x.transform.localPosition == locPatternBlock.randomlySelectedEnd.currentLocation);
                endBlockForceArray.ForEach(endForce => Destroy(endForce));
                SpawnNewBlock(locPatternBlock.forceEndBlock, locPatternBlock.randomlySelectedEnd, currentTileHolder, "EndBlock ");
            }

        }
    }
    public void customOnValidate()

    {
        return;
        if (reffNavMesh)
        {
            DestroyImmediate(reffNavMesh);

        }
        reffNavMesh = GameObject.CreatePrimitive(PrimitiveType.Cube);
        reffNavMesh.transform.position = transform.position;
        reffNavMesh.transform.localScale = new Vector3(GridWidthF, 0, GridLengthF);
        reffNavMesh.transform.parent = transform;
        //      public int GridWidthF = 4;
        //public int GridLengthF = 4;
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
