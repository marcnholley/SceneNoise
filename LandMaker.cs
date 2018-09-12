using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMaker : MonoBehaviour {

    public GameObject platformType;

    public int size = 50;

    public float scale = 7.00f;

    public bool enableHeight = true;

    public float scaleModifier = 5f;

	// Use this for initialization
	void Start () {

        for (var x = 0; x < size; x++)
        {
            for (var z = 0; z < size; z++)
            {
                var c = Instantiate(platformType, new Vector3(x, 0, z), Quaternion.identity) as GameObject;

                c.transform.parent = transform;
            }
        }
	}

    // Update is called once per frame
    void Update() {

        updateTransform();
        checkMouseClick();
    }

   void updateTransform()
    {
        foreach(Transform child in transform)
        {
            var height = Mathf.PerlinNoise(child.transform.position.x / scale,
                                           child.transform.position.z / scale);

            setMatColor(child, height);

            if (enableHeight)
                applyHeight(child, height);
            
        }
    }

    void setMatColor(Transform child, float height)
    {
        child.GetComponent<Renderer>().material.color = new Color(height, height, height, height);
    }

    void applyHeight(Transform child, float height)
    {
        var yValue = Mathf.RoundToInt(height * scaleModifier);

        var newVec3 = new Vector3(child.transform.position.x, yValue, child.transform.position.z);

        child.transform.position = newVec3;
    }

    void checkMouseClick()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 10f))
        {
            if (Input.GetMouseButtonDown(1))
                Destroy(hit.transform.gameObject);
            else if(Input.GetMouseButtonDown(0))
            {
                createCube(hit);
            }
        }
    }

    void createCube(RaycastHit hit)
    {
        int triIndex = hit.triangleIndex;
        GameObject clone;

        Debug.Log(hit.triangleIndex);

        switch (triIndex)
        {
            case 0:
            case 1:

                clone = Instantiate(
                    platformType,
                    new Vector3(hit.transform.position.x,
                                 hit.transform.position.y,
                                (hit.transform.position.z + 1f)),
                    Quaternion.identity) as GameObject;
                return;
            case 2:
            case 3:

                clone = Instantiate(
                    platformType,
                    new Vector3(hit.transform.position.x,
                                 hit.transform.position.y + 1f,
                                (hit.transform.position.z)),
                    Quaternion.identity) as GameObject;
                return;
            case 4:
            case 5:

                clone = Instantiate(
                    platformType,
                    new Vector3(hit.transform.position.x,
                                 hit.transform.position.y,
                                (hit.transform.position.z - 1f)),
                    Quaternion.identity) as GameObject;
                return;
            case 6:
            case 7:

                clone = Instantiate(
                    platformType,
                    new Vector3(hit.transform.position.x,
                                 hit.transform.position.y - 1f,
                                (hit.transform.position.z)),
                    Quaternion.identity) as GameObject;
                return;
            case 8:
            case 9:

                clone = Instantiate(
                    platformType,
                    new Vector3(hit.transform.position.x - 1f,
                                 hit.transform.position.y,
                                (hit.transform.position.z)),
                    Quaternion.identity) as GameObject;
                return;
            case 10:
            case 11:

                clone = Instantiate(
                    platformType,
                    new Vector3(hit.transform.position.x + 1f,
                                 hit.transform.position.y,
                                (hit.transform.position.z)),
                    Quaternion.identity) as GameObject;
                return;
            default:
                return;
        }

    }
}
