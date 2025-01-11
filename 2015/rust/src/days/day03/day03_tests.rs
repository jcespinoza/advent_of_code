#![allow(unused)]
use crate::{
  common::{AocExample, PuzzleInputProvider, SteppedSolver, YEAR_NUM},
  days::day03::Day03Solver,
};

const DAY_NUM: i32 = 3;

#[test]
fn sample_01_part_one() {
  let raw_input = vec!["^v^v^v^v^v"];
  let expected_output: i64 = 2;

  let solver = Day03Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, expected_output);
}

#[test]
fn solution_part_one() {
  #[allow(dead_code)]
  let remote_input = PuzzleInputProvider::new_remote(YEAR_NUM, DAY_NUM)
    .read_input()
    .unwrap();
  let raw_input = remote_input.iter().map(|x| x.as_str()).collect();
  const EXPECTED_SOLUTION_PART1: i64 = 2572;

  let solver = Day03Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART1);
}

#[test]
fn sample_01_part_two() {
  let raw_input = vec!["^v"];
  let expected_output: i64 = 3;

  let solver = Day03Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
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
  const EXPECTED_SOLUTION_PART2: i64 = 2631;

  let solver = Day03Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART2);
}

#[test]
fn examples_part_one() {
  let examples: Vec<AocExample<i64>> = vec![
    AocExample {
      raw_input: vec!["^v^v^v^v^v"],
      expected_output: 2,
    },
    AocExample {
      raw_input: vec![">"],
      expected_output: 2,
    },
    AocExample {
      raw_input: vec!["^>v<"],
      expected_output: 4,
    },
  ];

  let solver = Day03Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };

  for (index, example) in examples.iter().enumerate() {
    let input = solver.parse_input_one(example.raw_input.clone());
    let result = solver.solve_part_one(input);

    assert_eq!(
      result, example.expected_output,
      "Failed for Example: {:?}",
      index
    );
  }
}

#[test]
fn examples_part_two() {
  let examples: Vec<AocExample<i64>> = vec![
    AocExample {
      raw_input: vec!["^>v<"],
      expected_output: 3,
    },
    AocExample {
      raw_input: vec!["^v^v^v^v^v"],
      expected_output: 11,
    },
  ];

  let solver = Day03Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };

  for (index, example) in examples.iter().enumerate() {
    let input = solver.parse_input_two(example.raw_input.clone());
    let result = solver.solve_part_two(input);

    assert_eq!(
      result, example.expected_output,
      "Failed for Example: {:?}",
      index
    );
  }
}
