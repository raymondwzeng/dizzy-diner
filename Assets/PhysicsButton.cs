using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
The code in this class is sourced from Justin Barnett on YouTube, who makes fantastic XR Unity tutorials.

Source: https://youtu.be/HFNzVMi5MSQ
*/

[RequireComponent(typeof(AudioSource))]
public class PhysicsButton : MonoBehaviour
{

    public UnityEvent onPressed, onReleased;

    [SerializeField] private float threshold = 0.1f; //Controls when the event fires.
    [SerializeField] private float deadZone = 0.025f; //Deadzone in the middle to make sure that the event doesn't rapid-fire.

    public AudioClip pressSound;
    private bool isPressed;
    private Vector3 beginPosition;
    private ConfigurableJoint joint;

    // Start is called before the first frame update
    void Start()
    {
        beginPosition = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPressed && GetDistance() + threshold >= 1) {
            pressed();
        } else if (isPressed && GetDistance() - threshold <= 0) {
            released();
        }
    }

    private float GetDistance() {
        var distance = Vector3.Distance(beginPosition, transform.localPosition) / joint.linearLimit.limit;

        if(Mathf.Abs(distance) < deadZone) distance = 0;
        return Mathf.Clamp(distance, -1f, 1f);
    }

    private void pressed() {
        if(pressSound != null) AudioSource.PlayClipAtPoint(pressSound, beginPosition);
        isPressed = true;
        onPressed.Invoke();
    }

    private void released() {
        isPressed = false;
        onReleased.Invoke();
    }
}
