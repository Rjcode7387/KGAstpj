#pragma once

#include"Sudoku.h"

class MSolution : public MSudoku
{
private:
	bool backTracking();

public:
	MSolution();
	~MSolution();

	bool Solution();


};
/*백트래킹의 핵심 개념
후보 생성: 가능한 해의 후보를 생성합니다.
유효성 검사: 현재 후보가 문제의 조건을 만족하는지 확인합니다.
재귀적 탐색: 유효하다면 다음 단계로 진행하고, 그렇지 않다면 이전 단계로 되돌아갑니다.
해를 찾을 때까지 반복: 가능한 모든 경로를 탐색합니다.*/
