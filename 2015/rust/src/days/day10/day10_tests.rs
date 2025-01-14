#![allow(unused)]
use crate::{
  common::{AocExample, PuzzleInputProvider, SteppedSolver, YEAR_NUM},
  days::day10::Day10Solver,
};

const DAY_NUM: i32 = 10;

#[test]
fn sample_01_part_one() {
  let raw_input = vec!["1"];
  let expected_output: i64 = 6;

  let solver = Day10Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    steps: Some(5),
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, expected_output);
}

#[test]
fn solution_part_one() {
  let raw_input = vec!["1321131112"];
  const EXPECTED_SOLUTION_PART1: i64 = 492982;

  let solver = Day10Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    steps: None,
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART1);
}

#[test]
fn solution_part_two() {
  let raw_input = vec!["1321131112"];
  const EXPECTED_SOLUTION_PART2: i64 = 6989950;

  let solver = Day10Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    steps: None,
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART2);
}
