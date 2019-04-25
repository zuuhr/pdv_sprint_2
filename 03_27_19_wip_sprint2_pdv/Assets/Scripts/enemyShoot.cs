using System.Collections.Generic;
using UnityEngine;

public class enemyShoot : MonoBehaviour
{
    public List<GameObject> army = new List<GameObject>();
    public float shootingRespawnTime; //gap time between bullet firing
    float timeLimit; //gap time in real time units 
    public GameObject bullet; //assigned bullet
    void Start()
    {
        timeLimit = Time.time + shootingRespawnTime; //set timer
    }

    void Update()
    {
        float timeLeft = timeLimit - Time.time;
        if (timeLeft < 0) //when time is up 
        {
            shoot(); 
            timeLimit = Time.time + shootingRespawnTime; //set new timer
        }
    }

    void shoot()
    {
        int randomAlien = (int) Random.Range(0, army.Count); //select a random alien from the game
        GameObject chosenAlien = army[randomAlien];
        Instantiate(bullet, chosenAlien.transform.position, chosenAlien.transform.rotation); //Spawns a bullet from that alien
    }
}
