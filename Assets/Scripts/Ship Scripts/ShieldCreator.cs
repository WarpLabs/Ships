using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ShieldCreator : MonoBehaviour {

    private List<float> childXPos;
    private List<float> childYPos;
    private Vector3[] shieldCorners;

    public BoxCollider2D shield;
    private LineRenderer shieldLineRenderer;

    private float a = 0;
    private Color color = new Color(0.369f, 0.957f, 1.0f);

    Vector2 botLeft;
    Vector2 botRight;
    Vector2 topLeft;
    Vector2 topRight;

    float xlength;
    float ylength;
    float xOffset;
    float yOffset;

    public float shieldWidth; 

	public bool CreateShieldOnStart;

	public bool CreateShield;

	void Start () {

		if (CreateShieldOnStart)
			StartCoroutine (CreateShieldOutline ());

	}

	void Update () {

		if (CreateShield) {
			StartCoroutine (CreateShieldOutline ());
			CreateShield = false;
		}

	}

    IEnumerator CreateShieldOutline()
    {
        //Initialize Arrays and Lists
        childXPos = new List<float>();
        childYPos = new List<float>();
        shieldCorners = new Vector3[10];

        //Check for shieldGenerator
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.CompareTag("ShieldGenerator") == true && shield.gameObject.activeSelf == false )
            {
                shield.gameObject.SetActive(true);
            }
        }    

        //Add each ship tile's transform.x and transform.y into two seperate lists
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.CompareTag("Wall"))
            {
                float x = child.position.x;
                float y = child.position.y;

                childXPos.Add(x);
                childYPos.Add(y);
            }
            
        }

        //Find the corners of the ship by determining max/min values and then adding/subtracting shield width
        botLeft = new Vector2(FindExtreme(childXPos, false), FindExtreme(childYPos, false));
        botRight = new Vector2(FindExtreme(childXPos, true), FindExtreme(childYPos, false));
        topLeft = new Vector2(FindExtreme(childXPos, false), FindExtreme(childYPos, true));
        topRight = new Vector2(FindExtreme(childXPos, true), FindExtreme(childYPos, true));

        //Determine length of ship and set size of shield collider
        float xlength = Mathf.Abs(topLeft.x - topRight.x);
        float ylength = Mathf.Abs(topLeft.y - botLeft.y);
        shield.size = new Vector2(xlength, ylength);

        //Determine centre of ship tile's and set centre of shield transform (and shield box collider by extension)
        float xOffset = (topLeft.x + topRight.x) / 2f;
        float yOffset = (topLeft.y + botLeft.y) / 2f;
        shield.transform.position = new Vector3(xOffset,yOffset, 0);

        //Sets points of corners into an array of Vector3s for LineRenderer, doubles back to reduce "triangle effect" of lines
        shieldCorners[0] = ConvertToVector3Local(botLeft, xOffset, yOffset);
        shieldCorners[1] = ConvertToVector3Local(botRight, xOffset, yOffset);
        shieldCorners[2] = ConvertToVector3Local(topRight, xOffset, yOffset);
        shieldCorners[3] = ConvertToVector3Local(topLeft, xOffset, yOffset);
        shieldCorners[4] = ConvertToVector3Local(botLeft, xOffset, yOffset);

        shieldCorners[5] = ConvertToVector3Local(botLeft, xOffset, yOffset);
        shieldCorners[6] = ConvertToVector3Local(topLeft, xOffset, yOffset);
        shieldCorners[7] = ConvertToVector3Local(topRight, xOffset, yOffset);
        shieldCorners[8] = ConvertToVector3Local(botRight, xOffset, yOffset);
        shieldCorners[9] = ConvertToVector3Local(botLeft, xOffset, yOffset);

        shieldLineRenderer = shield.GetComponent<LineRenderer>();
        shieldLineRenderer.SetVertexCount(shieldCorners.Length);        
        shieldLineRenderer.SetPositions(shieldCorners);

        //Brings up transparency of shield gradually, activates collider at 70% opacity
        while(a != 1)
        {
            a += 0.1f;
            color.a = a;
            shieldLineRenderer.SetColors(color, color);

            yield return new WaitForSeconds(0.15f);

            if (a >= 0.7f)
            {
                shield.GetComponent<BoxCollider2D>().enabled = true;                
            }
        }
        


    }


    //Converts Vector2 to Vector3 with Z-Coordinate as 0.0f, also accounts for offset
    Vector3 ConvertToVector3Local(Vector2 vector2, float xOff, float yOff)
    {   
        float xCoord = vector2.x - xOff;
        float yCoord = vector2.y - yOff;

        //Debug.Log(new Vector3(xCoord, yCoord, 0));
        return new Vector3(xCoord, yCoord, 0);
    }

    //Finds max/min value in a list and then adds/subtracts shieldWidth
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
            result += shieldWidth; 
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
            result -= shieldWidth;
        }

        return result;
    }


}
