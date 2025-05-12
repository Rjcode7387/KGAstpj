#pragma once

#include"Sudoku.h"

enum TakeDispalay {TIME , BLANK};

class Display : public MSudoku
{
private:
	void DrawBox();
	void PrintMenu(int n, int& y);

public:
	Display(){}
	~Display(){}

    int menu(); // 메뉴 선택 함수
    void subMenu(int undoCount); // 서브 메뉴 함수
    void setNumber(int n, int x, int y); // 숫자 설정 함수
    void showTimeBlock(int type, int data); // 시간 블록 표시 함수
    void finishMessage(bool isMsg); // 완료 메시지 표시 함수
    void printPuzzle(); // 퍼즐 출력 함수
    void printPuzzle(int num, bool isColor); // 색상 옵션이 있는 퍼즐 출력 함수


};

