#![allow(unused)]
use crate::common::SteppedSolver;

use super::{find_aunt, AuntSue, MachineResult};

#[derive(Debug)]
pub struct Day16Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Vec<AuntSue>, Vec<AuntSue>, i64, i64> for Day16Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<AuntSue> {
    input.iter().map(|s| AuntSue::from(*s)).collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<AuntSue> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, aunts: Vec<AuntSue>) -> i64 {
    let machine_result = MachineResult {
      children: 3,
      cats: 7,
      samoyeds: 2,
      pomeranians: 3,
      akitas: 0,
      vizslas: 0,
      goldfish: 5,
      trees: 3,
      cars: 2,
      perfumes: 1,
    };

    find_aunt(&aunts, &machine_result)
  }

  fn solve_part_two(&self, aunts: Vec<AuntSue>) -> i64 {
    unimplemented!()
  }
}
