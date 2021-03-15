using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Cell
{
    public Vector3Int coordinate;
    public Cellblock[] cellBlocks = new Cellblock[8];
    public Cell[] neighbours = new Cell[6];
    public int scale;
    public int neighbourConfigurationNumber;
    public Cell(Vector3Int _coordinate, int _scale)
    {
        scale = _scale;
        coordinate = _coordinate;
    }

    public void SetCellBlockLocations()
    {
        Vector3Int cPos = coordinate;
        float pos = 0.5f;
        float offset = 0.25f;


        cellBlocks[0] = new Cellblock(new Vector3(-offset, -offset, -offset));
        cellBlocks[1] = new Cellblock(new Vector3(pos - offset, -offset, -offset));
        cellBlocks[2] = new Cellblock(new Vector3(-offset, -offset, pos - offset));
        cellBlocks[3] = new Cellblock(new Vector3(pos - offset, -offset, pos - offset));
        cellBlocks[4] = new Cellblock(new Vector3(-offset, pos - offset, -offset));
        cellBlocks[5] = new Cellblock(new Vector3(pos - offset, pos - offset, -offset));
        cellBlocks[6] = new Cellblock(new Vector3(-offset, pos - offset, pos - offset));
        cellBlocks[7] = new Cellblock(new Vector3(pos - offset, pos - offset, pos - offset));
    }

    public void CalculateConfiguration()
    {
        neighbourConfigurationNumber = 0;
        if (neighbours[0] != null)
        {
            neighbourConfigurationNumber++;
        }
        if (neighbours[1] != null)
        {
            neighbourConfigurationNumber+=2;
        }
        if (neighbours[2] != null)
        {
            neighbourConfigurationNumber += 4;
        }
        if (neighbours[3] != null)
        {
            neighbourConfigurationNumber += 8;
        }
        if (neighbours[4] != null)
        {
            neighbourConfigurationNumber += 16;
        }
        if (neighbours[5] != null)
        {
            neighbourConfigurationNumber += 32;
        }
    }

}
