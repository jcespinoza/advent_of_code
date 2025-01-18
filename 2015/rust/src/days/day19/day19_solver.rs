#![allow(unused)]
use crate::common::SteppedSolver;

use super::{Machine, Replacement};

#[derive(Debug)]
pub struct Day19Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Machine, Machine, i64, i64> for Day19Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Machine {
    // take all but the last two elements from the input
    let replacements = input
      .iter()
      .take(input.len() - 2)
      .map(|x| Replacement::from(*x))
      .collect();

    // take the last element from the input
    let target_molecule = input[input.len() - 1].to_string();
    Machine {
      replacements,
      target_molecule,
    }
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Machine {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, input: Machine) -> i64 {
    unimplemented!()
  }

  fn solve_part_two(&self, input: Machine) -> i64 {
    unimplemented!()
  }
}
