using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A "gravity" object that pulls all objects under the correct layermask like a magnet.
/// </summary>
public class GenericPull : MonoBehaviour,IActivateable
{
   [SerializeField] private bool isActive;// displays variable in editor for designer testing
    private Rigidbody rbody;// rigid body variable for accessing physics and other garabage
    
    [SerializeField] Collider[] ObjectsToPull;// array of colliders found by the Overlapsphere cast, exposed to editor
    [SerializeField] float Radius;// determines the radius/size of the sphere cast, exposed to editor
    private float Gravpull;// private variable to be used with the pull of this object
    public LayerMask mask;// mask to be used with sphere cast, any object that is not set with the proper layer will be ignored by sphere cast
    //if you want an object to be effected by this the layer must be set to Pull or whatever layer number eight is

   
    private void Awake()// awake method called before start
    {
        mask = 1 << 8;// must have layer number eight to be effected
        Gravpull = 9.8f;// force of pull set to the magnitude of gravity
        rbody = GetComponent<Rigidbody>();// assigns the rigidbody of the gameobject this script is attached to the variable rbody
        isActive = false;// sets the bool to false
    }
    // Use this for initialization
    void Start ()
    {
        // nothing to see here
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        ObjectsToPull = Physics.OverlapSphere(transform.position, Radius, mask);// finds all coliders that also have the matching layer and adds to array
        if (isActive)// if isActive is equal to true
        {      
            foreach (Collider c in ObjectsToPull)// operates the following code on each object in this array
            {
                 Vector3 PullDir = transform.position - c.transform.position;// sets a direction in which the object needs to move
                 c.attachedRigidbody.AddForce(PullDir * Gravpull, ForceMode.Force);// applies the direction and force to move the object
                //repeat for each object in array
            }   
        }
	}

    private void OnDrawGizmos()// editor method
    {
        Gizmos.DrawWireSphere(transform.position, Radius);// draws the sphere cast for designer benefit
    }

    public bool SetActive()//implementation of Interface
    {
        isActive = !isActive;// assigns true or false depending on the current value of isActive
        return isActive;
    }
}
