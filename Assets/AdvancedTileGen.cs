using System.Collections.Generic;

public class AdvancedTileGen : Basic3dTileGridGen
{
    public List<PatternBlock> paternBlocks;
    public int indexToCreatePatternOn = 0;

    public override void PostGenActions()

    {
        createBlocksPattern();
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
              var savedLocation = replaceableBlock.currentLocation;
              Destroy(replaceableBlock.prefab);
              var newBlock = Instantiate(patternBlock.prefab, currentTileHolder.tileHolder.transform);
              newBlock.transform.localPosition = savedLocation;
              newBlock.name = "Patern Block " + savedLocation;

          });

       });
    }
}
