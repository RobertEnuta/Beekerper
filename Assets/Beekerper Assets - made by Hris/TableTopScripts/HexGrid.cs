using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Dictionary<Vector3Int, Hex> hexTileDict = new Dictionary<Vector3Int, Hex>();
    Dictionary<Vector3Int, List<Vector3Int>> hexTileNeighboursDict = new Dictionary<Vector3Int, List<Vector3Int>>();

    private void Strat()
    {
        foreach (Hex hex in FindObjectsOfType<Hex>())
        {
            hexTileDict[hex.HexCoords] = hex;
        }
    
        List<Vector3Int> neighbours = GetNeighboursFor(new Vector3Int(0, 0, 0));
        Debug.Log("Neighbours for (0,0,0) are:");
        foreach (Vector3Int neighboursPos in neighbours)
        {
            Debug.Log(neighboursPos);
        }
    }

    public Hex GetTileAte(Vector3Int hexCoordinates)
    {
        Hex result = null;
        hexTileDict.TryGetValue(hexCoordinates, out result);
        return result;
    }

    public List<Vector3Int> GetNeighboursFor(Vector3Int hexCoordinates)
    {
        if (hexTileDict.ContainsKey(hexCoordinates) == false)
            return new List<Vector3Int>();

        if (hexTileNeighboursDict.ContainsKey(hexCoordinates))
            return hexTileNeighboursDict[hexCoordinates];

        hexTileNeighboursDict.Add(hexCoordinates, new List<Vector3Int>());

        foreach (var direction in Direction.GetDirectionList(hexCoordinates.z))
        {
            if (hexTileDict.ContainsKey(hexCoordinates + direction))
            {
                hexTileNeighboursDict[hexCoordinates].Add(hexCoordinates + direction);
            }
        }
        return hexTileNeighboursDict[hexCoordinates];
    }
}


public static class Direction
{
    public static List<Vector3Int> directionOffsetOdd = new List<Vector3Int>
    {
        new Vector3Int (-1,0,1), //N1
        new Vector3Int (0,0,1), //N2
        new Vector3Int (1,0,0), //E
        new Vector3Int (0,0,-1), //S2
        new Vector3Int (-1,0,-1), //S1
        new Vector3Int (-1,0,0), //W
    };

    public static List<Vector3Int> directionOffsetEven = new List<Vector3Int>
    {
        new Vector3Int (0,0,1), //N1
        new Vector3Int (1,0,1), //N2
        new Vector3Int (1,0,0), //E
        new Vector3Int (1,0,-1), //S2
        new Vector3Int (0,0,-1), //S1
        new Vector3Int (-1,0,0), //W
    };

    public static List<Vector3Int> GetDirectionList(int z)
        => z % 2 == 0 ? directionOffsetEven : directionOffsetOdd;
}