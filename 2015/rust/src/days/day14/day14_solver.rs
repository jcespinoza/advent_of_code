#![allow(unused)]
use std::collections::HashMap;

use crate::common::SteppedSolver;

use super::{compute_scores_after_seconds, Reindeer, Stat};

#[derive(Debug)]
pub struct Day14Solver {
  pub day: i32,
  pub year: i32,
  pub time: Option<i32>,
}

impl SteppedSolver<Vec<Reindeer>, Vec<Reindeer>, i64, i64> for Day14Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<Reindeer> {
    input.iter().map(|s| Reindeer::from(*s)).collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<Reindeer> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, reindeers: Vec<Reindeer>) -> i64 {
    let scores: HashMap<String, Stat> =
      compute_scores_after_seconds(&reindeers, self.time.unwrap_or(2503));

    let winning_distance = scores.values().map(|s| s.distance).max().unwrap();
    winning_distance as i64
  }

  fn solve_part_two(&self, reindeers: Vec<Reindeer>) -> i64 {
    let scores: HashMap<String, Stat> =
      compute_scores_after_seconds(&reindeers, self.time.unwrap_or(2503));

    let winning_points = scores.values().map(|s| s.points).max().unwrap();
    winning_points as i64
  }
}
