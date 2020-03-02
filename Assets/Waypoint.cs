using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Color exporedColor=Color.gray;
    
    Vector2Int gridPos;
    const int gridSize = 10;
    public bool isEplored = false;
    public Waypoint exploredFrom;
    public bool isPlaceable = true;

    //[SerializeField] Tower towerPrefab;
    //MeshRenderer topMeshRenderer;
    // Start is called before the first frame update
    void Start()
    {

    }


    public int getGridSize()
    {
        return gridSize;
    }

    public Vector2Int getGridPos()
    {
        gridPos.x = Mathf.RoundToInt(transform.position.x / gridSize);
        gridPos.y = Mathf.RoundToInt(transform.position.z / gridSize);
        return gridPos;
    }

    //public void SetTopColor(Color color)
    //{
    //    MeshRenderer topMeshRenderer = this.transform.Find("top").GetComponent<MeshRenderer>();
    //    topMeshRenderer.material.color = color;
   // }
    // Update is called once per frame


    private void OnMouseOver()
    {
        // print("mouse on"+gameObject.name);
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlaceable)
            {
                FindObjectOfType<TowerFactory>().AddTower(this);
               
            }
            else
            {
                print("Cant place here");
            }
            print(gameObject.name + "clicked");
        }
      
    }
    void Update()
    {
        if (isEplored == true)
        {
            MeshRenderer topMeshRenderer = this.transform.Find("top").GetComponent<MeshRenderer>(); 
            topMeshRenderer.material.color = exporedColor;
        }
            
    }
}
