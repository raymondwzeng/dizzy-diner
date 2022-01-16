using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
The source code in this class follows a tutorial from Justin Barnett on YouTube.


Playlist: https://www.youtube.com/playlist?list=PLwz27aQG0IILSkaQreklXY_qAfr-rUbtC
*/

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{

    //Physics 
    [SerializeField] private GameObject followObject;
    [SerializeField] private float followTranslateSpeed = 30f;
    [SerializeField] private float followRotateSpeed = 100f;
    [SerializeField] private Vector3 offsetPosition;
    [SerializeField] private Vector3 offsetRotation;
    
    private Transform followTarget;
    private Rigidbody body;

    //Anims
    Animator animator;
    SkinnedMeshRenderer mesh;
    private float gripTarget;
    private float triggerTarget;

    private float gripCurrent;
    private float triggerCurrent;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        followTarget = followObject.transform;
        body = GetComponent<Rigidbody>();
        body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.mass = 20f;

        body.position = followTarget.position;
        body.rotation = followTarget.rotation;

        animator = GetComponent<Animator>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();
        PhysicsMove();
    }

    internal void SetGrip(float value) {
        gripTarget = value;
    }

    internal void SetTrigger(float value) {
        triggerTarget = value;
    }

    void AnimateHand() {
        if(gripTarget != gripCurrent) {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
            animator.SetFloat("Grip_Weight", gripCurrent);
        }
        if(triggerTarget != triggerCurrent) {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * speed);
            animator.SetFloat("Trigger_Weight", triggerCurrent);
        }
    }

    void PhysicsMove() {
        var oPosition = followTarget.position + offsetPosition;
        var distance = Vector3.Distance(oPosition, transform.position);
        body.velocity = (oPosition - transform.position).normalized * followTranslateSpeed * distance;

        var oRotation = followTarget.rotation * Quaternion.Euler(offsetRotation);
        //Rotation through weird quaternion magic, thanks internet
        var quat = oRotation * Quaternion.Inverse(body.rotation);
        quat.ToAngleAxis(out float angle, out Vector3 axis);
        body.angularVelocity = angle * axis * Mathf.Deg2Rad * followRotateSpeed;
    }

    public void ToggleVisibility() {
        mesh.enabled = !mesh.enabled;
    }
}
