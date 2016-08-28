using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShieldCreator : MonoBehaviour {

    private List<float> childXPos;
    private List<float> childYPos;
    //private Vector3[] shieldVis;

    public BoxCollider2D shield;
    //private LineRenderer shieldLine;



    Vector2 botLeft;
    Vector2 botRight;
    Vector2 topLeft;
    Vector2 topRight;

    float xlength;
    float ylength;
    float xOffset;
    float yOffset;

    public float shieldWidth; 


    void Start()
    {

        childXPos = new List<float>();
        childYPos = new List<float>();
        //shieldVis = new Vector3[5];


        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.CompareTag("ShieldGenerator") == true && shield.gameObject.activeSelf == false )
            {
                shield.gameObject.SetActive(true);
            }
        }

        

        

        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            float x = child.position.x;
            float y = child.position.y;

            childXPos.Add(x);
            childYPos.Add(y);


        }

        botLeft = new Vector2(FindExtreme(childXPos, false) - shieldWidth, FindExtreme(childYPos, false) - shieldWidth);
        botRight = new Vector2(FindExtreme(childXPos, true) + shieldWidth, FindExtreme(childYPos, false) - shieldWidth);
        topLeft = new Vector2(FindExtreme(childXPos, false) - shieldWidth, FindExtreme(childYPos, true) + shieldWidth);
        topRight = new Vector2(FindExtreme(childXPos, true) + shieldWidth, FindExtreme(childYPos, true) + shieldWidth);

        //shieldVis[4] = (new Vector3(FindExtreme(childXPos, false) - shieldWidth - shield.transform.position.x, FindExtreme(childYPos, false) - shieldWidth - shield.transform.position.y, 10));
        //shieldVis[3] = (new Vector3(FindExtreme(childXPos, true) + shieldWidth - shield.transform.position.x, FindExtreme(childYPos, false) - shieldWidth - shield.transform.position.y, 10));
        //shieldVis[1] = (new Vector3(FindExtreme(childXPos, false) - shieldWidth - shield.transform.position.x, FindExtreme(childYPos, true) + shieldWidth - shield.transform.position.y, 10));
        //shieldVis[2] = (new Vector3(FindExtreme(childXPos, true) + shieldWidth - shield.transform.position.x, FindExtreme(childYPos, true) + shieldWidth - shield.transform.position.y, 10));
        //shieldVis[0] = (new Vector3(FindExtreme(childXPos, false) - shieldWidth - shield.transform.position.x, FindExtreme(childYPos, false) - shieldWidth - shield.transform.position.y, 10));

        float xlength = Mathf.Abs(topLeft.x - topRight.x);
        float ylength = Mathf.Abs(topLeft.y - botLeft.y);

        float xOffset = (topLeft.x + topRight.x) / 2f;
        float yOffset = (topLeft.y + botLeft.y) / 2f;

        xOffset -= shield.transform.position.x;
        yOffset -= shield.transform.position.y;


        shield.offset = new Vector2(xOffset, yOffset);
        shield.size = new Vector2(xlength, ylength);

        //shieldLine = shield.GetComponent<LineRenderer>();
        //shieldLine.SetVertexCount(5);
        //shieldLine.SetWidth(0.5f, 0.5f);
        //shieldLine.SetPositions(shieldVis);

    }



    float FindExtreme(List<float> list, bool maxtrueminfalse)
    {
        float result = list[0];

        if (maxtrueminfalse == true)
        {
            foreach (float a in list)
            {
                if (a > result)
                {
                    result = a;
                }
            }

        }

        if (maxtrueminfalse == false)
        {
            foreach (float a in list)
            {
                if (a < result)
                {
                    result = a;
                }
            }
        }

        return result;

    }


}
