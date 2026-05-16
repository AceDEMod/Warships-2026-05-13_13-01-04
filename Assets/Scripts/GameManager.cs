using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Grid playerGrid;
    public Grid botGrid;
    public Fleet playerFleet;
    public Fleet botFleet;
    public ShipPlacement shipPlacement;

    public bool isPlayerTurn = true;
    public bool gameOver = false;

    public void playerAttack(int row, int col) {
        if (!isPlayerTurn || gameOver) {return;}
        if (botGrid.cells[row, col].isHit) {return;}

        bool hitShip = botGrid.cells[row, col].onClick();
        if (hitShip) {
            Ship hitShipLocation = botFleet.getShip(row, col);
            
            if(hitShipLocation != null && !hitShipLocation.isShipSunk()) { hitShipLocation.takeDamage(); }

            if (botFleet.checkFleetStatus())
            {
                gameOver = true;
                return;
                Debug.Log("Player wins!");
            }

        } else {
            Debug.Log("Miss!");
        }

        isPlayerTurn = false;
        botAttack();

    }

    public void botAttack() { }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shipPlacement = new ShipPlacement();
        shipPlacement.placeShipsRandom(playerGrid, playerFleet);
        shipPlacement.placeShipsRandom(botGrid, botFleet);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
