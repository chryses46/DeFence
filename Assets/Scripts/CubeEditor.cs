using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase] //Tells editor this GameObject is what you want to select
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        CubeSnap(); // Sets cube grid snap
    }

    private void CubeSnap()
    {
        SnapToGrid();
        RenameLabel();

    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();
        transform.position = new Vector3(
            waypoint.GetGridPos().x * gridSize,
            0f,
            waypoint.GetGridPos().y * gridSize
        );
    }

    private void RenameLabel()
    {
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        int gridSize = waypoint.GetGridSize();
        string textLabel = 
            waypoint.GetGridPos().x+ 
            "," + 
            waypoint.GetGridPos().y;
        textMesh.text = textLabel;
        gameObject.name = "Cube " + textLabel;
    }
}
