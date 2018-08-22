using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://en.wikipedia.org/wiki/Maze_generation_algorithm
//The depth-first search algorithm of maze generation is frequently implemented using backtracking:
//1.Make the initial cell the current cell and mark it as visited
//2.While there are unvisited cells
//  1.If the current cell has any neighbours which have not been visited
//      1.Choose randomly one of the unvisited neighbours
//      2.Push the current cell to the stack
//      3.Remove the wall between the current cell and the chosen cell
//      4.Make the chosen cell the current cell and mark it as visited
//  2.Else if stack is not empty
//      1.Pop a cell from the stack
//      2.Make it the current cell

public class BackTracker : MonoBehaviour
{
    #region Member Variables
    private Cell currentCell;
    List<Cell> visitedCells;
    Stack<Cell> cellStack = new Stack<Cell>();
    List<Cell> neighbours;
    #endregion

    #region Algorithm
    public IEnumerator BackTrack(Cell[,] Cells)
    {
        //Algorithm
        //1.Make the initial cell the current cell and mark it as visited
        MakeInitialCellCurrentCell(Cells);
        MarkCurrentCellVisited();

        //2.While there are unvisited cells
        while (visitedCells.Count < Cells.Length)
        {
            //2.1.If the current cell has any neighbours which have not been visited
            GetCurrentCellNeighbours();
            if (neighbours.Count > 0)
            {
                //This is just for the animation pause.
                yield return new WaitForSeconds(0.1f);

                //2.1.1.Choose randomly one of the unvisited neighbours
                Cell chosenNeighbour = neighbours[UnityEngine.Random.Range(0, neighbours.Count)];

                //2.1.2.Push the current cell to the stack
                cellStack.Push(currentCell);
                //2.1.3.Remove the wall between the current cell and the chosen cell
                RemoveWallBetweenCurrentCellAndChosenNeighbour(chosenNeighbour);
                //2.1.4.Make the chosen cell the current cell and mark it as visited
                currentCell = chosenNeighbour;
                MarkCurrentCellVisited();
            }
            //2.2.Else if stack is not empty
            else if (visitedCells.Count > 0)
            {
                //2.2.1.Pop a cell from the stack
                Cell cell = cellStack.Pop();
                //2.2.2.Make it the current cell
                currentCell = cell;
            }
        }
        MarkEntryAndExitPoints(Cells);
    }
    #endregion

    #region Algorithm Implementation
    private static void MarkEntryAndExitPoints(Cell[,] Cells)
    {
        Cells[0, 0].CellSprite.color = Color.red;
        Cells[(Cells.GetLength(0) - 1), (Cells.GetLength(1) - 1)].CellSprite.color = Color.red;
    }

    private void GetCurrentCellNeighbours()
    {
        neighbours = currentCell.GetNeighbours();
        for (int i = 0; i < neighbours.Count; i++)
        {
            if (visitedCells.Contains(neighbours[i]))
            {
                neighbours.RemoveAt(i);
                i--;
            }
        }
    }

    private void MarkCurrentCellVisited()
    {
        currentCell.IsVisited = true;
        visitedCells.Add(currentCell);
    }

    private void MakeInitialCellCurrentCell(Cell[,] Cells)
    {
        currentCell = Cells[0, 0];
        visitedCells = new List<Cell>();
    }

    private void RemoveWallBetweenCurrentCellAndChosenNeighbour(Cell chosenNeighbour)
    {
        currentCell.RemoveWallTowardsNeighbour(chosenNeighbour);
        chosenNeighbour.RemoveWallTowardsNeighbour(currentCell);
    }
    #endregion
}
