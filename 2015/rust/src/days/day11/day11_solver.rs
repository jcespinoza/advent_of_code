#![allow(unused)]
use std::iter;

use crate::{
  common::SteppedSolver,
  days::day11::{compute_next_password, is_password_valid},
};

use super::{generate_next_valid_password, Password};

#[derive(Debug)]
pub struct Day11Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<String, String, String, String> for Day11Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> String {
    input.first().unwrap().to_string()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> String {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, old_password: String) -> String {
    generate_next_valid_password(Password::from(old_password.as_str()))
  }

  fn solve_part_two(&self, old_password: String) -> String {
    unimplemented!()
  }
}
