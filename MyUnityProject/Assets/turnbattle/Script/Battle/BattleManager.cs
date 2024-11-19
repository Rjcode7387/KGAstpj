/// <summary>
/// ���� �ý����� �ٽ� ���� Ŭ����
/// �� ����, ���� ���� ���� ���� ���
/// </summary>
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
public enum EnemyPokemonType
{
    Pidgey,
    Spearow,
    Pidgeotto
}

public class BattleManager : MonoBehaviour
{
    //���� ������ ���
    public List<PlayerPoketmon>PlayerTeam { get; private set; }
    public List<EnemyPoketmon>EnemyTeam { get; private set; }

    //Queue���������� �ӵ��� ������� �ϴ� ���� ���ӿ��� Queue���°� �����ϴٰ� �Ѵ�.
    private Queue<PoketmonCharacter> turn;
    //���� ���� ����
    public BattleState CurrentState { get; private set; }

    private void Start()
    {
        InitializBattle();
    }

    //���� �ʱ�ȭ �޼���
    private void InitializBattle()
    {
        

        SetupTeams();
        DetermineTurnOrder();
        CurrentState = BattleState.Start;
        StartBattle();
    }
    private void SetupTeams()
    {
        PlayerTeam = new List<PlayerPoketmon>();
        EnemyTeam = new List<EnemyPoketmon>();
        turn = new Queue<PoketmonCharacter>();

        CreateInitialTeams();
    }
    private void CreateInitialTeams()
    {
        // ����: �÷��̾� ���ϸ� ����
        PlayerPoketmon charmander = new PlayerPoketmon("���̸�", 100, 15, 10, 12);
        charmander.Skills.Add(new Skill("ȭ�����", 30, 5));
        PlayerTeam.Add(charmander);

        PlayerPoketmon squirtle = new PlayerPoketmon("���α�", 95, 13, 12, 10);
        squirtle.Skills.Add(new Skill("������", 25, 5));
        PlayerTeam.Add(squirtle);

        PlayerPoketmon bulbasaur = new PlayerPoketmon("�̻��ؾ�", 90, 12, 11, 8);
        bulbasaur.Skills.Add(new Skill("����ä��", 20, 5));
        PlayerTeam.Add(bulbasaur);

        EnemyPokemonType enemyType = (EnemyPokemonType)Random.Range(0, 3);

        string enemyName;
        int enemyHp, enemyAtk, enemyDef, enemySpd;

        switch (enemyType)
        {
            case EnemyPokemonType.Pidgey:
                enemyName = "����";
                enemyHp = 80;
                enemyAtk = 12;
                enemyDef = 8;
                enemySpd = 15;
                break;

            case EnemyPokemonType.Spearow:
                enemyName = "������";
                enemyHp = 75;
                enemyAtk = 14;
                enemyDef = 7;
                enemySpd = 16;
                break;

            case EnemyPokemonType.Pidgeotto:
                enemyName = "����";
                enemyHp = 90;
                enemyAtk = 16;
                enemyDef = 9;
                enemySpd = 14;
                break;
            default:    // �⺻�� �߰�
                enemyName = "����";
                enemyHp = 80;
                enemyAtk = 12;
                enemyDef = 8;
                enemySpd = 15;
                break;
        }

        // ���õ� ���ϸ� 3���� ����
        for (int i = 0; i < 3; i++)
        {
            EnemyTeam.Add(new EnemyPoketmon(enemyName, enemyHp, enemyAtk, enemyDef, enemySpd));
        }
    }


    /// <summary>
    /// �� ������ �����ϴ� �޼���
    /// �� ĳ������ �ӵ� ���� �������� ���� ����
    /// </summary>
    private void DetermineTurnOrder()
    {
        List<PoketmonCharacter> allCharacters = new List<PoketmonCharacter>();
        allCharacters.AddRange(PlayerTeam);
        allCharacters.AddRange(EnemyTeam);

        // �ӵ��� ���� ������ ����
        allCharacters.Sort((a, b) => b.AttackSpeed .CompareTo(a.AttackSpeed));

        turn.Clear();
        foreach (var character in allCharacters)
        {
            turn.Enqueue(character);
        }
    }

