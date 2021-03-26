using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cellblock
{
    public Vector3 position;
    public GameObject prefab;
    public Vector3 orientation;
    public Cellblock(Vector3 _position, Vector3 _orientation)
    {
        position = _position;
        orientation = _orientation;
    }

    public void SetPrefab(GameObject _prefab)
    {
        prefab = _prefab;
    }

}
