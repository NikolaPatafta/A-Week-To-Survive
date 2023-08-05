using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int scoreCounter;

    public Vector3 playerPosition;

    public GameData()
    {
        this.scoreCounter = 0;
        playerPosition = Vector3.zero;
    }

}
