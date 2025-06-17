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

    int menu(); // �޴� ���� �Լ�
    void subMenu(int undoCount); // ���� �޴� �Լ�
    void setNumber(int n, int x, int y); // ���� ���� �Լ�
    void showTimeBlock(int type, int data); // �ð� ��� ǥ�� �Լ�
    void finishMessage(bool isMsg); // �Ϸ� �޽��� ǥ�� �Լ�
    void printPuzzle(); // ���� ��� �Լ�
    void printPuzzle(int num, bool isColor); // ���� �ɼ��� �ִ� ���� ��� �Լ�


};

