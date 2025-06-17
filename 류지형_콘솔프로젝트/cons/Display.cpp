#include "Display.h"

void Display::DrawBox()
{
   
    //������ �׵θ� �� ��輱
    const char Box[ ][5][3] = {
        {"��", "��", "��", "��", "��"},
        {"��", " ", "��", "��", "��"},
        {"��", "��", "��", "��", "��"},
        {"��", "��", "��", "��", "��"},
        {"��", "��", "��", "��", "��"}
    };
    //�÷��Է�
    int outColor = BLUE;
    int inColor = DARK_GRAY;
    int dx = BX * 2; // �迭�� �׵θ� �� ��輱 �Է�
    int dy = BY * 2;
    int x; int y;
    x = y = BX * BY * 2 + 1;

   
    setColor(outColor);
    for (int i = 0; i < y; i += dy) {
        for (int j = 1; j < x; j += 2) {
            gotoxy(j * 2, i); 
            printf("%s%s", Box[0][1], Box[0][1]);
        }
    }
    
    for (int i = 1; i < y-1; i += 2) {
        for (int j = 0; j < x; j += dx) {
            gotoxy(j * 2, i);
            printf("%s", Box[1][0]);
            gotoxy(j * 2, i + 1);
            printf("%s", Box[2][3]);
            if ((i + 1) % dy == 0) {
                gotoxy(j * 2, i + 1);
                printf("%s", Box[3][3]);
            }
        }
    }

    
    for (int j = 2; j < x-1; j += 2) {
        gotoxy(j * 2, 0);
        printf("%s", Box[0][2]);
        gotoxy(j * 2, y-1);
        printf("%s", Box[4][2]);
    }
    
    for (int j = dx; j < x-1; j += dx) {
        gotoxy(j * 2, 0);
        printf("%s", Box[0][3]);
        gotoxy(j *2, y-1);
        printf("%s", Box[4][3]);
    }
    
    for (int i = 2; i < y-1; i += 2) {
        gotoxy(0, i);
        printf("%s", Box[2][0]);
        gotoxy(dx * dy, i);
        printf("%s", Box[2][4]);
    }
    
    for (int i = dy; i < y-1; i += dy) {
        gotoxy(0, i);
        printf("%s", Box[3][0]);
        gotoxy(dx * dy, i);
        printf("%s", Box[3][4]);
    }

    gotoxy(0, 0);
    printf("%s", Box[0][0]);
    gotoxy(0, y - 1);
    printf("%s", Box[4][0]);
    gotoxy(dx * dy, 0);
    printf("%s", Box[0][4]);
    gotoxy(dx * dy, y - 1);
    printf("%s", Box[4][4]);
    setColor(inColor);
    //����
    for (int i = 1; i < y - 1; i++)
    {
        for (int j = 1; j < x - 1; j++)
        {
            gotoxy(j * 2, i);
            // ������ �׸���
            if (i & 1 && j % 2 == 0 && j % dx && i % dy && j < x - 1) {
                if (j < x - 1) {  // ���� üũ �߰�
                    printf("%s", Box[1][2]);  // Box[2][1] ��� Box[1][2] ���
                }
            }
            // ���� �׸���
            if (i % 2 == 0 && j % 2 == 0 && j % dx && i % dy && j < x - 1) {
                if (i < y - 1) {  // ���� üũ �߰�
                    printf("%s", Box[2][1]);  // Box[2][2] ��� Box[2][1] ���
                }
            }
        }
    }
    setColor(WHITE);
  
    
}

