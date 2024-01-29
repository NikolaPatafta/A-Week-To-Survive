using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public int scoreCounter;
    public int dayCounter;
    public float playerHealth;
    public float gameTime;
    public int dayInGame;

    public Vector3 playerPosition;

    public GameData()
    {
        this.scoreCounter = 11;
        this.dayCounter = 31;
        playerPosition = new Vector3(720, 0, 770);
        this.playerHealth = 88;
        this.gameTime = 75f;
        this.dayInGame = 31;
    }

}
