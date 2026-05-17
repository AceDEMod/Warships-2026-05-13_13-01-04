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

    public void playerAttack(int row, int col)
    {
        Debug.Log("Player attacking (" + row + ", " + col + ")");
        if (!isPlayerTurn || gameOver)
        {
            Debug.Log("Can't attack - not your turn or game over");
            return;
        }
        if (botGrid.cells[row, col].isHit)
        {
            Debug.Log("Cell already hit!");
            return;
        }

        botGrid.cells[row, col].onClick();

        Ship hitShipLocation = botFleet.getShip(row, col);

        if (hitShipLocation != null)
        {
            if (!hitShipLocation.isShipSunk())
            {
                hitShipLocation.takeDamage();
            }
            Debug.Log("Hit!");

            if (botFleet.checkFleetStatus())
            {
                gameOver = true;
                Debug.Log("Player wins!");
                return;
            }
        }
        else
        {
            Debug.Log("Miss!");
        }

        isPlayerTurn = false;
        botAttack();
    }

    public void botAttack()
    {
        Debug.Log("Bot attacking...");
        Cell target = findRandomCell(playerGrid);
        if (target == null)
        {
            Debug.Log("No valid target found");
            return;
        }

        int row = target.row;
        int col = target.col;

        bool hitShip = target.onClick();
        if (hitShip)
        {
            Debug.Log("Bot hit a ship at (" + row + ", " + col + ")");
            Ship hitShipLocation = playerFleet.getShip(row, col);

            if (hitShipLocation != null && !hitShipLocation.isShipSunk())
            {
                hitShipLocation.takeDamage();
            }

            if (playerFleet.checkFleetStatus())
            {
                gameOver = true;
                Debug.Log("Bot wins!");
                return;
            }
        }
        else
        {
            Debug.Log("Bot missed at (" + row + ", " + col + ")");
        }
        isPlayerTurn = true;
    }

    public Cell findRandomCell(Grid grid)
    {
        Cell cell = null;
        int maxAttempts = 300;

        while ((cell == null || cell.isHit) && maxAttempts > 0)
        {
            int randRow = Random.Range(0, 10);
            int randCol = Random.Range(0, 10);
            cell = grid.cells[randRow, randCol];
            maxAttempts--;
        }
        return cell;
    }

    void Start()
    {
        Debug.Log("Game starting...");
        shipPlacement = new ShipPlacement();
        Debug.Log("Placing player ships...");
        shipPlacement.placeShipsRandom(playerGrid, playerFleet);
        foreach (Ship ship in playerFleet.ships) { ship.markOccupiedCells(); }
        Debug.Log("Placing bot ships...");
        shipPlacement.placeShipsRandom(botGrid, botFleet);
        Debug.Log("Game started! Player's turn.");
    }

    void Update()
    {

    }
}