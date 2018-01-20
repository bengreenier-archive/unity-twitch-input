using System.Collections;
using System.Collections.Generic;
using TwitchInput.Core;
using UnityEngine;

public class TwitchInputExample_Character_Control : MonoBehaviour
{
    public Transform SpawnPosition;
    public float Speed = 5;

    private new Rigidbody2D rigidbody2D;

    private void Start()
    {
        this.rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // move right
        if (TwInput.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.D))
        {
            this.rigidbody2D.AddForce(new Vector2(this.Speed * 1, 0));
        }
        // move left
        else if (TwInput.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.A))
        {
            this.rigidbody2D.AddForce(new Vector2(this.Speed * -1, 0));
        }
        // jump
        else if (TwInput.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.W))
        {
            this.rigidbody2D.AddForce(new Vector2(0, 2 * this.Speed));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Respawn")
        {
            this.transform.position = this.SpawnPosition.position;
            this.rigidbody2D.velocity = Vector2.zero;
            this.rigidbody2D.angularVelocity = 0;
        }
    }
}
