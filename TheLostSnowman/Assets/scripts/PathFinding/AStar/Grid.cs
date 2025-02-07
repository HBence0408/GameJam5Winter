using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinderGrid : MonoBehaviour
{
    private Node[,] grid;
    [SerializeField] private Vector2 gridWorldSize;
    [SerializeField] private float gridCellSize;
    [SerializeField] private LayerMask walkableLayerMask;
    [SerializeField] private LayerMask unWalkableLayerMask;
    [SerializeField] private Vector3 bottomLeftCell;
    [SerializeField] private bool showMeTheMatrix;
    private static PathFinderGrid instance;
    public static PathFinderGrid Instance { get => instance; }

    private Vector3 CalculateBottomLeftCell()
    {
        return new Vector3(transform.position.x - gridWorldSize.x / 2 + gridCellSize / 2, 0, transform.position.z - gridWorldSize.y / 2 + gridCellSize / 2);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("multiple grid singletons");
        }
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        grid = new Node[Mathf.RoundToInt(gridWorldSize.x), Mathf.RoundToInt(gridWorldSize.y)];
        bottomLeftCell = CalculateBottomLeftCell();
        Vector3 currentCell = CalculateBottomLeftCell();

        for (int i = 0; i < Mathf.RoundToInt(gridWorldSize.x); i++)
        {
            currentCell.z = bottomLeftCell.z;
            for (int j = 0; j < Mathf.RoundToInt(gridWorldSize.y); j++)
            {
                bool isWalkable = true;
                Collider[] colliders = Physics.OverlapSphere(currentCell, gridCellSize / 2, unWalkableLayerMask);
                if (colliders.Length > 0)
                {
                    isWalkable = false;
                }

                grid[i, j] = new Node(isWalkable,currentCell,i,j);
                currentCell.z += gridCellSize;
            }
            currentCell.x += gridCellSize;
        }
    }

    public List<Node> FindNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int i = -1; i <= 1 ; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }
                try
                {
                    neighbours.Add(grid[node.I + i, node.J + j].Clone() as Node);
                }
                catch (IndexOutOfRangeException)
                {

                    continue;
                }  
            }
        }
        return neighbours;
    }

    public Node FindNode(Vector3 point)
    {
        try
        {
            return grid[Mathf.RoundToInt( (point.x - bottomLeftCell.x) /gridCellSize),Mathf.RoundToInt( ((point.z) - bottomLeftCell.z) / gridCellSize)].Clone() as Node;
        }
        catch (Exception)
        {
            Debug.LogWarning("an error has ocured finding the node at: " + point.x + " " + point.y);
            return null;
        } 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(CalculateBottomLeftCell() + new Vector3(gridWorldSize.x * gridCellSize / 2, 0, gridWorldSize.y * gridCellSize / 2), new Vector3(gridWorldSize.x * gridCellSize, 1, gridWorldSize.y * gridCellSize));
        Gizmos.color = Color.black;
        Gizmos.DrawCube(CalculateBottomLeftCell(), new Vector3(gridCellSize, gridCellSize, gridCellSize));
        if (showMeTheMatrix)
        {
            Gizmos.DrawCube(CalculateBottomLeftCell(), new Vector3(gridCellSize,gridCellSize,gridCellSize));
            if (grid != null)
            {
                foreach (Node n in grid)
                {
                    Gizmos.color = n.Walkable ? Color.green : Color.red;
                    Gizmos.DrawCube(n.WorldPosition, new Vector3(gridCellSize / 2, gridCellSize, gridCellSize / 2));
                }
            }
            Gizmos.color = Color.black;   
        }
    }
}
