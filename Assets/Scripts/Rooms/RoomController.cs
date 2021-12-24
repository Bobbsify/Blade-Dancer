using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*!! TO BE PLACED IN DEFAULT ROOM PREFABS TO ALLOW FOR CORRECT ROOM GENERATION UPON LOAD !!*/

public class RoomController : MonoBehaviour
{
    [Header("Set them as same length to allow dictionary compilation (Element 0 is associated to Element 0 of Type Of Position Rleative To Transform and so on..)")]

    [SerializeField]
    [Tooltip("(Element 0 is associated to Element 0 and so on..)")]
    private List<Transform> transformsOfPositionType;

    [SerializeField]
    [Tooltip("(Element 0 is associated to Element 0 of Transforms Of Position Type and so on..)")]
    private List<PositionType> typeOfPositionRelativeToTransform;

    private Dictionary<PositionType, Vector3> associatedPositions;

    private List<Vector3> corners = new List<Vector3>();

    private const float centerMercy = 5.0f;

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

    public Vector3 GetPos(PositionType pos)
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

                    toPass = RandomRangeVectorNoCenter(bottomLeft, topCenter);
                    break;

                case PositionType.AnyRight:
                    associatedPositions.TryGetValue(PositionType.BotRightCorner, out bottomRight);

                    toPass = RandomRangeVectorNoCenter(topCenter, bottomRight);
                    break;

                case PositionType.AnyCorner:
                    toPass = corners[Random.Range(0, corners.Count)];
                    break;

                case PositionType.Random:
                    associatedPositions.TryGetValue(PositionType.BotRightCorner, out bottomRight);
                    associatedPositions.TryGetValue(PositionType.TopLeftCorner, out topLeft);

                    toPass = RandomRangeVectorNoCenter(topLeft, bottomRight);
                    break;
                default:
                    throw new System.Exception("Unkown Position Type " + pos + " as a Randomized position (Could this be a fixed position in the wrong place?)");
            }
        }
        return toPass;
    }

    public Vector3 RandomRangeVectorNoCenter(Vector3 leftmost, Vector3 rightmost)
    {
        Vector3 center;
        associatedPositions.TryGetValue(PositionType.Center, out center);

        float randomX;
        float randomZ;
        do
        {
            randomX = Random.Range(leftmost.x, rightmost.x);
        } while (randomX >= center.x - centerMercy && randomX <= center.x + centerMercy);
        do
        {
            randomZ = Random.Range(leftmost.z, rightmost.z);
        } while (randomZ >= center.z - centerMercy && randomZ <= center.z + centerMercy);
        return new Vector3(randomX, center.y, randomZ);
    }
}
