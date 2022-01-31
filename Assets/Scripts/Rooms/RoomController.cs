using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*!! TO BE PLACED IN DEFAULT ROOM PREFABS TO ALLOW FOR CORRECT ROOM GENERATION UPON LOAD !!*/

public class RoomController : MonoBehaviour
{
    [Header("Set them as same length to allow dictionary compilation")]

    [SerializeField]
    [Tooltip("(Element 0 is associated to Element 0 of typeOfPositionRelativeToTransform and so on..)")]
    private List<Transform> transformsOfPositionType;

    [SerializeField]
    [Tooltip("(Element 0 is associated to Element 0 of Transforms Of Position Type and so on..)")]
    private List<PositionType> typeOfPositionRelativeToTransform;

    [SerializeField]
    [Range(1.0f,20.0f)]
    private float defaultCornerPercentage = 10.0f;

    private Dictionary<PositionType, Vector3> associatedPositions = new Dictionary<PositionType, Vector3>();

    private List<Vector3> corners = new List<Vector3>();

    private const float centerMercy = 1.0f;

    private void Awake()
    {
        if (transformsOfPositionType.Count == typeOfPositionRelativeToTransform.Count)
        {
            for (int i = 0; i < transformsOfPositionType.Count; i++)
            {
                associatedPositions.Add(typeOfPositionRelativeToTransform[i], transformsOfPositionType[i].position);
                if (typeOfPositionRelativeToTransform[i].ToString().ToLower().Contains("corner"))
                {
                    corners.Add(transformsOfPositionType[i].position);
                }
            }
        }
        else
        {
            throw new System.IndexOutOfRangeException("Lists of " + this + "are not of the same lenth:" + transformsOfPositionType.Count + " & " + typeOfPositionRelativeToTransform.Count);
        }
    }

    public Vector3 GetPos(PositionType pos,Dictionary<Vector3,Vector3> positionsOccupied) //Position --> Collider width
    {
        Vector3 toPass = new Vector3(0, 0, 0);
        if ((int)pos > 3) //If position is not random
        {
            associatedPositions.TryGetValue(pos, out toPass);
        }
        else
        {
            Vector3 topCenter;
            Vector3 bottomLeft;
            Vector3 bottomRight;
            Vector3 topLeft;

            associatedPositions.TryGetValue(PositionType.TopCenter, out topCenter);
            switch (pos)
            {
                case PositionType.AnyLeft:
                    associatedPositions.TryGetValue(PositionType.BotLeftCorner, out bottomLeft);

                    toPass = RandomRangeVectorNoCenter(bottomLeft, topCenter, positionsOccupied);
                    break;

                case PositionType.AnyRight:
                    associatedPositions.TryGetValue(PositionType.BotRightCorner, out bottomRight);

                    toPass = RandomRangeVectorNoCenter(topCenter, bottomRight, positionsOccupied);
                    break;

                case PositionType.AnyCorner:
                    toPass = corners[Random.Range(0, corners.Count)];
                    break;

                case PositionType.Random:
                    associatedPositions.TryGetValue(PositionType.BotRightCorner, out bottomRight);
                    associatedPositions.TryGetValue(PositionType.TopLeftCorner, out topLeft);

                    toPass = RandomRangeVectorNoCenter(topLeft, bottomRight, positionsOccupied);
                    break;
                case PositionType.NoCorner:
                    associatedPositions.TryGetValue(PositionType.BotRightCorner, out bottomRight);
                    associatedPositions.TryGetValue(PositionType.TopLeftCorner, out topLeft);

                    float distanceX = Mathf.Abs(bottomRight.x - topLeft.x);
                    float distanceZ = Mathf.Abs(bottomRight.z - topLeft.z);

                    float xMod = distanceX * defaultCornerPercentage / 100;
                    float zMod = distanceZ * defaultCornerPercentage / 100;

                    bottomRight = new Vector3(bottomRight.x - xMod, bottomRight.y, bottomRight.z + zMod);
                    topLeft = new Vector3(topLeft.x + xMod, topLeft.y, topLeft.z - zMod);

                    toPass = RandomRangeVectorNoCenter(topLeft, bottomRight, positionsOccupied);
                    break;
                default:
                    throw new System.Exception("Unkown Position Type " + pos + " as a Randomized position (Could this be a fixed position in the wrong place?)");
            }
        }
        return toPass;
    }

    private Vector3 RandomRangeVectorNoCenter(Vector3 leftmost, Vector3 rightmost, Dictionary<Vector3,Vector3> positionsAndWidths)
    {
        Vector3 center;
        associatedPositions.TryGetValue(PositionType.Center, out center);

        float randomX;
        float randomZ;

        bool isXOk;
        bool isZOk;
        do
        {
            isXOk = true;
            randomX = Random.Range(leftmost.x, rightmost.x);
            foreach (KeyValuePair<Vector3, Vector3> positionAndWidth in positionsAndWidths) 
            {
                isXOk = !checkInBetween(randomX, positionAndWidth.Key.x - positionAndWidth.Value.x, positionAndWidth.Key.x + positionAndWidth.Value.x);
                    if (isXOk) { break; }
            }
            isXOk = isXOk && !checkInBetween(randomX,center.x - centerMercy, center.x + centerMercy); //x is in correct position
        } while (!isXOk);
        do
        {
            isZOk = true;
            randomZ = Random.Range(leftmost.z, rightmost.z);
            foreach (KeyValuePair<Vector3, Vector3> positionAndWidth in positionsAndWidths)
            {
                isZOk = !checkInBetween(randomZ, positionAndWidth.Key.z - positionAndWidth.Value.z, positionAndWidth.Key.z + positionAndWidth.Value.z);
                if (isZOk) { break; }
            }
            isZOk = isZOk && !checkInBetween(randomZ, center.z - centerMercy, center.z + centerMercy); //z is in correct position
        } while (!isZOk);

        return new Vector3(randomX, center.y, randomZ);
    }

    private bool checkInBetween(float value, float min, float max) 
    {
        if (min > max) 
        {
            float temp = min;
            min = max;
            max = temp;
        }
        return (value >= min && value <= max);
    }
}
