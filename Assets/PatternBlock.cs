using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public enum EdgeType
{
    Start,
    End
};
[Serializable]
public enum EdgeVector
{
    X,
    Z,
    X_And_Z,
    X_Or_Z
}
[Serializable]
public class ReplaceRuleLogic
{

    public bool MustAlwaysConnect;
    public EdgeVector startingVector;
    public EdgeVector endVector;

    public bool MustEndOnX;
    public bool MustEndOnZ;
}
[Serializable]
public class PatternBlock : BlockObjectWithRandom
{


    [Tooltip("If you need to start with a certain block ")]
    public BlockObjectWithRandom forceStartBlock;
    [Tooltip("If you need to end with a certain block ")]

    public BlockObjectWithRandom forceEndBlock;


    public ReplaceRuleLogic replaceRules;
    public List<BlockObjectWithRandom> possibleStarts;
    public List<BlockObjectWithRandom> possibleEnds;

    public BlockObjectWithRandom randomlySelectedStart;
    public BlockObjectWithRandom randomlySelectedEnd;
    public float edgeX;
    public float edgeZ;
    // doing this just to make the inspector easier to use 
    private List<BlockObjectWithRandom> cachedLocalBlocks;
    public List<BlockObjectWithRandom> blocksToReplace;
    public List<BlockObjectWithRandom> GeneratePattern(List<BlockObjectWithRandom> blockObs, BlockObjectWithRandom startBlock = null, BlockObjectWithRandom endBlock = null)
    {
        cachedLocalBlocks = blockObs;
        edgeX = blockObs.Max(x => x.currentLocation.x);
        edgeZ = blockObs.Max(x => x.currentLocation.z);

        SelectEdgeBlock(blockObs, EdgeType.Start);
        SelectEdgeBlock(blockObs, EdgeType.End);
        addFillerBlocks(randomlySelectedStart);
        // UnityEngine.Random.Range(


        // blocksToReplace.Add(randomlySelectedStart);
        //blocksToReplace.Add(randomlySelectedEnd);
        return blocksToReplace;
    }
    private void addFillerBlocks(BlockObjectWithRandom lastBlock, bool forceZ = true)
    {

        if (lastBlock.currentLocation.x == edgeX && lastBlock.currentLocation.z == edgeZ)
        {
            //return;
        }
        Vector3 newLocation = new Vector3();

        //  if (UnityEngine.Random.value < 0.5f)

        if ((UnityEngine.Random.value < 0.5f) && lastBlock.currentLocation.x != edgeX)
        {
            forceZ = false;
            newLocation = lastBlock.currentLocation + new Vector3(1, 0, 0);
            // incerment on x 
        }
        else if (lastBlock.currentLocation.z != edgeZ)
        //else

        {
            forceZ = true;
            newLocation = lastBlock.currentLocation + new Vector3(0, 0, 1);

            // incerment on z 
        }

        var blockToReplace = cachedLocalBlocks.Find(block => block.currentLocation == newLocation);
        blocksToReplace.Add(blockToReplace);
        if (randomlySelectedEnd == blockToReplace)
        {
            return;
        }

        addFillerBlocks(blockToReplace, forceZ);

    }
    private void SelectEdgeBlock(List<BlockObjectWithRandom> blockObs, EdgeType edgeType)
    {
        List<BlockObjectWithRandom> possibleBlocks = new List<BlockObjectWithRandom>();
        Vector3 filterVectorX_Z = new Vector3(edgeX, 0, edgeZ);
        var filterVector = replaceRules.endVector;
        if (edgeType == EdgeType.Start)
        {
            filterVector = replaceRules.startingVector;
            filterVectorX_Z = new Vector3(0, 0, 0);
        }

        if (filterVector == EdgeVector.X)
        {
            possibleBlocks = blockObs.FindAll(block =>
              block.currentLocation.x == filterVectorX_Z.x
             );

        }
        if (filterVector == EdgeVector.Z)
        {

            possibleBlocks = blockObs.FindAll(block =>
                      block.currentLocation.z == filterVectorX_Z.z
                          );
        }
        if (filterVector == EdgeVector.X_And_Z)
        {
            possibleBlocks = blockObs.FindAll(block => block.currentLocation.x == filterVectorX_Z.x && block.currentLocation.z == filterVectorX_Z.z);


        }
        if (filterVector == EdgeVector.X_Or_Z)
        {

            possibleBlocks = blockObs.FindAll(block => block.currentLocation.x == filterVectorX_Z.x || block.currentLocation.z == filterVectorX_Z.z);

        }


        if (edgeType == EdgeType.Start)
        {    //   filterVectorX_Z = new Vector3(0, 0, 0);
            possibleStarts = possibleBlocks;
            randomlySelectedStart = possibleStarts.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        }
        else
        {
            possibleEnds = possibleBlocks;
            randomlySelectedEnd = possibleEnds.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

        }
    }
}
