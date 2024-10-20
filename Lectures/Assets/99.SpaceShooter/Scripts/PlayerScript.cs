using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SpaceShooter
{
    public class PlayerScript : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float boundaryMinSize = -4.5f;
        public float boundaryMaxSize = 4.5f;

        public GameObject gameOverMessage;
        void Update()
        {
            //Input : InputManager의 기능을 활용하여 입력 처리를 할수 있는 클래스
            //Input.GetAxis() : 미리 정의되어있는 입력 축의 값을 가져옴
            //예 : Horizontal : X축, Vertical : Y축

            float x = Input.GetAxis("Horizontal");

            transform.Translate(new Vector3(x, 0) * Time.deltaTime * moveSpeed);

            if (transform.position.x < boundaryMinSize)
            {
                transform.position = new Vector3(boundaryMinSize, transform.position.y);
            }
            else if (transform.position.x > boundaryMaxSize)
            {
                transform.position = new Vector3(boundaryMaxSize, transform.position.y);
            }

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                print("Game Over");
                GameOver();
            }
        }

        public void GameOver()
        {
            gameOverMessage.SetActive(true);
            Time.timeScale = 0;
        }

        
    }
}
