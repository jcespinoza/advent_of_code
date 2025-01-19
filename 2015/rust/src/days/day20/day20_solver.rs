#![allow(unused)]
use std::collections::HashMap;

use crate::common::SteppedSolver;

#[derive(Debug)]
pub struct Day20Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<usize, usize, i64, i64> for Day20Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> usize {
    input.first().unwrap().parse().unwrap()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> usize {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, target_present_count: usize) -> i64 {
    let mut house_present_count = vec![0; (target_present_count / 10)];

    for elf_number in 1..house_present_count.len() {
      for house_number in (elf_number..house_present_count.len()).step_by(elf_number) {
        house_present_count[house_number] += elf_number * 10;
      }
    }

    let house_with_target_count = house_present_count
      .iter()
      .position(|h| h >= &target_present_count)
      .unwrap();

    house_with_target_count as i64
  }

  fn solve_part_two(&self, target_present_count: usize) -> i64 {
    let mut house_present_count = vec![0; (target_present_count / 10)];

    for elf_number in 1..house_present_count.len() {
      for house_number in (elf_number..house_present_count.len())
        .step_by(elf_number)
        .take(50)
      {
        house_present_count[house_number] += elf_number * 11;
      }
    }

    let house_with_target_count = house_present_count
      .iter()
      .position(|h| h >= &target_present_count)
      .unwrap();

    house_with_target_count as i64
  }
}
