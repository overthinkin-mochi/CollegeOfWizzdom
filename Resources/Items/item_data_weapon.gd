extends ItemData
class_name ItemDataWeapon

enum WeaponType {MELEE ,THROWABLE ,RANGED ,MAGIC}

@export var Attack: int
@export var Speed: int
@export var weaponType: WeaponType
