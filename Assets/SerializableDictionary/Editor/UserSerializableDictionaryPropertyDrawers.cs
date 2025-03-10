using UnityEditor;

[CustomPropertyDrawer(typeof(TypeGunDecoratorDict))]
[CustomPropertyDrawer(typeof(TypeGunDecoratorAndBoolDict))]
[CustomPropertyDrawer(typeof(TypeEffectAndBoolDict))]
[CustomPropertyDrawer(typeof(TypeEffectComponentDict))]
public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}