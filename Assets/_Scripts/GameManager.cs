using Sourav.Utilities.Scripts.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Cell CellPrefab;
    public Transform Holder;
    public GameObject Algorithm;

    public int Width, Height;
    private Cell[,] Cells;
    private List<GameObject> CellObjects = new List<GameObject>();

    [Button("",ButtonWorkType.RunTimeOnly)]
    public void GenerateGrid()
    {
        DestroyAnyPreviousCells();
        InstantiateCells();
        SetNeighbours();
        GenerateMaze();
    }

    private void GenerateMaze()
    {
        StartCoroutine(Algorithm.GetComponent<BackTracker>().BackTrack(Cells));
    }

    private void InstantiateCells()
    {
        Cells = new Cell[Height, Width];
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                Cells[i, j] = Instantiate(CellPrefab, new Vector3(i, j, 0), Quaternion.identity) as Cell;
                Cells[i, j].SetUp(i, j);
                Cells[i, j].transform.parent = Holder;
                CellObjects.Add(Cells[i, j].gameObject);
            }
        }
    }

    private void SetNeighbours()
    {
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                if(i != Height - 1)
                {
                    Cells[i, j].SetNeighbour(Cells[i + 1, j], SideType.Right);
                }
                if(i != 0)
                {
                    Cells[i, j].SetNeighbour(Cells[i - 1, j], SideType.Left);
                }
                if(j != Width - 1)
                {
                    Cells[i, j].SetNeighbour(Cells[i, j + 1], SideType.Top);
                }
                if (j != 0)
                {
                    Cells[i, j].SetNeighbour(Cells[i, j - 1], SideType.Bottom);
                }
            }
        }
    }

    private void DestroyAnyPreviousCells()
    {
        for (int i = 0; i < CellObjects.Count; i++)
        {
            Destroy(CellObjects[i]);
        }
        CellObjects.Clear();
    }
}
