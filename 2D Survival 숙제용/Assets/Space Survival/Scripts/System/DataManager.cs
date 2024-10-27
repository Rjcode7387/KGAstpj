using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonManager<DataManager>
{
	// PlayerPrefs Ŭ���� : ����̽��� ����� ���� �����͸� �ҷ����ų� ���� �����͸� �����ϴ� ����� ����ϴ� Ŭ����
	// �ַ� ���� �Լ��� ȣ���Ͽ� ��� Ȱ��

	public bool clearPrefsOnStart;

	IEnumerator Start()
	{
		if (clearPrefsOnStart)
		{
			PlayerPrefs.DeleteAll();  // ��� ���� ������ ����
		}
		yield return null;  // �� ������ ����
		OnLoad();
	}

	// Load
	public void OnLoad()
	{
		// ����̽��� ����� ���� ������ �� ���� Ű�� �ش��ϴ� ���� ������ (Ű:string, �⺻��:int)
		int totalKillCount = PlayerPrefs.GetInt("TotalKillCount", 0); // ���嵥���Ͱ� ��� null ������ ���� �⺻���� ������ �� �ִ� �����ε�� �Լ��� �ִ�.
		GameManager.Instance.player.totalKillCount = totalKillCount;
	}

	// Save
	public void OnSave()
	{
		int totalKillCount = GameManager.Instance.player.totalKillCount;

		// ���� PlayerPrefs�� ĳ�ÿ� ���� �Է� (Ű:string, ��:object)
		PlayerPrefs.SetInt("TotalKillCount", totalKillCount);

		// �������� �� Save()�� ȣ���ؾ� ���� �Ϸ�
		PlayerPrefs.Save();
	}

	// ���μ����� ����� �� ȣ��Ǵ� �޽��� �Լ�
	private void OnApplicationQuit()
	{
		OnSave();
	}
}
