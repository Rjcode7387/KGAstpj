#include "Solution.h"
//��Ʈ��ŷ Solution ������� ���� Ȯ��
bool MSolution::backTracking()
{
    while (1)//�ذ�å�� ã�������� �ݺ�
    {
        if (!pop()) return false;
        if (Data.n > Data.idx)//����� �� �ִ� ���� �ִ��� Ȯ��
        {
            Sudoku[Data.y][Data.x] = Data.cells[Data.idx++];//������
            push();//���� �ܰ�� �̵�
            break;
        }
        else Sudoku[Data.y][Data.x] = 0;//�� �ʱ�ȭ
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
    top = -1;//���� �ʱ�ȭ

    while (1)
    {
        n = findCell();//�� ã��
        if (n == FINISH) break;
        else if (n)
        {
            Data.idx = 0;
            Data.n = findCandiNums(Data.x, Data.y, true);//�ĺ� ���� ã��
            if (Data.n > 1) shuffle(Data.n);//�ĺ� ���ڰ� 2�� �̻��̸� ����
            Sudoku[Data.y][Data.x] = Data.cells[Data.idx++];//ù �ĺ� ���� ��ġ
            push();//���� ���¸� ���ÿ� ����
        }
        else if (!backTracking()) return false;//�ĺ��� ������ ��Ʈ��ŷ �õ�
    }
    return true;
}