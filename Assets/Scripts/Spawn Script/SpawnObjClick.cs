using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjClick : MonoBehaviour
{
    Ray myRay;
    RaycastHit hit;

    public GameObject Cube;
    public GameObject Sphere;
    public GameObject Cylinder;

    public bool spawnCube = false;
    public bool spawnSphere = false;
    public bool spawnCylinder = false;


    void Update()
    {
        myRay = Camera.main.ScreenPointToRay(Input.mousePosition); // Tells the ray that it will come from the center of the main camera (Vector direction)

        if(spawnCube == true)
        {
            CubeSpawn();
        }
        if(spawnSphere == true)
        {
            SphereSpawn();
        }
        if(spawnCylinder == true)
        {
            CylinderSpawn();
        }
    }

    public void OnButtonClickCube()
    {
            spawnCube = true;
            spawnSphere = false;
            spawnCylinder = false;

    }
    public void OnButtonClickSphere()
    {
        spawnCube = false;
        spawnSphere = true;
        spawnCylinder = false;
    }
    public void OnButtonClickCylinder()
    {
        spawnCube = false;
        spawnSphere = false;
        spawnCylinder = true;
    }

    public void CubeSpawn()
    {
        if (Physics.Raycast(myRay, out hit))
        {
            if (Input.GetMouseButtonUp(0))
            {
                Instantiate(Cube, hit.point, Quaternion.identity); //creates prefab wherever the user clicked
                                                                                  //
                spawnCube = false;
            }
        }
    }
    public void SphereSpawn()
    {
        if (Physics.Raycast(myRay, out hit))
        {
            if (Input.GetMouseButtonUp(0))
            {
                Instantiate(Sphere, hit.point, Quaternion.identity); //creates prefab wherever the user clicked
                                                                                  //
                spawnSphere = false;
            }
        }
    }
    public void CylinderSpawn()
    {
        if (Physics.Raycast(myRay, out hit))
        {
            if (Input.GetMouseButtonUp(0))
            {
                Instantiate(Cylinder, hit.point, Quaternion.identity); //creates prefab wherever the user clicked
                                                                                  //
                spawnCylinder = false;
            }
        }
    }

}
