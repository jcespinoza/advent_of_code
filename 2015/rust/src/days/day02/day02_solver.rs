#![allow(unused)]
use crate::common::SteppedSolver;

use super::GiftBox;

#[derive(Debug)]
pub struct Day02Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Vec<GiftBox>, Vec<GiftBox>, i64, i64> for Day02Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<GiftBox> {
    input
      .iter()
      .map(|x| {
        let parts: Vec<i32> = x.split("x").map(|x| x.parse().unwrap()).collect();
        let length: u32 = parts[0].try_into().unwrap();
        let width: u32 = parts[1].try_into().unwrap();
        let height: u32 = parts[2].try_into().unwrap();

        GiftBox::new(length, width, height)
      })
      .collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<GiftBox> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, boxes: Vec<GiftBox>) -> i64 {
    let mut total_sum: i64 = 0;

    for gift_box in boxes {
      let surface_area = gift_box.surface_area();
      let slack_area = gift_box.smallest_area();
      total_sum += (surface_area + slack_area) as i64;
    }

    total_sum
  }

  fn solve_part_two(&self, boxes: Vec<GiftBox>) -> i64 {
    let mut total_length: i64 = 0;
    for gift_box in boxes {
      let smallest_perimeter = gift_box.smallest_perimeter();
      let volume = gift_box.volume();
      total_length += (smallest_perimeter + volume) as i64;
    }

    total_length
  }
}
