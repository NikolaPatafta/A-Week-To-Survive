using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAndBowScript : MonoBehaviour
{
    private Rigidbody mybody;

    public float speed = 30f;

    public float deactivate_timer = 3f;

    public float damage = 15f;

    void Awake()
    {
       mybody = GetComponent<Rigidbody>(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeactivateGameObject", deactivate_timer);
    }

    public void Launch(Camera mainCamera)
    {
        mybody.velocity = mainCamera.transform.forward * speed;

        transform.LookAt(transform.position + mybody.velocity);
    }

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider target)
    {
        //nakon dodira s gameobjektom dekativiramo game object 
    }
}
