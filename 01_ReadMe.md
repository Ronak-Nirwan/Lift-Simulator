Hey Thanks for giving me the opportunity to work on this Simulation task I Really enjoyed working on it !!!



Project uses Unity 6.0 (6000.0.69f1) editor version 



***Important Note :- the aspect ratio should 9:16 with camera size set to 20***



The simulation is designed and programmed based on the given requirements 

The lift starts at random floors and stops at 1st floor till any lift is called from any floor (Using the red UI Button)

the nearest lift responds to call and a debug.log is generated each time a lift receives a call



The scripts are divided based on the role for each script following the SOLID principle when needed
and Naming standards are used for variable and function naming everywhere


public variable and functions - PascalCase

private variable - \_camelCase



The scripts include :-

DoorController - for the behaviour of the doors to open and close when lift arrives at the floor

FloorCallButton - for calling the lift at any floor (Red UI Button Near the 1st lift)

FloorSystem - To handle the location of floors and managing the door components for each lift

LiftController - to control each lift individually, and have all the actions the lift performs

LiftDirection - to manage the states and direction of the lift during movement 

LiftManager - to manage each lift and send to the requested floor on call (sends the nearest lift by checking it's distance and direction)

LiftPanelButton - to extend in future to select the floor number in which player wants to go  

all the in-game components have prefab created for them to use easily in the future

Thanks for the opportunity 
I'll be waiting for a reply and feedback,
Best Regards !!

