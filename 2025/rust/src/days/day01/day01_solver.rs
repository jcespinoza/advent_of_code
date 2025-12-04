#![allow(unused)]

use super::{causes_pointing_to_zero, Rotation};
use crate::common::SteppedSolver;

#[derive(Debug)]
pub struct Day01Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Vec<Rotation>, Vec<Rotation>, i64, i64> for Day01Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<Rotation> {
    // The input has a rotation in each line like "L3" or "R2"
    input
      .iter()
      .map(|line| {
        let (dir, dist) = line.split_at(1);
        let distance: i32 = dist.parse().unwrap();
        match dir {
          "L" => Rotation::Left(distance),
          "R" => Rotation::Right(distance),
          _ => panic!("Invalid direction"),
        }
      })
      .collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<Rotation> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, input: Vec<Rotation>) -> i64 {
    // Initially the arrow is pointing to the value 50
    // We need to find how many times the arrow points to 0 after all rotations
    let mut arrow_position: i32 = 50;
    let mut zero_count: i32 = 0;
    // The dial has 100 positions (0 to 99)
    for rotation in input {
      // Move the arrow according to the rotation
      match rotation {
        Rotation::Left(dist) => {
          arrow_position = (arrow_position - dist + 100) % 100;
        }
        Rotation::Right(dist) => {
          arrow_position = (arrow_position + dist) % 100;
        }
      }
      // Check if the arrow points to 0 and add it to the count
      if arrow_position == 0 {
        zero_count += 1;
      }
    }
    zero_count as i64
  }

  fn solve_part_two(&self, input: Vec<Rotation>) -> i64 {
    // For this part instead of counting how many times the arrow points to 0,
    // we need to find the number of times the arrow would point to zero in its path to the new position
    let mut arrow_position: i32 = 50;
    let mut zero_count: i32 = 0;
    // The dial has 100 positions (0 to 99)
    let dial_size: i32 = 100;
    for rotation in input {
      let start_position = arrow_position;
      // Move the arrow according to the rotation
      match rotation {
        Rotation::Left(dist) => {
          let new_arrow_position = (arrow_position - dist + 100) % 100;
          // Count zeros in the path
          // For that we need to find out how many times the distance wraps around the dial
          if dist >= dial_size {
            // Full wraps
            let full_wraps = dist / dial_size;
            zero_count += full_wraps;
          }

          // Create a rotation for the remaining distance
          let rotation = Rotation::Left(dist % dial_size);

          if causes_pointing_to_zero(&rotation, start_position) {
            zero_count += 1;
          }

          arrow_position = new_arrow_position;
        }
        Rotation::Right(dist) => {
          let new_arrow_position = (arrow_position + dist) % 100;
          // Count zeros in the path
          // For that we need to find out how many times the distance wraps around the dial
          if dist >= dial_size {
            // Full wraps
            let full_wraps = dist / dial_size;
            zero_count += full_wraps;
          }

          // Create a rotation for the remaining distance
          let rotation = Rotation::Right(dist % dial_size);

          if causes_pointing_to_zero(&rotation, start_position) {
            zero_count += 1;
          }

          arrow_position = new_arrow_position;
        }
      }
    }
    zero_count as i64
  }
}
