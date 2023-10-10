using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGUIToggleGroup : MonoBehaviour
{
    public CustomGUIToggle[] toggles;

    //��¼��һ��Ϊtrue��toggle
    private CustomGUIToggle frontTurTog;
    private void Start()
    {
        if (toggles.Length == 0)
            return;

        //ͨ��������Ϊÿ��Toggle��Ӽ����¼�����
        for(int i = 0; i < toggles.Length; i++)
        {
            CustomGUIToggle toggle = toggles[i];
            toggle.changeValue += (value) =>
            {
                //�������value��trueʱ Ҫ�������������false
                if (value)
                {
                    for (int j = 0; j < toggles.Length; j++)
                    {
                        //�����бհ� toggle����һ�������������ı���
                        //����ı���������������
                        if (toggles[j] != toggle)
                        {
                            toggles[j].isSel = false;
                        }
                    }
                    //��¼��һ��Ϊtrue��toggle
                    frontTurTog = toggle;
                }
                //�������ֵ��false ��ʱҪ�ж� 
                //��ǰ���false�����toggle�ǲ�����һ��Ϊtrue��
                //����� �Ͳ�Ӧ���������false
                else if (toggle == frontTurTog) 
                {
                    //ǿ�Ƹĳ�true
                    toggle.isSel = true;
                }
            };
        }
    }
}
