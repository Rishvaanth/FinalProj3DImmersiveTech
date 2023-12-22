using System;
using System.Collections;
using System.Collections.Generic;
using OVR.OpenVR;
using UnityEngine;

public class MainMenuSelect : MonoBehaviour
{
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayEnd = transform.position+transform.forward*100f;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward,out hit)){
            rayEnd = hit.point;
            if(hit.collider.CompareTag("Math")){
                if(OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger)>0.09f){
                    // loadSelectedScene("MathMenu");
                    ScenesManager.Instance.LoadScene(ScenesManager.Scene.MathMenu);
                }
            }else if(hit.collider.CompareTag("Geometry")){
                if(OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger)>0.09f){
                    ScenesManager.Instance.LoadScene(ScenesManager.Scene.GeometryMenu);
                }
            }else if(hit.collider.CompareTag("Graph")){
                if(OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger)>0.09f){
                    ScenesManager.Instance.LoadScene(ScenesManager.Scene.GraphMenu);
                }
            }else if(hit.collider.CompareTag("MainMenu")){
                if(OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger)>0.09f){
                    ScenesManager.Instance.LoadScene(ScenesManager.Scene.MainMenu);
                }
            }
        }
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, rayEnd);
    }
}
