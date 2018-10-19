using UnityEngine;

public static class Copy
{
    public static T CopyComponentToGameobject<T>(T origin, GameObject target) 
        where T : Component
    {
        System.Type type = origin.GetType();
        Component copyComponent = target.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();

        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copyComponent, field.GetValue(origin));
            Debug.Log(field.GetValue(origin));
        }

        return copyComponent as T;
    }
}
