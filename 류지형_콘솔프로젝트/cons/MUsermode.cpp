#include "MUsermode.h"
//퍼즐 초기화 밑 저장을 위한 함수
bool MUsermode::Read(int& blk)
{
    try {
        // 파일 이름 생성
        std::string fileName = "sudoku" + std::to_string(fileNums) + ".txt";
        std::ifstream file(fileName);

        if (!file.is_open()) {
            errMsg();
            return false;
        }

        blk = 0;
        Size = MAX_SIZE;
        std::string line;
        int i = 0;

        // 한 줄씩 읽어서 처리
        while (std::getline(file, line) && i < Size) {
            for (int j = 0; j < line.length() && j < Size; j++) {
                int digit = getDigit(line[j]);
                if (digit < 0) {
                    if (i == 0) Size = j;
                    break;
                }
                Sudoku[i][j] = digit;
                Puzzle[i][j] = digit;
                if (digit == 0) blk++;
            }
            i++;
        }

        file.close();
        return true;
    }
    catch (const std::exception& e) {
        errMsg();
        return false;
    }
}
//에러 메세지
void MUsermode::errMsg()
{
	printf("파일을 열수 없습니다.\n");
	fileNums = 1;
}


//퍼즐을 저장하기 위한 함수
bool MUsermode::Write()
{
    try {
        // 파일 이름 생성 (번호 증가)
        std::string fileName = "sudoku" + std::to_string(++fileNums) + ".txt";
        std::ofstream file(fileName);

        if (!file.is_open()) {
            errMsg();
            return false;
        }

        // 퍼즐 데이터 쓰기
        for (int i = 0; i < Size; i++) {
            for (int j = 0; j < Size; j++) {
                file << Puzzle[i][j];
            }
            file << '\n';
        }

        file.close();
        Copy(Base, Puzzle);
        return true;
    }
    catch (const std::exception& e) {
        errMsg();
        return false;
    }
}
