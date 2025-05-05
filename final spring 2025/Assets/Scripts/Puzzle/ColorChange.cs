using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public bool isChangeable;
    
    public List<Material> colorMaterials = new List<Material>();

    public int solutionColor;


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isChangeable)
        {

        }
        else if (!isChangeable)
        {

        }
    }

    void gemColorChange()
    {

    }

}
