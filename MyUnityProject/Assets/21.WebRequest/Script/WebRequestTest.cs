using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class WebRequestTest : MonoBehaviour
{
    public string imageURL = "https://picsum.photos/500";
    public Image targetImage;
    public RawImage targetRawImage;

    private void Start()
    {
        _ = StartCoroutine(GetWebTexture(imageURL));
    }

    IEnumerator GetWebTexture(string url)
    {
        //http�� �� ��û(Request)�� ���� ��ü ����

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

        //�ڷ�ƾ�� ���� �����κ��� ����(Response)�� ���� ������ �񵿱�� ����ϴ� ��ü�� �޾���
        UnityWebRequestAsyncOperation operation = www.SendWebRequest();

        yield return operation;

        if (www.result != UnityWebRequest.Result.Success)
        {
            //�� ��� ���� �߻�
            print($" Http  ��� ���� : {www.error}");
        }
        else
        {
            //�ؽ��� �ٿ�ε� ����
            Texture texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            //�ؽ��ĸ� ��Ÿ�ӿ��� Sprite�� ��ȯ
            Sprite sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            //image�� sprite ��ü
            targetImage.sprite = sprite;

            targetRawImage.texture = texture;

        }
    }



}
