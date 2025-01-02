using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameShoot : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    private int score = 0;
    [SerializeField] private int Enemycount = 3;
    private void Start()
    {
        for (int i = 0; i < Enemycount; i++)
        {
            CreateEnemy();
        }
        scoreText.text = "점수 : 0";
    }
    //생성
    public void CreateEnemy()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        sphere.transform.position = new Vector3
            (Random.Range(-3f, 3f),
            Random.Range(1f, 3f),
            Random.Range(1f, 5f)
            );

        sphere.GetComponent<Renderer>().material.color = Random.ColorHSV();

        
        sphere.AddComponent<EnemyHit>().game = this;
    }

    public void AddScore()
    {
        score += 10;
        scoreText.text = $"점수: {score}";
        CreateEnemy();
    }



}
