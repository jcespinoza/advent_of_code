#![allow(unused)]
use crate::common::SteppedSolver;

use super::{process_battle, Battle, BattleResult, Boss};

#[derive(Debug)]
pub struct Day22Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Boss, Boss, i64, i64> for Day22Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Boss {
    /* input is a vec of two lines of the form:
       Hit Points: X
       Damage: Y
    */

    let mut boss_hit_points = 0;
    let mut boss_damage = 0;

    let parsed_numbers: Vec<i32> = input
      .iter()
      .map(|s| {
        let parts = s.split(": ");
        let mut parts = parts.skip(1);
        parts.next().unwrap().parse::<i32>().unwrap()
      })
      .collect();

    boss_hit_points = parsed_numbers[0];
    boss_damage = parsed_numbers[1];

    Boss {
      hit_points: boss_hit_points,
      damage: boss_damage,
    }
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Boss {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, boss: Boss) -> i64 {
    let battle = create_battle(boss);

    let result: BattleResult = process_battle(battle, false);

    result.mana_spent as i64
  }

  fn solve_part_two(&self, boss: Boss) -> i64 {
    let battle = create_battle(boss);

    let result: BattleResult = process_battle(battle, true);

    result.mana_spent as i64
  }
}

fn create_battle(boss: Boss) -> Battle {
  let battle = Battle {
    player_hp: 50,
    player_mana: 500,
    player_armor: 0,
    player_mana_spent: 0,
    boss_hp: boss.hit_points,
    boss_damage: boss.damage,
    shield_timer: 0,
    poison_timer: 0,
    recharge_timer: 0,
  };
  battle
}
