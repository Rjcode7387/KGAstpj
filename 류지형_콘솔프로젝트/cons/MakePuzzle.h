#pragma once

#include "Sudoku.h"
#include "Solution.h"
//게임 난이도
enum { END, EASY, NORMAL, HARD, VERYHARD, EXTRA, USERMODE };

class MMakePuzzle : public MSudoku
{
private:
	MSolution sol;

	void fillArr(int n, int pm);
	void shuffle();
	bool Compare();
	int getBlank(int n);
public:
	MMakePuzzle() {};
	~MMakePuzzle() {};

	void makeArr();
	void makePuzzle(int n);

};

