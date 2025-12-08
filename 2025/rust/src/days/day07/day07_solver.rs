#![allow(unused)]
use crate::{common::SteppedSolver, days::day07::TachyonManifold};

#[derive(Debug)]
pub struct Day07Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<TachyonManifold, TachyonManifold, i64, i64> for Day07Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> TachyonManifold {
    TachyonManifold::from(input.join("\n").as_str())
  }

  fn parse_input_two(&self, input: Vec<&str>) -> TachyonManifold {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, mut input: TachyonManifold) -> i64 {
    // trigger the beam and extend the tachyon paths
    input.extend_tachyon_beams();

    // Count the number of splitters that were hit by tachyons
    input.count_hit_splitters()
  }

  fn solve_part_two(&self, input: TachyonManifold) -> i64 {
    unimplemented!()
  }
}
