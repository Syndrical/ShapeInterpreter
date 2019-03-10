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

    enum Shape { SPHERE, CUBE, CYLINDER, LINE, DELETE };

    public SteamVR_Action_Boolean grabPinchAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
    public SteamVR_Action_Boolean touchPadAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("touchPad");
    public SteamVR_Action_Boolean grabGripAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
    public SteamVR_Input_Sources handType;
    
    public GameObject cube_selector;
    public GameObject sphere_selector;
    public GameObject cylinder_selector;
    public GameObject line_selector;
    public GameObject cube;
    public GameObject sphere;
    public GameObject cylinder;
    public GameObject line;
    Shape currentShape;
    Color color; 
    
    private LineRenderer lr; 
    private LineRenderer spherelr; 
    private LineRenderer cubelr; 
    private LineRenderer cylinderlr; 
    private LineRenderer linelr; 
    private bool isTriggerPressed;

   
    private float lowestX = 0;
    private float lowestY = 0;
    private float lowestZ = 0;
    private float highestX = 0;
    private float highestY = 0;
    private float highestZ = 0;
    
    private float lineLength; 
    Quaternion rot = new Quaternion();
    private Vector3 pressedDownPos; 
    
    private Hand hand;
    private int i;               // Loop counter
    private int indexCounter;   
    
    public void Awake() {
        hand = GetComponent<Hand>();
        
        spherelr = sphere_selector.AddComponent<LineRenderer>();
        cylinderlr = cylinder_selector.AddComponent<LineRenderer>();
        linelr = line_selector.AddComponent<LineRenderer>();
        cubelr = cube_selector.AddComponent<LineRenderer>();
        
        spherelr.SetWidth(0.03f, 0.03f);
        cylinderlr.SetWidth(0.03f, 0.03f);
        linelr.SetWidth(0.03f, 0.03f);
        cubelr.SetWidth(0.03f, 0.03f);
        
        lr = spherelr;
        
        lr.positionCount = 0;
        currentShape = Shape.SPHERE;
        cube_selector.SetActive(false);
        sphere_selector.SetActive(true);
        cylinder_selector.SetActive(false);
        line_selector.SetActive(false);
        indexCounter = 0;
        isTriggerPressed = false;
    }
    
    public void Update() {
        color = GameObject.Find("ColorPalette").GetComponent<Renderer>().material.color;
        if (grabPinchAction.GetStateUp(handType)) {
            isTriggerPressed = false;
            GameObject newShape;    
            // Change Shape

            float xDiff = Mathf.Abs(highestX - lowestX); 
            float yDiff = Mathf.Abs(highestY - lowestY); 
            float zDiff = Mathf.Abs(highestZ - lowestZ);
            Debug.Log("X diff is " + xDiff);
            Debug.Log("Y diff is " + yDiff);
            Debug.Log("Z diff is " + zDiff);
            float scale;

            if (xDiff > yDiff) {
                scale = xDiff;
            } else {
                scale = yDiff;
            }
            if (scale < zDiff) {
                scale = zDiff;
            }


            Vector3 pos = new Vector3(lowestX + (xDiff/2), lowestY + (yDiff/2), lowestZ + (zDiff/2));
            Debug.Log(pos);
            
            switch (currentShape) {
                case Shape.SPHERE: 
                    newShape = Instantiate(sphere, pos, Quaternion.identity);
                    newShape.GetComponent<Renderer>().material.color = color;
                    newShape.transform.localScale = new Vector3(scale, scale, scale);
                    Debug.Log("scale is " + newShape.transform.localScale);
                    Debug.Log("scale is " + scale);
                    break;
                case Shape.CUBE: 
                    newShape = Instantiate(cube, pos, Quaternion.identity);
                    newShape.GetComponent<Renderer>().material.color = color;
                    newShape.transform.localScale = new Vector3(scale, scale, scale);
                    break;
                case Shape.CYLINDER: 
                    newShape = Instantiate(cylinder, pos, Quaternion.identity);
                    newShape.GetComponent<Renderer>().material.color = color;
                    newShape.transform.localScale = new Vector3(scale/2, scale/2, scale/2);
                    break;
                case Shape.LINE:
                    lineLength = Vector3.Distance(pressedDownPos, hand.transform.position);
                    rot = Quaternion.FromToRotation( Vector3.up, pressedDownPos - hand.transform.position);
                    newShape = Instantiate(line, pos, rot);
                    newShape.GetComponent<Renderer>().material.color = color;
                    newShape.transform.localScale = new Vector3(0.005f, lineLength*0.5f, 0.005f);
                    break;
                case Shape.DELETE: 
                    break;
            }  
            
            lr.positionCount = 0;
            indexCounter = 0;
            Debug.Log("Trigger was pressed.");
            
        }
        
        if (grabPinchAction.GetStateDown(handType)) {
            Debug.Log("Trigger was pressed.");
            isTriggerPressed = true;

            highestX = hand.transform.position.x;
            highestY = hand.transform.position.y;
            highestZ = hand.transform.position.z;
            lowestX = hand.transform.position.x;
            lowestY = hand.transform.position.y;
            lowestZ = hand.transform.position.z;
            
            pressedDownPos = hand.transform.position;
            
        }
        
        if (isTriggerPressed) {
            lr.positionCount = indexCounter+1;
            lr.SetPosition(indexCounter, hand.transform.position);
            indexCounter++;

            if (hand.transform.position.x < lowestX) {
                lowestX = hand.transform.position.x;
            }
            if (hand.transform.position.z < lowestZ) {
                lowestZ = hand.transform.position.z;
            }
            if (hand.transform.position.y < lowestY) {
                lowestY = hand.transform.position.y;
            }
            if (hand.transform.position.x > highestX) {
                highestX = hand.transform.position.x;
            }
            if (hand.transform.position.y > highestY) {
                highestY = hand.transform.position.y;
            }
            if (hand.transform.position.z > highestZ) {
                highestZ = hand.transform.position.z;
            }
        }
        
        if (touchPadAction.GetStateDown(handType)) {
            
            // Draw Shape
            switch (currentShape) {
                case Shape.SPHERE: 
                    currentShape = Shape.CUBE;
                    cube_selector.SetActive(true);
                    sphere_selector.SetActive(false);
                    cylinder_selector.SetActive(false);
                    line_selector.SetActive(false);
                    lr = cubelr;
                    break;
                case Shape.CUBE: 
                    currentShape = Shape.CYLINDER;
                    cube_selector.SetActive(false);
                    sphere_selector.SetActive(false);
                    cylinder_selector.SetActive(true);
                    line_selector.SetActive(false);
                    lr = cylinderlr;
                    break;
                case Shape.CYLINDER: 
                    currentShape = Shape.LINE;
                    cube_selector.SetActive(false);
                    sphere_selector.SetActive(false);
                    cylinder_selector.SetActive(false);
                    line_selector.SetActive(true);
                    lr = linelr;
                    break;
                case Shape.LINE: 
                    currentShape = Shape.DELETE;
                    cube_selector.SetActive(false);
                    sphere_selector.SetActive(false);
                    cylinder_selector.SetActive(false);
                    line_selector.SetActive(false);
                    break;
                case Shape.DELETE: 
                    currentShape = Shape.SPHERE;
                    cube_selector.SetActive(false);
                    sphere_selector.SetActive(true);
                    cylinder_selector.SetActive(false);
                    line_selector.SetActive(false);
                    lr = spherelr;
                    break;
            }
       
            Debug.Log("Touchpad was pressed. Shape is "+currentShape);
        
        }
        
        if (grabGripAction.GetStateDown(handType)) {
            // Change color
            Debug.Log("Grip button was pressed.");
        }
    
    }

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
