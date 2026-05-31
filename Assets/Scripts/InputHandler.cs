using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameManager gameManager;
    public Grid botGrid;

void Awake()
{
    Debug.Log("INPUTHANDLER AWAKE");
}


    // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
{

    Debug.Log("INPUTHANDLER START");
    if (gameManager == null)
        gameManager = FindFirstObjectByType<GameManager>();

    if (botGrid == null)
        botGrid = FindFirstObjectByType<Grid>();

    if (gameManager == null) Debug.LogError("GameManager not found in scene!", this);
    if (botGrid == null) Debug.LogError("BotGrid not found in scene!", this);
}

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("====================== InputHandler Update called =====================");

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Screen touched");

            if (botGrid == null)
            {
                botGrid = FindFirstObjectByType<Grid>();

                if (botGrid == null)
                {
                    Debug.LogError("botGrid is STILL NULL");
                    return;
                }

                Debug.Log("botGrid found dynamically");


                Grid[] grids = FindObjectsByType<Grid>(
                    FindObjectsSortMode.None);

                Debug.Log("Grid count: " + grids.Length);

                foreach (Grid g in grids)
                {
                    Debug.Log("Grid: " + g.name);
                }
            }

            Debug.Log("botGrid exists");
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Cell cell = hit.collider.GetComponent<Cell>();

                if (cell == null)
                {
                    cell = hit.collider.GetComponentInParent<Cell>();
                }

                Debug.Log("Collider hit: " + hit.collider.name);

                if (cell != null)
                {
                    Debug.Log("Found Cell: " + cell.row + "," + cell.col);
                }
                else
                {
                    Debug.Log("No Cell component found");
                }
                if (cell != null && botGrid.cells[cell.row, cell.col] == cell)
                {
                    Debug.Log("======================== PLAYER ATTACK ========================");
                    if (gameManager.isPlayerTurn && !gameManager.gameOver)
                    {
                        gameManager.playerAttack(cell.row, cell.col);
                        Debug.Log("Cell clicked! ");
                    }
                }

                Debug.Log("Hit object: " + hit.collider.name);

                if (cell != null)
                {
                    Debug.Log("Cell found: " + cell.row + "," + cell.col);
                }
            }
        }
    }
}
