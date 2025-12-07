#![allow(unused)]
use crate::{common::SteppedSolver, days::day05::IngrendientDb};

#[derive(Debug)]
pub struct Day05Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<IngrendientDb, IngrendientDb, i64, i64> for Day05Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> IngrendientDb {
    IngrendientDb::from(input)
  }

  fn parse_input_two(&self, input: Vec<&str>) -> IngrendientDb {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, input: IngrendientDb) -> i64 {
    let mut fresh_count: i64 = 0;

    // Iterate ver the available ingredient IDs
    for &id in &input.available_ids {
      // Check if the ID falls within any of the fresh ID ranges
      let is_fresh = input
        .fresh_id_ranges
        .iter()
        .any(|&(start, end)| id >= start && id <= end);
      if is_fresh {
        fresh_count += 1;
      }
    }
    fresh_count
  }

  fn solve_part_two(&self, input: IngrendientDb) -> i64 {
    unimplemented!()
  }
}
