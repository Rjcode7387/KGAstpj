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
    // 플레이어와 적 포켓몬들을 저장하는 List
    [SerializeField] private List<Poketmon> playerPoketmons = new List<Poketmon>();  // 플레이어의 포켓몬 목록
    [SerializeField] private List<Poketmon> enemyPoketmons = new List<Poketmon>();   // 적의 포켓몬 목록
    [SerializeField] private float attackDelay = 2f;    // 공격 사이의 대기 시간
    // UI 관련 변수들
    public GameObject actionPanel;     // 행동 선택 패널 (공격/방어 버튼이 있는 패널)
    public Button attackButton;        // 공격 버튼
    public Button defendButton;        // 방어 버튼


    // 게임 상태 관리 변수들
    private Poketmon selectedPoketmon;      // 현재 선택된 포켓몬
    private int actionCount = 0;            // 현재 턴에서 행동한 포켓몬 수 카운트
    private bool isPlayerTurn = true;       // true: 플레이어 턴, false: 적 턴
    private bool isTurnProcessing = false;  // 턴 처리 중인지 확인하는 플래그
    private bool isAttackMode = false;      // 공격 모드인지 확인하는 변수

    // 게임 시작시 호출되는 함수
    void Start()
    {
        StartPlayerTurn();            // 플레이어 턴으로 시작
        actionPanel.SetActive(false); // 행동 패널은 처음에 비활성화
        // 적 포켓몬의 선택 버튼 제거
        foreach (Poketmon enemy in enemyPoketmons)
        {
            enemy.selectButton.gameObject.SetActive(false);
        }
        // 버튼 초기 상태 설정
        attackButton.interactable = true;
        defendButton.interactable = true;
    }
    private void Update()
    {
        // Q키를 누르면 필드씬으로 돌아가기
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneController.Instance.LoadFieldScene();
        }
    }

    // 플레이어 턴 시작시 초기화하는 함수
    void StartPlayerTurn()
    {
        isPlayerTurn = true;
        actionCount = 0;  // 행동 카운트 초기화
        Debug.Log("플레이어 턴 시작!");

        // 모든 플레이어 포켓몬의 선택 가능 상태 활성화
        foreach (Poketmon poketmon in playerPoketmons)
        {
            poketmon.hasActed = false;     // 행동 상태 초기화
            poketmon.EnableSelection(true); // 선택 가능하도록 활성화
        }
        isTurnProcessing = false;  // 턴 처리 상태 초기화
    }

    // 포켓몬 선택시 호출되는 함수
    public void OnPoketmonSelect(Poketmon poketmon)
    {
        // 플레이어 턴이 아니거나 이미 행동한 포켓몬이면 리턴
        if (!isPlayerTurn || poketmon.hasActed) return;

        // 이전에 선택된 포켓몬이 있다면 모든 UI 초기화
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

        // 공격 버튼 상호작용 활성화
        attackButton.interactable = true;
        defendButton.interactable = true;

    }

    // 공격 버튼 클릭시 호출되는 함수
    public void OnAttackButton()
    {
        actionPanel.SetActive(false); // 행동 패널 숨김
        isAttackMode = true;  // 공격 모드 활성화

        // 모든 적 포켓몬의 타겟 버튼 활성화
        foreach (Poketmon enemy in enemyPoketmons)
        {
            enemy.EnableTargetButton(true);
        }
    }
    //굥격 전체의 수행 순서 관리 코루틴
    private IEnumerator ProcessAttackSequence(Poketmon attacker, Poketmon target)
    {
        //공격자의 애니메이션이 끝날때 까지 대기
        yield return StartCoroutine(attacker.ManageAllAttacks(target));

       

        //행동 완료 처리
        actionCount++;
        attacker.hasActed = true;

        // 모든 플레이어 포켓몬이 행동했는지 확인
        if (actionCount >= playerPoketmons.Count)
        {
            // 적 턴으로 전환
            StartCoroutine(StartEnemyTurnWithDelay());
        }
        else
        {
            // 아직 행동하지 않은 포켓몬들의 선택 버튼 활성화
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


    // 공격 대상 선택시 호출되는 함수
    public void OnTargetSelect(Poketmon target)
    {
        if (!isAttackMode || isTurnProcessing) return;  // 공격 모드가 아니거나 턴 처리 중이면 무시
        isTurnProcessing = true;       // 턴 처리 시작
        isAttackMode = false;          // 공격 모드 비활성화
        foreach (Poketmon enemy in enemyPoketmons)
        {
            enemy.EnableTargetButton(false);
        }

        // 플레이어 공격 실행
        Debug.Log($"{selectedPoketmon.name}이(가) {target.name}을(를) 공격합니다!");
       

        // 공격 시작
        StartCoroutine(ProcessAttackSequence(selectedPoketmon, target));

    }
    // 적 턴 시작 전 딜레이를 주는 코루틴
    private IEnumerator StartEnemyTurnWithDelay()
    {
        yield return new WaitForSeconds(1f);
        StartEnemyTurn();
    }
    // 적 턴 시작시 초기화하는 함수
    void StartEnemyTurn()
    {
        isPlayerTurn = false;
        actionCount = 0;  // 행동 카운트 초기화
        Debug.Log("적 턴 시작!");

        // 플레이어 포켓몬들의 상태 초기화
        foreach (Poketmon poketmon in playerPoketmons)
        {
            poketmon.EnableSelection(false);
            poketmon.hasActed = false;
        }

        //적 포켓몬의 자동 공격 시작
        StartCoroutine(ProcessEnemyActionCoroutine());
    }

    // 적 포켓몬의 행동을 처리하는 함수
    private IEnumerator ProcessEnemyActionCoroutine()
    {
        if (actionCount >= enemyPoketmons.Count)
        {
            yield return new WaitForSeconds(1f);  // 턴 전환 전 대기
            isTurnProcessing = false;
            
            // 적 포켓몬들의 상태 초기화
            foreach (Poketmon enemy in enemyPoketmons)
            {
                enemy.hasActed = false;
            }

            StartPlayerTurn();  // 다시 플레이어 턴으로
            yield break;
        }
        // 현재 행동할 적 포켓몬과 무작위 타겟 선택
        Poketmon enemyPoketmon = enemyPoketmons[actionCount];
        Poketmon randomTarget = playerPoketmons[UnityEngine.Random.Range(0, playerPoketmons.Count)];

        // 공격 실행 전 잠시 대기
        yield return new WaitForSeconds(attackDelay);

        // 플레이어의 공격과 동일한 방식으로 애니메이션 실행
        yield return StartCoroutine(enemyPoketmon.ManageAllAttacks(randomTarget));

        // 공격 실행
        Debug.Log($"{enemyPoketmon.name}이(가) {randomTarget.name}을(를) 공격합니다!");
       

        actionCount++;
        enemyPoketmon.hasActed = true;
        // 다음 적 포켓몬의 행동 처리
        StartCoroutine(ProcessEnemyActionCoroutine());
    }

    

}
