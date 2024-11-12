using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillLevelupPanel : MonoBehaviour
{
	public RectTransform list;
	public SkillLevelupButton buttonPrefab;


	// �÷��̾ �������� �ϸ� �г� Ȱ��ȭ ��û
	public void LevelUpPanelOpen(List<Skill> skillList, Action<Skill> callback)
	{
		gameObject.SetActive(true);
		Time.timeScale = 0f;

		// ��ų 2�� UI�� ǥ���� ����
		List<Skill> selectedSkillList = new();
		while (selectedSkillList.Count < 2) // 2���� ��ų�� ���õ� ������ �ݺ�
		{
			int ranNum = Random.Range(0, skillList.Count);  // ������ ���� �ϳ� �̱�
			Skill selectedSkill = skillList[ranNum];        // �����ϰ� ���õ� ��ų ��������
			if (selectedSkillList.Contains(selectedSkill))  // �̹� ���� ��ų�� �� ��������
			{
				continue;
			}
			selectedSkillList.Add(selectedSkill);           // ���� ��ų�� List�� �ֱ�.

			// ���õ� ��ų�� �������ϴ� ��ư ����
			SkillLevelupButton skillButton = Instantiate(buttonPrefab, list);

			skillButton.SetSkillSelectButton(selectedSkill.skillName, () =>
			{
				callback(selectedSkill);
				LevelUpPanelClose();
			});
		}
	}

	// ������ �г��� ���� �� LevelUpPanelOpen�� callback�� ȣ��
	public void LevelUpPanelClose()
	{
		foreach (Transform buttons in list)
		{
			Destroy(buttons.gameObject);
		}
		Time.timeScale = 1f;
		gameObject.SetActive(false);
	}
}
