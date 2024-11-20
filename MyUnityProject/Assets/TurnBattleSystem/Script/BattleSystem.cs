using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class BattleSystem : MonoBehaviour
{
    // �÷��̾�� �� ���ϸ���� �����ϴ� List
    [SerializeField] private List<Poketmon> playerPoketmons = new List<Poketmon>();  // �÷��̾��� ���ϸ� ���
    [SerializeField] private List<Poketmon> enemyPoketmons = new List<Poketmon>();   // ���� ���ϸ� ���
    [SerializeField] private float attackDelay = 2f;    // ���� ������ ��� �ð�
    // UI ���� ������
    public GameObject actionPanel;     // �ൿ ���� �г� (����/��� ��ư�� �ִ� �г�)
    public Button attackButton;        // ���� ��ư
    public Button defendButton;        // ��� ��ư


    // ���� ���� ���� ������
    private Poketmon selectedPoketmon; // ���� ���õ� ���ϸ�
    private int actionCount = 0;       // ���� �Ͽ��� �ൿ�� ���ϸ� �� ī��Ʈ
    private bool isPlayerTurn = true;  // true: �÷��̾� ��, false: �� ��
    private bool isTurnProcessing = false;  // �� ó�� ������ Ȯ���ϴ� �÷���

    // ���� ���۽� ȣ��Ǵ� �Լ�
    void Start()
    {
        StartPlayerTurn();            // �÷��̾� ������ ����
        actionPanel.SetActive(false); // �ൿ �г��� ó���� ��Ȱ��ȭ
    }

    // �÷��̾� �� ���۽� �ʱ�ȭ�ϴ� �Լ�
    void StartPlayerTurn()
    {
        isPlayerTurn = true;
        actionCount = 0;  // �ൿ ī��Ʈ �ʱ�ȭ
        Debug.Log("�÷��̾� �� ����!");

        // ��� �÷��̾� ���ϸ��� ���� ���� ���� Ȱ��ȭ
        foreach (Poketmon poketmon in playerPoketmons)
        {
            poketmon.hasActed = false;     // �ൿ ���� �ʱ�ȭ
            poketmon.EnableSelection(true); // ���� �����ϵ��� Ȱ��ȭ
        }
        isTurnProcessing = false;  // �� ó�� ���� �ʱ�ȭ
    }

    // ���ϸ� ���ý� ȣ��Ǵ� �Լ�
    public void OnPoketmonSelect(Poketmon poketmon)
    {
        // �÷��̾� ���� �ƴϰų� �̹� �ൿ�� ���ϸ��̸� ����
        if (!isPlayerTurn || poketmon.hasActed) return;

        selectedPoketmon = poketmon;
        // ������ ���ϸ� ��ġ ���� �ൿ �г� ǥ��
        actionPanel.SetActive(true);
     
    }

    // ���� ��ư Ŭ���� ȣ��Ǵ� �Լ�
    public void OnAttackButton()
    {
        actionPanel.SetActive(false); // �ൿ �г� ����

        // ��� �� ���ϸ��� Ÿ�� ��ư Ȱ��ȭ
        foreach (Poketmon enemy in enemyPoketmons)
        {
            enemy.EnableTargetButton(true);
        }
    }

    // ���� ��� ���ý� ȣ��Ǵ� �Լ�
    public void OnTargetSelect(Poketmon target)
    {
        if (isTurnProcessing) return;  // �� ó�� ���̸� ����
        isTurnProcessing = true;       // �� ó�� ����
        foreach (Poketmon enemy in enemyPoketmons)
        {
            enemy.EnableTargetButton(false);
        }

        // �÷��̾� ���� ����
        Debug.Log($"{selectedPoketmon.name}��(��) {target.name}��(��) �����մϴ�!");
        target.TakeDamage(selectedPoketmon.attackPower);

        actionCount++;
        selectedPoketmon.hasActed = true;

        // ��� �÷��̾� ���ϸ��� �ൿ�ߴٸ� �� ������
        if (actionCount >= playerPoketmons.Count)
        {
            StartCoroutine(StartEnemyTurnWithDelay());
        }
        else
        {
            // ���� �ൿ���� ���� ���ϸ���� ���� ��ư Ȱ��ȭ
            foreach (Poketmon pokemon in playerPoketmons)
            {
                if (!pokemon.hasActed)
                {
                    pokemon.EnableSelection(true);
                }
            }
            isTurnProcessing = false;
        }

    }
    // �� �� ���� �� �����̸� �ִ� �ڷ�ƾ
    private IEnumerator StartEnemyTurnWithDelay()
    {
        yield return new WaitForSeconds(1f);
        StartEnemyTurn();
    }
    // �� �� ���۽� �ʱ�ȭ�ϴ� �Լ�
    void StartEnemyTurn()
    {
        isPlayerTurn = false;
        actionCount = 0;  // �ൿ ī��Ʈ �ʱ�ȭ
        Debug.Log("�� �� ����!");

        // �÷��̾� ���ϸ���� ���� �ʱ�ȭ
        foreach (Poketmon poketmon in playerPoketmons)
        {
            poketmon.EnableSelection(false);
            poketmon.hasActed = false;
        }

        //�� ���ϸ��� �ڵ� ���� ����
        StartCoroutine(ProcessEnemyActionCoroutine());
    }

    // �� ���ϸ��� �ൿ�� ó���ϴ� �Լ�
    private IEnumerator ProcessEnemyActionCoroutine()
    {
        if (actionCount >= enemyPoketmons.Count)
        {
            yield return new WaitForSeconds(1f);  // �� ��ȯ �� ���
            isTurnProcessing = false;
            
            // �� ���ϸ���� ���� �ʱ�ȭ
            foreach (Poketmon enemy in enemyPoketmons)
            {
                enemy.hasActed = false;
            }

            StartPlayerTurn();  // �ٽ� �÷��̾� ������
            yield break;
        }
        // ���� �ൿ�� �� ���ϸ�� ������ Ÿ�� ����
        Poketmon enemyPoketmon = enemyPoketmons[actionCount];
        Poketmon randomTarget = playerPoketmons[UnityEngine.Random.Range(0, playerPoketmons.Count)];

        // ���� ���� �� ��� ���
        yield return new WaitForSeconds(attackDelay);

        // ���� ����
        Debug.Log($"{enemyPoketmon.name}��(��) {randomTarget.name}��(��) �����մϴ�!");
        randomTarget.TakeDamage(enemyPoketmon.attackPower);

        actionCount++;
        // ���� �� ���ϸ��� �ൿ ó��
        StartCoroutine(ProcessEnemyActionCoroutine());
    }

    

}