    private void StartBattle()
    {
        Debug.Log("���� ����!");
        ProcessNextTurn();
    }
    /// <summary>
    /// ���� ���� ó���ϴ� �޼���
    /// </summary>
    private void ProcessNextTurn()
    {
        // ���� ���� ���� üũ
        if (CheckBattleEnd()) return;

        // ���� ���� ĳ���͸� ������
        PoketmonCharacter currentCharacter = turn.Dequeue();
        // ���� ���带 ���� �ٽ� ť�� ���� �߰�
        turn.Enqueue(currentCharacter);

        // ĳ���� Ÿ�Կ� ���� �ٸ� ó��
        if (currentCharacter is PlayerPoketmon)
        {
            CurrentState = BattleState.PlayerTurn;
            StartPlayerTurn((PlayerPoketmon)currentCharacter);
        }
        else
        {
            CurrentState = BattleState.EnemyTurn;
            ProcessEnemyTurn((EnemyPoketmon)currentCharacter);
        }
    }
    /// <summary>
    /// �÷��̾��� ���� �����ϴ� �޼���
    /// </summary>
    private void StartPlayerTurn(PlayerPoketmon poketmon)
    {
        Debug.Log($"{poketmon.Name}�� ��!");
        // ���⼭ UI �Ŵ����� ���� �÷��̾� �ൿ �޴��� ǥ��
        // UI�� ������ ���� �ʿ�
    }

    /// �� �������� ���� ó���ϴ� �޼���
    /// </summary>
    private void ProcessEnemyTurn(EnemyPoketmon poketmon)
    {
        Debug.Log($"{poketmon.Name}�� ��!");

        // ������ AI: �������� �÷��̾� Ÿ�� ����
        if (PlayerTeam.Count > 0)
        {
            // �������� Ÿ�� ����
            PlayerPoketmon target = PlayerTeam[Random.Range(0, PlayerTeam.Count)];

            // ������ ���
            int damage = Mathf.Max(1, poketmon.Attack - target.Defense);
            target.TakeDamage(damage);

            Debug.Log($"{poketmon.Name}��(��) {target.Name}���� {damage} �������� �������ϴ�!");
        }

        // ���� �� ����
        ProcessNextTurn();
    }

    /// <summary>
    /// ���� ���� ������ üũ�ϴ� �޼���
    /// </summary>
    private bool CheckBattleEnd()
    {
        // ��� �÷��̾� ���ϸ��� ���������� üũ
        bool allPlayersDead = PlayerTeam.TrueForAll(p => !p.IsAlive);
        // ��� �� ���ϸ��� ���������� üũ
        bool allEnemiesDead = EnemyTeam.TrueForAll(e => !e.IsAlive);

        if (allPlayersDead)
        {
            CurrentState = BattleState.Lose;
            Debug.Log("���� �й�");
            return true;
        }
        if (allEnemiesDead)
        {
            CurrentState = BattleState.Win;
            Debug.Log("���� �¸�!");
            return true;
        }
        return false;
    }
    /// <summary>
    /// �÷��̾��� �ൿ�� ó���ϴ� �޼���
    /// </summary>
    public void OnPlayerAction(PlayerPoketmon actor, EnemyPoketmon target)
    {
        if (CurrentState != BattleState.PlayerTurn) return;

        // ��ų�� �ִٸ� ù ��° ��ų ���
        Skill skill = actor.Skills.Count > 0 ? actor.Skills[0] : null;

        // ������ ��� �� ����
        int damage = BattleProcessor.CalculateDamage(actor, target, skill);
        target.TakeDamage(damage);

        Debug.Log($"{actor.Name}��(��) {target.Name}���� {damage} �������� �־����ϴ�!");


        // ���� �� ����
        ProcessNextTurn();
    }

}
