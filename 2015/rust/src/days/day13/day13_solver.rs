#![allow(unused)]
use std::collections::{HashMap, HashSet};

use itertools::Itertools;

use crate::common::SteppedSolver;

use super::{SeatingCondition, UnitChange};

#[derive(Debug)]
pub struct Day13Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Vec<SeatingCondition>, Vec<SeatingCondition>, i64, i64> for Day13Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<SeatingCondition> {
    input.iter().map(|x| SeatingCondition::from(*x)).collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<SeatingCondition> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, input: Vec<SeatingCondition>) -> i64 {
    let all_guests: HashSet<String> = input.iter().map(|x| x.guest.clone()).collect();
    let happiness_map = get_happiness_map(input, &all_guests);

    compute_max_happiness(all_guests, happiness_map)
  }

  fn solve_part_two(&self, input: Vec<SeatingCondition>) -> i64 {
    let mut all_guests: HashSet<String> = input.iter().map(|x| x.guest.clone()).collect();
    all_guests.insert("Myself".to_string());
    let happiness_map = get_happiness_map(input, &all_guests);

    compute_max_happiness(all_guests, happiness_map)
  }
}

fn compute_max_happiness(
  all_guests: HashSet<String>,
  happiness_map: HashMap<(String, String), i32>,
) -> i64 {
  let mut max_happiness = 0;
  for seating_arrangement in all_guests.iter().permutations(all_guests.len()) {
    let mut happiness = 0;
    for i in 0..seating_arrangement.len() {
      let guest = &seating_arrangement[i];
      let neighbor = &seating_arrangement[(i + 1) % seating_arrangement.len()];
      happiness += match guest {
        val if val.as_str() == "Myself" => &0,
        _ => happiness_map
          .get(&(guest.to_string(), neighbor.to_string()))
          .unwrap(),
      };
      happiness += match neighbor {
        val if val.as_str() == "Myself" => &0,
        _ => happiness_map
          .get(&(neighbor.to_string(), guest.to_string()))
          .unwrap(),
      };
    }

    if happiness > max_happiness {
      max_happiness = happiness;
    }
  }
  max_happiness as i64
}

fn get_happiness_map(
  input: Vec<SeatingCondition>,
  all_guests: &HashSet<String>,
) -> HashMap<(String, String), i32> {
  let mut happiness_map: HashMap<(String, String), i32> = HashMap::new();
  for guest in all_guests.iter() {
    for neighbor in all_guests.iter() {
      if guest == neighbor {
        continue;
      }

      let mut happiness = 0;
      for condition in input.iter() {
        if condition.guest == *guest && condition.neighbor == *neighbor {
          match condition.change {
            UnitChange::Gain(units) => happiness += units,
            UnitChange::Lose(units) => happiness -= units,
          }
        }
      }

      happiness_map.insert((guest.clone(), neighbor.clone()), happiness);
    }
  }
  happiness_map
}
