using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/*
The source code in this class follows a tutorial from Justin Barnett on YouTube.


Playlist: https://www.youtube.com/playlist?list=PLwz27aQG0IILSkaQreklXY_qAfr-rUbtC
*/

[RequireComponent(typeof(ActionBasedController))]
public class HandController : MonoBehaviour
{
    //Grab action-based controller
    ActionBasedController controller;

    //Grab the hand
    public Hand hand;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ActionBasedController>();
    }

    // Update is called once per frame
    void Update()
    {
        hand.SetGrip(controller.selectAction.action.ReadValue<float>());
        hand.SetTrigger(controller.activateAction.action.ReadValue<float>());
    }
}
