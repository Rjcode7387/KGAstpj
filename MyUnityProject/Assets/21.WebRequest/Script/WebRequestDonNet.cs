using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class WebRequestDonNet : MonoBehaviour
{
    public string imageURL = "https://picsum.photos/500";
    public Image targetImage;
    public RawImage targetRawImage;

    private async void Start()
    {
        await GetTexture();
        print("GetTexture() ȣ������!");
    }

    private async Task GetTexture()
    {
        using (HttpClient client = new HttpClient())
        {
            //await Ű���尡 ������ ���⼭ ������ ��ȯ  �� ������ �񵿱� ���·� �����
            byte[] response = await client.GetByteArrayAsync(imageURL);

            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(response);
            targetRawImage.texture = texture;
            //��������Ʈ ��ȯ �غ���
            print("Texture �ٿ�ε� �Ϸ�");
            Sprite sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            targetImage.sprite = sprite;

        }
    }
}
