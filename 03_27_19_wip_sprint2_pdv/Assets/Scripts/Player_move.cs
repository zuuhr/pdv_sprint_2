using UnityEngine;

public class Player_move : MonoBehaviour
{
    public int left_limit = -9;
    public int right_limit = 9;
    void Start()
    {
        gameObject.GetComponent<Transform>().position = new Vector3(0.0f, -0.10f, -12.0f);
    }
    void Update()
    {
        if (gameObject.transform.position.x < right_limit && Input.GetKey("right"))
            gameObject.GetComponent<Transform>().position = new Vector3(gameObject.transform.position.x + 0.15f, gameObject.transform.position.y, gameObject.transform.position.z);
        else if (gameObject.transform.position.x > left_limit && Input.GetKey("left"))
            gameObject.GetComponent<Transform>().position = new Vector3(gameObject.transform.position.x - 0.15f, gameObject.transform.position.y, gameObject.transform.position.z);
    }
}