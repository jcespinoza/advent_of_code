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

    // proactively forcing on the lights located at the corners to keep the logic simple
    let mut starting_grid = starting_grid.clone();
    let last_col = starting_grid[0].len() - 1;
    let last_row = starting_grid.len() - 1;
    starting_grid[0][0] = 1;
    starting_grid[0][last_col] = 1;
    starting_grid[last_row][0] = 1;
    starting_grid[last_row][last_col] = 1;

    let on_lights = animate_lights(starting_grid, steps, true);
    on_lights as i64
  }
}
