using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class CubeEditor : MonoBehaviour
{
    int gridSize;
    Waypoint waypoint;
    void Awake()
    {
        //Debug.Log("Editor causes this Awake");
        waypoint  = gameObject.GetComponent<Waypoint>();

    }
    void Update()
    {


        //Debug.Log("Editor causes this Update");


        SnapToGrid();

        UpdateLabel();

    }

    private void SnapToGrid()
    {
        gridSize = waypoint.getGridSize();
        transform.position = new Vector3(waypoint.getGridPos().x * gridSize, 5f, waypoint.getGridPos().y * gridSize);
    }

    void UpdateLabel()
    {
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        string labelText = waypoint.getGridPos().x + " , " + waypoint.getGridPos().y;
        textMesh.text = labelText;
        gameObject.name = labelText;
    }
}
