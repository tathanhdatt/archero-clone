using System;

[Serializable]
public class TypeGunDecoratorDict : SerializableDictionary<GunDecoratorType, GunDecorator>
{
}

[Serializable]
public class TypeGunDecoratorAndBoolDict : SerializableDictionary<GunDecoratorType, bool>
{
}

[Serializable]
public class TypeEffectAndBoolDict : SerializableDictionary<EffectType, bool>
{
}
[Serializable]
public class TypeEffectComponentDict : SerializableDictionary<EffectType, DamageDealEffectComponent>
{
}

[Serializable]
public class TypeAndUIEquipmentDict : SerializableDictionary<EquipmentType, EquipmentUI>
{
}
