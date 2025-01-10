use std::collections::HashMap;

use crate::{common::SolverRunner, days::day01::Day01Solver};

#[derive(Debug, Eq, Hash, PartialEq)]
pub enum DayNum {
  Day01 = 1,
  //NEXT_ENUM_ENTRY
}

impl TryFrom<i32> for DayNum {
  type Error = ();

  fn try_from(value: i32) -> Result<Self, Self::Error> {
    match value {
      x if x == DayNum::Day01 as i32 => Ok(DayNum::Day01),
      //NEXT_ENUM_TRY_FROM
      _ => Err(()),
    }
  }
}

// Create and populate the HashMap
pub fn create_solver_map() -> HashMap<DayNum, Box<dyn SolverRunner>> {
  let mut solver_map: HashMap<DayNum, Box<dyn SolverRunner>> = HashMap::new();

  solver_map.insert(DayNum::Day01, create_solver(2024, 1));

  // Add more entries as needed

  solver_map
}

pub fn create_solver(year: i32, day: i32) -> Box<dyn SolverRunner> {
  // Convert i32 day to a DayNum
  let day_num = DayNum::try_from(day);
  if day_num.is_err() {
    panic!("Day is not implemented");
  }
  match day_num.unwrap() {
    DayNum::Day01 => Box::new(Day01Solver { day: 1, year }),
    //NEXT_ENUM_MATCH
  }
}
