using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateDoorHealth : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text doorHealthText;

    public void UpdateHealth(float health, float maxhealth)
    {
        float normalHealth = Mathf.Clamp01(health / maxhealth);
        image.fillAmount = normalHealth;
        doorHealthText.text = Mathf.RoundToInt(normalHealth * 100).ToString();
    }

}
