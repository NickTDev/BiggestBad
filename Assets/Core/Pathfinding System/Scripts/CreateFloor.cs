using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateFloor : MonoBehaviour
{
    public GameObject floorPrefab;
    public int rows, columns;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Instantiate(floorPrefab, new Vector3(this.transform.position.x + j, this.transform.position.y, this.transform.position.z + i), Quaternion.identity, this.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
