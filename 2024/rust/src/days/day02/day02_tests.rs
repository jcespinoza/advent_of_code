#![allow(unused)]
use crate::{
  common::{PuzzleInputProvider, SteppedSolver},
  days::day02::Day02Solver,
};

const DAY_NUM: i32 = 2;
const YEAR_NUM: i32 = 2024;

pub struct AocExample<'a, TResult> {
  pub expected_output: TResult,
  pub raw_input: Vec<&'a str>,
}

#[test]
fn test_sample_02_part_one() {
  let raw_input = vec![];
  let expected_output: i64 = 0;

  let solver = Day02Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, expected_output);
}

#[test]
#[ignore]
fn test_solution_part_one() {
  #[allow(dead_code)]
  let remote_input = PuzzleInputProvider::new_remote(YEAR_NUM, DAY_NUM)
    .read_input()
    .unwrap();
  let raw_input = remote_input.iter().map(|x| x.as_str()).collect();
  const EXPECTED_SOLUTION_PART1: i64 = 0;

  let solver = Day02Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART1);
}

#[test]
#[ignore]
fn test_sample_02_part_two() {
  let raw_input = vec![];
  let expected_output: i64 = 0;

  let solver = Day02Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, expected_output);
}

#[test]
#[ignore]
fn test_solution_part_two() {
  let remote_input = PuzzleInputProvider::new_remote(YEAR_NUM, DAY_NUM)
    .read_input()
    .unwrap();
  let raw_input = remote_input.iter().map(|x| x.as_str()).collect();
  const EXPECTED_SOLUTION_PART2: i64 = 0;

  let solver = Day02Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART2);
}

#[test]
#[ignore]
fn test_examples_part_one() {
  let examples: Vec<AocExample<i64>> = vec![AocExample {
    raw_input: vec![],
    expected_output: 0,
  }];

  let solver = Day02Solver {
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
#[ignore]
fn test_examples_part_two() {
  let examples: Vec<AocExample<i64>> = vec![AocExample {
    raw_input: vec![],
    expected_output: 0,
  }];

  let solver = Day02Solver {
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
