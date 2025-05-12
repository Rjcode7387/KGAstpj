#include "Solution.h"
//백트랙킹 Solution 헤더파일 설명 확인
bool MSolution::backTracking()
{
    while (1)//해결책을 찾을때까지 반복
    {
        if (!pop()) return false;
        if (Data.n > Data.idx)//사용할 수 있는 값이 있는지 확인
        {
            Sudoku[Data.y][Data.x] = Data.cells[Data.idx++];//값설정
            push();//다음 단계로 이동
            break;
        }
        else Sudoku[Data.y][Data.x] = 0;//셀 초기화
    }
    return true;   
}

MSolution::MSolution()
{
}
MSolution::~MSolution()
{
}


bool MSolution::Solution()
{
    int n;
    top = -1;//스택 초기화

    while (1)
    {
        n = findCell();//빈셀 찾기
        if (n == FINISH) break;
        else if (n)
        {
            Data.idx = 0;
            Data.n = findCandiNums(Data.x, Data.y, true);//후보 숫자 찾기
            if (Data.n > 1) shuffle(Data.n);//후보 숫자가 2개 이상이면 섞기
            Sudoku[Data.y][Data.x] = Data.cells[Data.idx++];//첫 후보 숫자 배치
            push();//현재 상태를 스택에 저장
        }
        else if (!backTracking()) return false;//후보가 없으면 백트랭킹 시도
    }
    return true;
}