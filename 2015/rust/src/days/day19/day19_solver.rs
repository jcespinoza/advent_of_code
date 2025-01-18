#![allow(unused)]
use std::collections::HashSet;

use crate::common::SteppedSolver;

use super::{common_parse, generate_distinct_molecules, Machine, Replacement};

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

  fn solve_part_two(&self, input: Machine) -> i64 {
    let mut steps = 0;
    let mut target = input.target_molecule.clone();
    let mut replacements = input.replacements.clone();

    while target != "e" {
      for replacement in replacements.iter() {
        if target.contains(&replacement.result) {
          target = target.replacen(&replacement.result, &replacement.source, 1);
          steps += 1;
          break;
        }
      }
    }

    steps
  }
}
