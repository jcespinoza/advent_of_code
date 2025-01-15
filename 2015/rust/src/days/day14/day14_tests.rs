#![allow(unused)]
use crate::{
  common::{AocExample, PuzzleInputProvider, SteppedSolver, YEAR_NUM},
  days::day14::Day14Solver,
};

const DAY_NUM: i32 = 14;

#[test]
fn sample_1000sec_part_one() {
  let raw_input = vec![
    "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.",
    "Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.",
  ];
  let expected_output: i64 = 1120;

  let solver = Day14Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    time: Some(1000),
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, expected_output);
}

#[test]
fn sample_1sec_part_one() {
  let raw_input = vec![
    "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.",
    "Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.",
  ];
  let expected_output: i64 = 16;

  let solver = Day14Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    time: Some(1),
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, expected_output);
}

#[test]
fn sample_10sec_part_one() {
  let raw_input = vec![
    "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.",
    "Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.",
  ];
  let expected_output: i64 = 160;

  let solver = Day14Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    time: Some(10),
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
  const EXPECTED_SOLUTION_PART1: i64 = 2696;

  let solver = Day14Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    time: None,
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART1);
}

#[test]
fn sample_140sec_part_two() {
  let raw_input = vec![
    "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.",
    "Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.",];
  let expected_output: i64 = 139;

  let solver = Day14Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    time: Some(140),
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, expected_output);
}

#[test]
fn sample_1000sec_part_two() {
  let raw_input = vec![
    "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.",
    "Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.",];
  let expected_output: i64 = 689;

  let solver = Day14Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    time: Some(1000),
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
  const EXPECTED_SOLUTION_PART2: i64 = 1084;

  let solver = Day14Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
    time: None,
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART2);
}
