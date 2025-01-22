#![allow(unused)]
use itertools::{iproduct, Itertools};

use crate::{
  common::SteppedSolver,
  days::day21::{ItemStat, ARMOR, NONE_NAME, RINGS, WEAPONS},
};

use super::{Character, ShopItem};

#[derive(Debug)]
pub struct Day21Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Character, Character, i64, i64> for Day21Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Character {
    let stats: Vec<&str> = input
      .iter()
      .map(|line| line.split(": ").collect::<Vec<&str>>())
      .map(|list| list[1])
      .collect();

    Character {
      hit_points: stats[0].parse::<i32>().unwrap(),
      damage: stats[1].parse::<i32>().unwrap(),
      armor: stats[2].parse::<i32>().unwrap(),
    }
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Character {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, boss_stats: Character) -> i64 {
    let all_item_combos: Vec<ItemStat> = iproduct!(&WEAPONS, &ARMOR, &RINGS, &RINGS)
      .filter(|(_, _, ring_left, ring_right)| {
        // can not equip same ring on both hands
        ring_left.name != ring_right.name || ring_left.name == NONE_NAME
      })
      .map(|(weapon, armor, ring_left, ring_right)| {
        let cost = weapon.cost + armor.cost + ring_left.cost + ring_right.cost;
        let damage = weapon.damage + ring_left.damage + ring_right.damage;
        let armor = armor.armor + ring_left.armor + ring_right.armor;
        ItemStat {
          cost,
          damage,
          armor,
        }
      })
      .filter(|combo| {
        let player = Character::new(100, combo.armor, combo.damage);
        player.wins_against(&boss_stats)
      })
      .sorted_by(|a, b| a.cost.cmp(&b.cost))
      .collect();

    for combo in all_item_combos {
      let player = Character::new(100, combo.armor, combo.damage);
      if player.wins_against(&boss_stats) {
        return combo.cost as i64;
      }
    }

    0
  }

  fn solve_part_two(&self, boss_stats: Character) -> i64 {
    unimplemented!()
  }
}
