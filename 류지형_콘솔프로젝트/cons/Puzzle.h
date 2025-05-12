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
    int levelofDiff;//User Mode인가 아닌가
    int blankCount;
    int x, y, ch;
    int t1, sec;
    int sNum; //현재입력된 숫자
    int cNum; //컬러로 지정된 숫자.
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

