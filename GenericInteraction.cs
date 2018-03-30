using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericInteraction : MonoBehaviour {

    [SerializeField] int Raydistance; // integer variable to declare the distance of raycast, assigned in UI editor
    private RaycastHit hit = new RaycastHit(); // private Raycast Hit object assigned when raycast hits an object

    private void Awake()
    {
        //requirements needed to set before, ie. find objects and add to array
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Debug.DrawRay(transform.position, this.transform.forward, Color.green);/*
        shows the raycast in editor scene inorder to help determine correct ray distance and whether it is touching an object
        */
        if ((Physics.Raycast(this.transform.position, this.transform.forward, out hit, Raydistance))&&Input.GetMouseButtonDown(0))// condition: raycast returns object and Left mouse button is down
        {
            hit.transform.gameObject.GetComponent<IActivateable>().SetActive(); //calls the interface IActivateables method called SetActive
            Debug.Log("Activated!");
            Debug.Log(hit.transform.gameObject.name);//Logs the new tag of the hit object in the editor console
        }


    }
   
}
