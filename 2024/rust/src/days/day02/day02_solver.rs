#![allow(unused)]
use crate::common::SteppedSolver;

use super::Report;

#[derive(Debug)]
pub struct Day02Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Vec<Report>, Vec<Report>, i64, i64> for Day02Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<Report> {
    input
      .iter()
      .map(|x| {
        let levels = x.split(' ').map(|x| x.parse::<i32>().unwrap()).collect();
        Report { levels }
      })
      .collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<Report> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, reports: Vec<Report>) -> i64 {
    let safe_reports = reports.iter().filter(|x| x.is_safe()).count() as i64;
    safe_reports
  }

  fn solve_part_two(&self, reports: Vec<Report>) -> i64 {
    unimplemented!()
  }
}
