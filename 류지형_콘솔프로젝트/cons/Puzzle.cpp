#include "Puzzle.h"

MPuzzle::MPuzzle()
{

}
//현재입력한 숫자가 후보군에 들어가면
//후보군에 없는 들어갈 수 없는 숫자
bool MPuzzle::checkNum()
{
    int i, n;

    n = findCandiNums(x, y, false);
    for (i = 0; i < n; i++)
    {
        if (Cells[i] == sNum) return true;
    }

    return false; 
}
//힌트를 보여주는 함수
void MPuzzle::Hint()
{
    int i, n;
    int k = 14, X = BX * BY * 4 + 2;
    int dx, dy;
    const char* str = "ABCDEFG";

    delHint();
    if (Puzzle[y][x]) return;
    n = findCandiNums(x, y, false);
    for (i = 0; i < n; i++)
    {
        dx = (Cells[i] - 1) % BX;
        dy = (Cells[i] - 1) / BY;
        gotoxy(X + dx, k + dy);
        if (Cells[i] < 10) printf("%d", Cells[i]);
        else printf("%c", str[Cells[i] - 10]);
    }
}
//보여준 힌트를 지우는 함수
void MPuzzle::delHint()
{
    int i, k = 14;

    gotoxy(BX * BY * 4 + 2, k);
    for (i = 1; i <= Size; i++)
    {
        printf(" ");
        if (i % BX == 0) gotoxy(BX * BY * 4 + 2, ++k);
    }
}
//여기서 해답을메세지 박스를 통해 출력할수있다. 출력할때 솔루션이되면 출력 안되면 출력이 안되게 만든다.
void MPuzzle::getSolution()
{
    HWND hWnd = FindWindow(NULL, L"Sudoku Puzzle");
    int i, j, idx = 0;//이중반복문에서 사용할 i,h인덱스와 솔루션 문자역 인덱스 idx
    wchar_t Sol[512]; // wchar_t로 변경 //Sol솔루션 저장하는데 사용됨
    const wchar_t* str = L"ABCDEFG"; // 10이상의 숫자를 알파벳으로 전환
    //wchar_t는 "wide character type"의 약자로, 일반적인 char 타입보다 
    // 더 많은 문자를 저장할 수 있는 데이터 타입입니다. 주로 유니코드 문자열을 처리할 때 사용됩니다.
   //함수 호출후 솔루션이 존재 확인 후 없다면 경고메세지 출력후 종료
    if (!sol.Solution())
    {
        MessageBox(hWnd, L"답이없는 문제 입니다.", L"알림", MB_OK);
        return;
    }

    for (i = 0; i < Size; i++)
    {
        for (j = 0; j < Size; j++)
        {
            if (Sudoku[i][j] < 10) Sol[idx++] = Sudoku[i][j] + L'0';
            else Sol[idx++] = str[Sudoku[i][j] - 10];
            if ((j + 1) % BX == 0) Sol[idx++] = ' ';
            if ((i + 1) % BY == 0 && j == Size - 1) Sol[idx++] = '\n';
        }
        Sol[idx++] = L'\n';
    }
    Sol[idx] = L'\0';
    MessageBox(hWnd, Sol, L"정답", MB_OK); //문제가 즉 결과가 솔루션과 맞다면 메세지 박스로 표시
    Copy(Sudoku, Puzzle);
}
//시간을 프린트해주는 함수
void MPuzzle::printTime()
{
    dsp.showTimeBlock(TIME, sec);
    ++sec;
    t1 = clock();
    gotoxy(x * 4 + 2, y * 2 + 1);
}
//지울수있게 수정하는 함수 단 바로 전에 입력한 수부터 가능
void MPuzzle::Undo()
{
    if (!pop()) return;
    undoCount--;
    Blanks++;
    x = Data.x;
    y = Data.y;
    Puzzle[y][x] = 0;
    Copy(Sudoku, Puzzle);
    dsp.setNumber(0, x * 4 + 2, y * 2 + 1);
    dsp.showTimeBlock(BLANK, Blanks);
    dsp.subMenu(undoCount);
}
//퍼즐의 상태 업데이트와 유효성 검사 입력 숫자가 기존에있는 숫자와 다를경우 색깔을 다르게 표기
void MPuzzle::inputNum()
{
    int isColor = false;

    sNum = getDigit(ch);
    if (sNum < 0 || sNum > Size) return;
    isRun = true;

    if (Puzzle[y][x])
    {
        if (cNum != sNum)
        {
            isColor = true;
            cNum = sNum;
        }
        else
        {
            isColor = false;
            cNum = 0;
        }
        dsp.printPuzzle(cNum, isColor);
        return;
    }

    if (!checkNum())
    {
        printf("%c", '\a');
        return;
    }
    Puzzle[y][x] = sNum;
    Copy(Sudoku, Puzzle);
    dsp.setNumber(sNum, x * 4 + 2, y * 2 + 1);

    Data.x = x;
    Data.y = y;
    push();
    undoCount++;
    Blanks--;

    if (Blanks == 0)
    {
        isRun = false;
        dsp.finishMessage(true);
    }
    dsp.showTimeBlock(BLANK, Blanks);
    dsp.subMenu(undoCount);
}
//키컨트롤을 담당하는 함수
void MPuzzle::keyControl()
{
    while (1)
    {
        if (clock() - t1 >= 999 && isRun) printTime();
        if (!_kbhit()) { Sleep(2); continue; }

        ch = _getch();
        if (ch == 0xE0)
        {
            ch = _getch();
            switch (ch)
            {
            case LEFT: x--; break;
            case RIGHT: x++; break;
            case UP: y--; break;
            case DOWN: y++; break;
            case DEL: Undo(); break;
            }
            getXY();
        }
        else {
            switch (ch) {
            case ESC: return;
            case 'h':
            case 'H': showHint = !showHint; break;
            case 'n':
            case 'N': nextGame(); break;
            case 'r':
            case 'R': resetGame(); break;
            case 's':
            case 'S': getSolution();  break;
            case 'u':
            case 'U': Undo();  break;
                // 새로 추가된 저장 기능
            case 'w':
            case 'W':
                if (user.Write()) {
                    gotoxy(BX * BY * 4 + 2, 16);
                    printf("저장 완료!");
                    Sleep(1000);
                    gotoxy(BX * BY * 4 + 2, 16);
                    printf("          ");
                }
                break;
            default:  // 숫자 입력 처리 추가
                if (!Base[y][x]) {  // 초기값이 아닌 경우에만 입력 가능
                    inputNum();  // 숫자 입력 처리
                }
                break;
            }
            if (showHint) Hint();
            else delHint();
            gotoxy(x * 4 + 2, y * 2 + 1);
        }
    }
}
//xy좌표를 검증및 벗어나지 않게 조정
void MPuzzle::getXY()
{
    if (x < 0) x = 0;
    else if (x >= Size) x = Size - 1;
    if (y < 0) y = 0;
    else if (y >= Size) y = Size - 1;
    gotoxy(x * 4 + 2, y * 2 + 1);
}
//저장된 퍼즐데이터를 가져오는 함수
bool MPuzzle::getData()
{
    if (levelofDiff == USERMODE)
    {
        if (!user.Read(Blanks)) return false;
    }
    else
    {
        pzl.makePuzzle(levelofDiff);
    }
    Copy(Base, Puzzle);
    return true;
}

