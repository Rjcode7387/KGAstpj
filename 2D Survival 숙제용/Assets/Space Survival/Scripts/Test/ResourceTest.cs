using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTest : MonoBehaviour
{
    public SpriteRenderer sp1;

    public SpriteRenderer sp2;
    //Resources ���� : ������Ʈ�� Assets���� ���� Resources��� �̸��� ������ ������ ���
    //�ش� ���� ���� ���� �� ����Ƽ ���Ҹ��� Ȱ�� ������(��:Sprite, Texture, Mesh ,Prefab, ��) ������
    //�̸� �޸𸮿� �ε��Ͽ� ��Ÿ�ӿ��� ������ �� �ֵ��� �ϴ� ����.
    //����: �̸� ������ �ش� ���ҽ��� ���ε� ���� �ʾƵ� ��Ÿ�ӿ��� �ε尡 ����.
    //�ε� �ӵ��� ������.
    //����: ��쿡 ���� ���ʿ��� ���ҽ��� �޸𸮸� �����ϰ� ���� �� ������, �����ڰ� ���� �����ϱⰡ �����.
    //������ ���ϵ��� �ε��ϰſ� Ȱ���� �� �����Ƿ�, ���� ������Ʈ�� ������Ÿ���ο� �ַ� ���̸�,
    //���̺꼭�� ���ӿ����� ����� �����ϴ� ��.
    //��ü ���� �ý��� : Asset Bundle->���� ����ϰ��ӿ� ���� ����
    //                 Addressable Assests -> �ֽ� ���ҽ� ���� �ý���. ���������� ���� �ʰ� �Ű�
    //                                         ����� ������ ���Ƽ� ���� �������� ���� �ʴ�.

    //Resources ������ �����ϴ� ��� : ��� �����ϰ� Assests������ ���� �����Ҽ� ������
    //���� ������ "/"�� �����Ͽ� ��θ� ����.
    //Assests/Textures/Resources/PlayerSprites/player1.png
    //Resource.Load("PlayerSprites/player1");

    public Texture texture;
    

    private void Start()
    {
        Sprite sprite1 = Resources.Load<Sprite>("sprite1");
        Sprite sprite2 = Resources.Load<Sprite>("sprite2");

        texture = Resources.Load<Texture>("sprite1");

        Enemy enemyResource = Resources.Load<Enemy>("Prefabs/EnemyResource");

        sp1.sprite = sprite1;
        sp2.sprite = sprite2;

        Instantiate(enemyResource);
        
    }

}
