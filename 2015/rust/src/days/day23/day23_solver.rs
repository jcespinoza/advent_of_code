#![allow(unused)]
use crate::common::SteppedSolver;

use super::{Computer, Instruction};

#[derive(Debug)]
pub struct Day23Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Vec<Instruction>, Vec<Instruction>, i64, i64> for Day23Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<Instruction> {
    input.iter().map(|x| Instruction::from(*x)).collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<Instruction> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, instructions: Vec<Instruction>) -> i64 {
    let mut computer = Computer::default();

    computer.run_program(&instructions);

    computer.reg_b
  }

  fn solve_part_two(&self, instructions: Vec<Instruction>) -> i64 {
    let mut computer = Computer::default();
    computer.reg_a = 1;
    
    computer.run_program(&instructions);

    computer.reg_b
  }
}
