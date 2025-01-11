#![allow(unused)]
use crate::common::SteppedSolver;

use super::{parse_instruction, Instruction};

#[derive(Debug)]
pub struct Day06Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Vec<Instruction>, Vec<Instruction>, i64, i64> for Day06Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<Instruction> {
    input
      .iter()
      .map(|string_ref| Instruction::parse(string_ref))
      .collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<Instruction> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, instructions: Vec<Instruction>) -> i64 {
    // A grid of boolean values, every cell initialized with false
    let mut light_grid = vec![vec![false; 1000]; 1000];

    for instruction in instructions {
      // Apply every instruction on the grid
      match instruction {
        Instruction::TurnOn(start, end) => {
          for row in start.row..=end.row {
            for col in start.col..=end.col {
              light_grid[row as usize][col as usize] = true;
            }
          }
        }
        Instruction::TurnOff(start, end) => {
          for row in start.row..=end.row {
            for col in start.col..=end.col {
              light_grid[row as usize][col as usize] = false;
            }
          }
        }
        Instruction::Toggle(start, end) => {
          for row in start.row..=end.row {
            for col in start.col..=end.col {
              light_grid[row as usize][col as usize] = !light_grid[row as usize][col as usize];
            }
          }
        }
      }
    }

    let count_lights_on = light_grid
      .iter()
      .flatten()
      .filter(|is_it_on| **is_it_on)
      .count();

    count_lights_on as i64
  }

  fn solve_part_two(&self, instructions: Vec<Instruction>) -> i64 {
    unimplemented!()
  }
}
