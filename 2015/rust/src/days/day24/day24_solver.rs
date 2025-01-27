#![allow(unused)]
use crate::common::SteppedSolver;

use super::get_lowest_entanglement;

#[derive(Debug)]
pub struct Day24Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Vec<i32>, Vec<i32>, i64, i64> for Day24Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<i32> {
    input.iter().map(|x| x.parse().unwrap()).collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<i32> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, package_weights: Vec<i32>) -> i64 {
    get_lowest_entanglement(package_weights, 3)
  }

  fn solve_part_two(&self, package_weights: Vec<i32>) -> i64 {
    get_lowest_entanglement(package_weights, 4)
  }
}
