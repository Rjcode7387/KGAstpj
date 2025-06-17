#pragma once
#include <fstream>
#include <stdexcept>
#include <string>
#include "Sudoku.h"

class MUsermode : public MSudoku
{
private:
    int  fileNums;
    
    std::string fileName;

    void errMsg();   

public:
    MUsermode() : fileNums(1) {}
    ~MUsermode() { ; }

    bool Read(int& blk);
    bool Write();
};

