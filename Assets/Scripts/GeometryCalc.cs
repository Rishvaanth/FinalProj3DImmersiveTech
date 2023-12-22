using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using UnityEngine;
using TMPro;

public class GeometryCalc : MonoBehaviour
{
    public Material indiMat;
    public Material actMat;
    private Material tmpMat;

    private char func ;
    private int ans;
    private LineRenderer lineRenderer;

    private float[] values = new float[3];
    

    private GameObject indicatedGameObj = null;
    private GameObject selectedGameObj = null;
    public GameObject keyBoard;
    private bool select = false;
    private bool v3 = false;
    private string shape;

    public TextMeshPro msg;
    
    public TextMeshPro t;

    private Vector3 pos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        values[0] = 0.00f;
        values[1] = 0.00f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayEnd = transform.position + transform.forward * 100.0f;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            rayEnd = hit.point;
            if (hit.collider.tag == "2D"){
                values[0]=0.00f;
                values[1]=0.00f;
                values[2]=0.00f;
                Renderer renderer = hit.collider.gameObject.GetComponent<Renderer>();
                if (indicatedGameObj == null && selectedGameObj == null)                    //change the color of the indicated gameobject
                {

                    tmpMat = renderer.material;
                    renderer.material = indiMat;
                    indicatedGameObj = hit.collider.gameObject;
                }
                    msg.text = "";
                    msg.text = "Click A to calculate Area and X to calculate Perimeter";
                    shape = hit.collider.name;
                    if (OVRInput.Get(OVRInput.RawButton.A) == true){
                        keyBoard.SetActive(true);
                        func = 'a';
                        msg.text = "Enter the values to calculate area";
                    }
                    else if (OVRInput.Get(OVRInput.RawButton.X) == true){
                        keyBoard.SetActive(true);
                        func = 'p';
                        msg.text = "Enter the values to calculate Perimeter";
                    }
            }
            else if (hit.collider.tag == "3D"){
                
                Renderer renderer = hit.collider.gameObject.GetComponent<Renderer>();
                if (indicatedGameObj == null && selectedGameObj == null)                    //change the color of the indicated gameobject
                {

                    tmpMat = renderer.material;
                    renderer.material = indiMat;
                    indicatedGameObj = hit.collider.gameObject;
                }
                if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0.09f){
                    keyBoard.SetActive(true);
                    shape = hit.collider.name;
                    v3 = true;
                    func = 'v';
                    msg.text = "Enter the values to calculate volume";
                    values[0]=0.00f;
                    values[1]=0.00f;
                    values[2]=0.00f;
                }
            }
            else if (indicatedGameObj != null)
            {
                indicatedGameObj.GetComponent<Renderer>().material = tmpMat;
                indicatedGameObj = null;
                msg.text = "";
            }
            if (hit.collider.tag == "Button")
            {
                Renderer renderer = hit.collider.gameObject.GetComponent<Renderer>();
                if (indicatedGameObj == null && selectedGameObj == null)                    //change the color of the indicated gameobject
                {

                    tmpMat = renderer.material;
                    renderer.material = indiMat;
                    indicatedGameObj = hit.collider.gameObject;
                }
                if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0.09f)              //change the color of the gameobject if it is selected
                {

                    
                    if (selectedGameObj == null)
                    {
                        selectedGameObj = hit.collider.gameObject;
                        renderer.material = actMat;


                    }
                    if(select == false)
                    {
                        if (selectedGameObj.name == "CLR")
                        {
                            t.text = "";
                        }
                        else if(selectedGameObj.name == "="){
                            if(values[0] == 0.00f){
                                values[0] = float.Parse(t.text);
                                t.text="";
                                msg.text = "Enter the next value";
                            }
                            else if(values[0] != 0.00f && values[1] == 0.00f){
                                values[1] = float.Parse(t.text);
                                t.text="";
                            }
                            else if(v3 == true){
                                values[2] = float.Parse(t.text);
                                t.text="";
                            }
                            if ( shape == "Rectangle" && values[0] != 0.00f && values[1] != 0.00f){
                                Calculate(func);
                            }
                            else if ( shape == "Cuboid" && values[0] != 0.00f && values[1] != 0.00f && values[2] != 0.00f){
                                Calculate(func);
                            }
                            else if(shape!= "Rectangle" && shape != "Cuboid" && values[0] != 0.00f){
                                Calculate(func);
                            }
                            

                        }
                        else
                        {
                            t.text += selectedGameObj.name;
                        }
                        select = true;
                    }
                    

                }
                else if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) < 0.1f)
                {
                    renderer.material = indiMat;
                    selectedGameObj = null;
                    select = false;
                }
            }
            else if (indicatedGameObj != null)
            {
                indicatedGameObj.GetComponent<Renderer>().material = tmpMat;
                indicatedGameObj = null;
            }

        }
        else if (indicatedGameObj != null && selectedGameObj == null)
        {
            indicatedGameObj.GetComponent<Renderer>().material = tmpMat;
            indicatedGameObj = null;
        }


        if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) < 0.1f && selectedGameObj != null)
        {
            selectedGameObj.GetComponent<Renderer>().material = tmpMat;
            selectedGameObj = null;
            select = false;
            
        }


        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, rayEnd);

    }

    void Calculate(char f)
    {
        keyBoard.SetActive(false);
        msg.text = "";
        if (f == 'a'){
            switch(shape){
                case "Square":
                    msg.text = (values[0]*values[0]).ToString();
                    break;
                case "Circle":
                    msg.text = (3.14*values[0]*values[0]).ToString();
                    break;
                case "Rectangle":
                    msg.text = (values[0]*values[1]).ToString();
                    break;
                default:
                    msg.text = "Error";
                    break;
            }
        }
        else if(f=='p'){
            switch(shape){
                case "Square":
                    msg.text = (values[0]* 4.00f).ToString();
                    break;
                case "Circle":
                    msg.text = (2.00f* 3.14f* values[0]).ToString();
                    break;
                case "Rectangle":
                    msg.text = (2.00f*(values[0]+values[1])).ToString();
                    break;
                default:
                    msg.text = "Error";
                    break;
            }
        }
        else if(f=='v'){
            switch(shape){
                case "Cube":
                    msg.text = (values[0]*values[0]*values[0]).ToString();
                    break;
                case "Sphere":
                    msg.text = ((4.00f/3.00f)*3.14f*(values[0]*values[0]*values[0])).ToString();
                    break;
                case "Cuboid":
                    msg.text = (values[0]*values[1]*values[2]).ToString();
                    break;
                default:
                    msg.text = "Error";
                    break;
            }
        }
        
        values[0]=0.00f;
        values[1]=0.00f;
        values[2]=0.00f;
    }
}

