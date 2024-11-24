using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

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
    private Poketmon selectedPoketmon;      // ���� ���õ� ���ϸ�
    private int actionCount = 0;            // ���� �Ͽ��� �ൿ�� ���ϸ� �� ī��Ʈ
    private bool isPlayerTurn = true;       // true: �÷��̾� ��, false: �� ��
    private bool isTurnProcessing = false;  // �� ó�� ������ Ȯ���ϴ� �÷���
    private bool isAttackMode = false;      // ���� ������� Ȯ���ϴ� ����

    // ���� ���۽� ȣ��Ǵ� �Լ�
    void Start()
    {
        StartPlayerTurn();            // �÷��̾� ������ ����
        actionPanel.SetActive(false); // �ൿ �г��� ó���� ��Ȱ��ȭ
        // �� ���ϸ��� ���� ��ư ����
        foreach (Poketmon enemy in enemyPoketmons)
        {
            enemy.selectButton.gameObject.SetActive(false);
        }
        // ��ư �ʱ� ���� ����
        attackButton.interactable = true;
        defendButton.interactable = true;
    }
    private void Update()
    {
        // QŰ�� ������ �ʵ������ ���ư���
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneController.Instance.LoadFieldScene();
        }
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

        // ������ ���õ� ���ϸ��� �ִٸ� ��� UI �ʱ�ȭ
        if (selectedPoketmon != null)
        {
            actionPanel.SetActive(false);
            foreach (Poketmon enemy in enemyPoketmons)
            {
                enemy.EnableTargetButton(false);
            }
        }

        selectedPoketmon = poketmon;
        actionPanel.SetActive(true);

        // ���� ��ư ��ȣ�ۿ� Ȱ��ȭ
        attackButton.interactable = true;
        defendButton.interactable = true;

    }

    // ���� ��ư Ŭ���� ȣ��Ǵ� �Լ�
    public void OnAttackButton()
    {
        actionPanel.SetActive(false); // �ൿ �г� ����
        isAttackMode = true;  // ���� ��� Ȱ��ȭ

        // ��� �� ���ϸ��� Ÿ�� ��ư Ȱ��ȭ
        foreach (Poketmon enemy in enemyPoketmons)
        {
            enemy.EnableTargetButton(true);
        }
    }
    //���� ��ü�� ���� ���� ���� �ڷ�ƾ
    private IEnumerator ProcessAttackSequence(Poketmon attacker, Poketmon target)
    {
        //�������� �ִϸ��̼��� ������ ���� ���
        yield return StartCoroutine(attacker.ManageAllAttacks(target));

       

        //�ൿ �Ϸ� ó��
        actionCount++;
        attacker.hasActed = true;

        // ��� �÷��̾� ���ϸ��� �ൿ�ߴ��� Ȯ��
        if (actionCount >= playerPoketmons.Count)
        {
            // �� ������ ��ȯ
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


    // ���� ��� ���ý� ȣ��Ǵ� �Լ�
    public void OnTargetSelect(Poketmon target)
    {
        if (!isAttackMode || isTurnProcessing) return;  // ���� ��尡 �ƴϰų� �� ó�� ���̸� ����
        isTurnProcessing = true;       // �� ó�� ����
        isAttackMode = false;          // ���� ��� ��Ȱ��ȭ
        foreach (Poketmon enemy in enemyPoketmons)
        {
            enemy.EnableTargetButton(false);
        }

        // �÷��̾� ���� ����
        Debug.Log($"{selectedPoketmon.name}��(��) {target.name}��(��) �����մϴ�!");
       

        // ���� ����
        StartCoroutine(ProcessAttackSequence(selectedPoketmon, target));

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

        // �÷��̾��� ���ݰ� ������ ������� �ִϸ��̼� ����
        yield return StartCoroutine(enemyPoketmon.ManageAllAttacks(randomTarget));

        // ���� ����
        Debug.Log($"{enemyPoketmon.name}��(��) {randomTarget.name}��(��) �����մϴ�!");
       

        actionCount++;
        enemyPoketmon.hasActed = true;
        // ���� �� ���ϸ��� �ൿ ó��
        StartCoroutine(ProcessEnemyActionCoroutine());
    }

    

}
