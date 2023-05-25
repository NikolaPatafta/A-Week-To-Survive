using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DayAndNightSystem : MonoBehaviour
{
    public float currentTime;
    public float dayLengthMinutes;
    public Text timeText;
    //public TextMeshProUGUI textMeshPro;
    
    private float rotationSpeed;
    private float midday;
    private float translateTime;
    public int day = 1;
    public float currentHours;
    public bool isDay = true;

    [SerializeField]
    private SpawnHordeZombies spawnHordeZombies;
    [SerializeField]
    private HealthScript healthScript;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 360 / dayLengthMinutes / 60;
        midday = dayLengthMinutes * 60/2;
        if(dayLengthMinutes == 5)
        {
            currentTime = 75f;
        }
        else if(dayLengthMinutes == 10)
        {
            currentTime = 150f;
        }
        else if(dayLengthMinutes == 0.5)
        {
            currentTime = 7.5f;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!healthScript.IsDead())
        {
            currentTime += Time.deltaTime;
            translateTime = (currentTime / (midday * 2));

            float t = translateTime * 24f;

            float hours = Mathf.Floor(t);
            currentHours = hours;

            string displayHours = hours.ToString();

            if (hours >= 24)
            {
                displayHours = (hours - 24).ToString();
            }

            t *= 60;
            float minutes = Mathf.Floor(t % 60);
            string displayMinutes = minutes.ToString();
            if (minutes < 10)
            {
                displayMinutes = "0" + minutes.ToString();
            }

            DaytoSpawnHorde();

            CheckDayTime();

            string displayTime = "Day: " + day + " Time: " + displayHours + ":" + displayMinutes;

            timeText.text = displayTime;

            transform.Rotate(new Vector3(1, 0, 0) * rotationSpeed * Time.deltaTime);

        }
    }


    private void CheckDayTime()
    {
        if (currentHours >= 6 && currentHours <= 19)
        {
            isDay = true;
        }
        else
        {
            isDay = false;
        }
    }

    public void DaytoSpawnHorde()
    {
        if (currentTime >= midday * 2)
        {
            day++;
            if (day == 2 || day == 7 || day == 14)
            {
                spawnHordeZombies.StartCoroutine("spawnHordeZombies");
            }
            currentTime = 0;
        }
    }
}
