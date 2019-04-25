using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_movement_2D : MonoBehaviour
{ //movement
    public float maxSpeed;
    public float acceleration;
    public float frictionConstant;
    public static Vector3 lookat;
    public static Vector3 thisPosition;
    Vector3 velocity;
    
    public GameObject bullet;
    private bool whileShooting;
    float advanceTime;
    public string gameOverScene;
    void Start()
    {
        velocity = new Vector3(0.0f, 0.0f, 0.01f);
    }

    void FixedUpdate()
    {
        if (Input.GetKey("right"))velocity.x += acceleration; //right
        if (Input.GetKey("left")) velocity.x -= acceleration; //left
        if (Input.GetKey("up"))velocity.z += acceleration; //up
        if (Input.GetKey("down"))velocity.z -= acceleration; //down
        if (velocity.magnitude >= maxSpeed) velocity = velocity.normalized * maxSpeed; //the velocity cannot be over the maximun speed set
        gameObject.GetComponent<CharacterController>().Move(velocity * Time.deltaTime); //apply movement to character controller
        lookat = Vector3.Normalize(new Vector3(velocity.x, 0.0f, velocity.z));
        thisPosition = transform.position;
        transform.LookAt(transform.position + lookat); //lookat direction is movement direction
        velocity *= frictionConstant; //if no buttons are pressed the player will start to slow down
        lookat = this.transform.forward;
        if ((Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) && !whileShooting) { //shooting
            Instantiate(bullet, this.transform.position, this.transform.rotation );
            advanceTime = Time.time + 0.5f; //set timer cooldown
            whileShooting = true; //cooldown 
        }
        float timeLeft = advanceTime - Time.time; //duration 
        if (timeLeft < 0 && whileShooting) whileShooting = false;//time's up
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("boss")) //If the player hits a defensive wall or an alien
        {
            Destroy(other.gameObject);
            SceneManager.LoadScene(gameOverScene); //game over scene
            Destroy(this.gameObject);
        }
    }
}