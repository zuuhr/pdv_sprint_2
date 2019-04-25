using UnityEngine;
using UnityEngine.SceneManagement;

public class alien_movement_minus13 : MonoBehaviour
{
    Vector3 movement;
    public int speed;

    void Start() => movement = new Vector3(0,0,-1) * speed; //movement
    
    void Update() => transform.Translate(movement * Time.deltaTime); //movement
    
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "main_area") //if alien is leaving the game area
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player") //if alien hits the player
        {
            SceneManager.LoadScene("Menu_prueba_muerte"); //game over scene
            Destroy(collider);
        }
        if (collider.tag == "Wall")
        {
            Destroy(collider.gameObject); //if an alien collides against a wall, they would destroy it
            Destroy(gameObject);
        }
    }
}