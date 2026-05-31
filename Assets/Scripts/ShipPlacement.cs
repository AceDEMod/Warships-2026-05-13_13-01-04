using UnityEngine;

public class ShipPlacement
{
    private GameObject[] shipPrefabs;

    public void SetShipPrefabs(GameObject[] prefabs)
    {
        shipPrefabs = prefabs;
    }

    int[] shipSizes = new int[] { 5, 4, 3, 3, 2 };

    public void placeShipsRandom(Grid grid, Fleet fleet, bool isBot)
    {
        foreach (int size in shipSizes)
        {
            bool placed = false;
            while (!placed)
            {
                int randRow = Random.Range(0, 10);
                int randCol = Random.Range(0, 10);
                bool isHorizontal = Random.value > 0.5f;
                if (canPlaceShip(grid, fleet, randRow, randCol, size, isHorizontal))
                {
                    Ship newShip = new Ship(size, randRow, randCol, isHorizontal, grid);
                    newShip.calculateOccupiedCells();
                    fleet.addShip(newShip);

                    Debug.Log(
                        (isBot ? "[BOT]" : "[PLAYER]") +
                        " Ship placed -> size: " + size +
                        " start: (" + randRow + "," + randCol + ")" +
                        " horizontal: " + isHorizontal
                    );

                    if (!isBot)
                    {
                        Cell startCell = grid.cells[randRow, randCol];

                        float cellSpacing = Vector3.Distance(
                            grid.cells[0, 0].transform.position,
                            grid.cells[0, 1].transform.position
                        );

                        Vector3 direction;

                        if (isHorizontal)
                            direction = startCell.transform.right;
                        else
                            direction = startCell.transform.forward;

                        Vector3 offset = direction * ((size - 1) * cellSpacing * 0.5f);

                        Vector3 spawnPosition =
                            startCell.transform.position +
                            offset +
                            Vector3.up * 0.01f;

                        Quaternion spawnRotation =
                            Quaternion.LookRotation(direction, Vector3.up);

                        GameObject prefabToSpawn = shipPrefabs[size - 2];

                        GameObject ship = Object.Instantiate(
                            prefabToSpawn,
                            spawnPosition,
                            spawnRotation,
                            grid.transform 
                        );
                    }

                    Debug.Log("==== FLEET SUMMARY ====");
                    foreach (Ship ship in fleet.GetShips())
                    {
                        Debug.Log(
                            (isBot ? "[BOT]" : "[PLAYER]") +
                            " Ship size: " + ship.size +
                            " cells: " + ship.occupiedCells.Count
                        );
                    }
                
                    placed = true;
                }
            }
        }
    }

    public bool canPlaceShip(Grid grid, Fleet fleet, int row, int col, int size, bool isHorizontal)
    {
        if (isHorizontal)  {
            if (col + size > 10) {return false;}
        }
        else {
            if (row + size > 10) { return false;}
        }

        for (int i = 0; i < size; i++) {
            int checkRow = isHorizontal ? row : row + i;
            int checkCol = isHorizontal ? col + i : col;
            if (checkRow >= 10 || checkCol >= 10) { return false;}
            if (fleet.getShip(checkRow, checkCol) != null) { return false;}
        }
        return true;
    }
}