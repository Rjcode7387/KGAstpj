#include "Puzzle.h"

#include <iostream>
#include <fstream>
#include <vector>
using namespace std;
//메인 실행화면
int main()
{
    SetConsoleTitle(L"Sudoku Puzzle");
    srand((unsigned)time(NULL));
    MPuzzle pzl;

    pzl.playGame();
    return 0;


    //std::ifstream readFile;
    ////읽을 목적의 파일 선언
    //readFile.open("소스.txt");
    //vector<string> v;
    ////파일 열기
    //if(readFile.is_open())
    //{
    //    while(!readFile.eof())
    //    {
    //        char arr[256];
    //        readFile.getline(arr, 256);
    //        v.push_back(arr);
    //    }
    //}
    //for (auto nxt : v)
    //{
    //    cout << nxt << "\n";
    //}
}