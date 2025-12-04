#![allow(unused)]
use crate::{common::SteppedSolver, days::day03::BatteryBank};

#[derive(Debug)]
pub struct Day03Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Vec<BatteryBank>, Vec<BatteryBank>, i64, i64> for Day03Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<BatteryBank> {
    // Each line represents a battery bank
    input.iter().map(|line| BatteryBank::from(*line)).collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<BatteryBank> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, input: Vec<BatteryBank>) -> i64 {
    let number_of_batteries = 2;
    let mut total_charge: i64 = 0;

    for bank in input {
      let top_batteries = bank.top_n_batteries(number_of_batteries);
      let charge_str: String = top_batteries.iter().map(|b| b.to_string()).collect();
      let charge = charge_str.parse::<i64>().unwrap();
      total_charge += charge;
    }

    total_charge
  }

  fn solve_part_two(&self, input: Vec<BatteryBank>) -> i64 {
    let number_of_batteries = 12;
    let mut total_charge: i64 = 0;

    for bank in input {
      let top_batteries = bank.top_n_batteries(number_of_batteries);
      let charge_str: String = top_batteries.iter().map(|b| b.to_string()).collect();
      let charge = charge_str.parse::<i64>().unwrap();
      total_charge += charge;
    }

    total_charge
  }
}
