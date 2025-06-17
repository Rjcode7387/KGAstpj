#include "MakePuzzle.h"

//퍼즐판에 빈칸과 숫자 랜덤 생성
void MMakePuzzle::makePuzzle(int n)
{
    int j, diff = n;
    Blanks = 0;

    n = getBlank(n);
    makeArr();
    shuffle();
    while (Blanks != n)
    {
        if (!pop())
        {
            if (diff != EXTRA) return;
            else if (Blanks < 58)
            {
                Blanks = 0;
                makeArr();
                shuffle();
                pop();
            }
            else return;
        }
        fillArr(0, 1);

        for (j = 0; j < Size; j++)
        {
            sol.Solution();
            if (Compare()) continue;
            fillArr(Data.n, -1);
            break;
        }
    }
}
//스도쿠에 특정 위치에 숫자를 넣고 빈칸의 수를 관리하는 기능 퍼즐 생성 및 수정에 유용
void MMakePuzzle::fillArr(int n, int pm)
{
    Sudoku[Data.y][Data.x] = n;
    Puzzle[Data.y][Data.x] = n;
    Blanks += pm;
}
//행과 열을 조정하고 필요할 경우 백트랙킹을 통해 이전으로 돌아가기 위한 함수 
void MMakePuzzle::makeArr()
{
    int i, j, num, cnt = 0;

    memset(Sudoku, 0, sizeof(Sudoku));
    for (i = 0, j = 0; i < Size; )
    {
        num = findCandiNums(j, i, true);
        if (num)
        {
            MSudoku::shuffle(num);
            Sudoku[i][j] = Data.cells[0];
            ++j == Size ? i++, j = 0, cnt = 0 : 0;
        }

        else if (++cnt < i * 50)
        {
            memset(Sudoku[i], 0, sizeof(Sudoku[i]));
            j = 0;
        }

        else
        {
            while (i % BY)
            {
                memset(Sudoku[i--], 0, sizeof(Sudoku[i]));
            }
            memset(Sudoku[i], 0, sizeof(Sudoku[i]));
            j = cnt = 0;
        }
    }
    Copy(Base, Sudoku);   //BASE와 puzzle은 복사해서 나중에 사용할수 있게한다.
    Copy(Puzzle, Sudoku);
}
//빈공간 수 난이도 조절
int MMakePuzzle::getBlank(int n)
{
    int blk = 0;

    switch (n)
    {
    case EASY: blk = 30; break;
    case NORMAL: blk = 37; break;
    case HARD: blk = 44; break;
    case VERYHARD: blk = 51; break;
    case EXTRA: blk = 58; break;
    default: break;
    }
    blk = rand() % 7 + blk;

    return blk;
}
//퍼즐안 숫자 셔플
void MMakePuzzle::shuffle()
{
    int i, j, idx, n;
    int tmp[9 * 9];
    top = -1;

    for (i = 0; i < 9 * 9; i++)
    {
        tmp[i] = i;
    }

    for (i = 9 * 9; i > 0; i--)
    {
        idx = rand() % i;
        n = tmp[idx];
        Data.x = n % 9;
        Data.y = n / 9;
        Data.n = Base[Data.y][Data.x];
        push();
        for (j = idx; j < i - 1; j++)
        {
            tmp[j] = tmp[j + 1];
        }
    }
}

//베이스와 스도쿠를 비교해서 맞지않으면 false 맞으면 true리턴
bool MMakePuzzle::Compare()
{
    int i, j;

    for (i = 0; i < Size; i++)
    {
        for (j = 0; j < Size; j++)
        {
            if (Base[i][j] != Sudoku[i][j])
            {
                Copy(Sudoku, Puzzle);
                return false;
            }
        }
    }
    Copy(Sudoku, Puzzle);
    return true;
}




