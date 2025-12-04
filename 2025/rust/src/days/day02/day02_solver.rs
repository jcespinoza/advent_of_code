#![allow(unused)]
use crate::{
  common::SteppedSolver,
  days::day02::{identify_invalid_ids_part_one, Range},
};

#[derive(Debug)]
pub struct Day02Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Vec<Range>, Vec<i32>, i64, i64> for Day02Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<Range> {
    // The input will be a list of ranges in the format "start-end", separated by commas
    input
      .iter()
      .flat_map(|line| line.split(','))
      .map(Range::from)
      .collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<i32> {
    unimplemented!()
  }

  fn solve_part_one(&self, input: Vec<Range>) -> i64 {
    let mut invalid_ids: Vec<i64> = Vec::new();

    for range in input.iter() {
      let mut ids = identify_invalid_ids_part_one(range);
      invalid_ids.append(&mut ids);
    }

    invalid_ids.iter().copied().sum()
  }

  fn solve_part_two(&self, input: Vec<i32>) -> i64 {
    unimplemented!()
  }
}
