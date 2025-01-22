use std::cmp::min;

pub static NONE_NAME: &str = "None";
#[derive(Debug, Clone)]
pub struct Character {
  pub hit_points: i32,
  pub armor: i32,
  pub damage: i32,
}

#[derive(Debug, PartialEq)]
pub enum ItemType {
  Weapon,
  Armor,
  Ring,
}

pub static WEAPONS: [ShopItem; 5] = [
  ShopItem {
    item_type: ItemType::Weapon,
    name: "Dagger",
    cost: 8,
    damage: 4,
    armor: 0,
  },
  ShopItem {
    item_type: ItemType::Weapon,
    name: "Shortsword",
    cost: 10,
    damage: 5,
    armor: 0,
  },
  ShopItem {
    item_type: ItemType::Weapon,
    name: "Warhammer",
    cost: 25,
    damage: 6,
    armor: 0,
  },
  ShopItem {
    item_type: ItemType::Weapon,
    name: "Longsword",
    cost: 40,
    damage: 7,
    armor: 0,
  },
  ShopItem {
    item_type: ItemType::Weapon,
    name: "Greataxe",
    cost: 74,
    damage: 8,
    armor: 0,
  },
];

pub static ARMOR: [ShopItem; 6] = [
  ShopItem {
    item_type: ItemType::Armor,
    name: NONE_NAME,
    cost: 0,
    damage: 0,
    armor: 0,
  },
  ShopItem {
    item_type: ItemType::Armor,
    name: "Leather",
    cost: 13,
    damage: 0,
    armor: 1,
  },
  ShopItem {
    item_type: ItemType::Armor,
    name: "Chainmail",
    cost: 31,
    damage: 0,
    armor: 2,
  },
  ShopItem {
    item_type: ItemType::Armor,
    name: "Splintmail",
    cost: 53,
    damage: 0,
    armor: 3,
  },
  ShopItem {
    item_type: ItemType::Armor,
    name: "Bandedmail",
    cost: 75,
    damage: 0,
    armor: 4,
  },
  ShopItem {
    item_type: ItemType::Armor,
    name: "Platemail",
    cost: 102,
    damage: 0,
    armor: 5,
  },
];

pub static RINGS: [ShopItem; 8] = [
  ShopItem {
    item_type: ItemType::Ring,
    name: NONE_NAME,
    cost: 0,
    damage: 0,
    armor: 0,
  },
  ShopItem {
    item_type: ItemType::Ring,
    name: NONE_NAME,
    cost: 0,
    damage: 0,
    armor: 0,
  },
  ShopItem {
    item_type: ItemType::Ring,
    name: "Damage +1",
    cost: 25,
    damage: 1,
    armor: 0,
  },
  ShopItem {
    item_type: ItemType::Ring,
    name: "Damage +2",
    cost: 50,
    damage: 2,
    armor: 0,
  },
  ShopItem {
    item_type: ItemType::Ring,
    name: "Damage +3",
    cost: 100,
    damage: 3,
    armor: 0,
  },
  ShopItem {
    item_type: ItemType::Ring,
    name: "Defense +1",
    cost: 20,
    damage: 0,
    armor: 1,
  },
  ShopItem {
    item_type: ItemType::Ring,
    name: "Defense +2",
    cost: 40,
    damage: 0,
    armor: 2,
  },
  ShopItem {
    item_type: ItemType::Ring,
    name: "Defense +3",
    cost: 80,
    damage: 0,
    armor: 3,
  },
];

#[derive(Debug, PartialEq)]
pub struct ItemStat {
  pub cost: i32,
  pub damage: i32,
  pub armor: i32,
}

#[derive(Debug, PartialEq)]
pub struct ShopItem<'a> {
  pub item_type: ItemType,
  pub name: &'a str,
  pub cost: i32,
  pub damage: i32,
  pub armor: i32,
}

impl Character {
  pub fn new(hit_points: i32, armor: i32, damage: i32) -> Character {
    Character {
      hit_points,
      armor,
      damage,
    }
  }

  pub fn new_with_items(hit_points: i32, items: Vec<&ShopItem>) -> Character {
    let mut character = Character::new(hit_points, 0, 0);

    assert!(items.len() <= 4 && !items.is_empty());

    for item in items {
      character.equip(item);
    }
    character
  }

  pub fn equip(&mut self, item: &ShopItem) {
    match item.item_type {
      ItemType::Weapon => self.damage += item.damage,
      ItemType::Armor => self.armor += item.armor,
      ItemType::Ring => {
        self.damage += item.damage;
        self.armor += item.armor;
      }
    }
  }

  pub fn wins_against(&self, other: &Character) -> bool {
    let turns_for_player = divide_with_rounding(
      other.hit_points,
      get_actual_damage(self.damage, other.armor),
    );
    let turns_for_other =
      divide_with_rounding(self.hit_points, get_actual_damage(other.damage, self.armor));

    turns_for_player <= turns_for_other
  }
}

fn divide_with_rounding(a: i32, b: i32) -> i32 {
  (a / b) + (a % b != 0) as i32
}

pub fn get_actual_damage(damage: i32, armor: i32) -> i32 {
  let raw_damage = damage - armor;
  if raw_damage < 1 {
    1
  } else {
    raw_damage
  }
}
