using UnityEngine;
using UnityEngine.SceneManagement;

public class alien_boss_bullet_script : MonoBehaviour
{
    private Rigidbody rb; 
    private int speed = 20;
    private int rebounds_number; //variable that handles the # of times the bullet has bounced
    public static bool change; //variable that handles the color change 

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>(); //acquires the rigidbody of the gameobject
        rb.velocity = new Vector3(0 , 0, -1) * speed; //initial moving direction
        transform.LookAt(transform.position + rb.velocity.normalized); //The lookat vector will point towards the movement direction
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Player")) || (other.gameObject.CompareTag("Wall")))  //if the bullet hits either the player or a wall
        {
            
            if (other.gameObject.CompareTag("Player"))  SceneManager.LoadScene("Menu_prueba_derrota"); //end game 
            if (other.gameObject.CompareTag("Wall"))  change = true; //To manage alien color change 
            Destroy(other.gameObject); //destroy the object colliding
            Destroy(this.gameObject); //destroy this object
        }
        if (over13_variables.rebounds && (other.gameObject.CompareTag("wall_rebounds_l") || other.gameObject.CompareTag("wall_rebounds_r") || other.gameObject.CompareTag("wall_rebounds_t")
            || other.gameObject.CompareTag("wall_rebounds_b"))) //collides with the surrounding walls
        {
            if (rebounds_number < 3)
            {
                //left & right walls will switch the x value of the movement vector and randomize the z value 
                if (other.gameObject.CompareTag("wall_rebounds_l") || other.gameObject.CompareTag("wall_rebounds_r")) 
                    rb.velocity = (new Vector3(-rb.velocity.x, 0.0f, Random.Range(-5.0f, 5.0f))).normalized * speed;
                //top & bottom walls will switch the z value of the movement vector and randomize the x value 
                if (other.gameObject.CompareTag("wall_rebounds_t") || other.gameObject.CompareTag("wall_rebounds_b"))
                    rb.velocity = (new Vector3(Random.Range(-5.0f, 5.0f), 0.0f, -rb.velocity.z)).normalized * speed;
                transform.LookAt(transform.position + rb.velocity.normalized); //The lookat vector will point towards the movement direction
                rebounds_number++; 
            }
            else Destroy(this.gameObject); //The third bounce will destroy the bullet

        }
    }
}
