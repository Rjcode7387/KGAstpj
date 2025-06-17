#include "Sudoku.h"


int MSudoku::Size = 9;
int MSudoku::BX = 3;
int MSudoku::BY = 3;
int MSudoku::Blanks = 0;
int MSudoku::Sudoku[MAX_SIZE][MAX_SIZE] = { {0} };
int MSudoku::Puzzle[MAX_SIZE][MAX_SIZE] = { {0} };
int MSudoku::Base[MAX_SIZE][MAX_SIZE] = { {0} };

void MSudoku::shuffle(int n)
{
    int i, j, k = 0, idx;
    for (i = 0; i < n; i++) Cells[i] = Data.cells[i];

    for (i = n; i > 0; i--)
    {
        idx = rand() % i;
        Data.cells[k++] = Cells[idx];
        for (j = idx; j < i - 1; j++)
        {
            Cells[j] = Cells[j + 1];
        }
    }
}

void MSudoku::getBlanks()
{
    int i, j;
    Blanks = 0;

    for (i = 0; i < Size; i++)
    {
        for (j = 0; j < Size; j++)
        {
            if (Base[i][j] == 0) Blanks++;
        }
    }
}

void MSudoku::Init(int sdk[][MAX_SIZE])
{
    int i, j;

    for (i = 0; i < Size; i++)
    {
        for (j = 0; j < Size; j++)
        {
            sdk[i][j] = 0;
        }
    }
}

void MSudoku::Copy(int dest[][MAX_SIZE], int src[][MAX_SIZE])
{
    int i, j;

    for (i = 0; i < Size; i++)
    {
        for (j = 0; j < Size; j++)
        {
            dest[i][j] = src[i][j];
        }
    }
}
//콘솔에 텍스트 출력하기: WriteConsole과 같은 함수를 사용해 출력할 때 이 핸들을 사용합니다.
//콘솔의 속성 변경 : 콘솔의 텍스트 색상이나 크기 등을 변경할 때도 활용됩니다.
//일반적으로, GetStdHandle(STD_OUTPUT_HANDLE) 함수를 통해 STD_OUTPUT_HANDLE에 대한 핸들을 얻고, 이 핸들을 사용하여 출력 작업을 수행합니다.

void MSudoku::gotoxy(int x, int y)
{
	COORD XY = { (short)x, (short)y };
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), XY);
}

void MSudoku::setColor(int color)
{
    SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), color);
}

void MSudoku::push()
{
    Stack[++top] = Data;
}

bool MSudoku::pop()
{
    if (top == -1)return false;
    Data = Stack[top--];
	return true;
}

int MSudoku::findCandiNums(int x, int y, bool isFill)
{
    int i, j, col, row, n = 0;
    int tmp[MAX_SIZE + 1] = { 0, };

    col = (x / BX) * BX;
    row = (y / BY) * BY;

    for (i = row; i < row + BY; i++)
    {
        for (j = col; j < col + BX; j++)
        {
            tmp[Sudoku[i][j]] = 1;
        }
    }

    for (i = 0; i < Size; i++)
    {
        tmp[Sudoku[i][x]] = 1;
        tmp[Sudoku[y][i]] = 1;
    }

    for (i = 1; i <= Size; i++)
    {
        if (tmp[i] == 0)
        {
            if (isFill) Data.cells[n++] = i;
            else Cells[n++] = i;
        }
    }
    return n;
}

int MSudoku::findCell()
{
    int i, j, n, min = FINISH;

    for (i = 0; i < Size; i++)
    {
        for (j = 0; j < Size; j++)
        {
            if (Sudoku[i][j]) continue;
            n = findCandiNums(j, i, false);
            if (n == 0) return n;
            if (n < min)
            {
                min = n;
                Data.x = j;
                Data.y = i;
            }
            if (min == 1) return min;
        }
    }
    return min;
}

int MSudoku::getDigit(int ch)
{
    if (ch >= '0' && ch <= '9') ch = ch - '0';
    else if (ch >= 'A' && ch <= 'G') ch = ch - 'A' + 10;
    else if (ch >= 'a' && ch <= 'g') ch = ch - 'a' + 10;
    else ch = -1;

    return ch;
}
