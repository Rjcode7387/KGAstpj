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
/*��Ʈ��ŷ�� �ٽ� ����
�ĺ� ����: ������ ���� �ĺ��� �����մϴ�.
��ȿ�� �˻�: ���� �ĺ��� ������ ������ �����ϴ��� Ȯ���մϴ�.
����� Ž��: ��ȿ�ϴٸ� ���� �ܰ�� �����ϰ�, �׷��� �ʴٸ� ���� �ܰ�� �ǵ��ư��ϴ�.
�ظ� ã�� ������ �ݺ�: ������ ��� ��θ� Ž���մϴ�.*/
