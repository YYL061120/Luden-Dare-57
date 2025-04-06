using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnexcavedFloorGenerator : MonoBehaviour
{
    public static UnexcavedFloorGenerator unexcavedFloorGenerator;
    public GameObject UnexcavedFloors;
    public List<GameObject> theList = new List<GameObject>();
    [SerializeField] private float TheRealyYAxis = 0;
    public int maxnumberofUnexcaved;

    public float CameAmountDown;


    public GameObject wall1;
    public GameObject wall2;
    public float wallNextYpos;
    [SerializeField] private int numbeforeWalls=3;
    public void GenerateNewFloors()
    {
        if (theList.Count <= maxnumberofUnexcaved)
        {
            int generationCount = maxnumberofUnexcaved - theList.Count;
            for (int i = 0; i < generationCount; i++)
            {
                numbeforeWalls++;
                TheRealyYAxis -= 1;
                GameObject temp= Instantiate(UnexcavedFloors, new Vector3(this.transform.position.x, this.transform.position.y + TheRealyYAxis, this.transform.position.z),Quaternion.identity,this.transform);
                MouseControl.mouseControl.minY -= CameAmountDown;
                theList.Add(temp);
            }
        }
    }

    private void Awake()
    {
        wall1 = GameObject.Find("Wall1");
        wall2 = GameObject.Find("Wall2");
        unexcavedFloorGenerator = this;

        if (theList.Count <= maxnumberofUnexcaved)
        {

            int generationCount = maxnumberofUnexcaved - theList.Count;
            for (int i = 0; i < generationCount; i++)
            {
                TheRealyYAxis -= 1;
                GameObject temp = Instantiate(UnexcavedFloors, new Vector3(this.transform.position.x, this.transform.position.y + TheRealyYAxis, this.transform.position.z), Quaternion.identity, this.transform);
                theList.Add(temp);
            }
        }
    }



    private void FixedUpdate()
    {
        GenerateNewFloors();
        GenerateWalls();
    }

    public void GenerateWalls()
    {
            if (numbeforeWalls>=13)
            {
                wallNextYpos -= wall1.transform.localScale.y;
                Instantiate(wall1, new Vector3(wall1.transform.position.x, wall1.transform.position.y + wallNextYpos, wall1.transform.position.z), Quaternion.identity);
                Instantiate(wall2, new Vector3(wall2.transform.position.x, wall2.transform.position.y + wallNextYpos, wall2.transform.position.z), Quaternion.identity);
                numbeforeWalls = 0;
            }
        
    }
}
