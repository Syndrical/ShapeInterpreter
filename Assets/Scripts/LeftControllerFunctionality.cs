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

public class LeftControllerFunctionality : MonoBehaviour {

    public SteamVR_Input_Sources handType;
    enum Colours {BLACK, BLUE, CYAN, GREEN, MAGENTA, RED, WHITE, YELLOW};
    public SteamVR_Action_Boolean touchPadAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("touchPad");
    Colours currentColour;
    public Color colour;
    public GameObject visibleColorPalette;

 
    private Hand hand;

    
    public void Awake() {
        hand = GetComponent<Hand>();
        currentColour = Colours.BLACK;
    }
    
    
    public void Update() {   
        if (touchPadAction.GetStateDown(handType)) {
        
            switch (currentColour) {
                case Colours.BLACK:
                    currentColour = Colours.BLUE;
                    visibleColorPalette.GetComponent<Renderer>().material.color = Color.blue;
                    colour = Color.blue;
                    break;
                case Colours.BLUE: 
                    currentColour = Colours.CYAN;
                    visibleColorPalette.GetComponent<Renderer>().material.color = Color.cyan;
                    colour = Color.cyan;
                    break;
                case Colours.CYAN: 
                    currentColour = Colours.GREEN;
                    visibleColorPalette.GetComponent<Renderer>().material.color = Color.green;
                    colour = Color.green;
                    break;
                case Colours.GREEN: 
                    currentColour = Colours.MAGENTA;
                    visibleColorPalette.GetComponent<Renderer>().material.color = Color.magenta;
                    colour = Color.magenta;
                    break;
                case Colours.MAGENTA: 
                    currentColour = Colours.RED;
                    visibleColorPalette.GetComponent<Renderer>().material.color = Color.red;
                    colour = Color.red;
                    break;
                case Colours.RED: 
                    currentColour = Colours.WHITE;
                    visibleColorPalette.GetComponent<Renderer>().material.color = Color.white;
                    colour = Color.white;
                    break;
                case Colours.WHITE:
                    currentColour = Colours.YELLOW;
                    visibleColorPalette.GetComponent<Renderer>().material.color = Color.yellow; 
                    colour = Color.yellow;
                    break;
                case Colours.YELLOW: 
                    currentColour = Colours.BLACK;
                    visibleColorPalette.GetComponent<Renderer>().material.color = Color.black;
                    colour = Color.black;
                    break;
                    
            }
            Debug.Log("Touchpad was pressed. Colour is "+currentColour);
        
        }
    }
}

   
