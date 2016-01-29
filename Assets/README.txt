Procedural Level Generator Project:
To run:
Open the project in Unity and be sure that the scene PGDungeon is open.

Camera Controls:
WASD  => Move camera Up, Right, Down, Left
Q     => Zoom Out
E     => Zoom In

Level generation controls:
Z    => Creates Grid
X    => Creates Rooms
C    => Connects Rooms using corridors
V    => Cleans up by merging overlapping rooms. (I chose to overlap rooms in order to create different room shapes).

About the Level generation: 

All level generation parameters are controlled by the LevelGenerator object on the Editor UI. More specifically the Tile Generator Script on that Object. All these must be set before level generation starts.

Grid Width and Grid Height => Set the dimensions of the grid in UNITS. Unit is the basic distance mesurement in Unity.

Tiles per Unit: Set the size of one Tile. This determines the density of the grid.

RNG Seed: number from 000 to 999 used to position the rooms and determine their sizes. Room sizes are inversely proportional to total number of rooms.

Total Rooms: The number of rooms to be generated on the grid

Corridor Type: How the rooms are connected to each other. Rooms connect themselves usually from lower to higher on the grid, but that behavior varies.

* All to All: Every single room connects to every single room.

* Max 1 per room: Each room will connect to only one other room. That means rooms will have a corridor arriving to it  and aonther leaving from it.

* Max 2 per room: Each room will connect to 2 other rooms. That means rooms will have between 2 to 4 corridors connecting to it.

* Max 3 per room: Each room will connect to 3 other rooms. That means rooms will have between 3 to 6 corridors connecting to it.

The many ways to break the generator:

* Since the level generator is not optimized, it can generate too many objects and essentially break Unity. Try to avoid big and high density grids. Usually grids with 10.000 Tiles start lagging Unity and more will make it crash.
"Number of tiles in the grid = GridHeight * GridWidth * TilesPerUnit^2"

*Rooms are set to a minimal 3x3 size.

* Demanding too many rooms in a small grid will cause an index out of range error. A good rule of thumb is make total amount of rooms lower than 1/10 of Width*Height (i.e a 10x10 grids => less than 10 rooms are usually fine).

* One room with invalid coordinates is usually generated and corridors will try to reach it. Not square grids tend to increase the number of invalid rooms.
As I am writing this I figured out a way to possibly stop this from happening (make the createRoom funtion return a bool when it fails; add an out Room Variable to it; and only add a Room to the list when this function returns true) but i will implement this later in favor of capstone.

Feel free to e-mail me with any questions.