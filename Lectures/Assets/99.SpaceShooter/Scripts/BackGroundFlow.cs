using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class BackGroundFlow : MonoBehaviour
    {
        public float flowSpeed;

        private void Update()
        {
            //transform : �� ������Ʈ�� �����Ǿ��ִ� ������Ʈ�� Transform ������Ʈ
            //Transform.translate : ���� Position���� �Ķ������ Vector�� ��ŭ Position�� �̵�
            transform.Translate(Vector3.down /*new Vector3(0,-1,0)*/ * Time.deltaTime * flowSpeed);
            if (transform.position.y < -2.55f)
            {
                transform.position = Vector3.zero;
            }
        }
    }
}
