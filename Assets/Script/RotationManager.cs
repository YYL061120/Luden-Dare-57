using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GridBrushBase;

public class RotationManager : MonoBehaviour
{
    public GameObject Player;

    [Header("Rotating State")]
    public bool isRotating = false;
    public bool canRotate = true;
    public bool canRead = true;

    [Space(10), Header("Rotating Parameters")]
    public GameObject rotatingCenter;
    public Vector2Int playerFacing = new Vector2Int(0, 0);
    public float currentRotation = 0f;
    public float targetRotation = 0f;
    public float rotatingSpeed = 5f;
    public String rotatingDirection;
    public Dictionary<Vector3Int, Transform> cubeDictionary;
    public Dictionary<Vector3Int, Vector3> anchorPositionDictionary;

    [Space(10), Header("Cubes")]
    public GameObject tweakCube;
    public List<Transform> cubesList;

    private void Awake()
    {
        tweakCube = GameObject.Find("Cubes");
        foreach (Transform child in tweakCube.transform)
        {
            cubesList.Add(child);
        }
        CreateAnchorPosDictionary();
        InitCubeDictionary();
    }

    public void cubeDictionaryInitializer()
    {
        cubeDictionary = new Dictionary<Vector3Int, Transform>();

        foreach (Transform child in tweakCube.transform)
        {
            Vector3Int cubePosition = new Vector3Int(int.Parse(child.name.Substring(1, 1)), int.Parse(child.name.Substring(2, 1)), int.Parse(child.name.Substring(3, 1)));

            if (!cubeDictionary.ContainsKey(cubePosition)) cubeDictionary[cubePosition] = child;
        }
    }

    public void CreateAnchorPosDictionary()
    {
        anchorPositionDictionary = new Dictionary<Vector3Int, Vector3>();

        foreach (Transform child in cubesList)
        {
            Vector3Int coord = new Vector3Int(int.Parse(child.name.Substring(1, 1)), int.Parse(child.name.Substring(2, 1)), int.Parse(child.name.Substring(3, 1)));
            anchorPositionDictionary.Add(coord, child.position);
        }
    }

