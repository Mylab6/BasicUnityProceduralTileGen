# BasicUnityProceduralTileGen
Very basic unity procedural title gen 

# Updated for 2020.2 !


Now uses:
"com.unity.ai.navigation.components": "https://github.com/Unity-Technologies/NavMeshComponents.git#package",

If the project breaks try going to package manager , then in project. Click NavMesh Components , and then import the samples 
To check out some basic nav in procedurally generated maps, use the 'Nav_Fixed' scene . 
Advancedlab is where I'm doing most of my work right now. So this is a bruteforce way of of doing it, I'm not combining meshes or anything like that. In general I'd suggest against going above 100x100, the system still works , but Unity's editor can really start to slow down( it crashed hard for me at 1000x1000 . 

Right now I'm looking to make a small tower defense game with this tool, but I could see it being a good foundation for a ton of small projects . 

For now this will ONLY generate tiles in 2 dimensions, as the project progresses I plan to add more options. 
Also , this ONLY will run at runtime, but I'm working on a 'bake' scene option. 




MIT License , but I would love you to drop me a message if you make anything cool ! 

