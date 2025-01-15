#![allow(unused)]
use crate::common::SteppedSolver;

use super::Ingredient;

#[derive(Debug)]
pub struct Day15Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Vec<Ingredient>, Vec<Ingredient>, i64, i64> for Day15Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<Ingredient> {
    input.iter().map(|x| Ingredient::from(*x)).collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<Ingredient> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, input: Vec<Ingredient>) -> i64 {
    unimplemented!()
  }

  fn solve_part_two(&self, input: Vec<Ingredient>) -> i64 {
    unimplemented!()
  }
}
