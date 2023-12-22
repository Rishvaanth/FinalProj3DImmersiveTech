using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keyboard : MonoBehaviour
{
    public Material indiMat;
    public Material actMat;
    private Material tmpMat;
    private int ans;
    private LineRenderer lineRenderer;

    private GameObject indicatedGameObj = null;
    private GameObject selectedGameObj = null;
    private bool select = false;
    private float posOffset = 0.0f;


    
    public TextMeshPro t;

    private Vector3 pos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayEnd = transform.position + transform.forward * 100.0f;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            rayEnd = hit.point;
            if (hit.collider.tag == "Button")
            {
                Renderer renderer = hit.collider.gameObject.GetComponent<Renderer>();
                if (indicatedGameObj == null && selectedGameObj == null)                    //change the color of the indicated gameobject
                {

                    tmpMat = renderer.material;
                    renderer.material = indiMat;
                    indicatedGameObj = hit.collider.gameObject;
                }
                if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0.9f)              //change the color of the gameobject if it is selected
                {

                    if (selectedGameObj == null)
                    {
                        selectedGameObj = hit.collider.gameObject;
                        renderer.material = actMat;
                        posOffset = Vector3.Distance(transform.position, selectedGameObj.transform.position);


                    }
                    if(select == false)
                    {
                        if (selectedGameObj.name == "CLR")
                        {
                            t.text = "";
                        }
                        else if(selectedGameObj.name == "="){
                            Calculate(t.text);

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

    void Calculate(string exp)
    {
        int Oprator = 0;
        char OpratorChar=' ';
        int Oprand = 0;
        
        foreach (char item in exp)
        {
            if (item == '+' || item == '-' || item == '*' || item == '/')
            {
                Oprator += 1;
                OpratorChar = item;
            }
            else
            {
                if (Oprand == 0)
                {
                    ans = (int)char.GetNumericValue(item);

                }
                Oprand += 1;
                if (Oprand > 1)
                {
                    switch (OpratorChar)
                    {
                        case '+':
                            ans += (int)char.GetNumericValue(item);
                            break;
                        case '-':
                            ans -= (int)char.GetNumericValue(item);
                            break;
                        case '*':
                            ans *= (int)char.GetNumericValue(item);
                            break;
                        case '/':
                            ans /= (int)char.GetNumericValue(item);
                            break;
                        case ' ':
                            break;
                        default:
                            t.text = "Error";
                            break;
                    }
                    OpratorChar = ' ';
                }
                
            }
            if(Oprator > Oprand)
            {
                t.text = "Error";
            }

        }
        t.text = "";
        t.text = ans.ToString();

        
    }
}
