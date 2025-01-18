#![allow(unused)]
use crate::common::SteppedSolver;

use super::animate_lights;

#[derive(Debug)]
pub struct Day18Solver {
  pub day: i32,
  pub year: i32,
  pub steps: Option<i32>,
}

impl SteppedSolver<Vec<Vec<i32>>, Vec<Vec<i32>>, i64, i64> for Day18Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<Vec<i32>> {
    input
      .iter()
      .map(|line| {
        line
          .chars()
          .map(|c| match c {
            '.' => 0,
            '#' => 1,
            _ => panic!("Invalid character in input"),
          })
          .collect()
      })
      .collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<Vec<i32>> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, starting_grid: Vec<Vec<i32>>) -> i64 {
    let steps = self.steps.unwrap_or(100);

    let on_lights = animate_lights(starting_grid, steps, false);
    on_lights as i64
  }

  fn solve_part_two(&self, starting_grid: Vec<Vec<i32>>) -> i64 {
    let steps = self.steps.unwrap_or(100);

    let on_lights = animate_lights(starting_grid, steps, true);
    on_lights as i64
  }
}