void MPuzzle::getXY(int& x, int& y)
{
    for (y = 0; y < Size; y++)
    {
        for (x = 0; x < Size; x++)
        {
            if (Puzzle[y][x] == 0) return;
        }
    }
}
//게임에 들어갔을 인터페이스에서 사용자에게 필요한 정보제공을 도와주는 함수
void MPuzzle::initPuzzle(bool isReset)
{
    showHint = false;
    cNum = false;
    undoCount = 0;
    top = -1;

    if (!isReset)
    {
        sec = 0;
        t1 = clock();
        isRun = false;
    }
    printTime();
    dsp.finishMessage(false);
    dsp.subMenu(undoCount);
    getXY(x, y);
    gotoxy(x * 4 + 2, y * 2 + 1);
}
//게임 초기화
void MPuzzle::resetGame()
{
    Copy(Puzzle, Base);
    Copy(Sudoku, Base);
    getBlanks();
    dsp.printPuzzle();
    initPuzzle(true);
}
//다음게임으로 넘기기
void MPuzzle::nextGame()
{
    getData();
    dsp.printPuzzle();
    initPuzzle(false);
}
//이 함수로 게임에 흐름을 파악할수 있다. 난이도 선택->데이터 가져오기 ->퍼즐출력 ->ㅍ퍼즐 초기화 -> 키입력 처리
//이 과정을 반복하여 사용자가 원하는 만큼 게임을 진행할 수 있도록 설계 하였다.
void MPuzzle::playGame()
{
    while (1)
    {
        if (!(levelofDiff = dsp.menu())) break;
        if (!getData()) continue;

        dsp.printPuzzle();
        initPuzzle(false);
        keyControl();
    }
}
