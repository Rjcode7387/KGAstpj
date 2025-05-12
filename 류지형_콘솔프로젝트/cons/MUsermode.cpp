#include "MUsermode.h"
//���� �ʱ�ȭ �� ������ ���� �Լ�
bool MUsermode::Read(int& blk)
{
    try {
        // ���� �̸� ����
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

        // �� �پ� �о ó��
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
//���� �޼���
void MUsermode::errMsg()
{
	printf("������ ���� �����ϴ�.\n");
	fileNums = 1;
}


//������ �����ϱ� ���� �Լ�
bool MUsermode::Write()
{
    try {
        // ���� �̸� ���� (��ȣ ����)
        std::string fileName = "sudoku" + std::to_string(++fileNums) + ".txt";
        std::ofstream file(fileName);

        if (!file.is_open()) {
            errMsg();
            return false;
        }

        // ���� ������ ����
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
