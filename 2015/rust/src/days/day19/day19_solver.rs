#![allow(unused)]
use std::collections::HashSet;

use itertools::Itertools;

use crate::common::SteppedSolver;

use super::{
  common_parse, generate_distinct_molecules, get_steps_until_target, Machine, Replacement,
};

#[derive(Debug)]
pub struct Day19Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Machine, Machine, i64, i64> for Day19Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Machine {
    common_parse(input, false)
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Machine {
    common_parse(input, true)
  }

  fn solve_part_one(&self, machine: Machine) -> i64 {
    let distinct_molecules: HashSet<String> = generate_distinct_molecules(&machine);
    distinct_molecules.len() as i64
  }

  fn solve_part_two(&self, machine: Machine) -> i64 {
    let sorted_replacements_iter = machine
      .replacements
      .iter()
      .sorted_by(|a, b| b.source.cmp(&a.source));
    let replacements: Vec<Replacement> = sorted_replacements_iter.cloned().collect();
    get_steps_until_target(&replacements, &machine.target_molecule)
  }
}
