using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
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

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 360 / dayLengthMinutes / 60;
        midday = dayLengthMinutes * 60/2;
        currentTime = 7.50f;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += 1 * Time.deltaTime;
        translateTime = (currentTime / (midday * 2));

        float t = translateTime * 24f;

        float hours = Mathf.Floor(t);

        string displayHours = hours.ToString();

        if(hours >= 24)
        {
            displayHours = (hours - 24).ToString();
        }

        t *= 60;
        float minutes = Mathf.Floor(t % 60);
        string displayMinutes = minutes.ToString();
        if(minutes < 10) 
        {
            displayMinutes = "0" + minutes.ToString();
        }

        if(currentTime >= midday * 2)
        {
            day++;
            currentTime = 0;
        }

        string displayTime = "Day: " + day + " Time: " + displayHours + ":" + displayMinutes;

        timeText.text = displayTime;

        //textMeshPro.text = displayTime;

        transform.Rotate(new Vector3(1, 0, 0) * rotationSpeed * Time.deltaTime);
    }

}
