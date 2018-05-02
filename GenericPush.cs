using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// using IActivateable interface, is a pushable object using explosive force physics.
/// </summary>
public class GenericPush : MonoBehaviour,IActivateable
{
    [SerializeField] private bool isActive; // private bool to be activated from FPSController GenericInteraction script
    private Rigidbody rbody;
    private GameObject pusher;
    [SerializeField] int power;

    private void Awake()
    {
        pusher = GameObject.FindGameObjectWithTag("Player");
        rbody = GetComponent<Rigidbody>();
        isActive = false;//initializes is active to false upon runtime
    }
    
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (isActive)
        {
            rbody.AddExplosionForce(power, new Vector3(pusher.transform.forward.x,0,pusher.transform.forward.z),0f,0.5f,ForceMode.Acceleration);
            isActive = !isActive;
        }
    }

    public bool SetActive()
    {
        isActive = !isActive;
        return isActive;
    }
}
