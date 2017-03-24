using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    public enum ControlType
    {
        WASD,
        ARROW
    }
    ;
    public ControlType controlType = ControlType.WASD;
    Rigidbody2D rigidBody;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {

        if (controlType == ControlType.WASD)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
                Jump();

            if (Input.GetKey(KeyCode.A))
                MoveLeft();

            if (Input.GetKey(KeyCode.D))
                MoveRight();

            if (Input.GetKey(KeyCode.S))
                MoveDown();

        } else if (controlType == ControlType.ARROW)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                Jump();

            if (Input.GetKey(KeyCode.LeftArrow))
                MoveLeft();

            if (Input.GetKey(KeyCode.RightArrow))
                MoveRight();

            if (Input.GetKey(KeyCode.DownArrow))
                MoveDown();
        }
    }

    void Jump()
    {
        rigidBody.AddForce(new Vector2(0, 1000));
    }

    void MoveLeft()
    {
        rigidBody.AddForce(new Vector2(-20, 0));
    }

    void MoveRight()
    {
        rigidBody.AddForce(new Vector2(20, 0));
    }

    void MoveDown()
    {
        rigidBody.AddForce(new Vector2(0, -1000));
    }
}
