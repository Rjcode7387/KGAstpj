#include "Puzzle.h"

#include <iostream>
#include <fstream>
#include <vector>
using namespace std;
//���� ����ȭ��
int main()
{
    SetConsoleTitle(L"Sudoku Puzzle");
    srand((unsigned)time(NULL));
    MPuzzle pzl;

    pzl.playGame();
    return 0;


    //std::ifstream readFile;
    ////���� ������ ���� ����
    //readFile.open("�ҽ�.txt");
    //vector<string> v;
    ////���� ����
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