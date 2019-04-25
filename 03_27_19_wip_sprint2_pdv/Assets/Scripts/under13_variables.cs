using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class under13_variables : MonoBehaviour
{
    public GameObject alien;
    private bool alienAppearance;
    private float timer;
    //Every second an alien will appear
    void Start() => timer = Time.time + 1; //set timer to spawn aliens

    void Update()
    {
        if (!alienAppearance)
        {
            float timeLeft = timer - Time.time; //duration 
            if (timeLeft < 0) alienAppearance = true;//time's up
        }
        else if (alienAppearance)
        {
            Debug.Log("Alien appearance"); //So we can check
            Instantiate(alien, new Vector3(Random.Range(-12, 6), 0f, 16f), this.transform.rotation); 
            alienAppearance = false;
            timer = Time.time + 1; //set timer to spawn aliens
        }
    }
}