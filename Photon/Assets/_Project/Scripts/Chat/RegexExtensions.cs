using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class RegexExtensions
{
    //Regex : Regular Expression(정규 표현식)

    //닉네임으로 0~9, a~z, A~z. 가 ~ 힣 안에 포함되는 완성형 한글과 영문/숫자만 포함하는 정규식

    static Regex nicknameRegex = new Regex(@"[^0-9A-Za-z가-힣]");

    //Regex 사용하는 방벙 1. Regex 클래스 사용

    public static bool NicknameValidate(this string nickname)
    {
        return false == nicknameRegex.IsMatch(nickname);
    }

    //2.Regex 포맷 문자열로 변환
    
    //문자열 입력중에 미완성형 한글을 허용 하는 정규식
    const string INPUT_FORM = @"[^0-9A-Za-z가-힣ㄱ-ㅎㅏ-ㅣㆍᆞᆢ]";

    public static string ToValidString(this string param)
    {
        return Regex.Replace(param,INPUT_FORM,"",RegexOptions.Singleline);
    }

    //위대한 한들을 천박하게 ㅆ는 넘들 차단
    const string COMPLETE_HANGUL = @"[^가-힣]";
    //일반 비속어
    static List<string> fword = new()
    {
        "시발","개객기"
    };

    //변형 비속어
    static List<string> irregularFword = new()
    {
        "^^l발","ㄴㅇㅁ"
    };
    public static bool ContainsFword(this string param)
    {
        if(string.IsNullOrEmpty(param))return false;

        //변형 비속어를 먼저 검사

        if (irregularFword.Exists(x => param.Contains(x))) return true;

        //완성형 한글만 남김 예 : 예쁜!말 =>예쁜말
        param = Regex.Replace(param, COMPLETE_HANGUL, "",RegexOptions.Singleline);

        return fword.Exists(x => param.Contains(x));
        
            
        
    }
}
