using UnityEditor;

[CustomPropertyDrawer(typeof(TypeGunDecoratorDict))]
[CustomPropertyDrawer(typeof(TypeGunDecoratorAndBoolDict))]
[CustomPropertyDrawer(typeof(TypeEffectAndBoolDict))]
[CustomPropertyDrawer(typeof(TypeEffectComponentDict))]
[CustomPropertyDrawer(typeof(TypeAndUIEquipmentDict))]
public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}