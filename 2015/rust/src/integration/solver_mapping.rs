use crate::{
  common::SolverRunner,
  // All Day Solvers
  days::day01::Day01Solver,
  days::day02::Day02Solver,
  //NEXT_IMPORT
};

#[derive(Debug, Eq, Hash, PartialEq)]
pub enum DayNum {
  Day01 = 1,
  Day02 = 2,
  //NEXT_ENUM_ENTRY
}

impl TryFrom<i32> for DayNum {
  type Error = ();

  fn try_from(value: i32) -> Result<Self, Self::Error> {
    match value {
      x if x == DayNum::Day01 as i32 => Ok(DayNum::Day01),
      x if x == DayNum::Day02 as i32 => Ok(DayNum::Day02),
      //NEXT_ENUM_TRY_FROM
      _ => Err(()),
    }
  }
}

pub fn create_solver(year: i32, day: i32) -> Box<dyn SolverRunner> {
  let day_num = DayNum::try_from(day);
  if day_num.is_err() {
    panic!("Day is not implemented");
  }
  match day_num.unwrap() {
    DayNum::Day01 => Box::new(Day01Solver { day: 1, year }),
    DayNum::Day02 => Box::new(Day02Solver { day: 2, year }),
    //NEXT_ENUM_MATCH
  }
}
