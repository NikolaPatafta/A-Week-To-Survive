using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : ScriptableObject
{
    public string name;
    public string description;
    public Sprite icon;

    public virtual void Use()
    {
        Debug.Log(name + "Was used.");
    }



}
