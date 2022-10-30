using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    // Start is called before the first frame update

    public int nodeInt;
    public Text textObject;

    public int xPos;
    public int yPos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getNode(){
        return nodeInt;
    }

    public void setNode(int newInt){
        nodeInt = newInt;
        textObject.text = nodeInt.ToString();
    }
}
