# BasicUnityProceduralTileGen
Very basic unity procedural title gen 


Insurctions , clone the repo and open up BreakieWall in the Assets folder. I wanted to see if I could make a breakable wall effect, but simply 
switch the'Tower' prefab refrenced  in the TileGen object to a different block. 
Advancedlab is where I'm doing most of my work right now. So this is a bruteforce way of of doing it, I'm not combining meshes or anything like that. In general I'd suggest against going above 100x100, the system still works , but Unity's editor can really start to slow down( it crashed hard for me at 1000x1000 . 

Right now I'm looking to make a small tower defense game with this tool, but I could see it being a good foundation for a ton of small projects . 

For now this will ONLY generate tiles in 2 dimensions, as the project progresses I plan to add more options. 
Also , this ONLY will run at runtime, but I'm working on a 'bake' scene option. 

Relies upon 
https://github.com/Unity-Technologies/NavMeshComponents - May remove and go with a more basic path finding solution 
Which has been released under the MIT License, I REALLY want to show off using nav agenets to move around generated levels , but that's not really possible without adding the Unity scripts. 

Easy Buttons, another Mit project 
https://github.com/madsbangh/EasyButtons


