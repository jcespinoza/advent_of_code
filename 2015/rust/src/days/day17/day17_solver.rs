#![allow(unused)]
use crate::common::SteppedSolver;

use super::{get_container_combinations, get_min_container_combinations};

#[derive(Debug)]
pub struct Day17Solver {
  pub day: i32,
  pub year: i32,
  pub liters: Option<i32>,
}

impl SteppedSolver<Vec<i32>, Vec<i32>, i64, i64> for Day17Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<i32> {
    input.iter().map(|x| x.parse().unwrap()).collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<i32> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, original_sizes: Vec<i32>) -> i64 {
    let mut container_sizes = original_sizes.clone();
    container_sizes.sort();
    let combinations = get_container_combinations(container_sizes, self.liters.unwrap_or(150));

    combinations.len().try_into().unwrap()
  }

  fn solve_part_two(&self, original_sizes: Vec<i32>) -> i64 {
    let mut container_sizes = original_sizes.clone();
    container_sizes.sort();
    let combinations = get_min_container_combinations(container_sizes, self.liters.unwrap_or(150));

    combinations.len().try_into().unwrap()
  }
}
