#![allow(unused)]
use crate::{
  common::SteppedSolver,
  days::day06::{compute_answers, CephaloProblemSet, Operation},
};

#[derive(Debug)]
pub struct Day06Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<CephaloProblemSet, CephaloProblemSet, i64, i64> for Day06Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> CephaloProblemSet {
    CephaloProblemSet::from(input, false)
  }

  fn parse_input_two(&self, input: Vec<&str>) -> CephaloProblemSet {
    CephaloProblemSet::from(input, true)
  }

  fn solve_part_one(&self, input: CephaloProblemSet) -> i64 {
    // vector to hold the results of the operations in each column; should match the number of columns of the grid
    let results = compute_answers(input);

    // Now return the sum of all column results
    results.iter().sum()
  }

  fn solve_part_two(&self, input: CephaloProblemSet) -> i64 {
    // vector to hold the results of the operations in each column; should match the number of columns of the grid
    let results = compute_answers(input);

    // Now return the sum of all column results
    results.iter().sum()
  }
}
