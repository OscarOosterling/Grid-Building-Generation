﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    int gridSize = 3;
    public int scale;
    Dictionary<Vector3Int, Cell> cells = new Dictionary<Vector3Int, Cell>();
    public GameObject block;
    public GameObject colliderBlock;

    void Start()
    {

        //CreateCells();
        // CreateCubes();
        CreateGround();
    }

    private void FixedUpdate()
    {
        Vector3 a = this.transform.position;

        Vector3 b = new Vector3(Mathf.Floor(a.x), Mathf.Floor(a.y), Mathf.Floor(a.z));

        //this is the gameobject
        this.transform.position = b;
    }


    private void CreateGround()
    {
        GameObject ground = new GameObject();
        ground.name = "ground";
        ground.transform.parent = this.transform;

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Cube);
                plane.transform.position = new Vector3Int(i * scale, 0, j * scale);
                plane.transform.localScale *= scale;
                Vector3Int intVector = Vector3Int.FloorToInt(plane.transform.position) / scale;
                plane.name = intVector.ToString();
                cells.Add(intVector, new Cell(intVector,scale));
                plane.transform.parent = ground.transform;
            }
        }
    }

    public void CreateCell(Vector3Int position)
    {
        if (cells.ContainsKey(position))
        {
            return;
        }
        Cell cellObject = new Cell(position,scale);
        cells.Add(position, cellObject);

        CreateCube(position);
        AddPossibleNeigbours();
    }

    public void DestroyCell(Vector3Int position)
    {
        if (!cells.ContainsKey(position))
        {
            return;
        }
        cells.Remove(position);
        DestroyCube(position);
        AddPossibleNeigbours();

    }

    private void DestroyCube(Vector3Int position)
    {
        Destroy(GameObject.Find(position.ToString()));
    }

    private void CreateCube(Vector3Int position)
    {
        GameObject cube = Instantiate(colliderBlock);
        cube.transform.position = position*scale;
        cube.name = position.ToString();
        cube.transform.localScale *= scale;

        cube.transform.parent = this.transform;
        cells[position].SetCellBlockLocations();
        for (int i = 0; i < cells[position].cellBlocks.Length; i++)
        {
            GameObject cubeBlock = Instantiate(block);
            cubeBlock.transform.parent = cube.transform;
            cubeBlock.transform.localScale *= scale*.5f;
            cubeBlock.transform.localPosition = cells[position].cellBlocks[i].position;
            cubeBlock.name = "CubeBlock: " + i + " xyz: " + cells[position].cellBlocks[i].position;
        }
    }

    public void AddPossibleNeigbours()
    {
        foreach (KeyValuePair<Vector3Int, Cell> entry in cells)
        {
            int x = (int)entry.Key.x;
            int y = (int)entry.Key.y;
            int z = (int)entry.Key.z;

            Cell[] neighbours = new Cell[6];
            neighbours[0] = cells.ContainsKey(new Vector3Int(x + 1, y, z)) ? cells[new Vector3Int(x + 1, y, z)]: null;
            neighbours[1] = cells.ContainsKey(new Vector3Int(x - 1, y, z)) ? cells[new Vector3Int(x - 1, y, z)] : null;
            neighbours[2] = cells.ContainsKey(new Vector3Int(x, y + 1, z)) ? cells[new Vector3Int(x, y + 1, z)] : null;
            neighbours[3] = cells.ContainsKey(new Vector3Int(x, y - 1, z)) ? cells[new Vector3Int(x, y - 1, z)] : null;
            neighbours[4] = cells.ContainsKey(new Vector3Int(x, y, z + 1)) ? cells[new Vector3Int(x, y, z + 1)] : null;
            neighbours[5] = cells.ContainsKey(new Vector3Int(x, y, z - 1)) ? cells[new Vector3Int(x, y, z - 1)] : null;

            entry.Value.neighbours = neighbours;
            entry.Value.CalculateConfiguration();
            if(entry.Value.coordinate == new Vector3Int(0, 1, 0))
            {
                Debug.Log(entry.Value.neighbourConfigurationNumber);
            }
        }
        //ReturnAllNeighbours();
    }

    private void ReturnAllNeighbours()
    {
        foreach (KeyValuePair<Vector3Int, Cell> entry in cells)
        {
            if (entry.Key == new Vector3Int(0, 1, 0))
            {
                for (int i = 0; i < entry.Value.neighbours.Length; i++)
                {
                    if (entry.Value.neighbours[i] != null)
                    {
                        Debug.Log("Cell: " + entry.Key + " N " + i + ": " + entry.Value.neighbours[i].coordinate);
                    }
                }
            }
        }
    }
}
