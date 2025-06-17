#pragma once

#include<iostream>
#include<cstdlib>
#include <conio.h>
#include <windows.h>
#include <ctime>
#include<string>


#define MAX_SIZE 16
#define FINISH 17

#define BACK_SPACE 8
#define ENTER 13
#define ESC 27
#define UP 72
#define DOWN 80
#define LEFT 75
#define RIGHT 77
#define PAGE_UP 73
#define PAGE_DOWN 81
#define DEL 83 

enum Color
{
    BLACK,            /*  0 : ��� */
    DARK_BLUE,       /*  1 : ��ο� �Ķ� */
    DARK_GREEN,      /*  2 : ��ο� �ʷ� */
    DARK_SKY_BLUE,   /*  3 : ��ο� �ϴ� */
    DARK_RED,        /*  4 : ��ο� ���� */
    DARK_VIOLET,     /*  5 : ��ο� ���� */
    DARK_YELLOW,     /*  6 : ��ο� ��� */
    GRAY,            /*  7 : ȸ�� */
    DARK_GRAY,       /*  8 : ��ο� ȸ�� */
    BLUE,            /*  9 : �Ķ� */
    GREEN,           /* 10 : �ʷ� */
    SKY_BLUE,        /* 11 : �ϴ� */
    RED,             /* 12 : ���� */
    VIOLET,          /* 13 : ���� */
    YELLOW,          /* 14 : ��� */
    WHITE            /* 15 : �Ͼ� */

};

struct CellINFO
{
    int x, y;
    int n;
    int idx;
    int cells[MAX_SIZE];
};

class MSudoku
{
private:


protected:
    static int Size;
    static int BX;
    static int BY;
    static int Blanks;
    static int Sudoku[MAX_SIZE][MAX_SIZE];
    static int Puzzle[MAX_SIZE][MAX_SIZE];
    static int Base[MAX_SIZE][MAX_SIZE];

    int top;
    int Cells[MAX_SIZE];
    CellINFO Stack[MAX_SIZE * MAX_SIZE];
    CellINFO Data;

    void shuffle(int n);
    void getBlanks();
    void Init(int sdk[][MAX_SIZE]);
    void Copy(int dest[][MAX_SIZE], int src[][MAX_SIZE]);
    void gotoxy(int x, int y);//Ŀ���� X,Y��ǥ�� �Ű��ִ� �Լ�
    void setColor(int color);//�ܼ� ������ �����ϴ� �Լ�
    void push();//PUSH�� POP�� ��Ʈ��ŷ�� ���̴��Լ�  
    bool pop();
    int findCandiNums(int x, int y, bool isFill);
    int findCell();
    int getDigit(int ch);//�Է¹��� ���ڸ� ���� �Է����� �޾� ���ڷ� ��ȯ
    //MSudoku();
public:
    
};
