using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBlock : MonoBehaviour
{
    Grid grid;
    public GameObject gridObject;

    public MoveCamera moveCameraScript;

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
    }

    void RemoveOrAddBlocks(bool createOrDestroy)
    {
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
        if (hit)
        {
            Vector3 position = new Vector3(Mathf.Round(hitInfo.collider.transform.position.x),
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
