using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Poketmon : MonoBehaviour
{
    // === 포켓몬 기본 정보 ===
    public string pokemonName;        // 포켓몬 이름
    public int hp = 100;             // 체력
    public int attackPower = 20;     // 공격력
    public bool hasActed = false;    // 이번 턴에 행동했는지 여부

    // === UI 컴포넌트 참조 ===
    public Button selectButton;       // 포켓몬 선택용 버튼
    public Button targetButton;       // 타겟 선택용 버튼
    public Text hpText;               // HP 표시할 텍스트
    
    // 배틀 시스템 참조
    private BattleSystem battleSystem;


    void Start()
    {
        // 씬에서 BattleSystem 찾아서 참조
        battleSystem = FindObjectOfType<BattleSystem>();
        UpdateHPDisplay(); // 초기 HP 표시
    }

    // 포켓몬 선택 가능/불가능 설정
    public void EnableSelection(bool enable)
    {
        // 이미 행동했으면 선택 불가능
        selectButton.interactable = enable && !hasActed;
    }

    // 타겟 버튼 활성화/비활성화
    public void EnableTargetButton(bool enable)
    {
        targetButton.gameObject.SetActive(enable);
    }

    // 포켓몬 선택시 호출되는 함수
    public void OnSelect()
    {
        battleSystem.OnPoketmonSelect(this);
    }

    // 타겟으로 선택시 호출되는 함수
    public void OnTargetSelect()
    {
        battleSystem.OnTargetSelect(this);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        UpdateHPDisplay();
        Debug.Log($"{pokemonName}이(가) {damage} 데미지를 받았습니다. 남은 HP: {hp}");
    }

    // HP 표시 업데이트
    void UpdateHPDisplay()
    {
        if (hpText != null)
        {
            hpText.text = $"HP: {hp}";
        }
    }
}


