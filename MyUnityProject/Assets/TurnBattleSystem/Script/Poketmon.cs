using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Poketmon : MonoBehaviour
{
    // === ���ϸ� �⺻ ���� ===
    public string pokemonName;        // ���ϸ� �̸�
    public int hp = 100;             // ü��
    public int attackPower = 20;     // ���ݷ�
    public bool hasActed = false;    // �̹� �Ͽ� �ൿ�ߴ��� ����

    // === UI ������Ʈ ���� ===
    public Button selectButton;       // ���ϸ� ���ÿ� ��ư
    public Button targetButton;       // Ÿ�� ���ÿ� ��ư
    public Text hpText;               // HP ǥ���� �ؽ�Ʈ
    
    // ��Ʋ �ý��� ����
    private BattleSystem battleSystem;


    void Start()
    {
        // ������ BattleSystem ã�Ƽ� ����
        battleSystem = FindObjectOfType<BattleSystem>();
        UpdateHPDisplay(); // �ʱ� HP ǥ��
    }

    // ���ϸ� ���� ����/�Ұ��� ����
    public void EnableSelection(bool enable)
    {
        // �̹� �ൿ������ ���� �Ұ���
        selectButton.interactable = enable && !hasActed;
    }

    // Ÿ�� ��ư Ȱ��ȭ/��Ȱ��ȭ
    public void EnableTargetButton(bool enable)
    {
        targetButton.gameObject.SetActive(enable);
    }

    // ���ϸ� ���ý� ȣ��Ǵ� �Լ�
    public void OnSelect()
    {
        battleSystem.OnPoketmonSelect(this);
    }

    // Ÿ������ ���ý� ȣ��Ǵ� �Լ�
    public void OnTargetSelect()
    {
        battleSystem.OnTargetSelect(this);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        UpdateHPDisplay();
        Debug.Log($"{pokemonName}��(��) {damage} �������� �޾ҽ��ϴ�. ���� HP: {hp}");
    }

    // HP ǥ�� ������Ʈ
    void UpdateHPDisplay()
    {
        if (hpText != null)
        {
            hpText.text = $"HP: {hp}";
        }
    }
}


