use std::collections::HashMap;

use crate::{common::solver::SolverRunner, days::day01::day01_solver::Day01Solver};

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
