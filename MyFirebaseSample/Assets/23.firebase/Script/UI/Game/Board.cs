using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public Cell[] cells;
    public Dictionary<string, Cell> cellDictionary = new Dictionary<string, Cell>();

    public GameObject dieMark;
    public GameObject deadMark;
    public bool isHost;
    public int turnCount;
    public bool isMyTurn;
    [SerializeField]private Text turnText;

    private void Awake()
    {
        cells = GetComponentsInChildren<Cell>();
        int cellNum = 0;
        for (int y = 0; y < 15; y++)
        {
            for (int x = 0; x < 15; x++)
            {
                cells[cellNum].coodinate = $"{(char)(x + 65)}{y+1}";
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
        if (!isMyTurn)
        {
            Debug.Log("상대방 턴입니다!");
            return;
        }

        if (cell.transform.childCount > 0)
        {
            Debug.Log("이미 돌이 놓여있습니다!");
            return;
        }
        GameObject prefab = isHost ? dieMark : deadMark;
        Instantiate(prefab, cell.transform, false);

        Turn turn = new Turn()
        {
            isHostTurn = isHost,
            coodinate = cell.coodinate
        };
        FirebaseManager.instance.SendTurn(turnCount, turn);
        isMyTurn = false;  
        turnCount++;


    }
    public void SetTurn(bool myTurn)
    {
        isMyTurn = myTurn;

        // UI 업데이트
        if (turnText != null)
        {
            turnText.text = isMyTurn ? "내 턴입니다!" : "상대방 턴입니다...";
            turnText.color = isMyTurn ? Color.green : Color.red;
        }
    }
    public void PlaceMark(bool isBlue, string coodinate)
    {
        GameObject prefab = isBlue ? dieMark : deadMark;
        Cell targetCell = cellDictionary[coodinate];
        GameObject mark = Instantiate(prefab, targetCell.transform, false);

        if (CheckWin(coodinate))
        {
            string winner = isBlue ? "다이" : "데드";
            Debug.Log($"{winner} 승리!");
            UIManager.Instance.PopupOpen<UIDialogPopup>()
                .SetPopup("게임 종료", $"{winner}이 승리했습니다!");
        }

        bool nextTurn = (isBlue != isHost);  
        SetTurn(nextTurn);

        turnCount++;

    }
    private bool CheckWin(string coordinate)
    {
        int x = coordinate[0] - 'A';
        int y = int.Parse(coordinate.Substring(1)) - 1;

        
        return CheckLine(x, y, 1, 0)  
            || CheckLine(x, y, 0, 1)  
            || CheckLine(x, y, 1, 1)  
            || CheckLine(x, y, 1, -1);
    }
    private bool CheckLine(int x, int y, int dx, int dy)
    {
        int count = 1;
        bool isBlack = cellDictionary[$"{(char)(x + 'A')}{y+1}"].transform.GetChild(0).name.Contains("Black");

        for (int dir = -1; dir <= 1; dir += 2)
        {
            for (int i = 1; i < 5; i++)
            {
                int newX = x + (dx * i * dir);
                int newY = y + (dy * i * dir);

                
                if (newX < 0 || newX >= 15 || newY < 0 || newY >= 15) break;

                string nextCoord = $"{(char)(newX + 'A')}{newY + 1}";
                Cell cell = cellDictionary[nextCoord];

                if (cell.transform.childCount == 0) break;
                if (cell.transform.GetChild(0).name.Contains("Black") != isBlack) break;

                count++;
            }
        }
        return count >= 5;
    }
}
