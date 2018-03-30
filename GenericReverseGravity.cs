using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericReverseGravity : MonoBehaviour,IActivateable {

    private bool isActive; // private bool to be activated from FPSController GenericInteraction script
    [SerializeField] float waitTime; // arbitrary time to wait
    private bool stillActive;// private bool to check whether isActive is still true
    
    private Rigidbody rbody;// rigidbody variable
    [SerializeField] private float inverseGravity;// float to be used with gravity direction


    private void Awake()
    {
        
        isActive = false;//initializes is active to false upon runtime
        rbody = GetComponent<Rigidbody>();// gets the rigid body component of the gameobject this is attached to
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        

        if (isActive)//condition: bool isActive is equal to true
        {
            rbody.useGravity = false;//turns off the use of gravity for the attached rigid body
            rbody.AddForce(new Vector3(rbody.velocity.x, inverseGravity, rbody.velocity.z), ForceMode.Acceleration);// applies force to the rigid body moving it
           //inverse gravity is exposed in the editor to be set by designer(recommended is 9.8)
            StartCoroutine(CheckingActive());//begins CheckingActive Coroutine
            StartCoroutine(WaitForBoolChange());//begins WaitForBoolChange Coroutine
        }
        else
        {
            rbody.useGravity = true;// turns gravity back on for the attached rigidbody
           
        }
	}
    private IEnumerator CheckingActive()//defintion of CheckingActive Coroutine
    {
        while (isActive)// Condition : isActive is equal to true
        {
            stillActive = isActive;//assigns the value of isActive(true) to stillActive;
            yield return null;// resumes unity frames
        }
        stillActive = false;//assigns value of false
    }
   private  IEnumerator WaitForBoolChange()//definition of WaitForBoolChange Coroutine
    {
        float startTime = Time.time;//initializes starttime

        while (startTime + waitTime > Time.time && stillActive)//begins loop testing the value of starttime & waittime is greater than the current time, also if still active is true
        {
            yield return null;//resumes the unity frames
        }
        isActive = false;//assigns the value of false to isActive if conditions above are no longer met.

    }

    public bool SetActive()//implements the interface
    {
        isActive = !isActive;// assigns true or false depending on the current value of isActive;
        return isActive;
    }
}
