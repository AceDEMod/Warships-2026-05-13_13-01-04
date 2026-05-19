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

                    if (!isBot)
                    {
                        // 1. Get base cell position (Each cube step is 0.6 units)
                        Vector3 startCellPosition = grid.cells[randRow, randCol].transform.position;
                        Vector3 spawnPosition = new Vector3(startCellPosition.x, startCellPosition.y, startCellPosition.z);

                        // 2. Apply custom offsets based on your exact feedback
                        float xOffset = 0f;
                        float zOffset = 0f;
                        float yOffset = 0.1f; // Default height slightly above grid

                        if (size == 5)
                        {
                            yOffset = 0.4f; 
                            if (isHorizontal) 
                            {
                                xOffset = 1.4f; 
                            }
                            else 
                            {
                                zOffset = -1.8f; 
                            }
                        }
                        else if (size == 4)
                        {
                            yOffset = 0.4f; 
                            if (isHorizontal) 
                            {
                                xOffset = 1.1f; 
                                zOffset = 0.2f; 
                            }
                            else 
                            {
                                zOffset = -1.2f; 
                            }
                        }
                        else if (size == 3)
                        {
                            yOffset = 0.4f; 
                            if (isHorizontal) 
                            {
                                xOffset = 0.8f; 
                            }
                            else 
                            {
                                zOffset = -0.9f; 
                            }
                        }
                        else if (size == 2)
                        {
                           
                            yOffset = 0.4f; 
                            if (isHorizontal) xOffset = 0.3f;
                            else zOffset = -0.5f;
                        }

                        spawnPosition.x += xOffset;
                        spawnPosition.y += yOffset;
                        spawnPosition.z += zOffset;

                        Quaternion spawnRotation = isHorizontal ? Quaternion.Euler(0, 90, 0) : Quaternion.identity;
                        GameObject prefabToSpawn = shipPrefabs[size - 2]; 

                        Object.Instantiate(prefabToSpawn, spawnPosition, spawnRotation, grid.cells[randRow, randCol].transform);
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