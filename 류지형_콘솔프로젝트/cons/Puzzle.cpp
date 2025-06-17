#include "Puzzle.h"

MPuzzle::MPuzzle()
{

}
//�����Է��� ���ڰ� �ĺ����� ����
//�ĺ����� ���� �� �� ���� ����
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
//��Ʈ�� �����ִ� �Լ�
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
//������ ��Ʈ�� ����� �Լ�
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
//���⼭ �ش����޼��� �ڽ��� ���� ����Ҽ��ִ�. ����Ҷ� �ַ���̵Ǹ� ��� �ȵǸ� ����� �ȵǰ� �����.
void MPuzzle::getSolution()
{
    HWND hWnd = FindWindow(NULL, L"Sudoku Puzzle");
    int i, j, idx = 0;//���߹ݺ������� ����� i,h�ε����� �ַ�� ���ڿ� �ε��� idx
    wchar_t Sol[512]; // wchar_t�� ���� //Sol�ַ�� �����ϴµ� ����
    const wchar_t* str = L"ABCDEFG"; // 10�̻��� ���ڸ� ���ĺ����� ��ȯ
    //wchar_t�� "wide character type"�� ���ڷ�, �Ϲ����� char Ÿ�Ժ��� 
    // �� ���� ���ڸ� ������ �� �ִ� ������ Ÿ���Դϴ�. �ַ� �����ڵ� ���ڿ��� ó���� �� ���˴ϴ�.
   //�Լ� ȣ���� �ַ���� ���� Ȯ�� �� ���ٸ� ���޼��� ����� ����
    if (!sol.Solution())
    {
        MessageBox(hWnd, L"���̾��� ���� �Դϴ�.", L"�˸�", MB_OK);
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
    MessageBox(hWnd, Sol, L"����", MB_OK); //������ �� ����� �ַ�ǰ� �´ٸ� �޼��� �ڽ��� ǥ��
    Copy(Sudoku, Puzzle);
}
//�ð��� ����Ʈ���ִ� �Լ�
void MPuzzle::printTime()
{
    dsp.showTimeBlock(TIME, sec);
    ++sec;
    t1 = clock();
    gotoxy(x * 4 + 2, y * 2 + 1);
}
//������ְ� �����ϴ� �Լ� �� �ٷ� ���� �Է��� ������ ����
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
//������ ���� ������Ʈ�� ��ȿ�� �˻� �Է� ���ڰ� �������ִ� ���ڿ� �ٸ���� ������ �ٸ��� ǥ��
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
//Ű��Ʈ���� ����ϴ� �Լ�
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
                // ���� �߰��� ���� ���
            case 'w':
            case 'W':
                if (user.Write()) {
                    gotoxy(BX * BY * 4 + 2, 16);
                    printf("���� �Ϸ�!");
                    Sleep(1000);
                    gotoxy(BX * BY * 4 + 2, 16);
                    printf("          ");
                }
                break;
            default:  // ���� �Է� ó�� �߰�
                if (!Base[y][x]) {  // �ʱⰪ�� �ƴ� ��쿡�� �Է� ����
                    inputNum();  // ���� �Է� ó��
                }
                break;
            }
            if (showHint) Hint();
            else delHint();
            gotoxy(x * 4 + 2, y * 2 + 1);
        }
    }
}
//xy��ǥ�� ������ ����� �ʰ� ����
void MPuzzle::getXY()
{
    if (x < 0) x = 0;
    else if (x >= Size) x = Size - 1;
    if (y < 0) y = 0;
    else if (y >= Size) y = Size - 1;
    gotoxy(x * 4 + 2, y * 2 + 1);
}
//����� �������͸� �������� �Լ�
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
//���ӿ� ���� �������̽����� ����ڿ��� �ʿ��� ���������� �����ִ� �Լ�
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
//���� �ʱ�ȭ
void MPuzzle::resetGame()
{
    Copy(Puzzle, Base);
    Copy(Sudoku, Base);
    getBlanks();
    dsp.printPuzzle();
    initPuzzle(true);
}
//������������ �ѱ��
void MPuzzle::nextGame()
{
    getData();
    dsp.printPuzzle();
    initPuzzle(false);
}
//�� �Լ��� ���ӿ� �帧�� �ľ��Ҽ� �ִ�. ���̵� ����->������ �������� ->������� ->������ �ʱ�ȭ -> Ű�Է� ó��
//�� ������ �ݺ��Ͽ� ����ڰ� ���ϴ� ��ŭ ������ ������ �� �ֵ��� ���� �Ͽ���.
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
