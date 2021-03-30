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

    public bool isGround;

    public List<GameObject> prefabs = new List<GameObject>();
    public Cell(Vector3Int _coordinate, int _scale, bool _isGround)
    {
        scale = _scale;
        coordinate = _coordinate;
        isGround = _isGround;
    }

    public void SetCellBlockLocations()
    {
        float offset = 0.25f;
        cellBlocks[0] = new Cellblock(new Vector3(-offset,  -offset,    -offset),   new Vector3(0, 180, 180));
        cellBlocks[1] = new Cellblock(new Vector3(offset,   -offset,    -offset),   new Vector3(0, 90,  180));
        cellBlocks[2] = new Cellblock(new Vector3(-offset,  -offset,    offset),    new Vector3(0, -90, 180));
        cellBlocks[3] = new Cellblock(new Vector3(offset,   -offset,    offset),    new Vector3(0, 0,   180));
        cellBlocks[4] = new Cellblock(new Vector3(-offset,  offset,     -offset),   new Vector3(0, -90, 0));
        cellBlocks[5] = new Cellblock(new Vector3(offset,   offset,     -offset),   new Vector3(0, 180, 0)); 
        cellBlocks[6] = new Cellblock(new Vector3(-offset,  offset,     offset),    new Vector3(0, 0,   0));
        cellBlocks[7] = new Cellblock(new Vector3(offset,   offset,     offset),    new Vector3(0, 90,  0));
    }

    public void CalculateConfiguration()
    {
        neighbourConfigurationNumber = 0;
        for (int i = 0; i < neighbours.Length; i++)
        {
            if (neighbours[i] != null)
            {
                neighbourConfigurationNumber += (int)Mathf.Pow(2, i);
            }
        }
        if (isGround)
        {
            neighbourConfigurationNumber = 64;
        }
    }

    public void SetCellBlockPrefabs()
    {
        for (int i = 0; i < cellBlocks.Length; i++)
        {
            cellBlocks[i].SetPrefab(prefabs[Configurations.configurationDictionary[neighbourConfigurationNumber][i]]);
        }
    }
}

public static class Configurations
{
    public static Dictionary<int, int[]> configurationDictionary = new Dictionary<int, int[]>()
    {
       {0,new int[]{0,0,0,0,0,0,0,0} },
        {1,new int[]{0,3,0,2,0,2,0,3} },
        {2,new int[]{2,0,3,0,3,0,2,0} },
        {3,new int[]{2,3,3,2,3,2,2,3} },
        {4,new int[]{0,0,0,0,1,1,1,1} },
        {5,new int[]{0,3,0,2,1,5,1,4} },
        {6,new int[]{2,0,3,0,4,1,5,1} },
        {7,new int[]{2,3,3,2,4,5,5,4} },
        {8,new int[]{1,1,1,1,0,0,0,0} },
        {9,new int[]{1,4,1,5,0,2,0,3} },
        {10,new int[]{5,1,4,1,3,0,2,0} },
        {11,new int[]{5,4,4,5,3,2,2,3} },
        {12,new int[]{1,1,1,1,1,1,1,1} },
        {13,new int[]{1,4,1,5,1,5,1,4} },
        {14,new int[]{5,1,4,1,4,1,5,1} },
        {15,new int[]{5,4,4,5,4,5,5,4} },
        {16,new int[]{0,0,2,3,0,0,3,2} },
        {17,new int[]{0,3,2,6,0,2,3,6} },
        {18,new int[]{2,0,6,3,3,0,6,2} },
        {19,new int[]{2,3,6,6,3,2,6,6} },
        {20,new int[]{0,0,2,3,1,1,4,5} },
        {21,new int[]{0,3,2,6,1,5,4,7} },
        {22,new int[]{2,0,6,3,4,1,7,5} },
        {23,new int[]{2,3,6,6,4,5,7,7} },
        {24,new int[]{1,1,5,4,0,0,3,2} },
        {25,new int[]{1,4,5,7,0,2,3,6} },
        {26,new int[]{5,1,7,4,3,0,6,2} },
        {27,new int[]{5,4,7,7,3,2,6,6} },
        {28,new int[]{1,1,5,4,1,1,4,5} },
        {29,new int[]{1,4,5,7,1,5,4,7} },
        {30,new int[]{5,1,7,4,4,1,7,5} },
        {31,new int[]{5,4,7,7,4,5,7,7} },
        {32,new int[]{3,2,0,0,2,3,0,0} },
        {33,new int[]{3,6,0,2,2,6,0,3} },
        {34,new int[]{6,2,3,0,6,3,2,0} },
        {35,new int[]{6,6,3,2,6,6,2,3} },
        {36,new int[]{3,2,0,0,5,4,1,1} },
        {37,new int[]{3,6,0,2,5,7,1,4} },
        {38,new int[]{6,2,3,0,7,4,5,1} },
        {39,new int[]{6,6,3,2,7,7,5,4} },
        {40,new int[]{4,5,1,1,2,3,0,0} },
        {41,new int[]{4,7,1,5,2,6,0,3} },
        {42,new int[]{7,5,4,1,6,3,2,0} },
        {43,new int[]{7,7,4,5,6,6,2,3} },
        {44,new int[]{4,5,1,1,5,4,1,1} },
        {45,new int[]{4,7,1,5,5,7,1,4} },
        {46,new int[]{7,5,4,1,7,4,5,1} },
        {47,new int[]{7,7,4,5,7,7,5,4} },
        {48,new int[]{3,2,2,3,2,3,3,2} },
        {49,new int[]{3,6,2,6,2,6,3,6} },
        {50,new int[]{6,2,6,3,6,3,6,2} },
        {51,new int[]{6,6,6,6,6,6,6,6} },
        {52,new int[]{3,2,2,3,5,4,4,5} },
        {53,new int[]{3,6,2,6,5,7,4,7} },
        {54,new int[]{6,2,6,3,7,4,7,5} },
        {55,new int[]{6,6,6,6,7,7,7,7} },
        {56,new int[]{4,5,5,4,2,3,3,2} },
        {57,new int[]{4,7,5,7,2,6,3,6} },
        {58,new int[]{7,5,7,4,6,3,6,2} },
        {59,new int[]{7,7,7,7,6,6,6,6} },
        {60,new int[]{4,5,5,4,5,4,4,5} },
        {61,new int[]{4,7,5,7,5,7,4,7} },
        {62,new int[]{7,5,7,4,7,4,7,5} },
        {63,new int[]{7,7,7,7,7,7,7,7} },
        {64,new int[]{4,4,4,4,4,4,4,4} }
    };
}
