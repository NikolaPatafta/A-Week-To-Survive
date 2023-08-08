using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public int scoreCounter;
    public int dayCounter;

    public Vector3 playerPosition;

    public GameData()
    {
        this.scoreCounter = 0;
        this.dayCounter = 0;
        playerPosition = Vector3.zero;
    }

}
