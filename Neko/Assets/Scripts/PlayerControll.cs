using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Nuetral,
    Ladder
}

public class PlayerControll : MonoBehaviour
{
    private Rigidbody rigidbody;
    [SerializeField] private float speed = 3;

    private State state;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        state = State.Nuetral;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Nuetral:
                float x = Input.GetAxis("Horizontal");
                rigidbody.velocity = new Vector3(x * speed, rigidbody.velocity.y, 0);
                rigidbody.AddForce(0, -0.5f, 0);
                rigidbody.useGravity = true;

                break;
            case State.Ladder:
                float y = Input.GetAxis("Vertical");
                rigidbody.velocity = new Vector3(0, y * speed, 0);
                rigidbody.useGravity = false;

                break;
        }

    }


    public void ChangeState(State newState)
    {
        state = newState;
    }
}
