Preeti Maan
pmaan6@gatech.edu
pmaan6
-----------------------------------------------------------------------------
-----------------------------------------------------------------------------
"Wrecked"

Readme file explaining how to play and what parts of the level to observe
technology requirements

-----------------------------------------------------------------------------
Guide to play:

Start by Clicking the "Start Game" button in the menu.

Controls:
Movements: WASD
Jump: Space
Crouch : C (Toggle)
Pull gun : X  (Toggle)
Shoot : F
OpenDoors : E

In Game Menu : Esc

Rules
1. Level 1 : Find and pick up buzzcard - Yellow card objects
2. Level 2 : Defeat boss zombie to win the game
3. You get three trials to finish the game or it restarts.
4. If the player falls through floor it dies.
5. A dormant or patrolling zombie can detect a walking player and chase and attack the player and cause player to lose HP and dies.
6. Defeating zombies will reward player with XP and HP collectibles.
7. XP for level up is not yet implemented.
8. There are two types of zombies:
    Simple zombies - Chase and attack player, cause low HP damage. Cannot detect if player is crouching.
    Patrol zombies - Wander, chase and attack player at high speed. Can detect if player even if crouching.
    Boss zombie - Chase and attack player at high speed, cause high HP damage. Can detect if player even if crouching.
9. Pick objects by walking over them.
    Pickables objects include:
    Cola - Gives a speed boost
    Buzzcard
    HPcollectable
    Keys
10. Green doors can be open with a picked key.


Testing:
Start scene - Menu

-----------------------------------------------------------------------------

Technology requirements met:

1. Game uses player charcter controlled by the user input.
2. Zombies use a state machine and nav mesh to patrol the area and chase player.
3. Bullets from player gun use rigidbody physics and detect collision with the zombies.
4. Game has different obstacles but feasible game play that is engaging and responsive to user input.
The zombies make scary sounds and screams when moving or attacking player and player makes dialogs on object interactions and on getting hurt. The user gets success and failure messages on screen.
5. The game has a main menu to start the game or quit. Inside the game levels Esc key brings up the In-game-menu to restart the game or quit it. The player gets three chances to complete the game or the game restarts. When the player dies or the level is updated the player gets a message and can proceed by pressing Enter key.
6. At any level the player can choose to crouch and be undetectable to the simple zombies but crouching will not help it to hide from the patrol and boss zombies so the player has to choose a wise strategy. If the player dies thw game restarts the level with half of its prior XP.



