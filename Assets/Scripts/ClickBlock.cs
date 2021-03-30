using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBlock : MonoBehaviour
{
    Grid grid;
    public GameObject gridObject;

    public GameObject ghostCube;

    public MoveCamera moveCameraScript;

    GameObject ghostBlockInstance = null;

    RaycastHit hitInfo;
    // Start is called before the first frame update
    void Start()
    {
        hitInfo = new RaycastHit();
        grid = gridObject.GetComponent<Grid>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RemoveOrAddBlocks(true);
        }
        if (Input.GetMouseButtonDown(1))
        {
            RemoveOrAddBlocks(false);
        }
        UpdateGhostBlock();
    }

    private void UpdateGhostBlock()
    {

        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
        if (hit)
        {
            
            if (ghostBlockInstance == null)
            {
                ghostBlockInstance = Instantiate(ghostCube, hitInfo.collider.transform.position+hitInfo.normal * grid.scale,Quaternion.identity);
                ghostBlockInstance.transform.localScale = ghostBlockInstance.transform.localScale*grid.scale;
            }
            ghostBlockInstance.active = true;
            ghostBlockInstance.transform.position = hitInfo.collider.transform.position + hitInfo.normal * grid.scale *.5f;
            Debug.Log(hitInfo.normal);
            ghostBlockInstance.transform.localScale = new Vector3((hitInfo.normal.x > 0.1f || hitInfo.normal.x<-0.1f) ? grid.scale*0.25f : grid.scale,
                                                                    (hitInfo.normal.y > 0.1f || hitInfo.normal.y < -0.1f) ? grid.scale * 0.25f : grid.scale,
                                                                    (hitInfo.normal.z > 0.1f || hitInfo.normal.z < -0.1f) ? grid.scale * 0.25f : grid.scale);

        }
        else
        {
            if (ghostBlockInstance != null)
            {
                ghostBlockInstance.active = false;
            }
        }
    }

    void RemoveOrAddBlocks(bool createOrDestroy)
    {
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
        if (hit)
        {
            Vector3 position = new Vector3( Mathf.Round(hitInfo.collider.transform.position.x),
                                            Mathf.Round(hitInfo.collider.transform.position.y),
                                            Mathf.Round(hitInfo.collider.transform.position.z));
            if (createOrDestroy)
            {
                position += hitInfo.normal * grid.scale;
                grid.CreateCell(Vector3Int.FloorToInt(position) / grid.scale);  
            }
            else
            {
                grid.DestroyCell(Vector3Int.FloorToInt(position) / grid.scale);
            }
        }
        else
        {
            moveCameraScript.StartRotate();
        }
    }
}
