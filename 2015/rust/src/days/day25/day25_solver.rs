#![allow(unused)]
use crate::{common::SteppedSolver, days::day03::Point};

use super::compute_code_at;

#[derive(Debug)]
pub struct Day25Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Point, Point, i64, i64> for Day25Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Point {
    let locations = input
      .first()
      .unwrap()
      .split(". ")
      .skip(1)
      .map(|sentence| {
        let parts = sentence.split(", ").collect::<Vec<&str>>();
        let row = parts[0].split(" ").last().unwrap().parse::<i32>().unwrap();
        let col = parts[1]
          .split(" ")
          .last()
          .unwrap()
          .trim_end_matches(".")
          .parse::<i32>()
          .unwrap();
        Point { row, col }
      })
      .collect::<Vec<Point>>();

    *locations.first().unwrap()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Point {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, target_location: Point) -> i64 {
    compute_code_at(target_location)
  }

  fn solve_part_two(&self, target_location: Point) -> i64 {
    unimplemented!()
  }
}
