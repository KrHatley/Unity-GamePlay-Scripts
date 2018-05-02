using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Meant to slow objects normal movement speed
/// </summary>

public class GenericSlow : MonoBehaviour,IActivateable
{
    [SerializeField] private bool isActive;
    [SerializeField] private float tempTimeScale;// set in editor, recommended value is 0.85
    [SerializeField] private float normTimeScale;
    [SerializeField] private List<Rigidbody> slowedDownBodies;
    [SerializeField] private float sphereOverlapRadius;
    [SerializeField] private Ray ray;
    [SerializeField] public RaycastHit[] Hits;
    private LayerMask mask;
    private bool wasActive;
    private bool stillActive;

    // [SerializeField] private Collider[] bodyColliders;
    //[SerializeField] private Rigidbody[] slowedBodies;

    // private Rigidbody rbodyTemp;

    //private RaycastHit hit;




    private void Awake()//first method called
    {
        mask = 1 << 9;
        ray = new Ray(transform.position, new Vector3(1, 1, 1));
        isActive = false;//initializes isActive as false before any other method is called.
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        
        //Physics.SphereCast(ray, sphereOverlapRadius, out hit);

        if (isActive)
        {
            GrabRigidBodies();
            
            SlowTime();
            StartCoroutine(CheckingActive());
        }
        else
        {
            StartCoroutine(WaitForBoolChange());
            //slowedDownBodies.RemoveAll(rigidbody);
        }
	}

    private void GrabRigidBodies()
    {
        if (!stillActive && isActive==true)
        {
            Hits = Physics.SphereCastAll(ray, sphereOverlapRadius, mask);
            foreach (RaycastHit item in Hits)
            {
                for (int i = 0; i < Hits.Length; i++)
                {
                    if (item.rigidbody != Hits[i].rigidbody)
                    {
                            slowedDownBodies.Add(item.rigidbody);//find a better way 
                    }
                }
            }
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
    private IEnumerator WaitForBoolChange()
    {
        while (stillActive==true)
        {
            yield return null;
        }
        if (Hits.Length>0)
        {
            ResumeNormalTime();
        }
       
        Array.Clear(Hits, 0, Hits.Length);
        slowedDownBodies.Clear();
    }
    private void ResumeNormalTime()
    {
        foreach (Rigidbody item in slowedDownBodies)
        {
            item.mass /= normTimeScale;
            item.velocity *= normTimeScale;
            item.angularVelocity *= normTimeScale;
        }
        
    }

    private void SlowTime()
    {
        foreach (Rigidbody item in slowedDownBodies)
        {
            item.mass /= tempTimeScale;
            item.velocity *= tempTimeScale;
            item.angularVelocity *= tempTimeScale;
        }
    }

    private void OnDrawGizmos()// editor method
    {
        Gizmos.DrawWireSphere(transform.position, sphereOverlapRadius);// draws the sphere cast for designer benefit
    }
    public bool SetActive()
    {
        isActive = !isActive;
        return isActive;
    }
}
