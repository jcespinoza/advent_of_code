#![allow(unused)]
use std::collections::HashMap;

use crate::common::SteppedSolver;

use super::{Offset, Point};

#[derive(Debug)]
pub struct Day03Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Vec<Offset>, Vec<Offset>, i64, i64> for Day03Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<Offset> {
    input.first().unwrap().chars().map(Offset::from).collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<Offset> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, offsets: Vec<Offset>) -> i64 {
    let mut deliveries: HashMap<Point, u16> = HashMap::new();
    let mut current_position = Point::from((0, 0));
    deliveries.insert(current_position, 1);

    for offset in offsets {
      current_position = current_position.move_by(offset);

      deliveries
        .entry(current_position)
        .and_modify(|v| *v += 1)
        .or_insert(1);
    }

    deliveries.len() as i64
  }

  fn solve_part_two(&self, offsets: Vec<Offset>) -> i64 {
    let mut deliveries: HashMap<Point, u16> = HashMap::new();
    let mut santa_position = Point::from((0, 0));
    let mut robot_position = Point::from((0, 0));
    deliveries.insert(santa_position, 2);

    for (index, offset) in offsets.iter().enumerate() {
      let position = if index % 2 == 0 {
        santa_position = santa_position.move_by(*offset);
        santa_position
      } else {
        robot_position = robot_position.move_by(*offset);
        robot_position
      };

      deliveries
        .entry(position)
        .and_modify(|v| *v += 1)
        .or_insert(1);
    }

    deliveries.len() as i64
  }
}
