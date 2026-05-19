using UnityEngine;

public class Cell : MonoBehaviour
{
    [Header("Cell Position")]
    public int row;
    public int col;

    [Header("Cell State")]
    public bool hasShip = false;
    public bool isHit = false;

    [Header("Colors")]
    public Color defaultColor = Color.white;
    public Color hitColor = Color.red;
    public Color missColor = Color.cyan;
    public Color shipColor = Color.green;


    public Renderer rend;

    void Awake() {
        rend = GetComponent<Renderer>();
        rend.material.color = defaultColor;
    }

    public bool onClick () {
        if (isHit) return false;
        isHit = true;

        if (hasShip){
            rend.material.color = hitColor;
            return true;
        }else{
            rend.material.color = missColor;
            return false;
        }
    }

    public void PlaceShip()
    {
        hasShip = true;
        rend.material.color = shipColor;
    }

    public void ResetCell()
    {
        hasShip = false;
        isHit = false;
        rend.material.color = defaultColor;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
