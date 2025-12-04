#![allow(unused)]
use crate::{common::SteppedSolver, days::day04::PaperWarehouse};

#[derive(Debug)]
pub struct Day04Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<PaperWarehouse, PaperWarehouse, i64, i64> for Day04Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> PaperWarehouse {
    PaperWarehouse::from_lines(input)
  }

  fn parse_input_two(&self, input: Vec<&str>) -> PaperWarehouse {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, warehouse: PaperWarehouse) -> i64 {
    warehouse.count_accessible_rolls()
  }

  fn solve_part_two(&self, warehouse: PaperWarehouse) -> i64 {
    let mut warehouse = warehouse.clone();
    warehouse.remove_accessible_rolls_queue();
    warehouse.count_removed_rolls()
  }
}
