/*


   Controller Functionality: This script provides the functionallity of 
   a HTC Vive controller grabbing objects and drawing.
   The code is based off the link below, with minor adjustments:
   https://www.raywenderlich.com/149239/htc-vive-tutorial-unity

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ControllerFunctionality : MonoBehaviour {

    /*private GameObject collidingObject;            // Object colliding with controller
    private GameObject objectInHand;              // Reference for object in hand
    private Hand hand; // Controller input

    void Awake()
    {
        hand = GetComponent<Hand>();   // Get controller reference
    }

    private void SetCollidingObject(Collider col)
    {
        // If already holding something, cannot hold something else
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        // Assign object as potential grab target
        collidingObject = col.gameObject;
    }

    // Set up as potential grab target
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    // Set up as potential grab target
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    // Controller out of object - Remove reference
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    // Grabbing an object
    private void GrabObject()
    {
        // Put object onto hand
        objectInHand = collidingObject;
        collidingObject = null;
        // Add joint to it so it won't disconnect from hand
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    // Add fixed joint so it doesn't break easily, return object
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    // Release object
    private void ReleaseObject()
    {
        // Make sure fixed join is attached
        if (GetComponent<FixedJoint>())
        {
            // Remove connection / joint
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            // Add speed / rotation when releasing
            objectInHand.GetComponent<Rigidbody>().velocity = hand.Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = hand.Controller.angularVelocity;
        }
        // Remove reference of attached object
        objectInHand = null;
    }

    // Update is called once per frame
    void Update ()
    {
        // Check if we should grab object
        if (hand.Controller.GetHairTriggerDown())
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }

        // Release object if holding one
        if (hand.Controller.GetHairTriggerUp())
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
    }*/
}
