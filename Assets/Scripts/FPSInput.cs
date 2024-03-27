using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour {

    private float speed = 9.0f;
    private float gravity = -9.8f;
    private float pushForce = 5.0f;

    private CharacterController charController;

    // Start is called before the first frame update
    void Start() {
        charController = GetComponent<CharacterController> ();
    }

    // Update is called once per frame
    void Update() {

        /**     Using transform.Translate(movement) - without Rigidbody or Character controller class
            float horizInput = Input.GetAxis("Horizontal");
            float vertInput = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(horizInput, 0, vertInput) * Time.deltaTime * speed;

            transform.Translate(movement);
        */

        //Using Unity's built in CHaracterController with Collision Detection
        float horizInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3 (horizInput, 0, vertInput);

        // Clamp magnitude to limit diagonal movement
        movement = Vector3.ClampMagnitude (movement, 1.0f);

        // take speed into account
        movement *= speed;

        //adding gravity to 'push' player to the ground 
        movement.y = gravity;
        
        // make movement processor independent
        movement *= Time.deltaTime;

        // Convert local to global coordinates
        movement = transform.TransformDirection (movement);

        //moving player using built in Character Controller
        //notice the difference compared to rigidbody movement and transform.Translate(movement)
        charController.Move (movement);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body != null && !body.isKinematic)
        {
            body.velocity = hit.moveDirection * pushForce;
        }
    }
}
