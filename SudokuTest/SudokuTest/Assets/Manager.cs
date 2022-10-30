using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update

    public int numSelect = 1;
    public GameObject[,] nodes = new GameObject[9,9];

    public GameObject nodePrefab;
    public Transform canvas;

    [SerializeField]  GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;
    [SerializeField] RectTransform canvasRect;
    
    RaycastHit hit; 

    GameObject clone;
    void Start()
    {
        for(int i=0;i<9;i++){
            for(int j=0;j<9;j++){
                clone = Instantiate(nodePrefab, transform.position + new Vector3(i*50,j*50,transform.position.z), Quaternion.identity);
                clone.transform.SetParent(canvas);
                clone.GetComponent<Node>().xPos=i;
                clone.GetComponent<Node>().yPos=j;
                nodes[i,j] = clone;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            numSelect = 1;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            numSelect = 2;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            numSelect = 3;
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            numSelect = 4;
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)){
            numSelect = 5;
        }
        if(Input.GetKeyDown(KeyCode.Alpha6)){
            numSelect = 6;
        }
        if(Input.GetKeyDown(KeyCode.Alpha7)){
            numSelect = 7;
        }
        if(Input.GetKeyDown(KeyCode.Alpha8)){
            numSelect = 8;
        }
        if(Input.GetKeyDown(KeyCode.Alpha9)){
            numSelect = 9;
        }

        if(Input.GetMouseButtonDown(0)){
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the game object
            m_PointerEventData.position = Input.mousePosition;
 
            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();
 
            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);
 
            if(results.Count > 0) Debug.Log("Hit " + results[0].gameObject.name);
            Debug.Log(results[0].gameObject.GetComponent<Node>().xPos);
            Debug.Log(results[0].gameObject.GetComponent<Node>().yPos);
            if(check(results[0].gameObject.GetComponent<Node>().xPos, results[0].gameObject.GetComponent<Node>().yPos, numSelect)){
                results[0].gameObject.GetComponent<Node>().setNode(numSelect);
            }
            
        }

        if(Input.GetMouseButtonDown(1)){
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the game object
            m_PointerEventData.position = Input.mousePosition;
 
            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();
 
            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);

            if(results.Count > 0) Debug.Log("Hit " + results[0].gameObject.name);
            Debug.Log(results[0].gameObject.GetComponent<Node>().xPos);
            Debug.Log(results[0].gameObject.GetComponent<Node>().yPos);
            results[0].gameObject.GetComponent<Node>().setNode(0);
        }
    }

    bool check(int nodeX, int nodeY, int numSelect){
        if(nodes[nodeX,nodeY].GetComponent<Node>().getNode() == numSelect) return false;

        Debug.Log("Not Dupe");

        if(cVert(nodeX, numSelect)){
            Debug.Log("Clear Vert");
            if(cHori(nodeY, numSelect)){
                Debug.Log("Clear Hori");
                if(cBox(nodeX, nodeY, numSelect)){
                    Debug.Log("Clear Box");
                    return true;
                }
            }
        }
        
        return false;
    }

    bool cVert(int nodeX, int numSelect){
        int instances=0;
        for(int j=0;j<9;j++){
            if(nodes[nodeX,j].GetComponent<Node>().getNode() == numSelect) instances++;
        }

        if(instances == 0) return true;
        return false;
    }

    bool cHori(int nodeY, int numSelect){
        int instances=0;
        for(int i=0;i<9;i++){
            if(nodes[i,nodeY].GetComponent<Node>().getNode() == numSelect) instances++;
        }

        if(instances == 0) return true;
        return false;
    }

    bool cBox(int nodeX, int nodeY, int numSelect){
        int instances=0;
        int boxX;
        int boxY;
        
        if(nodeX<3){
            boxX=0;
        }else if(nodeX>2 && nodeX < 6){
            boxX=1;
        }else{
            boxX=2;
        }

        if(nodeY<3){
            boxY=0;
        }else if(nodeY>2 && nodeY < 6){
            boxY=1;
        }else{
            boxY=2;
        }

        Debug.Log("Boxs "+ boxX + ", " + boxY);
        for(int i=boxX*3;i<boxX*3+3;i++){
            for(int j=boxY*3;j<boxY*3+3;j++){
                if(nodes[i,j].GetComponent<Node>().getNode() == numSelect) instances++;
            }
        }

        if(instances == 0) return true;
        return false;
    }
}
