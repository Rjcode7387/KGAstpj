#pragma once
#include "Sudoku.h"
#include "MUsermode.h"
#include "Solution.h"
#include "MakePuzzle.h"
#include "Display.h""
#include <windows.h>

class MPuzzle : public MSudoku
{
private:
    int levelofDiff;//User Mode�ΰ� �ƴѰ�
    int blankCount;
    int x, y, ch;
    int t1, sec;
    int sNum; //�����Էµ� ����
    int cNum; //�÷��� ������ ����.
    int undoCount;
    bool showHint, isRun;


    MSolution    sol;
    MMakePuzzle  pzl;
    Display     dsp;
    MUsermode    user;
    bool checkNum();
    bool getData();
    void nextGame();
    void resetGame();
    void getSolution();
    void Hint();
    void delHint();
    void getXY();
    void getXY(int& x, int& y);
    void printTime();
    void Undo();
    void inputNum();
    void initPuzzle(bool isReset);
    void keyControl();

public:
    MPuzzle();
    ~MPuzzle() { ; };

    void playGame();

};

