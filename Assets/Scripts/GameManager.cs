using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] shipPrefabs;

    private ShipPlacement shipPlacement;

    [SerializeField] private Grid playerGrid;
    [SerializeField] private Grid botGrid;
    [SerializeField] private Fleet playerFleet;
    [SerializeField] private Fleet botFleet;

    public bool isPlayerTurn = true;
    public bool gameOver = false;
    public float botAttackTimer = 0f;
    public float botAttackDelay = 2f;
    public string winner = "";

    void Start()
    {
        Debug.Log("Game starting...");
        shipPlacement = new ShipPlacement();
        shipPlacement.SetShipPrefabs(shipPrefabs);

        Debug.Log("Placing player ships...");
        // Passes the grids directly. The logic now snaps right to the cell objects.
        shipPlacement.placeShipsRandom(playerGrid, playerFleet, false);
        foreach (Ship ship in playerFleet.ships) { ship.markOccupiedCells(); }

        Debug.Log("Placing bot ships...");
        shipPlacement.placeShipsRandom(botGrid, botFleet, true);
    
        Debug.Log("Game started! Player's turn.");
    }

    void Update()
    {
        if (botAttackTimer > 0)
        {
            botAttackTimer -= Time.deltaTime;
            if (botAttackTimer <= 0)
            {
                botAttack();
            }
        }
    }

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
            botGrid.cells[row, col].rend.material.color = botGrid.cells[row, col].hitColor;
            if (!hitShipLocation.isShipSunk())
            {
                hitShipLocation.takeDamage();
            }
            Debug.Log("Hit!");

            if (botFleet.checkFleetStatus())
            {
                gameOver = true;
                winner = "player";
                Debug.Log("Player wins!");
                return;
            }
        }
        else
        {
            Debug.Log("Miss!");
        }
        botAttackTimer = botAttackDelay;
        isPlayerTurn = false;
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
                target.rend.material.color = target.hitColor;
                hitShipLocation.takeDamage();
            }

            if (playerFleet.checkFleetStatus())
            {
                gameOver = true;
                winner = "bot";
                Debug.Log("Bot wins!");
                return;
            }
        }
        else
        {
            target.rend.material.color = target.missColor;
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

    public void RestartGame()
    {
        Debug.Log("Restarting game...");
        isPlayerTurn = true;
        gameOver = false;
        winner = "";
        botAttackTimer = 0f;

        ResetGrid(playerGrid);
        ResetGrid(botGrid);

        playerFleet.ships.Clear();
        botFleet.ships.Clear();

        // Updated clean references for restarting the match state
        shipPlacement.placeShipsRandom(playerGrid, playerFleet, false);
        foreach (Ship ship in playerFleet.ships)
        {
            ship.calculateOccupiedCells();
            ship.markOccupiedCells();
        }

        shipPlacement.placeShipsRandom(botGrid, botFleet, true);
        foreach (Ship ship in botFleet.ships)
        {
            ship.calculateOccupiedCells();
        }

        Debug.Log("Game restarted! Player's turn.");
    }

    private void ResetGrid(Grid grid)
    {
        for (int row = 0; row < 10; row++)
        {
            for (int col = 0; col < 10; col++)
            {
                grid.cells[row, col].ResetCell();
            }
        }
    }
}