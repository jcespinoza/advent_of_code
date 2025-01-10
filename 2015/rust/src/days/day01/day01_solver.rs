use std::collections::HashMap;

use crate::common::SteppedSolver;

#[derive(Debug)]
pub struct Day01Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Vec<(i32, i32)>, Vec<(i32, i32)>, i64, i64> for Day01Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<(i32, i32)> {
    todo!()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<(i32, i32)> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, pairs: Vec<(i32, i32)>) -> i64 {
    todo!()
  }

  #[allow(unused)]
  fn solve_part_two(&self, pairs: Vec<(i32, i32)>) -> i64 {
    todo!()
  }
}
