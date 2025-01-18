#![allow(unused)]
use crate::{
  common::{AocExample, PuzzleInputProvider, SteppedSolver, YEAR_NUM},
  days::day19::Day19Solver,
};

const DAY_NUM: i32 = 19;

#[test]
fn sample_01_part_one() {
  let raw_input = vec!["H => HO", "H => OH", "O => HH", "", "HOH"];
  let expected_output: i64 = 4;

  let solver = Day19Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, expected_output);
}

#[test]
fn sample_02_part_one() {
  let raw_input = vec!["H => HO", "H => OH", "O => HH", "", "HOHOHO"];
  let expected_output: i64 = 7;

  let solver = Day19Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
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
  const EXPECTED_SOLUTION_PART1: i64 = 535;

  let solver = Day19Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART1);
}

#[test]
fn sample_01_part_two() {
  let raw_input = vec![
    "e => H", "e => O", "H => HO", "H => OH", "O => HH", "", "HOH",
  ];
  let expected_output: i64 = 3;

  let solver = Day19Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, expected_output);
}

#[test]
fn sample_02_part_two() {
  let raw_input = vec![
    "e => H", "e => O", "H => HO", "H => OH", "O => HH", "", "HOHOHO",
  ];
  let expected_output: i64 = 6;

  let solver = Day19Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, expected_output);
}

#[test]
#[ignore]
fn solution_part_two() {
  let remote_input = PuzzleInputProvider::new_remote(YEAR_NUM, DAY_NUM)
    .read_input()
    .unwrap();
  let raw_input = remote_input.iter().map(|x| x.as_str()).collect();
  const EXPECTED_SOLUTION_PART2: i64 = 0;

  let solver = Day19Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART2);
}
