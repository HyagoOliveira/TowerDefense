# Tower Defense

Tower Defense game made on Unity 2020.3.48 LTS

![Tower Defense Thumbnail](/Wiki/Thumbnail.png "Tower Defense")

Click [here](https://nostgames.itch.io/tower-defense) to play the last build directly on your browser.

## Game Rules

- Enemies come in infinite waves and try to reach the goal line.
- If an enemy reaches the goal line, the player loses Health Points according with the enemy type.
- Enemies don't follow a predefined path.
- For each enemy killed the player receives a currency reward and score points.
- To destroy enemies, player can buy new defenses and upgrade them with currency at any time.
- Towers can be placed anywhere, unless it completely blocks the enemies movement.
- The game ends when the player runs out of Health Points.
- The player High Score is saved locally and showed at GameOver.

## How to Play

- To place a dense tower, at the left corner of the screen, click on the Ballista, Cannon or Shocker button.
- Choose a place on the map and fix the tower.
- Now you can start a new wave of enemies by click on the Next Wave Button (over the towers buttons).
- At any moment, click on any tower to open the Upgrade Popup and upgrade this tower if you have enough currency.
- Keep doing this process until the enemy defeat you :)

## Technical Details

The game economy and layout details were based on the [Endless Siege](https://www.crazygames.com/game/endless-siege) game.

### SOLID code

All scripts are following the [SOLID principles](https://en.wikipedia.org/wiki/SOLID), where the Single Responsibility is one of the most important to develop game components. 
Each component does one thing alone and this reduce its complexity, producing maintainable code over the years.

Also classes were uncoupled to facilitate unit test. One such candidate is the class [CurrencyCalculator](/Assets/Scripts/Gameplay/CurrencyCalculator.cs). Unit tests can be easily created for this class since it does not depends on the Unity API.

### Using ScriptableObjects as Singletons

All settings for a match are centralized at the Match Settings ScriptableObject.

![Match Settings](/Wiki/MatchSettings.png "Match Settings")

There you can change the initial player health and currency, the enemies waves and towers.

Also, by using a ScriptableObjects, other assets (even scripts) can easily access it and consume its events, functions or properties, working like a Singleton pattern. 

Here is an example of a MonoBehaviour component taking action when the Scores increases:

```csharp
using TowerDefense.Managers;
   
public sealed class SomeComponent : MonoBehaviour
{
    [SerializeField] private MatchSettings settings;

    private void OnEnable() => settings.Score.OnIncreased += HandleScoreIncreased;
    private void OnDisable() => settings.Score.OnIncreased -= HandleScoreIncreased;

    private void HandleScoreIncreased() { // do things here }
}

```

### Enemy Pathfindig

 Enemies are using the Unity AI Navemesh system to find and traverse the map.

 When placing towers, the game checks whether that tower will invalidate the enemies path and show a visual feedback to the player, not allowing him to place the tower on that position.

 ![Checking Invalid Path one](/Wiki/CheckInvalidPath_00.gif "CheckInvalidPath 00")

 The checking also take account when other towers are blocking the path: 

 ![Checking Invalid Path two](/Wiki/CheckInvalidPath_01.gif "CheckInvalidPath 01")