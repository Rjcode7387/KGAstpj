using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
using UnityEngine.UI;

public class AttributeController : MonoBehaviour
{
    //scene에 모든 color 어트리뷰트를 찾아서 색을 입혀주는 역할로 만들고 싶다.
    private void Start()
    {

        ProcessSizeAttribute(this);
        //Color Attribute를 가진 필드를 찾자
        BindingFlags bind = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        MonoBehaviour[] monoBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

        foreach (MonoBehaviour monoBehaviour in monoBehaviours)
        {
            Type type = monoBehaviour.GetType();// 타입 정보를 가져옴.

            //List<FieldInfo> fieldInfos = new List<FieldInfo>(type.GetFields(bind));\           
            //List<FieldInfo>colorAttributeAttachedFields = fieldInfos.FindAll((x) => { return x.HasAttribute<ColorAttribute>(); });

            //리스트 등 collection에서 탐색은
            //Linq를 통해 간소화 할 수도 있음.
            //1.Linq에서 제공하는 확장 메서드 사용
            IEnumerable<FieldInfo> colorAttachedFields =
                type.GetFields(bind).Where(x => x.HasAttribute<ColorAttribute>());


            //2.SQL, 쿼리문과 비슷한 형태로도 사용이 가능. 
            colorAttachedFields = from field in type.GetFields(bind)
                                  where field.HasAttribute<ColorAttribute>()
                                  select field;

            foreach (FieldInfo fieldInfo in colorAttachedFields)
            {
                ColorAttribute att = fieldInfo.GetCustomAttribute<ColorAttribute>();
                object value = fieldInfo.GetValue(monoBehaviour);

                if (value is Renderer rend)
                {
                    rend.material.color = att.color;
                }
                else if (value is Graphic graph)
                {
                    graph.color = att.color;
                }
                else 
                {
                    Debug.LogError("Color 어트리뷰트가 잘못된 곳으로 가버렷어..");

                }
            }
        
        
        }
    
    }
    private void ProcessSizeAttribute(Component component)
    {
        BindingFlags bind = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        Type type = component.GetType();

        IEnumerable<FieldInfo> sizeAttachedFields =
            type.GetFields(bind).Where(x => x.HasAttribute<SizeAttribute>());

        foreach(FieldInfo fieldinfo in sizeAttachedFields)
        { 
            var sizeAttr = fieldinfo.GetCustomAttribute<SizeAttribute>();
            if (component.TryGetComponent<Transform>(out var recttransform))
            {
                recttransform.localScale = sizeAttr.size;
            }
            else if (component.TryGetComponent<Transform>(out var transform))           
            {
                transform.localScale = new Vector3(sizeAttr.size.x, sizeAttr.size.y);

            }
        }
    
    }


}
[AttributeUsage(AttributeTargets.Field,AllowMultiple = false,Inherited = true)]
public class ColorAttribute : Attribute
{
    public Color color;
    public ColorAttribute(float r = 0, float g = 0, float b = 0, float a = 1)
    {
        color = new Color(r, g, b, a);
    }

}

public static class AttributeHelper
{
    public static bool HasAttribute<T>(this MemberInfo info) where T : Attribute
    {
        return info.GetCustomAttributes(typeof(T),true).Length > 0;
    }
}



