using UnityEngine;
using System.Collections;

public class DoorSwitch : MonoBehaviour {

    public GameObject LeftDoor;
    public GameObject RightDoor;

    public float CloseSpeed;
    public bool Closed = true;
   

    private bool DoneSwitch = true;
    private BoxCollider2D Col2d;


    public bool PressToSwitch;

    void Start ()
    {
        Col2d = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (PressToSwitch)
        {
            Switch();
            PressToSwitch = false;
        }


        if (Closed)
            Col2d.enabled = true;
        else
            Col2d.enabled = false;
        
    }

    public void Switch ()
    {
        if (DoneSwitch)
        {
            if (Closed)
                Open();
            else
                Close();
        }

    }

    public void Open ()
    {
        DoneSwitch = false;
        StartCoroutine(GradualMove(LeftDoor, -transform.right, 0.5f, CloseSpeed));
        StartCoroutine(GradualMove(RightDoor, -transform.right, 0.5f, CloseSpeed));
        Closed = false;
    }

    public void Close ()
    {
        DoneSwitch = false;
        StartCoroutine(GradualMove(LeftDoor, transform.right, 0.5f, CloseSpeed));
        StartCoroutine(GradualMove(RightDoor, transform.right, 0.5f, CloseSpeed));
        Closed = true;

    }

    IEnumerator GradualMove (GameObject objectToMove, Vector3 direction, float distance, float moveTime)
    {
        float elapsedSteps = 0f;

        while (elapsedSteps < distance)
        {
            Vector3 moveDelta = Vector3.zero;
            
            moveDelta = direction * Time.deltaTime * distance / moveTime;

            objectToMove.transform.Translate(moveDelta);

            elapsedSteps += Time.deltaTime * distance / moveTime;

            yield return null;

        }

        DoneSwitch = true;
    }





}
