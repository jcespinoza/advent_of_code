#![allow(unused)]
use crate::{
  common::{AocExample, PuzzleInputProvider, SteppedSolver, YEAR_NUM},
  days::day17::Day17Solver,
};

const DAY_NUM: i32 = 17;

#[test]
fn sample_01_part_one() {
  let raw_input = vec!["20", "15", "10", "5", "5"];
  let expected_output: i64 = 4;

  let solver = Day17Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    liters: Some(25),
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, expected_output);
}

#[test]
fn solution_part_one() {
  let remote_input = PuzzleInputProvider::new_remote(YEAR_NUM, DAY_NUM)
    .read_input()
    .unwrap();
  let raw_input = remote_input.iter().map(|x| x.as_str()).collect();
  const EXPECTED_SOLUTION_PART1: i64 = 654;

  let solver = Day17Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    liters: None,
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART1);
}

#[test]
fn sample_01_part_two() {
  let raw_input = vec!["20", "15", "10", "5", "5"];
  let expected_output: i64 = 3;

  let solver = Day17Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    liters: Some(25),
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, expected_output);
}

#[test]
fn solution_part_two() {
  let remote_input = PuzzleInputProvider::new_remote(YEAR_NUM, DAY_NUM)
    .read_input()
    .unwrap();
  let raw_input = remote_input.iter().map(|x| x.as_str()).collect();
  const EXPECTED_SOLUTION_PART2: i64 = 57;

  let solver = Day17Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    liters: None,
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART2);
}
