use crate::{
  common::SolverRunner,
  // All Day Solvers
  days::day01::Day01Solver,
  days::day02::Day02Solver,
  days::day03::Day03Solver,
  days::day04::Day04Solver,
  days::day05::Day05Solver,
  days::day06::Day06Solver,
  days::day07::Day07Solver,
  days::day08::Day08Solver,
  days::day09::Day09Solver,
  days::day10::Day10Solver,
  days::day11::Day11Solver,
  days::day12::Day12Solver,
  days::day13::Day13Solver,
  days::day14::Day14Solver,
  days::day15::Day15Solver,
  days::day16::Day16Solver,
  days::day17::Day17Solver,
  days::day18::Day18Solver,
  days::day19::Day19Solver,
  days::day20::Day20Solver,
  days::day21::Day21Solver,
  days::day22::Day22Solver,
  days::day23::Day23Solver,
  //NEXT_IMPORT
};

#[derive(Debug, Eq, Hash, PartialEq)]
pub enum DayNum {
  Day01 = 1,
  Day02 = 2,
  Day03 = 3,
  Day04 = 4,
  Day05 = 5,
  Day06 = 6,
  Day07 = 7,
  Day08 = 8,
  Day09 = 9,
  Day10 = 10,
  Day11 = 11,
  Day12 = 12,
  Day13 = 13,
  Day14 = 14,
  Day15 = 15,
  Day16 = 16,
  Day17 = 17,
  Day18 = 18,
  Day19 = 19,
  Day20 = 20,
  Day21 = 21,
  Day22 = 22,
  Day23 = 23,
  //NEXT_ENUM_ENTRY
}

impl TryFrom<i32> for DayNum {
  type Error = ();

  fn try_from(value: i32) -> Result<Self, Self::Error> {
    match value {
      x if x == DayNum::Day01 as i32 => Ok(DayNum::Day01),
      x if x == DayNum::Day02 as i32 => Ok(DayNum::Day02),
      x if x == DayNum::Day03 as i32 => Ok(DayNum::Day03),
      x if x == DayNum::Day04 as i32 => Ok(DayNum::Day04),
      x if x == DayNum::Day05 as i32 => Ok(DayNum::Day05),
      x if x == DayNum::Day06 as i32 => Ok(DayNum::Day06),
      x if x == DayNum::Day07 as i32 => Ok(DayNum::Day07),
      x if x == DayNum::Day08 as i32 => Ok(DayNum::Day08),
      x if x == DayNum::Day09 as i32 => Ok(DayNum::Day09),
      x if x == DayNum::Day10 as i32 => Ok(DayNum::Day10),
      x if x == DayNum::Day11 as i32 => Ok(DayNum::Day11),
      x if x == DayNum::Day12 as i32 => Ok(DayNum::Day12),
      x if x == DayNum::Day13 as i32 => Ok(DayNum::Day13),
      x if x == DayNum::Day14 as i32 => Ok(DayNum::Day14),
      x if x == DayNum::Day15 as i32 => Ok(DayNum::Day15),
      x if x == DayNum::Day16 as i32 => Ok(DayNum::Day16),
      x if x == DayNum::Day17 as i32 => Ok(DayNum::Day17),
      x if x == DayNum::Day18 as i32 => Ok(DayNum::Day18),
      x if x == DayNum::Day19 as i32 => Ok(DayNum::Day19),
      x if x == DayNum::Day20 as i32 => Ok(DayNum::Day20),
      x if x == DayNum::Day21 as i32 => Ok(DayNum::Day21),
      x if x == DayNum::Day22 as i32 => Ok(DayNum::Day22),
      x if x == DayNum::Day23 as i32 => Ok(DayNum::Day23),
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
    DayNum::Day03 => Box::new(Day03Solver { day: 3, year }),
    DayNum::Day04 => Box::new(Day04Solver { day: 4, year }),
    DayNum::Day05 => Box::new(Day05Solver { day: 5, year }),
    DayNum::Day06 => Box::new(Day06Solver { day: 6, year }),
    DayNum::Day07 => Box::new(Day07Solver { day: 7, year }),
    DayNum::Day08 => Box::new(Day08Solver { day: 8, year }),
    DayNum::Day09 => Box::new(Day09Solver { day: 9, year }),
    DayNum::Day10 => Box::new(Day10Solver {
      day: 10,
      year,
      steps: None,
    }),
    DayNum::Day11 => Box::new(Day11Solver { day: 11, year }),
    DayNum::Day12 => Box::new(Day12Solver { day: 12, year }),
    DayNum::Day13 => Box::new(Day13Solver { day: 13, year }),
    DayNum::Day14 => Box::new(Day14Solver {
      day: 14,
      year,
      time: None,
    }),
    DayNum::Day15 => Box::new(Day15Solver { day: 15, year }),
    DayNum::Day16 => Box::new(Day16Solver { day: 16, year }),
    DayNum::Day17 => Box::new(Day17Solver {
      day: 17,
      year,
      liters: None,
    }),
    DayNum::Day18 => Box::new(Day18Solver {
      day: 18,
      year,
      steps: Some(100),
    }),
    DayNum::Day19 => Box::new(Day19Solver { day: 19, year }),
    DayNum::Day20 => Box::new(Day20Solver { day: 20, year }),
    DayNum::Day21 => Box::new(Day21Solver { day: 21, year }),
    DayNum::Day22 => Box::new(Day22Solver { day: 22, year }),
    DayNum::Day23 => Box::new(Day23Solver { day: 23, year }),
    //NEXT_ENUM_MATCH
  }
}
