using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class NavOptions
{
    public Boolean allowNav;
    public void addNav(List<BlockObjectWithRandom> childrenToAddNav)
    {
        if (allowNav)
        {
            childrenToAddNav.ForEach(childTransform =>
            {

                NavMeshAdder(childTransform.prefab);
            });

        }

    }

    public void NavMeshAdder(GameObject childTransform)
    {
        // NavMeshSurface nm = childTransform.AddComponent(typeof(NavMeshSurface)) as NavMeshSurface;
        //  NavMeshLink lk = childTransform.AddComponent(typeof(NavMeshLink)) as NavMeshLink;
        NavMeshSourceTag sourceTag = childTransform.AddComponent(typeof(NavMeshSourceTag)) as NavMeshSourceTag;
        //nm.buildHeightMesh = true;
        if (childTransform.transform.childCount != 0)
        {
            NavMeshObstacle obstacle = childTransform.AddComponent(typeof(NavMeshObstacle)) as NavMeshObstacle;
            // and for it's children 
            for (int i = 0; i < childTransform.transform.childCount; i++)
            {
                var childAgain = childTransform.transform.GetChild(i);
                NavMeshObstacle childObstacle = childAgain.gameObject.AddComponent(typeof(NavMeshObstacle)) as NavMeshObstacle;

                //   child.

            }

        }
        //nm.BuildNavMesh();
    }
}
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
    public NavOptions navOptions;
    public GameObject navMeshCubeChild;
    //public NavMeshSurface[] surfaces;
    public Transform[] objectsToRotate;
    public override void PostGenActions()

    {


        try
        {
            createBlocksPattern();
            navOptions.addNav(tileHolders[0].blocksInGame);   //.transform.GetChild )
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
    public void OnValidate()

    {

    }

    private void SpawnNewBlock(BlockObjectWithRandom patternBlock, BlockObjectWithRandom replaceableBlock, titleHolderObject currentTileHolder, String newBlockName = "Pattern Block ")
    {
        var savedLocation = replaceableBlock.currentLocation;
        Destroy(replaceableBlock.prefab);
        var newBlock = Instantiate(patternBlock.prefab, currentTileHolder.tileHolder.transform);
        newBlock.transform.localPosition = savedLocation;
        newBlock.name = newBlockName + savedLocation;
        newBlocks.Add(newBlock);
        if (navOptions.allowNav)
        {
            navOptions.NavMeshAdder(newBlock);

        }
    }

}