    public void InitCubeDictionary()
    {
        cubeDictionary = new Dictionary<Vector3Int, Transform>();

        for (int i = 1; i < 4 ; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                for (int k = 1; k < 4; k++)
                {
                    if (i != 2 && j != 2 && k != 2)
                    {
                        cubeDictionary.Add(new Vector3Int(i, j, k), FindNearestCube(anchorPositionDictionary[new Vector3Int(i, j, k)]));
                    }
                }
            }
        }
    }

    public void UpdateCubeDictionary()
    {
        foreach (Vector3Int coord in anchorPositionDictionary.Keys)
        {
            cubeDictionary[coord] = FindNearestCube(anchorPositionDictionary[coord]);
        }
    }

    private Transform FindNearestCube(Vector3 worldPosition)
    {
        Transform nearestCube = cubesList[0];
        foreach (Transform child in cubesList)
        {
            if (Vector3.Distance(worldPosition, child.position) < Vector3.Distance(worldPosition, nearestCube.position))
            {
                nearestCube = child;
            }
        }
        return nearestCube;
    }

    private void Update()
    {
        if (isRotating)
        {
            RotateCubes(rotatingCenter.gameObject, rotatingDirection);
        }
    }

    public void StartRotating(GameObject plane)
    {
        ///</summary>
        if (canRotate == true)
        {
            

            //Rotate horizontally
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                RotateHorizontal(Input.GetKeyDown(KeyCode.A) ? "left" : "right", plane);
            }

            //Rotate vertically
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W))
            {
                RotateVertical(Input.GetKeyDown(KeyCode.W) ? "up" : "down", plane);
            }
        }
    }

    public void RotateVertical(string direction, GameObject plane)
    {
        List<Transform> cubeSet = new List<Transform>();
        rotatingDirection = direction;
        canRotate = false;

        playerFacing = PlayerFacingChecker();
        if (playerFacing.x == 0) return;
        if (playerFacing.x > 0)
        {
            int selectedX = int.Parse(plane.transform.parent.name.Substring(1, 1)); //Exlude the cubes in other vertical levels
            foreach (Transform child in tweakCube.transform)
            {
                if (int.Parse(child.name.Substring(1, 1)) == selectedX)
                {
                    cubeSet.Add(child);
                }
            }
        }
        if (playerFacing.x < 0)
        {
            int selectedY = int.Parse(plane.transform.parent.name.Substring(2, 1));
            foreach (Transform child in tweakCube.transform)
            {
                if (int.Parse(child.name.Substring(2, 1)) == selectedY)
                {
                    cubeSet.Add(child);
                }
            }
        }
        Debug.Log("fail");


        rotatingCenter = new GameObject("Rotating Center"); //Instantiate a new gameObject at the transform of rotating center
        rotatingCenter.transform.position = CalculateRotationalCenter(cubeSet);

        //All rotating cubes are the child of rotatingCenter
        foreach (Transform cube in cubeSet)
        {
            cube.SetParent(rotatingCenter.transform);
        }

        isRotating = true;
    }

    public void RotateHorizontal(string direction, GameObject plane)
    {
        List<Transform> cubeSet = new List<Transform>();
        rotatingDirection = direction;
        canRotate = false;
        int selectedZ = int.Parse(plane.transform.parent.name.Substring(3, 1)); //Exlude the cubes in other horizontal levels

        foreach (Transform child in tweakCube.transform)
        {
            if (int.Parse(child.name.Substring(3, 1)) == selectedZ)
            {
                cubeSet.Add(child);
            }
        }

        rotatingCenter = new GameObject("Rotating Center"); //Instantiate a new gameObject at the transform of rotating center
        rotatingCenter.transform.position = CalculateRotationalCenter(cubeSet);

        //All rotating cubes are the child of rotatingCenter
        foreach (Transform cube in cubeSet)
        {
            cube.SetParent(rotatingCenter.transform);
        }

        isRotating = true;
    }

    //Calculate the position of the rotation center
    public Vector3 CalculateRotationalCenter(List<Transform> cubesSet)
    {
        Vector3 sum = Vector3.zero;

        foreach (Transform cube in cubesSet)
        {
            sum += cube.position;
        }

        return sum / cubesSet.Count;
    }

    public void RotateCubes(GameObject rotationCenter, String direction/*, float initialRotation*/)
    {
        float t = Time.deltaTime * rotatingSpeed;
        float tolerance = 0.01f;
        switch (direction)
        {
            case "left":
                float targetRotationL = -90;
                if (Mathf.Abs(currentRotation - targetRotationL) <= tolerance)
                {
                    AfterRotate(direction);
                    break;
                }
                currentRotation = Mathf.LerpAngle(currentRotation, targetRotationL, t);
                rotationCenter.transform.rotation = Quaternion.Euler(0, currentRotation, 0);
                break;

            case "right":
                float targetRotationR = 90;
                if (Mathf.Abs(currentRotation - targetRotationR) <= tolerance)
                {
                    AfterRotate(direction);
                    break;
                }
                currentRotation = Mathf.LerpAngle(currentRotation, targetRotationR, t);
                rotationCenter.transform.rotation = Quaternion.Euler(0, currentRotation, 0);
                break;
            case "up":
                float targetRotationU;
                if (playerFacing.x > 0 && playerFacing.y > 0) targetRotationU = -90;
                else if (playerFacing.x > 0 && playerFacing.y < 0) targetRotationU = 90;
                else if(playerFacing.x < 0 && playerFacing.y > 0) targetRotationU = 90;
                else if (playerFacing.x < 0 && playerFacing.y < 0) targetRotationU = -90;
                else return;

                if (Mathf.Abs(currentRotation - targetRotationU) <= tolerance)
                {
                    AfterRotate(direction);
                    break;
                }
                currentRotation = Mathf.LerpAngle(currentRotation, targetRotationU, t);
                if(playerFacing.x > 0) rotationCenter.transform.rotation = Quaternion.Euler(currentRotation, 0, 0);
                if(playerFacing.x < 0) rotationCenter.transform.rotation = Quaternion.Euler(0, 0, currentRotation);
                break;

            case "down":
                float targetRotationD;
                if (playerFacing.x > 0 && playerFacing.y > 0) targetRotationD = 90;
                else if (playerFacing.x > 0 && playerFacing.y < 0) targetRotationD = -90;
                else if (playerFacing.x < 0 && playerFacing.y > 0) targetRotationD = -90;
                else if (playerFacing.x < 0 && playerFacing.y < 0) targetRotationD = 90;
                else return;

                if (Mathf.Abs(currentRotation - targetRotationD) <= tolerance)
                {
                    AfterRotate(direction);
                    break;
                }
                currentRotation = Mathf.LerpAngle(currentRotation, targetRotationD, t);
                if (playerFacing.x > 0) rotationCenter.transform.rotation = Quaternion.Euler(currentRotation, 0, 0);
                if (playerFacing.x < 0) rotationCenter.transform.rotation = Quaternion.Euler(0, 0, currentRotation);
                break;
        }
    }

    public Vector2Int PlayerFacingChecker()
    {
        if (Vector3.Dot(Player.transform.forward, Vector3.forward) > 0.7f)
        {
            //positive z
            Debug.Log("Player is facing forward (positive Z)");
            return new Vector2Int(1,1);
        }
        else if (Vector3.Dot(Player.transform.forward, Vector3.back) > 0.7f)
        {
            //negative z
            Debug.Log("Player is facing backward (negative Z)");
            return new Vector2Int(1, -1);
        }
        else if (Vector3.Dot(Player.transform.forward, Vector3.right) > 0.7f)
        {
            //positive x
            Debug.Log("Player is facing right (positive X)");
        return new Vector2Int(-1, 1);
        }
        else if (Vector3.Dot(Player.transform.forward, Vector3.left) > 0.7f)
        {
            //negative x
            Debug.Log("Player is facing left (negative X)");
            return new Vector2Int(-1, -1);
        }
        else
        {
            Debug.Log("out of range");
            return new Vector2Int(0, 0);
        }
    }



    public void AfterRotate(String direction)
    {
        GameObject tweakCube = GameObject.Find("Cubes");
        //put the cubes back to tweak cube
        List<Transform> childRotated = new List<Transform>();
        foreach(Transform child in rotatingCenter.transform)
        {
            childRotated.Add(child);
        }
        foreach (Transform cube in childRotated)
        {
            //Renamer(direction);
            cube.SetParent(tweakCube.transform);
            Destroy(rotatingCenter.gameObject);
            isRotating = false;
            canRotate = true;
            currentRotation = 0f;
            targetRotation = 0f;
        }
        UpdateCubeDictionary();
        Renamer();
        CheckSideCompleted();
    }

    private void CheckSideCompleted()
    {
        throw new NotImplementedException();
    }

    public void Renamer()
    {
        foreach (Transform child in tweakCube.transform)
        {
            if (cubeDictionary.ContainsValue(child))
            {
                var childName = cubeDictionary.FirstOrDefault(x => x.Value == child).Key;
                child.gameObject.name = $"C{childName.x}{childName.y}{childName.z}";
            }
        }
    }
}
