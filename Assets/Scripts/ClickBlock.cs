using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBlock : MonoBehaviour
{
    Grid grid;
    public GameObject gridObject;

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
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {

                Vector3 position = new Vector3(Mathf.Round(hitInfo.collider.transform.position.x),
                                                Mathf.Round(hitInfo.collider.transform.position.y),
                                                Mathf.Round(hitInfo.collider.transform.position.z)) + hitInfo.normal * grid.scale;
                Vector3Int worldPosition = Vector3Int.FloorToInt(position);
                grid.CreateCell(worldPosition / grid.scale);
                Debug.Log(worldPosition / grid.scale);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                //Debug.Log("clicked"+hitInfo.collider.transform.position);

                Vector3 position = new Vector3(Mathf.Round(hitInfo.collider.transform.position.x),
                                                Mathf.Round(hitInfo.collider.transform.position.y),
                                                Mathf.Round(hitInfo.collider.transform.position.z));
                Vector3Int intPosition = Vector3Int.FloorToInt(position);
                grid.DestroyCell(intPosition / grid.scale);
            }
        }
    }
}
