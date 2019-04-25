using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class over13_variables : MonoBehaviour
{
    public static bool rebounds;
    public GameObject alien_boss;
    public Text scoreText;
    private int score;
    public static int score2;
    public int scoreValue;
    public static bool add;
    public static bool addBoss;
    private bool boss;
    private float timer;

    void Start()
    {
        UpdateScore(); //Initial score
        timer = Time.time + 10; //set timer to spawn boss
    }

    void Update()
    {
        score2 = score; //Score 
        if (score == 2900)  SceneManager.LoadScene("Menu_prueba_victoria"); //You win if you reach this amount of points
        if (add) AddScore(); //add points because one alien was destroyed
        if (addBoss) //add points because one boss alien was destroyed
        {
            scoreValue = 400;
            AddScore();
        }
        if (!boss) //If there is no boss ingame
        {
            float timeLeft = timer - Time.time; //duration 
            if (timeLeft < 0) boss = true; //time's up
        }
        else if (boss) //Boss spawns
        {
            Debug.Log("Boss is coming");
            Instantiate(alien_boss);
            boss = false;
            timer = Time.time + 10; //set timer to spawn boss
        }
    }
    public void updateBounce() //Manages the rebounds
    {
        if (rebounds == true) rebounds = false;
        else  rebounds = true;
    }
    public void AddScore() //Add points to score
    {
        score += scoreValue;
        UpdateScore();
        add = false;
        addBoss = false;
        scoreValue = 100;
    }
    void UpdateScore() => scoreText.text = "Score: " + score; //Show score in screen
}