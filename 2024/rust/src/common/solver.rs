use std::collections::HashMap;

use crate::days::day01::day01_solver::day01::Day01Solver;

pub trait SteppedSolver<TParsedInputOne, TParsedInputTwo, TResultOne, TResultTwo> {
  fn parse_input_one(&self, input: Vec<&str>) -> TParsedInputOne;
  fn parse_input_two(&self, input: Vec<&str>) -> TParsedInputTwo;
  fn solve_part_one(&self, input: TParsedInputOne) -> TResultOne;
  fn solve_part_two(&self, input: TParsedInputTwo) -> TResultTwo;
}

pub trait HasDayYear {
  fn day(&self) -> i32;
  fn year(&self) -> i32;
}

pub trait SolverRunner {
  fn solve_part_one_str(&self, input: Vec<&str>) -> String;
  fn solve_part_two_str(&self, input: Vec<&str>) -> String;
}

// Create and populate the HashMap
pub fn create_solver_map() -> HashMap<i32, Box<dyn SolverRunner>> {
  let mut solver_map: HashMap<i32, Box<dyn SolverRunner>> = HashMap::new();

  solver_map.insert(1, create_solver(2024, 1));

  // Add more entries as needed

  solver_map
}

pub fn create_solver(year: i32, day: i32) -> Box<dyn SolverRunner> {
  match day {
    1 => Box::new(Day01Solver { day: 1, year }),
    _ => panic!("Day not implemented"),
  }
}
