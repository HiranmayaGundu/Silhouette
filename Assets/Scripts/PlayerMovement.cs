using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public GameObject selectedPoly = null;
    public Tilemap tileMap = null;
    public const float timeToMove = 0.2f;
    public const int gridSize = 10;
    public const float spinSpeed = 20;

    private bool isMoving = false;
    private Vector3 oldPos;
    private Vector3 targetPos;

    private bool isRotating = false;

    private Vector3Int up = new Vector3Int(1, 0, 0);
    private Vector3Int down = new Vector3Int(-1, 0, 0);
    private Vector3Int left = new Vector3Int(0, 0, 1);
    private Vector3Int right = new Vector3Int(0, 0, -1);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /******* Move *******/

    public void MoveBoxUp() //positive x
    {
        if (!isMoving && selectedPoly != null && CanMove(up))
        {
            StartCoroutine(MoveBox(up));
        }
    }

    public void MoveBoxDown() //negative x
    {
        if (!isMoving && selectedPoly != null && CanMove(down))
        {
            StartCoroutine(MoveBox(down));
        }
    }

    public void MoveBoxLeft() //positive z
    {
        if (!isMoving && selectedPoly != null && CanMove(left))
        {
            StartCoroutine(MoveBox(left));
        }
    }

    public void MoveBoxRight() //negative z
    {
        if (!isMoving && selectedPoly != null && CanMove(right))
        {
            StartCoroutine(MoveBox(right));
        }
    }

    private bool CanMove(Vector3Int dir)
    {
        return true;
    }

    private IEnumerator MoveBox(Vector3Int dir)
    {
        isMoving = true;

        float elapsedTime = 0;

        oldPos = selectedPoly.transform.position;
        targetPos = oldPos + dir * gridSize;

        while (elapsedTime < timeToMove)
        {
            selectedPoly.transform.position = Vector3.Lerp(oldPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // the final position should be exactly targetPos at the end of the animation
        selectedPoly.transform.position = targetPos;

        isMoving = false;
    }

    /******* Rotate *******/
    public void ClockwiseRotate()
    {
        if(!isRotating && selectedPoly != null && CanRotate())
        {
            StartCoroutine(RotateBox(90 * Vector3.up));
        }
    }

    public void CounterClockwiseRotate()
    {
        if (!isRotating && selectedPoly != null && CanRotate())
        {
            StartCoroutine(RotateBox(-90 * Vector3.up));
        }
    }

    private bool CanRotate()
    {
        return true;
    }

    private IEnumerator RotateBox(Vector3 dir)
    {
        isRotating = true;

        float elapsedTime = 0;

        Quaternion startRotation = selectedPoly.transform.rotation;
        Quaternion targetRotation = selectedPoly.transform.rotation * Quaternion.Euler(dir);
        while (elapsedTime < timeToMove)
        {
            selectedPoly.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        selectedPoly.transform.rotation = targetRotation;

        isRotating = false;
    }
}
