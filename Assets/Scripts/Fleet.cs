using UnityEngine;
using System.Collections.Generic;

public class Fleet : MonoBehaviour
{
    public List<Ship> ships = new List<Ship>();

    public void addShip(Ship ship)
    {
        ships.Add(ship);
    }

    public Ship getShip(int row, int col)
    {
        foreach (Ship ship in ships)
        {
            if (ship.isCellOccupied(row, col))
            {
                return ship;
            }
        }
        return null;
    }

    public bool takeHit(int row, int col) {
        if (getShip(row, col) != null) { 
                getShip(row, col).takeDamage();
                return true;
            }
        return false;
    }
    public bool checkFleetStatus()
    {
        foreach (Ship ship in ships)
        {
            if (!ship.isShipSunk())
            {
                return false;
            }
        }
        return true;
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
