using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class alien_movement : MonoBehaviour
{
    //movement
    int right = 1; //'1' moves towards the right '-1' to the left
    float advanceTime; //time to move to the next row
    float advanceTranslation; //Q to move
    bool advance = false;// if true the alien will start advancing to the next row
    public GameObject zone_map; //game area where the aliens can stay

    //color management
    private int color;
    Color colorValue;
    List<GameObject> army;
    float randomTime;
    bool randomize = false;

    void Start()
    {
        if (transform.parent.tag == "odd") right *= -1; //if the alien is an odd or even numbered row, it's initial movement changes
        color = 1; 
        army = GameObject.Find("army").GetComponent<enemyShoot>().army; //Get the list of aliens ingame
    }

    void Update()
    {
        if (advance) {
            advanceTranslation = -0.1f; //speed
            float timeLeft = advanceTime - Time.time; //duration 
            if (timeLeft < 0) //time's up
            {
                advance = false;
                advanceTranslation = 0.0f;
                //Debug.Log("YA PARO"); //just to check
            }
            
        }

        //default movement & PAUSE menu management
        if (Time.timeScale == 0) 
        {
            transform.Translate((new Vector3(0.0f, 0.0f, 0.0f))); //Stops
        } else if (Time.timeScale == 1)
        {
            transform.Translate((new Vector3(right * 0.05f, 0.0f, advanceTranslation))); //Resumes
        }

        if (randomize) //timer
        {
            float timeLeft = randomTime - Time.time;
            if (timeLeft < 0) //times up
            {
                randomize = false;
                changeColor(); //Change all aliens' colors to a different one
                Debug.Log("CHANGE COLOR");
            }

        }
        if (collision_enemy.change || enemyShootingMove.change || alien_boss_bullet_script.change ) //if a bullet hits a defensive wall
        {
            if (!randomize) //Set the random color timer
            {
                randomize = true;
                randomTime = Time.time + 0.2f;
            }
            else //If another bullet hits a defensive wall before the time is over then all the aliens will get random colors
            {
                changeColorRandom();
                randomize = false;
            }
            enemyShootingMove.change = false; //sets the alien bullet hit to false
            collision_enemy.change = false; //sets the player bullet hit to false
            alien_boss_bullet_script.change = false; //sets the boss bullet hit to false
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "main_area") //if alien is leaving the game area
        {
            right *= -1; //'1' moves towards the right '-1' to the left
            advanceTime = Time.time + 0.15f; //Advances one row
            advance = true;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player") //if alien hits the player
        {
            SceneManager.LoadScene("Menu_prueba_derrota"); //Game Over scene
        }
        if (collider.tag == "Wall") //if alien hits a defensive wall
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
    }
    private void changeColor()
    {
        switch (color)
        {
            case 1: //MAGENTA
                colorValue = Color.magenta;
                break;
            case 2: //CYAN
                colorValue = Color.cyan;
                break;
            case 3: //GREEN
                colorValue = Color.green;
                break;
            default: //YELLOW
                colorValue = Color.yellow;
                break;
        }
        foreach (GameObject alien in army)
        {
            foreach (Renderer childRenderer in alien.GetComponentsInChildren<Renderer>()) childRenderer.material.SetColor("_Color", colorValue);
        }
        color++; //Every time gets a different color
        if (color > 4) color = 1; 
        enemyShootingMove.change = false;         //sets the alien bullet hit to false
        collision_enemy.change = false;           //sets the player bullet hit to false
        alien_boss_bullet_script.change = false;  //sets the boss bullet hit to false
    }
        
    private void changeColorRandom()
    {
        foreach (GameObject alien in army) 
        { 
            foreach (Renderer childRenderer in alien.GetComponentsInChildren<Renderer>()) childRenderer.material.SetColor("_Color", Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
        }
        enemyShootingMove.change = false;          //sets the alien bullet hit to false
        collision_enemy.change = false;            //sets the player bullet hit to false
        alien_boss_bullet_script.change = false;   //sets the boss bullet hit to false
    }

}
