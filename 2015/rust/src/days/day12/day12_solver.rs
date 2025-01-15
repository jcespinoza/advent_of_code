#![allow(unused)]
use crate::common::SteppedSolver;

use super::sum_numbers;

#[derive(Debug)]
pub struct Day12Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<serde_json::Value, serde_json::Value, i64, i64> for Day12Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> serde_json::Value {
    let input_str = input.join("");
    serde_json::from_str(&input_str).unwrap()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> serde_json::Value {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, input: serde_json::Value) -> i64 {
    let total_sum: i64 = sum_numbers(&input, None);
    total_sum
  }

  fn solve_part_two(&self, input: serde_json::Value) -> i64 {
    let total_sum: i64 = sum_numbers(&input, Some("red"));
    total_sum
  }
}