//���θ޴�
void Display::PrintMenu(int selectedIdx, int& y)
{
    
    const char* MainMenu[] = { "======= S U D O K U  P U Z Z L E =======",
                               "|                                      |",
                               "|            1. ��      ��             |",
                               "|                                      |",
                               "|            2. ��      ��             |",
                               "|                                      |",
                               "|            3. ��      ��             |",
                               "|                                      |",
                               "|            4. ��      ��             |",
                               "|                                      |",
                               "|            5. ��  ��  ��             |",
                               "|                                      |",
                               "|            6. Usermode               |",
                               "|                                      |",
                               "|            7. ��      ��             |",
                               "|                                      |",
                               "========================================"};
    
 
    const size_t menuSize = sizeof(MainMenu) / sizeof(MainMenu[0]);

    int x = 28 - static_cast<int>(strlen(MainMenu[0])) / 2;

    y = 13 - static_cast<int>(menuSize) / 2 - 1;

    // �޴� ���
    for (size_t i = 0; i < menuSize; i++) {
        gotoxy(x, y + static_cast<int>(i));
        setColor((i == static_cast<size_t>(selectedIdx * 2)) ? GREEN : WHITE);
        printf("%s", MainMenu[i]);
    }

    gotoxy(x, y + selectedIdx * 2);
}
//�޴�
int Display::menu()
{
	int y, yy, ch, num = 1;

	system("mode con: lines=25 cols=56");
	system("cls");
	PrintMenu(num, y);

	yy = y;
	y = yy + 2;
	while (true)
	{
		ch = _getch();
        if (ch == 0xE0) {
            ch = _getch();
            switch (ch) {
            case UP:    y -= 2; break;
            case DOWN:  y += 2; break;
            }
            if (y > 14 + yy) y = yy + 2;
            else if (y < yy + 2) y = yy + 14;

            num = (y - yy) / 2;
            PrintMenu(num, yy);
        }
        else if (ch == ESC || ch == '7') {
            return 0;
        }
        else if (ch == ENTER) {
            return num;
        }
        if (ch < '1' || ch > '7') continue;
        else break;
    }
    if (ch == '7') ch = '0';
    return ch - '0';

	
}
//���� �÷����� �����ʿ� ������ ����޴�
void Display::subMenu(int undoCount)
{
    const char* sMenu[] = {
        "ESC : ���θ޴�",
        " H  : ��Ʈ",
        " N  : ��������",
        " R  : �ʱ�ȭ",
        " S  : �� ����",
        " U  : ������ �̵�",
        " W  : ����"
    };
        int x = BX * BY * 4 + 2;

        for (int i = 0; i < sizeof(sMenu) / sizeof(sMenu[0]); i++) {
            if (sMenu[i][1] == 'U' && undoCount == 0) {
                setColor(DARK_GRAY);
            }
             else {
                 setColor(WHITE);
             }
                 gotoxy(x, i + 1);
              printf("%s", sMenu[i]);
        }

              showTimeBlock(BLANK, Blanks);
}
//���÷�etNumber(3, 5, 2);�� ȣ���ϸ� (5, 2) ��ġ�� "��"�� ��µ˴ϴ�.
//setNumber(10, 1, 1); �� ȣ���ϸ�(1, 1) ��ġ�� "��"�� ��µ˴ϴ�.
void Display::setNumber(int n, int x, int y)
{
    const char* str[] = {
        "  ", "��", "��", "��", "��", "��", "��", "��", "��", "��",
        "��", "��", "��", "��", "��", "��", "��"
    };

    gotoxy(x, y);
    printf("%s", str[n]);
}
//�����÷��� �ð�
void Display::showTimeBlock(int type, int data)
{
    int x = BX * BX * 4 + 2;
    int y = 9;

    setColor(SKY_BLUE);
    if (type == BLANK) {
        gotoxy(x, y);
        printf("Blank : %2d", data);
        setColor(WHITE);
        return;
    }

    int hh = data / 3600;
    int mm = (data / 60) % 60;
    int sec = data % 60;

    gotoxy(x, y + 1);
    printf("%02d : %02d : %02d", hh, mm, sec);
    setColor(WHITE);
}
//���� �޼���
void Display::finishMessage(bool isMsg)
{
    gotoxy(BX * BY * 4 + 2, 12);
    setColor(isMsg ? RED : WHITE);
    printf(isMsg ? "�����մϴ�." : "           ");
}
//���� ����Ʈ
void Display::printPuzzle()
{
    system("cls");
    DrawBox();
    subMenu(0);

    for (int i = 0, y = 1; i < Size; i++, y += 2) {
        for (int j = 0, x = 2; j < Size; j++, x += 4) {
            if (Base[i][j]) {
                setColor(GREEN);
                setNumber(Puzzle[i][j], x, y);
            }
        }
    }
    setColor(WHITE);
}
// ���� �ɼ��� �ִ� ���� ��� �Լ�
void Display::printPuzzle(int num, bool isColor)
{
    for (int i = 0, y = 1; i < Size; i++, y += 2) {
        for (int j = 0, x = 2; j < Size; j++, x += 4) {
            if (Puzzle[i][j]) {
                if (Base[i][j]) setColor(GREEN);
                else setColor(WHITE);
                if (isColor && Puzzle[i][j] == num) setColor(RED);
                setNumber(Puzzle[i][j], x, y);
            }
            else {
                setNumber(0, x, y);
            }
        }
    }
    setColor(WHITE);
}
