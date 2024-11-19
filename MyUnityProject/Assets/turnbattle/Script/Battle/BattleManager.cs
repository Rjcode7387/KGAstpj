/// <summary>
/// 전투 시스템의 핵심 관리 클래스
/// 턴 진행, 전투 상태 관리 등을 담당
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
    //전투 참가자 목록
    public List<PlayerPoketmon>PlayerTeam { get; private set; }
    public List<EnemyPoketmon>EnemyTeam { get; private set; }

    //Queue쓰는이유는 속도를 기반으로 하는 턴제 게임에서 Queue쓰는게 적합하다고 한다.
    private Queue<PoketmonCharacter> turn;
    //현재 전투 상태
    public BattleState CurrentState { get; private set; }

    private void Start()
    {
        InitializBattle();
    }

    //전투 초기화 메서드
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
        // 예시: 플레이어 포켓몬 생성
        PlayerPoketmon charmander = new PlayerPoketmon("파이리", 100, 15, 10, 12);
        charmander.Skills.Add(new Skill("화염방사", 30, 5));
        PlayerTeam.Add(charmander);

        PlayerPoketmon squirtle = new PlayerPoketmon("꼬부기", 95, 13, 12, 10);
        squirtle.Skills.Add(new Skill("물대포", 25, 5));
        PlayerTeam.Add(squirtle);

        PlayerPoketmon bulbasaur = new PlayerPoketmon("이상해씨", 90, 12, 11, 8);
        bulbasaur.Skills.Add(new Skill("덩굴채찍", 20, 5));
        PlayerTeam.Add(bulbasaur);

        EnemyPokemonType enemyType = (EnemyPokemonType)Random.Range(0, 3);

        string enemyName;
        int enemyHp, enemyAtk, enemyDef, enemySpd;

        switch (enemyType)
        {
            case EnemyPokemonType.Pidgey:
                enemyName = "구구";
                enemyHp = 80;
                enemyAtk = 12;
                enemyDef = 8;
                enemySpd = 15;
                break;

            case EnemyPokemonType.Spearow:
                enemyName = "깨비참";
                enemyHp = 75;
                enemyAtk = 14;
                enemyDef = 7;
                enemySpd = 16;
                break;

            case EnemyPokemonType.Pidgeotto:
                enemyName = "피죤";
                enemyHp = 90;
                enemyAtk = 16;
                enemyDef = 9;
                enemySpd = 14;
                break;
            default:    // 기본값 추가
                enemyName = "구구";
                enemyHp = 80;
                enemyAtk = 12;
                enemyDef = 8;
                enemySpd = 15;
                break;
        }

        // 선택된 포켓몬 3마리 생성
        for (int i = 0; i < 3; i++)
        {
            EnemyTeam.Add(new EnemyPoketmon(enemyName, enemyHp, enemyAtk, enemyDef, enemySpd));
        }
    }


    /// <summary>
    /// 턴 순서를 결정하는 메서드
    /// 각 캐릭터의 속도 값을 기준으로 순서 결정
    /// </summary>
    private void DetermineTurnOrder()
    {
        List<PoketmonCharacter> allCharacters = new List<PoketmonCharacter>();
        allCharacters.AddRange(PlayerTeam);
        allCharacters.AddRange(EnemyTeam);

        // 속도가 높은 순으로 정렬
        allCharacters.Sort((a, b) => b.AttackSpeed .CompareTo(a.AttackSpeed));

        turn.Clear();
        foreach (var character in allCharacters)
        {
            turn.Enqueue(character);
        }
    }

    private void StartBattle()
    {
        Debug.Log("전투 시작!");
        ProcessNextTurn();
    }
    /// <summary>
    /// 다음 턴을 처리하는 메서드
    /// </summary>
    private void ProcessNextTurn()
    {
        // 전투 종료 조건 체크
        if (CheckBattleEnd()) return;

        // 현재 턴의 캐릭터를 가져옴
        PoketmonCharacter currentCharacter = turn.Dequeue();
        // 다음 라운드를 위해 다시 큐의 끝에 추가
        turn.Enqueue(currentCharacter);

        // 캐릭터 타입에 따라 다른 처리
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
    /// 플레이어의 턴을 시작하는 메서드
    /// </summary>
    private void StartPlayerTurn(PlayerPoketmon poketmon)
    {
        Debug.Log($"{poketmon.Name}의 턴!");
        // 여기서 UI 매니저를 통해 플레이어 행동 메뉴를 표시
        // UI는 별도로 구현 필요
    }

    /// 적 디지몬의 턴을 처리하는 메서드
    /// </summary>
    private void ProcessEnemyTurn(EnemyPoketmon poketmon)
    {
        Debug.Log($"{poketmon.Name}의 턴!");

        // 간단한 AI: 랜덤으로 플레이어 타겟 선택
        if (PlayerTeam.Count > 0)
        {
            // 랜덤으로 타겟 선택
            PlayerPoketmon target = PlayerTeam[Random.Range(0, PlayerTeam.Count)];

            // 데미지 계산
            int damage = Mathf.Max(1, poketmon.Attack - target.Defense);
            target.TakeDamage(damage);

            Debug.Log($"{poketmon.Name}이(가) {target.Name}에게 {damage} 데미지를 입혔습니다!");
        }

        // 다음 턴 진행
        ProcessNextTurn();
    }

    /// <summary>
    /// 전투 종료 조건을 체크하는 메서드
    /// </summary>
    private bool CheckBattleEnd()
    {
        // 모든 플레이어 포켓몬이 쓰러졌는지 체크
        bool allPlayersDead = PlayerTeam.TrueForAll(p => !p.IsAlive);
        // 모든 적 포켓몬이 쓰러졌는지 체크
        bool allEnemiesDead = EnemyTeam.TrueForAll(e => !e.IsAlive);

        if (allPlayersDead)
        {
            CurrentState = BattleState.Lose;
            Debug.Log("전투 패배");
            return true;
        }
        if (allEnemiesDead)
        {
            CurrentState = BattleState.Win;
            Debug.Log("전투 승리!");
            return true;
        }
        return false;
    }
    /// <summary>
    /// 플레이어의 행동을 처리하는 메서드
    /// </summary>
    public void OnPlayerAction(PlayerPoketmon actor, EnemyPoketmon target)
    {
        if (CurrentState != BattleState.PlayerTurn) return;

        // 스킬이 있다면 첫 번째 스킬 사용
        Skill skill = actor.Skills.Count > 0 ? actor.Skills[0] : null;

        // 데미지 계산 및 적용
        int damage = BattleProcessor.CalculateDamage(actor, target, skill);
        target.TakeDamage(damage);

        Debug.Log($"{actor.Name}이(가) {target.Name}에게 {damage} 데미지를 주었습니다!");


        // 다음 턴 진행
        ProcessNextTurn();
    }

}
