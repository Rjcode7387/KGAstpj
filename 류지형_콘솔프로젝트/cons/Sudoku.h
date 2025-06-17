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
    BLACK,            /*  0 : 까망 */
    DARK_BLUE,       /*  1 : 어두운 파랑 */
    DARK_GREEN,      /*  2 : 어두운 초록 */
    DARK_SKY_BLUE,   /*  3 : 어두운 하늘 */
    DARK_RED,        /*  4 : 어두운 빨강 */
    DARK_VIOLET,     /*  5 : 어두운 보라 */
    DARK_YELLOW,     /*  6 : 어두운 노랑 */
    GRAY,            /*  7 : 회색 */
    DARK_GRAY,       /*  8 : 어두운 회색 */
    BLUE,            /*  9 : 파랑 */
    GREEN,           /* 10 : 초록 */
    SKY_BLUE,        /* 11 : 하늘 */
    RED,             /* 12 : 빨강 */
    VIOLET,          /* 13 : 보라 */
    YELLOW,          /* 14 : 노랑 */
    WHITE            /* 15 : 하양 */

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
    void gotoxy(int x, int y);//커서를 X,Y좌표로 옮겨주는 함수
    void setColor(int color);//콘솔 색깔을 지정하는 함수
    void push();//PUSH와 POP은 백트랙킹에 쓰이는함수  
    bool pop();
    int findCandiNums(int x, int y, bool isFill);
    int findCell();
    int getDigit(int ch);//입력받은 숫자를 문자 입력으로 받아 숫자로 변환
    //MSudoku();
public:
    
};
