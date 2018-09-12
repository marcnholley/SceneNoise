using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMaker : MonoBehaviour {

    public GameObject platformType;

    public int size = 50;

    public float scale = 7.00f;

    public float scaleModifier = 5f;

    public float offSetHeight = 7f;

    // Use this for initialization
    void Start () {

        for (var x = 0; x < size; x++)
        {
            for (var z = 0; z < size; z++)
            {
                var c = Instantiate(platformType, new Vector3(transform.position.x + x, 0, transform.position.z + z), Quaternion.identity) as GameObject;

                c.transform.parent = transform;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

        moveTransform();
	}

    void moveTransform()
    {
        var height = scaleModifier * Mathf.PerlinNoise(Time.time + (transform.position.x * scale)
                                                       ,Time.time + (transform.position.z * scale));

        var newVector = new Vector3(transform.position.x, height, transform.position.z);

        transform.position = newVector;

        foreach(Transform child in transform)
        {
            height = scaleModifier * Mathf.PerlinNoise(Time.time + (child.transform.position.x * scale)
                                                       ,Time.time + (child.transform.position.z * scale));

            setMatColor(child, height);
            applyHeight(child, height);
        }
    }

    void setMatColor(Transform child, float height)
    {
        child.GetComponent<Renderer>().material.color = new Color (0, height, height, 0);
    }

    void applyHeight(Transform child, float height)
    {
        var newVector = new Vector3(child.transform.position.x, height + offSetHeight, child.transform.position.z);

        child.transform.position = newVector;
    }


}
