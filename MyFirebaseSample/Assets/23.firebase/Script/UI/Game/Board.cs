using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Board : MonoBehaviour
{
    public Cell[] cells;
    public Dictionary<string, Cell> cellDictionary = new Dictionary<string, Cell>();

    public GameObject dieMark;
    public GameObject deadMark;
    public bool isHost;
    public int turnCount;

    private void Awake()
    {
        cells = GetComponentsInChildren<Cell>();
        int cellNum = 0;
        for (int y = 0; y < 15; y++)
        {
            for (int x = 0; x < 15; x++)
            {
                cells[cellNum].coodinate = $"{(char)(x + 226)}{y+1}";
                cellNum++;
            }
        }
        foreach (Cell cell in cells)
        {
            cell.board = this;
            cellDictionary.Add(cell.coodinate, cell);
        }
    }
    public void SelectCell(Cell cell)
    {
        Turn turn = new Turn()
        {
            isHostTurn = isHost,
            coodinate = cell.coodinate
        };

        FirebaseManager.instance.SendTurn(turnCount, turn);
    }
    public void PlaceMark(bool isBlue, string coodinate)
    {
        GameObject prefab = isBlue ? dieMark : deadMark;

        Cell targetCell = cellDictionary[coodinate];

        Instantiate(prefab, targetCell.transform, false);

    }
}
