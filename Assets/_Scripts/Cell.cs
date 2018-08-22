using Sourav.Utilities.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [Header("Attributes")]
    private int row;
    private int column;
    private bool isVisted;

    [Header("References")]
    public Neighbours Neighbours;
    public SpriteRenderer CellSprite;
    public GameObject LeftWall;
    public GameObject RightWall;
    public GameObject TopWall;
    public GameObject BottomWall;

    //Properties
    public int Row { get { return row; } }
    public int Column { get { return column; } }
    public bool HasNeighbourLeft { get { return Neighbours.Left != null; } }
    public bool HasNeighbourRight { get { return Neighbours.Right != null; } }
    public bool HasNeighbourTop { get { return Neighbours.Top != null; } }
    public bool HasNeighbourBottom { get { return Neighbours.Bottom != null; } }
    public bool IsEdge { get { return !HasNeighbourLeft || !HasNeighbourRight || !HasNeighbourTop || !HasNeighbourBottom; } }
    public bool IsVisited
    {
        get
        {
            return isVisted;
        }
        set
        {
            isVisted = value;
            if(isVisted)
            {
                CellSprite.color = Color.gray;
            }
            else
            {
                CellSprite.color = Color.white;
            }
        }
    }

    public void SetUp(int i, int j)
    {
        row = i;
        column = j;
        IsVisited = false;
        this.name = "[ "+row+" , "+column+" ]";
    }

    public void RemoveWallTowardsNeighbour(Cell cell)
    {
        if (HasNeighbourLeft && Neighbours.Left == cell)
            OpenWall(SideType.Left);
        if (HasNeighbourRight && Neighbours.Right == cell)
            OpenWall(SideType.Right);
        if (HasNeighbourTop && Neighbours.Top == cell)
            OpenWall(SideType.Top);
        if (HasNeighbourBottom && Neighbours.Bottom == cell)
            OpenWall(SideType.Bottom);
    }

    private void OpenWall(SideType Side)
    {
        switch(Side)
        {
            case SideType.Left:
                LeftWall.Hide();
                break;

            case SideType.Right:
                RightWall.Hide();
                break;

            case SideType.Top:
                TopWall.Hide();
                break;

            case SideType.Bottom:
                BottomWall.Hide();
                break;
        }
    }

    public List<Cell> GetNeighbours()
    {
        List<Cell> NeighbourList = new List<Cell>();
        if (HasNeighbourLeft)
            NeighbourList.Add(Neighbours.Left);
        if (HasNeighbourRight)
            NeighbourList.Add(Neighbours.Right);
        if (HasNeighbourTop)
            NeighbourList.Add(Neighbours.Top);
        if (HasNeighbourBottom)
            NeighbourList.Add(Neighbours.Bottom);

        return NeighbourList;
    }

    public void SetNeighbour(Cell neighbour, SideType type)
    {
        switch(type)
        {
            case SideType.Left:
                Neighbours.Left = neighbour;
                break;

            case SideType.Right:
                Neighbours.Right = neighbour;
                break;

            case SideType.Top:
                Neighbours.Top = neighbour;
                break;

            case SideType.Bottom:
                Neighbours.Bottom = neighbour;
                break;
        }
    }
}

[Serializable]
public class Neighbours
{
    public Cell Left;
    public Cell Right;
    public Cell Top;
    public Cell Bottom;
}

public enum SideType
{
    Left,
    Right,
    Top,
    Bottom
}