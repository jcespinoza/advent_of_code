use std::collections::HashMap;

use crate::common::SteppedSolver;

#[derive(Debug)]
pub struct Day01Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<String, String, i64, i64> for Day01Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> String {
    input.first().unwrap().to_string()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> String {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, instructions: String) -> i64 {
    let mut current_floor = 0;
    for instruction in instructions.chars() {
      match instruction {
        '(' => current_floor += 1,
        ')' => current_floor -= 1,
        _ => (),
      }
    }
    current_floor
  }

  fn solve_part_two(&self, instructions: String) -> i64 {
    let mut current_floor = 0;
    for (index, instruction) in instructions.chars().enumerate() {
      match instruction {
        '(' => current_floor += 1,
        ')' => current_floor -= 1,
        _ => (),
      }
      if current_floor == -1 {
        return index as i64 + 1;
      }
    }
    -1
  }
}
